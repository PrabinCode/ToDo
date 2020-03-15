using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToDo.DAL;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class ToDoListController : Controller
    {
        private ToDoContext db = new ToDoContext();
        public ActionResult Index()
        {
            if (Session["LOGGED_USERID"] == null || Session["LOGGED_USERNAME"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        private IEnumerable<TaskModel> GetTaskModels()
        {
            int UserID = Convert.ToInt32(Session["LOGGED_USERID"].ToString());
            string UserName = Session["LOGGED_USERNAME"].ToString();

            IEnumerable<TaskModel> taskModels = db.Task.ToList().Where(x => x.CreatedBy == UserID);
            int CompletedCount = 0;
            foreach (TaskModel taskModel in taskModels)
            {
                if (taskModel.IsComplete)
                {
                    CompletedCount++;
                }
            }
            ViewBag.CompletedCount = Math.Round(100f * ((float)CompletedCount / (float)taskModels.Count()));
            //UserModel currentUserId = db.User.Where(x => x.UserId.Equals(UserID) && x.Name.Equals(UserName)).FirstOrDefault<UserModel>();
            return taskModels;
        }

        public ActionResult BuildToDoTable()
        {
            return PartialView("_ToDoTable", GetTaskModels());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxCreate([Bind(Include = "Description")] TaskModel taskModel)
        {
            if (Session["LOGGED_USERID"] == null || Session["LOGGED_USERNAME"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            int UserID = Convert.ToInt32(Session["LOGGED_USERID"].ToString());
            taskModel.IsComplete = false;
            taskModel.CreatedBy = UserID;
            taskModel.CreatedAt = DateTime.Now;
            taskModel.UpdatedAt = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Task.Add(taskModel);
                db.SaveChanges();
            }

            return PartialView("_ToDoTable", GetTaskModels());
        }

        [HttpPost]
        public ActionResult AjaxUpdate(int? id, bool value)
        {
            if (Session["LOGGED_USERID"] == null || Session["LOGGED_USERNAME"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskModel taskModel = db.Task.Find(id);
            if (taskModel == null)
            {
                return HttpNotFound();
            }
            else
            {
                taskModel.IsComplete = value;
                if (ModelState.IsValid)
                {
                    db.Entry(taskModel).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return PartialView("_ToDoTable", GetTaskModels());
        }


        [HttpPost]
        public ActionResult AjaxDelete(int? id)
        {
            if (Session["LOGGED_USERID"] == null || Session["LOGGED_USERNAME"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            TaskModel taskModel = db.Task.Find(id);
            db.Task.Remove(taskModel);
            db.SaveChanges();
            return PartialView("_ToDoTable", GetTaskModels());

        }
    }
}