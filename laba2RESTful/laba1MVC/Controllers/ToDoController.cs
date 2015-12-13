using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using laba1MVC.Models;
using laba1MVC.Repository;


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
            List<ToDoModel> temp = ToDoRepository.Get();
            if (temp != null)
                ToDoList = temp;
            ToDoRepository.Save(ToDoList);
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
            ToDoList = ToDoRepository.Get();
            ToDoModel newModel = new ToDoModel();
            newModel.ToDo = toDo;
            newModel.State = false;
            ToDoList.Add(newModel);
           ToDoRepository.Save(ToDoList);
            return RedirectToAction("Index") ;
        }
       [HttpPost]
        public ActionResult SetChecked(string toDo, bool value)
        {
            ToDoList = ToDoRepository.Get();
            ToDoModel editModel = ToDoList.First(x => x.ToDo == toDo);
            editModel.State = value;
            ToDoRepository.Save(ToDoList);
            //return Redirect(Request.UrlReferrer.ToString());
            return RedirectToAction("Index", "ToDo");
        }

        [HttpGet]
        public ActionResult Delete(string toDo)
        {
            ToDoList = ToDoRepository.Get();
            ToDoModel model = ToDoList.Find(x => x.ToDo == toDo);
            ToDoList.Remove(model);
            ToDoRepository.Save(ToDoList);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ClearCompleted()
        {
            ToDoList = ToDoRepository.Get();
            List<ToDoModel> models = ToDoList.FindAll(x => x.State == true);
            ToDoList.RemoveAll(x => x.State == true);
            ToDoRepository.Save(ToDoList);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Sort(string condition)
        {
            ToDoList = ToDoRepository.Get();
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
            ToDoRepository.Save(ToDoList);
            return View("Index", result);
        }
       

        void Session_Start(object sender, EventArgs e)
        {
        }

        void Session_End(object sender, EventArgs e)
        {

        }

    }
}
