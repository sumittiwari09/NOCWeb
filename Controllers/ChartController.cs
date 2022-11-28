using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Metrica.Controllers
{
    public class ChartController : Controller
    {
        public ViewResult apex() => View();
        public ViewResult chartist() => View();
        public ViewResult chartjs() => View();
        public ViewResult flot() => View();
        public ViewResult justgage() => View();
        public ViewResult knob() => View();
        public ViewResult morris() => View();
        public ViewResult peity() => View();
        public ViewResult sparkline() => View();        
    }
}