using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Metrica.Controllers
{
    public class MapsController : Controller
    {
        public ViewResult Google() => View();
        public ViewResult Leaflet() => View();
        public ViewResult Vector() => View();
    }
}