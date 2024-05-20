using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CafeDuCoinAPI;
using CafeDuCoinAPI.Models;
using CafeDuCoinAPI.DTOs;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using NuGet.Protocol.Plugins;
using Microsoft.AspNetCore.Authorization;

namespace CafeDuCoinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private static readonly Random Random = new Random();
        private static readonly string[] Adjectives = new[] { "Crazy", "Wacky", "Funny", "Silly", "Wild", "Epic", "Goofy", "Bizarre", "Quirky", "Hilarious" };
        private static readonly string[] Nouns = new[] { "Banana", "Unicorn", "Ninja", "Pirate", "Robot", "Zombie", "Alien", "Dinosaur", "Monkey", "Penguin" };

        private readonly CafeDuCoinContext _context;

        public GamesController(CafeDuCoinContext context)
        {
            _context = context;
        }

        [HttpGet("/games")]
        public async Task<ActionResult<IEnumerable<GameCard>>> GetGames()
        {
            var gameWithStatus = await _context.Games
                .GroupJoin(
                    _context.Loans.Where(l => l.LoanReturnDate == null || l.LoanReturnDate.Value.ToLocalTime() > DateTime.Now),
                    game => game.ID,
                    loan => loan.Game.ID,
                    (game, loans) => new { Game = game, Loans = loans}
                )
                .SelectMany(
                    x => x.Loans.DefaultIfEmpty(),
                    (x, loan) => new GameCard
                    {
                        Name = x.Game.Name,
                        Description = x.Game.Description,
                        Available = loan == null ? true : false
                    }
                )
                .ToListAsync();

            return Ok(gameWithStatus);
        }

        [HttpGet("/games/{gameName}")]
        public async Task<ActionResult<GameCard>> GetGame(string gameName)
        {
            var game = await _context.Games.SingleOrDefaultAsync(x => x.Name == gameName);

            if (game == null)
            {
                return NotFound($"Game with name {gameName} not found.");
            }

            var available = await _context.Loans.Where(l => l.Game.ID == game.ID).OrderByDescending(x => x.LoanDate).FirstOrDefaultAsync();

            return new GameCard(game, available == null ? true : (available.LoanReturnDate == null ? false : true));
        }

        [HttpGet("/games/{gameName}/history")]
        public async Task<ActionResult<IEnumerable<GameHistory>>> GetHistory(string gameName)
        {
            var game = await _context.Games.SingleOrDefaultAsync(x => x.Name == gameName);
            if (game == null)
            {
                return NotFound($"Game with name {gameName} not found.");
            }

            var gameHistory = _context.Loans
                .Include(x => x.Game)
                .Include(x => x.User)
                .Where(x => x.Game.ID == game.ID);

            var history = new List<GameHistory>();
            foreach(var historyEntry in gameHistory)
                history.Add(new GameHistory(historyEntry));

            return Ok(history.OrderByDescending(x => x.LoanDate));
        }

        [HttpPost("/games/mock")]
        public async Task<ActionResult<IEnumerable<GameCard>>> MockGames()
        {
            var games = new List<GameCard>();

            for (int i = 0; i < 5; i++) // Generate 5 mock games
            {
                var randomName = GenerateName();

                var gameExists = await _context.Games.FirstOrDefaultAsync(x => x.Name == randomName);
                if (gameExists == null)
                {
                    var gameToAdd = new Game
                    {
                        Name = randomName,
                        Description = $"A fun and engaging game named {randomName}!"
                    };

                    _context.Add(gameToAdd);
                    games.Add(new GameCard(gameToAdd, true));
                }
            }

            await _context.SaveChangesAsync();

            return Ok(games);
        }

        private static string GenerateName()
        {
            var adjective = Adjectives[Random.Next(Adjectives.Length)];
            var noun = Nouns[Random.Next(Nouns.Length)];
            return $"{adjective} {noun}";
        }
    }
}
