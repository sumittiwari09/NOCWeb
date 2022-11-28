using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Metrica.Controllers
{
    public class IconController : Controller
    {
        public ViewResult materialdesign() => View();
        public ViewResult dripicons() => View();
        public ViewResult fontawesome() => View();
        public ViewResult themify() => View();
        public ViewResult feather() => View();
        public ViewResult typicons() => View();
        public ViewResult emoji() => View();
    }
}