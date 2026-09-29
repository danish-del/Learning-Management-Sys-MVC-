using LMS.Models;
using LMS.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly JwtService _jwtService;
        public AuthController(UserManager<AppUser> UserManager,
            SignInManager<AppUser> SignInManager, JwtService jwtService)
        {
            _userManager = UserManager;
            _signInManager = SignInManager;
            _jwtService = jwtService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string email,string password)
        {
            var user = new AppUser
            {
                UserName = username,
                Email = email
            };
            var result = await _userManager.CreateAsync(user, password);
            if(!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return RedirectToAction("Index");
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null)
            {
                return Unauthorized();
            }
            var result = await _signInManager.CheckPasswordSignInAsync(
                user, password, false);
            if(!result.Succeeded)
            {
                return Unauthorized();
            }
            var token = _jwtService.GenerateToken(user);
            return Ok(new
            {
                message = "Login Successfull!",
                token = token

            });
        }
    }
}
