using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDo.DAL;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class RegisterController : Controller
    {
        private ToDoContext db = new ToDoContext();
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["LOGGED_USERNAME"] == null || Session["LOGGED_USERNAME"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "ToDoList");
            }
        }
        [HttpPost]
        public ActionResult Index([Bind(Include = "Name,Email,Password,ConfirmPassword,SecretWord")] UserModel userModel)
        {
            UserModel userInfo = db.User.Where(x => (x.Email == userModel.Email)).FirstOrDefault<UserModel>();
            if (userInfo != null)
            {
                return Json(new { responseCode = "400", responseText = "Email Already Exist" },
                        JsonRequestBehavior.AllowGet);
            }

            userModel.CreatedDate = DateTime.Now;
            userModel.ModifiedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.User.Add(userModel);
                db.SaveChanges();
                return Json(new { responseCode = "200", responseText = "Success" },
                        JsonRequestBehavior.AllowGet);

            }
            return Json(new { responseCode = "400", responseText = "Something Went Wrong" },
                        JsonRequestBehavior.AllowGet);
        }
    }
}