using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MLM.Data;
using MLM.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace MLM.Controllers
{
    public class AuthController(AppDBContext _context) : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult RegisteUser(Users UserInfo)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Please fill all required fields correctly.";
                return View("Register", UserInfo);
            }


            var existingUser = _context.Users.FirstOrDefault(x => x.Email == UserInfo.Email);

            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "This email is already registered.");
                return View("Register", UserInfo);
            }
         
            var user = new Users
            {
                Name = UserInfo.Name,
                Email = UserInfo.Email,
                Password = UserInfo.Password,
                Phone = UserInfo.Phone
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            TempData["Success"] = "Registration successful. Please login.";

            return RedirectToAction("Login", "Auth");
        }

        [HttpPost]
        public IActionResult UserLogin(Users model)
        {
            //if (!ModelState.IsValid)
            //{
            //    ViewBag.Error = "Error";
            //    return View("Login", model);
            //}

            var user = _context.Users.FirstOrDefault(x =>
                x.Email == model.Email &&
                x.Password == model.Password
            );

            if (user == null)
            {
                ViewBag.Error = "Invalid email or password";
                return View("Login", model);
            }

            string token = GenerateJwtRoken(user);
            Response.Cookies.Append("access_token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(30)
            });

            //HttpContext.Session.SetString("UserEmail", user.Email);
            //HttpContext.Session.SetString("UserName", user.Name);

            return RedirectToAction("Index", "Home");
        }

        private string GenerateJwtRoken(Users user)
        {

            var jwtHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("asdfasdfadsfadsfadsfadsfadsfadsf");
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                       new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature
                    )
            };
            var token = jwtHandler.CreateToken(TokenDescriptor);
            return jwtHandler.WriteToken(token);

        }

    }
}
