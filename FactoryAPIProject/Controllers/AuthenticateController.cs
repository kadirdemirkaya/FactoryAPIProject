using FactoryAPIProject.Models;
using FactoryAPIProject.Statics;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FactoryAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly List<string> _tokens;

        public AuthenticateController(
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            IConfiguration configuration,
            SignInManager<AppUser> signInManager,
            List<string> tokens)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _tokens = tokens;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim> //Name
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                    foreach (var userRole in userRoles) //Role
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var token = GetToken(authClaims);

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo,
                        isSuccess = true
                    });
                }
            }
            return Ok(new
            {
                isSuccess = false
            });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors);
                return Ok(new
                {
                    isSuccess = false
                });
            }

            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error !", Message = "User already exists !", isSuccess = false });

            AppUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                Gender = model.Gender,
                FullName = model.Fullname,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            #region  ROL !
            var roleUser = new AppRole { Name = UserRoles.User };

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(roleUser);
            await _userManager.AddToRoleAsync(user, UserRoles.User); // !
            #endregion

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error !", Message = "User creation failed ! Please check user details and try again. please !", isSuccess = false });
            return Ok(new Response
            {
                Status = "Success",
                Message = "User created successfully!",
                isSuccess = true
            });
        }
        //aa
        [HttpPost]
        [Route("registeradmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error !", Message = "User already exists !", isSuccess = false });

            AppUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                Gender = model.Gender,
                FullName = model.Fullname,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error !", Message = "User creation failed! Please check user details and try again. please !", isSuccess = false });

            #region ROLE !
            var roleAdmin = new AppRole { Name = UserRoles.Admin };

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(roleAdmin);

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            #endregion

            return Ok(new Response { Status = "Success !", Message = "User created successfully !", isSuccess = true });
        }

        [HttpGet]
        [Route("LogOut")]
        public IActionResult LogOut([FromHeader] string token)
        {
            _tokens.Add(token);
            return Ok(new
            {
                message = "Çıkış İşlemi Başarili"
            });
        }

        [HttpGet]
        [Route("IsValidToken")]
        public IActionResult IsValidToken([FromHeader]string _token)
        {
            foreach (var token in _tokens)
            {
                if (token.ToString() == _token)
                {
                    return StatusCode(StatusCodes.Status200OK, new Response { Status = "Error !", Message = "Token is NOT VALID !", isSuccess = false });
                }
            }
            return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success !", Message = "Token is VALID !", isSuccess = true });
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
