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

        [Authorize]
        [HttpPut("/loans/manage/{gameName}")]
        public async Task<ActionResult> ManageLoan(string gameName)
        {
            var game = await _context.Games.SingleOrDefaultAsync(x => x.Name == gameName);

            var userName = User.Claims.FirstOrDefault(c => c.Type == "username")?.Value;
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == userName);

            if (game == null)
                return NotFound($"Game with name {gameName} not found.");
            if (user == null)
                return NotFound($"User with username {userName} not found.");

            var lastLoan = await _context.Loans
                .Include(x => x.User)
                .Include(x => x.Game)
                .Where(l => l.Game.ID == game.ID).OrderByDescending(x => x.LoanDate).FirstOrDefaultAsync();
            var available = lastLoan == null ? true : (lastLoan.LoanReturnDate == null ? false : true);

            var action = "";
            if(available)
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
            else
            {
                if (lastLoan.User.Id != user.Id)
                    return BadRequest("Someone else is owning this loan");

                lastLoan.LoanReturnDate = DateTime.UtcNow;

                _context.Entry(lastLoan).State = EntityState.Modified;

                action = "Loan closed";
            }

            await _context.SaveChangesAsync();
            return Ok(action);

        }
    }
}
