using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Metrica.Controllers
{
    public class EmailController : Controller
    {
        public ActionResult Inbox() => View();
        
        [ActionName("email-read")]
        public ActionResult EmailRead() => View();

        public ViewResult basic() => View();
        public ViewResult alert() => View();
        public ViewResult billing() => View();
    }
}