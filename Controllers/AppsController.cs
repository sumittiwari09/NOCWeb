using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Metrica.Controllers
{
    public class AppsController : Controller
    {
        public ViewResult Chat() => View();
        public ViewResult Contacts() => View();
        public ViewResult Calendar() => View();
        public ViewResult Invoice() => View();
    }
}