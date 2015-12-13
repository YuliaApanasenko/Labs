using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using laba1MVC.Models;

namespace laba1MVC.Controllers
{
    public class ToDoController : Controller
    {
        //
        // GET: /ToDo/
        List<ToDoModel> ToDoList = new List<ToDoModel>();
        [HttpGet]
       // [OutputCacheAttribute(VaryByParam = "*", Duration = 1, NoStore = true)]
        public ActionResult Index()
        {
            if ((List<ToDoModel>)Session["ToDoList"] != null)
            ToDoList = (List<ToDoModel>)Session["ToDoList"];
            Session["ToDoList"] = ToDoList;
            return View(ToDoList);
        }
        
        

        [HttpGet]
        public ActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(String toDo)
        {
            ToDoList = (List<ToDoModel>)Session["ToDoList"];
            ToDoModel newModel = new ToDoModel();
            newModel.ToDo = toDo;
            newModel.State = false;
            ToDoList.Add(newModel);
            Session["ToDoList"] = ToDoList;
            return RedirectToAction("Index") ;
        }
       [HttpPost]
        public ActionResult SetChecked(string toDo, bool value)
        {
            ToDoList = (List<ToDoModel>)Session["ToDoList"];
            ToDoModel editModel = ToDoList.First(x => x.ToDo == toDo);
            editModel.State = value;
            Session["ToDoList"] = ToDoList;
            //return Redirect(Request.UrlReferrer.ToString());
            return RedirectToAction("Index", "ToDo");
        }

        [HttpGet]
        public ActionResult Delete(string toDo)
        {
            ToDoList = (List<ToDoModel>)Session["ToDoList"];
            ToDoModel model = ToDoList.Find(x => x.ToDo == toDo);
            ToDoList.Remove(model);
            Session["ToDoList"] = ToDoList;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ClearCompleted()
        {
            ToDoList = (List<ToDoModel>)Session["ToDoList"];
            List<ToDoModel> models = ToDoList.FindAll(x => x.State == true);
            ToDoList.RemoveAll(x => x.State == true);
            Session["ToDoList"] = ToDoList;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Sort(string condition)
        {
            ToDoList = (List<ToDoModel>)Session["ToDoList"];
            List<ToDoModel> result = new List<ToDoModel>();
            switch (condition)
            {
                case "All":
                    result = ToDoList;
                    break;
                case "Active":
                    result = ToDoList.FindAll(x => x.State == false);
                    break;
                case "Completed":
                    result = ToDoList.FindAll(x => x.State == true);
                    break;
            }
            Session["ToDoList"] = ToDoList;
            return View("Index", result);
        }



    }
}
