using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MLM.Data;
using MLM.Models;

namespace MLM.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDBContext _context;

        public AuthController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult ClientRegisterReferralid()
        {
            return View();
        }
        public IActionResult MtsadminSignin()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UserLogin()
        {
            // If someone tries to directly access the POST URL via GET, redirect them to the login page
            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserLogin(Users model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                ViewBag.Error = "Email and Password are required.";
                return View("Login");
            }

            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == model.Email);
            
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Hash))
            {
                ViewBag.Error = "Invalid email or password.";
                return View("Login");
            }

            var token = GenerateJwtToken(user);
            Response.Cookies.Append("access_token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Set to false if not using HTTPS locally, but Program.cs seems to support HTTPS
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now.AddDays(30)
            });

            if (user.Role != null && user.Role.IsAdmin)
            {
                return RedirectToAction("AdminDashboard", "Admin");
            }
            return RedirectToAction("Dashboard", "Client");
        }

        private string GenerateJwtToken(Users user)
        {
            var claims = new System.Collections.Generic.List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, user.Id.ToString()),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, user.Email),
                new System.Security.Claims.Claim("RoleId", user.RoleId?.ToString() ?? "0")
            };

            var key = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("asdfasdfadsfadsfadsfadsfadsfadsf"));
            var creds = new Microsoft.IdentityModel.Tokens.SigningCredentials(key, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256);

            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: creds
            );

            return new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(token);
        }
        public IActionResult Signup()
        {
            return View();
        }
        public IActionResult Verify()
        {
            return View();
        }

    }
}
