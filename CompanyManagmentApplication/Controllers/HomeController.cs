using CompanyManagmentApplication.Database;
using CompanyManagmentApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace CompanyManagmentApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private Data dbcontext;
        public HomeController(ILogger<HomeController> logger , Data a)
        {
            this.dbcontext = a;
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index()
        {
            var show_product = dbcontext.Products.Include(s => s.Users).ToList();
           

            return View(show_product);
        }

        [Authorize]
        public IActionResult add_employ(string signup_error)
        {
            ViewBag.signup_error = signup_error;

            return View();
        }

        public IActionResult submit_employ(Users a , IFormFile user_img)
        {
            var cheak = dbcontext.users.FirstOrDefault(s => s.user_email == a.user_email);
            if (cheak == null) { 
            if(user_img != null)
            {
                var Filename = Path.GetFileName(user_img.FileName);
                var FilePath = Path.Combine("wwwroot/img", Filename);
                var dbpath = Path.Combine("img/", Filename);
                using(var stram = new FileStream(FilePath , FileMode.Create))
                {
                    user_img.CopyTo(stram);
                }
                a.user_img = dbpath;
                a.role_id = 3;
                dbcontext.Add(a);
                dbcontext.SaveChanges();
                return RedirectToAction("show_employ");
            }
            }
            else
            {
                return RedirectToAction("add_employ", new { signup_error = "Email Is Already Taken" });

            }
            return View();
        }

        [Authorize]
        public IActionResult show_employ()
        {
           var data = dbcontext.users.ToList();
            return View(data);
        }

       public IActionResult delete_employ(int? id)
        {
            var FintId = dbcontext.users.Find(id);
            if(FintId == null)
            {
                return Content("Could'n get the Id");
            }
            else
            {
                dbcontext.Remove(FintId);
                dbcontext.SaveChanges();
                return RedirectToAction("show_employ");
            }
        }

        public IActionResult product_model()
        {
            ViewBag.user_name = new SelectList(dbcontext.users, "user_id", "user_name");
            return View();
        }

        public IActionResult product_add(Products model)
        {
            var product_verify = "";

            Products products = new Products()
            {
                product_name = model.product_name,
                product_description = model.product_description,
                product_price = model.product_price,
                product_verify = product_verify,
                user_id = model.user_id
            };

            dbcontext.Add(products);
            dbcontext.SaveChanges();

            return RedirectToAction("product_model");
        }

        public IActionResult show_product()
        {
            var product = dbcontext.Products.Include(s => s.Users).ToList();
            return View(product);
        }

        public IActionResult verify_product(int? id)
        {
            var product_verify = dbcontext.Products.Include(s => s.Users).Where(s => s.user_id == id).ToList();
            return View(product_verify);
            
        }

        [HttpPost]
        public IActionResult p_verify()
        {
            var name = Request.Form["p_name"].ToString();
            var desc = Request.Form["p_desc"].ToString();
            var price  = Request.Form["p_price"].ToString();
            int user_id = int.Parse(Request.Form["user_id"]);
            int p_id = int.Parse(Request.Form["p_id"]);
            var p_verify = "1";

            Products products = new Products()
            {
                product_id = p_id,
                product_description = desc,
                product_name = name,
                product_price = price,
                user_id = user_id,
                product_verify = p_verify
            };
            dbcontext.Update(products);
            dbcontext.SaveChanges();
            return RedirectToAction("show_product");
        }

        [HttpPost]
        public IActionResult p_cancel()
        {
            var name = Request.Form["p_name"].ToString();
            var desc = Request.Form["p_desc"].ToString();
            var price = Request.Form["p_price"].ToString();
            int user_id = int.Parse(Request.Form["user_id"]);
            int p_id = int.Parse(Request.Form["p_id"]);
            var p_verify = "0";

            Products products = new Products()
            {
                product_id = p_id,
                product_description = desc,
                product_name = name,
                product_price = price,
                user_id = user_id,
                product_verify = p_verify
            };
            dbcontext.Update(products);
            dbcontext.SaveChanges();
            return RedirectToAction("show_product");
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}
