using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskApi.Api.Dtos;
using TasksApi.Infrastructure.Roles;

namespace TaskApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _UserManager;
        private readonly SignInManager<IdentityUser> _SignInManager;

        // private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly RoleManager<IdentityRole> _RoleManager;

        private readonly IConfiguration _Configuration;

        public AuthController(UserManager<IdentityUser> userManager
                , SignInManager<IdentityUser> signInManager
                , RoleManager<IdentityRole> roleManager
                , IConfiguration configuration)
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
            _RoleManager = roleManager;
            _Configuration = configuration;
        }

        [HttpPost]
        [Route("send-roles")]
        public async Task<IActionResult> SendRoles()
        {
            bool roleExists = await _RoleManager.RoleExistsAsync(UserRoles.Admin);
            if (roleExists)
            {
                return Ok("Seeding... Core roles already exists");
            }
            await _RoleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            await _RoleManager.CreateAsync(new IdentityRole(UserRoles.User));
            await _RoleManager.CreateAsync(new IdentityRole(UserRoles.Owner));
            return Ok("Seeding... Core roles has been created");
        }

        [HttpPost]
        [Route("register")]
        [Authorize(Roles = UserRoles.Owner)]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var userExists = await _UserManager.FindByNameAsync(model.UserName);

            if (userExists != null)
            {
                return BadRequest("User already exists");
            }

            var user = new IdentityUser
            {
                UserName = model.UserName,
                Email = model.Email
            };
            var result = await _UserManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                //var stringErrors = "User creation failed: "; 
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Errors", error.Description);
                    //stringErrors += error.Description;
                }
                return BadRequest(ModelState);
            }
            // add default role to all users
            await _UserManager.AddToRoleAsync(user, UserRoles.User);

            return Ok("User created successfully!");

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _UserManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect." });
            }

            var lIsPasswordValid = await _UserManager.CheckPasswordAsync(user, model.Password);

            if (!lIsPasswordValid)
            {
                return BadRequest(new { message = "Username or password is incorrect." });
            }


            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName!));
            claims.Add(new Claim("JWTID", Guid.NewGuid().ToString()));

            foreach (var UserRoles in await _UserManager.GetRolesAsync(user))
            {
                claims.Add(new Claim(ClaimTypes.Role, UserRoles));
            }

            string token = GenerateToken(claims);

            return Ok(new { token });
        }


        [HttpPost]
        [Route("logout")]   
        public async Task<IActionResult> Logout()
        {
            await _SignInManager.SignOutAsync();
            return Ok("User logged out successfully!");
        }

        [HttpPost]
        [Route("make-admin")]
        public async Task<IActionResult> MakeAdmin([FromBody] UpdatePermissioinDto model)
        {
            var user = await _UserManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                return BadRequest(new { message = "User not found." });
            }

            var result = await _UserManager.AddToRoleAsync(user, UserRoles.Admin);

            if (!result.Succeeded)
            {
                return BadRequest(new { message = "Failed to make user admin." });
            }

            return Ok("User is now an admin.");
        }

        [HttpPost]
        [Route("make-owner")]
        public async Task<IActionResult> MakeOwner([FromBody] UpdatePermissioinDto model)
        {
            var user = await _UserManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                return BadRequest(new { message = "User not found." });
            }

            var result = await _UserManager.AddToRoleAsync(user, UserRoles.Owner);

            if (!result.Succeeded)
            {
                return BadRequest(new { message = "Failed to make user owner." });
            }

            return Ok("User is now an owner.");
        }
        private string GenerateToken(List<Claim> claims)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _Configuration["Jwt:Issuer"],
                Audience = _Configuration["Jwt:Audience"],
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["Jwt:Key"]!))
                                                , SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return token;

            //esto es otra forma de crear el token

            // var tokenObject = new JwtSecurityToken(
            //     issuer: _Configuration["Jwt:Issuer"],
            //     audience: _Configuration["Jwt:Audience"],
            //     claims: claims,
            //     expires: DateTime.Now.AddDays(1),
            //     signingCredentials: new SigningCredentials(
            //         new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["Jwt:Key"]!)),
            //         SecurityAlgorithms.HmacSha256)
            // );

            // string tokenx = new JwtSecurityTokenHandler().WriteToken(tokenObject);
        }

    }


}

