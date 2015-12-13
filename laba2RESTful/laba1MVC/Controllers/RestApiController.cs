using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

using laba1MVC.Models;
using laba1MVC.Repository;
namespace laba1MVC.Controllers
{
    public class RestApiController : ApiController
    {
        //
        // GET: /RestApi/

        public IEnumerable<ToDoModel> GetModels()
        {


            return ToDoRepository.Get(); 
        }

    }
}
