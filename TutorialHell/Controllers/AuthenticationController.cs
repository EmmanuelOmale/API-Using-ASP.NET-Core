using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;
using MyWebApi.Models.Authentication.Login;
using MyWebApi.Models.Authentication.SignUp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Models.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly Tokenization _tokenization;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;

        public AuthenticationController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, 
            IConfiguration configuration, IMapper mapper, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _mapper = mapper;
            _signInManager = signInManager;
            _tokenization = new Tokenization(configuration);
        }
        [HttpPost("Register")]
       
        public async Task<IActionResult> Register([FromBody] RegisterDto registerUser)
        {
            // Check if user exist
            var result = _mapper.Map<RegisterDto, User>(registerUser);
            var userExist = await _userManager.FindByNameAsync(result.UserName);
            if (userExist != null)
            {
                return BadRequest(new ResponseDto("Error", "User already exists"));
            }
            // Add user to the database 
            User user = new()
            {
                UserName = result.UserName,
                Password = result.Password
            };
            var Result = await _userManager.CreateAsync(user, result.Password);
            if (!Result.Succeeded)
            {
                return BadRequest(new ResponseDto("Error", "Failed to create account"));
            }
            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            else if (await _roleManager.RoleExistsAsync("Admin"))
                await _userManager.AddToRoleAsync(user, "Admin");

            return Ok("User created Successfully");
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginModel)
        {
            // Checking User credentials
            var result = _mapper.Map<User>(loginModel);
            // Checking the user
            var user = await _userManager.FindByNameAsync(result.UserName);
            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            if(user != null)
            {
                var sign = await _signInManager.CheckPasswordSignInAsync(user, loginModel.Password, false);
                if (sign.Succeeded)
                {
                    var jwt = new Tokenization(_configuration);
                    var token = jwt.GenerateToken(user, role);

                    return Ok(token.ToString());
                }

            }
            return Unauthorized();
        }


    }
    
    
}
