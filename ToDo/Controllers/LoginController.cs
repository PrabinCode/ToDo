using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ToDo.DAL;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class LoginController : Controller
    {
        private readonly ToDoContext db = new ToDoContext();

        #region Log Out

        [HttpPost]
        public ActionResult LogOut(FormCollection collection)
        {
            Session["LOGGED_USERNAME"] = null;
            Session["LOGGED_USERNAME"] = null;
            return RedirectToAction("Index", "Login");
        }

        #endregion

        #region Login Index

        [HttpGet]
        public ActionResult Index()
        {
            if (Session["LOGGED_USERNAME"] == null || Session["LOGGED_USERNAME"] == null)
                return View("Index");
            return RedirectToAction("Index", "ToDoList");
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            var userName = collection["Email"] ?? string.Empty;
            var passWord = collection["Password"] ?? string.Empty;
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passWord))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var userModel = db.User.Where(x => x.Email == userName && x.Password == passWord).FirstOrDefault();
            if (userModel == null) return HttpNotFound();

            var loginModel = new LoginModel();
            loginModel.Email = userModel.Email;
            loginModel.Password = userModel.Password;
            loginModel.ConfirmPassword = userModel.ConfirmPassword;
            loginModel.LoginTime = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Login.Add(loginModel);
                db.SaveChanges();
            }

            Session["LOGGED_USERID"] = userModel.UserId;
            Session["LOGGED_USERNAME"] = userModel.Name;
            return RedirectToAction("Index", "ToDoList");
        }

        #endregion

        #region Forget Password

        [HttpGet]
        public ActionResult ForgetPassword()
        {
            if (Session["LOGGED_USERID"] == null || Session["LOGGED_USERNAME"] == null)
                return View();
            return RedirectToAction("Index", "ToDoList");
        }

        [HttpPost]
        public ActionResult ForgetPassword(FormCollection collection)
        {
            var Password = collection["Password"] ?? string.Empty;
            var ConfirmPassword = collection["ConfirmPassword"] ?? string.Empty;
            if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword) || Password != ConfirmPassword)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var Email = Session["Email"].ToString();
            var SecretWord = Session["SecretWord"].ToString();
            var userModel = new UserModel();
            userModel = db.User.Where(x => x.Email == Email && x.SecretWord == SecretWord).FirstOrDefault();
            userModel.Password = Password;
            userModel.ConfirmPassword = ConfirmPassword;
            userModel.ModifiedDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Entry(userModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult ForgetPasswordChecker(UserModel _userModel)
        {
            Session["Email"] = _userModel.Email;
            Session["SecretWord"] = _userModel.SecretWord;

            if (string.IsNullOrEmpty(_userModel.Email) || string.IsNullOrEmpty(_userModel.SecretWord))
                return Json(new {responseCode = "400", responseText = "Wrong Entry!! Check Again"},
                    JsonRequestBehavior.AllowGet);
            var userModel = db.User.Where(x => x.Email == _userModel.Email && x.SecretWord == _userModel.SecretWord)
                .FirstOrDefault();
            if (userModel == null)
                return Json(new {responseCode = "400", responseText = "Wrong Entry!! Check Again"},
                    JsonRequestBehavior.AllowGet);
            return Json(new {responseCode = "200", responseText = "Success"},
                JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}