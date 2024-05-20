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

namespace CafeDuCoinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly CafeDuCoinContext _context;

        public UsersController(CafeDuCoinContext context)
        {
            _context = context;
        }

        // Endpoint to retrieve loan history for a user by login
        [HttpGet("/users/{login}/loans")]
        public async Task<ActionResult<UserCard>> GetUserLoans(string login)
        {
            // Find the user with the provided login (username)
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == login);
            if (user == null)
            {
                return NotFound($"User with login {login} not found.");
            }

            // Get loans associated with the user
            var userWithLoans = _context.Loans
                .Include(x => x.User)
                .Include(x => x.Game)
                .Where(x => x.User.UserName == login);

            var history = new List<UserHistory>();
            foreach (var historyEntry in userWithLoans)
                history.Add(new UserHistory(historyEntry));

            // Return loan history sorted by loan date
            return Ok(history.OrderByDescending(x => x.LoanDate));
        }

    }
}
