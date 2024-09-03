using CompanyManagmentApplication.Database;
using CompanyManagmentApplication.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CompanyManagmentApplication.Controllers
{
    public class SignUpController : Controller
    {
        private Data dbcontext;

        public SignUpController(Data dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        [Authorize]
        public IActionResult Index(string error , string signup_error , string user_error)
        {
            ViewBag.error = error;
            ViewBag.signup_error = signup_error;
            ViewBag.user_error = user_error;

            return View();
        }

        [HttpPost]
        public IActionResult signup(Users user )
        {
                var name = Request.Form["user_name"].ToString();
                var phone = Request.Form["user_phone"].ToString();
                var email = Request.Form["user_email"].ToString();
                var password = Request.Form["user_password"].ToString();
                var address = Request.Form["user_address"].ToString();
                var role_id = 1;  
                var user_img = "img/download.jpeg";

             var cheak = dbcontext.users.FirstOrDefault(s => s.user_email == email);
            if(cheak == null) { 
                Users users = new Users()
                {
                    user_address = address,
                    user_email = email,
                    user_name = name,
                    user_password = password,
                    user_phone = phone,
                    role_id = role_id,
                    user_img = user_img
                };

                dbcontext.Add(users);
                dbcontext.SaveChanges();
                return RedirectToAction("SignIn");
            }
            else
            {
                return RedirectToAction("Index", new { signup_error = "Email Is Already Taken" });
            }
        }


    public IActionResult SignIn()
    {
        return View();
    }

        [HttpPost]
        public IActionResult Login(Users user)
        {

            //if (find_user == null)
            //{
            //    HttpContext.Session.SetString("User_name", find_user.user_name);
            //    HttpContext.Session.SetString("User_email", find_user.user_email);
            //    HttpContext.Session.SetString("User_img", find_user.user_img);
            //    Session_Class.User_Name = find_user.user_name;
            //    Session_Class.User_Email = find_user.user_email;
            //    Session_Class.User_Phone = find_user.user_phone;
            //    Session_Class.User_Address = find_user.user_address;
            //    Session_Class.User_Id = find_user.user_id;
            //    Session_Class.User_Img = find_user.user_img;
            //}
            var user_email = Request.Form["user_email"].ToString();
            var user_pass = Request.Form["user_password"].ToString();

            try {
               
                // Find the user in the database
                var find_user = dbcontext.users.FirstOrDefault(s => s.user_email == user_email && s.user_password == user_pass);
                if (find_user != null)
                {
                    ClaimsIdentity identity = null;
                    bool authorize = false;
                    // Check the user's role
                    if (find_user.role_id == 1)
                    {
                        identity = new ClaimsIdentity(new[]
                        {
                new Claim(ClaimTypes.Email, user_email),
                new Claim(ClaimTypes.Role, "Admin")
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                        authorize = true;
                           HttpContext.Session.SetString("User_name", find_user.user_name);
                           HttpContext.Session.SetString("User_email", find_user.user_email);
                           HttpContext.Session.SetString("User_img", find_user.user_img);
                           Session_Class.User_Name = find_user.user_name;
                           Session_Class.User_Email = find_user.user_email;
                           Session_Class.User_Phone = find_user.user_phone;
                           Session_Class.User_Address = find_user.user_address;
                           Session_Class.User_Id = find_user.user_id.ToString();
                           Session_Class.User_Img = find_user.user_img; //    HttpContext.Session.SetString("User_name", find_user.user_name);
                        Session_Class.role_id = find_user.role_id;

                    }
                    else if (find_user.role_id == 3)
                    {
                        identity = new ClaimsIdentity(new[]
                        {
            new Claim(ClaimTypes.Email, user_email),
            new Claim(ClaimTypes.Role, "Employ")
        }, CookieAuthenticationDefaults.AuthenticationScheme);
                        authorize = true;
                          HttpContext.Session.SetString("User_name", find_user.user_name);
                          HttpContext.Session.SetString("User_email", find_user.user_email);
                          HttpContext.Session.SetString("User_img", find_user.user_img);
                          Session_Class.User_Name = find_user.user_name;
                          Session_Class.User_Email = find_user.user_email;
                          Session_Class.User_Phone = find_user.user_phone;
                          Session_Class.User_Address = find_user.user_address;
                          Session_Class.User_Id = find_user.user_id.ToString();
                          Session_Class.User_Img = find_user.user_img;
                        Session_Class.role_id = find_user.role_id;
                    }

                    if (authorize)
                    {
                        var principal = new ClaimsPrincipal(identity);
                        var signIn = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


                        // Redirect based on the user's role
                        return RedirectToAction("Index", "Home");
                    }
                   
                    
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "User Not Found");
            }

            return RedirectToAction("Index", new { error = "Your Email OR Password is Incorrect" });

        }







    }
}