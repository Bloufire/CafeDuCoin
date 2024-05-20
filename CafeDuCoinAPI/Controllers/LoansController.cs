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
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CafeDuCoinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly CafeDuCoinContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoansController(CafeDuCoinContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // Endpoint to manage loans (borrow or return a game)
        [Authorize] // Requires authentication
        [HttpPut("/loans/manage/{gameName}")]
        public async Task<ActionResult> ManageLoan(string gameName)
        {
            // Get the game associated with the provided game name
            var game = await _context.Games.SingleOrDefaultAsync(x => x.Name == gameName);

            // Get the username of the current authenticated user
            var userName = User.Claims.FirstOrDefault(c => c.Type == "username")?.Value;
            // Find the user with the provided username
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == userName);

            // Return not found response if game or user not found
            if (game == null)
                return NotFound($"Game with name {gameName} not found.");
            if (user == null)
                return NotFound($"User with username {userName} not found.");

            // Get the last loan for the game
            var lastLoan = await _context.Loans
                .Include(x => x.User)
                .Include(x => x.Game)
                .Where(l => l.Game.ID == game.ID).OrderByDescending(x => x.LoanDate).FirstOrDefaultAsync();
            // Check if the game is available for loan
            var available = lastLoan == null ? true : (lastLoan.LoanReturnDate == null ? false : true);

            var action = "";
            if(available) // If the game is available, create a new loan
            {
                var loan = new Loan
                {
                    gameID = game.ID,
                    Game = game,
                    LoanDate = DateTime.UtcNow,
                    LoanReturnDate = null,
                    userID = user.Id,
                    User = user
                };

                _context.Loans.Add(loan);

                action = "Loan accepted";
            }
            else // If the game is already loaned, close the existing loan
            {
                // Check if the current user is the owner of the last loan
                if (lastLoan.User.Id != user.Id)
                    return BadRequest("Someone else is owning this loan");

                // Set the return date of the last loan to the current time
                lastLoan.LoanReturnDate = DateTime.UtcNow;

                _context.Entry(lastLoan).State = EntityState.Modified;

                action = "Loan closed";
            }

            // Save changes to the database
            await _context.SaveChangesAsync();
            return Ok(action);

        }
    }
}
