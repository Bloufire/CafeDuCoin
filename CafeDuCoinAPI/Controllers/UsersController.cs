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

        // Remove
        [HttpGet("/users/{login}/loans")]
        public async Task<ActionResult<UserCard>> GetUserLoans(string login)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == login);
            if (user == null)
            {
                return NotFound($"User with login {login} not found.");
            }

            var userWithLoans = _context.Loans
                .Include(x => x.User)
                .Include(x => x.Game)
                .Where(x => x.User.UserName == login);

            var history = new List<UserHistory>();
            foreach (var historyEntry in userWithLoans)
                history.Add(new UserHistory(historyEntry));

            return Ok(history.OrderByDescending(x => x.LoanDate));
        }

    }
}
