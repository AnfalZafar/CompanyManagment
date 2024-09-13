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
        public Models_Bind Models_Bind = new Models_Bind();
        public HomeController(ILogger<HomeController> logger , Data a)
        {
            this.dbcontext = a;
            _logger = logger;
            Models_Bind.Products = dbcontext.Products.ToList();
            Models_Bind.Users = dbcontext.users.ToList();
            Models_Bind.Messages = dbcontext.Messages.ToList();
            Models_Bind.message_Replays = dbcontext.message_Replays.ToList();
        }

        [Authorize]
        public IActionResult Index()
        {
            var show_product = dbcontext.Products.Include(s => s.Users).ToList();
            ViewBag.showProduct = show_product;

            return View(Models_Bind);
        }

        [Authorize]
        public IActionResult user_profile()
        {
            return View(Models_Bind);
        }

        [Authorize]
        public IActionResult add_employ(string signup_error)
        {
            ViewBag.signup_error = signup_error;

            return View(Models_Bind);
        }

        [HttpPost]
        public IActionResult submit_employ()
        {
            if (!string.IsNullOrEmpty(Session_Class.User_Id)) {
                var user_name = Request.Form["user_name"].ToString();
                var user_pass = Request.Form["user_pass"].ToString();
                var user_email = Request.Form["user_email"].ToString();
                var user_phone = Request.Form["user_phone"].ToString();
                var user_address = Request.Form["user_address"].ToString();
                var user_img = Request.Form.Files["user_img"];
                int role_id = 3;
                var cheak = dbcontext.users.FirstOrDefault(s => s.user_email == user_email);
                if (cheak == null) {
                    if (user_img != null && user_img.Length > 0)
                    {
                        var Filename = Path.GetFileName(user_img.FileName);
                        var FilePath = Path.Combine("wwwroot/img", Filename);
                        var dbpath = Path.Combine("img/", Filename);
                        using (var stram = new FileStream(FilePath, FileMode.Create))
                        {
                            user_img.CopyTo(stram);
                        }
                        Users products = new Users()
                        {
                            user_name = user_name,
                            user_phone = user_phone,
                            user_email = user_email,
                            user_password = user_pass,
                            user_address = user_address,
                            user_img = dbpath,
                            role_id = role_id
                        };

                        dbcontext.Add(products);
                        dbcontext.SaveChanges();
                        return RedirectToAction("show_employ");
                    }
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
           ViewBag.users = dbcontext.users.ToList();
            return View(Models_Bind);
        }

       public IActionResult delete_employ(int? id)
        {
            if (!string.IsNullOrEmpty(Session_Class.User_Id))
            {

                var FintId = dbcontext.users.Find(id);
                if (FintId == null)
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
            return Content("Could'n delete");
        }

        public IActionResult product_model()
        {
            ViewBag.user_name = new SelectList(dbcontext.users, "user_id", "user_name");
            return View(Models_Bind);
        }

        [HttpPost]
        public IActionResult product_add(Products model)
        {
            if (!string.IsNullOrEmpty(Session_Class.User_Id))
            {

                var p_name = Request.Form["name"].ToString();
                var p_price = Request.Form["price"].ToString();
                var p_desc = Request.Form["desc"].ToString();
                int user_id = int.Parse(Request.Form["user_id"]);
                var product_verify = "";

                Products products = new Products()
                {
                    product_name = p_name,
                    product_description = p_desc,
                    product_price = p_price,
                    product_verify = product_verify,
                    user_id = user_id
                };

                dbcontext.Add(products);
                dbcontext.SaveChanges();

                return RedirectToAction("product_model");
            }
            return Content("Can't add ");
        }

        [Authorize]
        public IActionResult show_product()
        {
            ViewBag.product = dbcontext.Products.Include(s => s.Users).ToList();
            return View(Models_Bind);
        }

        [Authorize]
        public IActionResult verify_product(int? id)
        {
            ViewBag.verify = dbcontext.Products.Include(s => s.Users).Where(s => s.user_id == id).ToList();
            return View(Models_Bind);
            
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
           var update = dbcontext.Products.Find(p_id);
            if (update == null)
            {
                return Content("Id not found");
            }
            dbcontext.Entry(update).CurrentValues.SetValues(products);
            dbcontext.SaveChanges();
            return RedirectToAction("show_product");
        }

        [Authorize]
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
            var delete = dbcontext.Products.Find(p_id);
            if (delete == null)
            {
                return Content("Id not found");
            }
            dbcontext.Entry(delete).CurrentValues.SetValues(products);
            dbcontext.SaveChanges();
            return RedirectToAction("show_product");
        }

        [Authorize]
        public IActionResult cancel_product()
        {
            ViewBag.cancel_product = dbcontext.Products.Include(s => s.Users).ToList();
            return View(Models_Bind);
        }

        [Authorize]
        public IActionResult pending_product()
        {
            ViewBag.pending_product = dbcontext.Products.Include(s => s.Users).ToList();
            return View(Models_Bind);
        }

        [Authorize]
        public IActionResult Reject_product()
        {
            ViewBag.reject_product = dbcontext.Products.Include(s => s.Users).ToList();
            return View(Models_Bind);
        }

        public IActionResult update_cancel_product(int? id)
        {
            return View();
        }

        [Authorize]
        public IActionResult your_pending_product()
        {
            ViewBag.your_product = dbcontext.Products.Include(s => s.Users).ToList();
            return View(Models_Bind);
        }

        [Authorize]
        public IActionResult admin_message(string id_message)
        {
            ViewBag.id_message = id_message;
            ViewBag.message = new SelectList(dbcontext.users.Where(s =>s.role_id == 1), "user_id", "user_name");
            return View(Models_Bind);
        }

        [HttpPost]
        public IActionResult submit_admin_message()
        {
            if (!string.IsNullOrEmpty(Session_Class.User_Id))
            {
              
                var message = Request.Form["message"].ToString();
                var to_email = Request.Form["to_email"].ToString();
                var from_email = Request.Form["from_email"].ToString();
               
                Messages messages = new Messages()
                {
                    message_object = message,
                    to_email = to_email,
                    from_email = from_email,
                };
                dbcontext.Add(messages);
                dbcontext.SaveChanges();
                return RedirectToAction("admin_message");
            }
            return RedirectToAction("signup" , "SignUp");
        }

        [Authorize]
        public IActionResult employ_message(string id_message)
        {
            ViewBag.id_message = id_message;
            return View(Models_Bind);
        }

        [HttpPost]
        public IActionResult submit_employ_message()
        {
            if (!string.IsNullOrEmpty(Session_Class.User_Id))
            {

                var message = Request.Form["message"].ToString();
                var to_email = Request.Form["to_email"].ToString();
                var from_email = Request.Form["from_email"].ToString();
                int user_id = int.Parse(Request.Form["user_id"]);

                Messages messages = new Messages()
                {
                    message_object = message,
                    to_email = to_email,
                    from_email = from_email,
                    user_id = user_id
                };
                dbcontext.Add(messages);
                dbcontext.SaveChanges();
                return RedirectToAction("admin_message");
            }
            return RedirectToAction("signup", "SignUp");
        }

        [Authorize]
        public IActionResult message_profile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = dbcontext.Messages.Where(s => s.message_id == id).ToList();

            if (message == null)
            {
                return NotFound();
            }

            ViewBag.find_id = message;
            return View(Models_Bind);
        }

        [HttpPost]
        public IActionResult replay_message()
        {
            int message_id = int.Parse(Request.Form["message_user_id"]);
            var replay_text = Request.Form["replay_text"].ToString();
            Message_Replay message_Replay = new Message_Replay()
            {
                message_id = message_id,
                Replay_text = replay_text
            };
            dbcontext.Add(message_Replay);
            dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult your_messages()
        {
            ViewBag.messages = dbcontext.message_Replays.Include(s => s.Messages).ToList();
      
            return View(Models_Bind);
        }

        public IActionResult replay_message_profile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var message_id = dbcontext.message_Replays.Include(s => s.Messages).Where(s => s.Id == id).ToList();
           if(message_id == null)
            {
                return Content("message not found");
            }
            ViewBag.message_id = message_id;
            return View(Models_Bind);
        }

        [HttpPost]
        public IActionResult replay_message_replay()
        {
            int message_id = int.Parse(Request.Form["message_user_id"]);
            var replay_text = Request.Form["replay_text"].ToString();
            Message_Replay message_Replay = new Message_Replay()
            {
                message_id = message_id,
                Replay_text = replay_text
            };
            dbcontext.Add(message_Replay);
            dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }   
        public IActionResult you_replay_messages()
        {
            ViewBag.messages = dbcontext.message_Replays.Include(s => s.Messages).ToList();
            return View(Models_Bind);
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
