using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Metrica.Controllers
{
    public class HelpdeskController : Controller
    {
        public ViewResult agents() => View();
        public ViewResult reports() => View();
        public ViewResult teckets() => View();
    }
}