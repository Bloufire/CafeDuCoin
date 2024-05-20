using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CafeDuCoinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        // Endpoint for user registration
        [HttpPost("/users/register")]
        public async Task<ActionResult> Register(RegisterModel registerModel)
        {
            // Check if model state is valid
            if (ModelState.IsValid) 
            {
                // Check if a user with the provided username already exists
                var userCheck = await _userManager.FindByNameAsync(registerModel.Username);
                if(userCheck != null)
                {
                    return NotFound($"User with username {registerModel.Username} already exists.");
                }

                // Create a new ApplicationUser object with the provided details
                var user = new ApplicationUser
                {
                    UserName = registerModel.Username,
                    Email = registerModel.Email
                };
                // Attempt to create the user with the provided password
                var result = await _userManager.CreateAsync(user, registerModel.Password);

                // If user creation is successful, return success response
                if (result.Succeeded)
                {
                    return Ok(new { Result = "User created successfully" });
                }

                // If user creation fails, add errors to model state and return bad request response
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return BadRequest(ModelState);
        }

        // Endpoint for user login
        [HttpPost("/users/login")]
        public async Task<ActionResult> Login(LoginModel loginModel)
        {
            // Check if model state is valid
            if (ModelState.IsValid)
            {
                // Find the user with the provided username
                var user = await _userManager.FindByNameAsync(loginModel.Username);

                // If user is found and password is correct, generate JWT token
                if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
                {
                    // Define claims for JWT token
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim("username", user.UserName)
                    };

                    // Generate symmetric security key and signing credentials
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    // Create JWT token with specified claims, expiration, and signing credentials
                    var token = new JwtSecurityToken(
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Issuer"],
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: creds);

                    // Return token in response
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token)
                    });
                }

                // Return unauthorized response if user is not found or password is incorrect
                return Unauthorized();
            }

            // Return bad request response if model state is invalid
            return BadRequest(ModelState);
        }
    }

    // Model for user registration
    public class RegisterModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    // Model for user login
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
