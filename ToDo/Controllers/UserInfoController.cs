using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ToDo.DAL;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class UserInfoController : Controller
    {
        private readonly ToDoContext db = new ToDoContext();

        // GET: UserInfo
        public ActionResult Index()
        {
            if (Session["LOGGED_USERID"] == null || Session["LOGGED_USERNAME"] == null)
                return RedirectToAction("Index", "Login");
            var UserID = Convert.ToInt32(Session["LOGGED_USERID"].ToString());
            var UserName = Session["LOGGED_USERNAME"].ToString();
            var currentUser = db.User.Where(x => x.UserId.Equals(UserID) && x.Name.Equals(UserName)).FirstOrDefault();
            return View(currentUser);
        }

        [HttpGet]
        public ActionResult UserInfoModify(int? id)
        {
            if (Session["LOGGED_USERID"] == null || Session["LOGGED_USERNAME"] == null)
                return RedirectToAction("Index", "Login");
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var userModel = db.User.Find(id);
            if (userModel == null) return HttpNotFound();
            return View(userModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserInfoModify([Bind(Include = "UserId,Name,Email,Password,ConfirmPassword,SecretWord")]
            UserModel userModel)
        {
            if (Session["LOGGED_USERID"] == null || Session["LOGGED_USERNAME"] == null)
                return RedirectToAction("Index", "Login");
            userModel.ModifiedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(userModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "ToDoList");
            }

            return View(userModel);
        }
    }
}