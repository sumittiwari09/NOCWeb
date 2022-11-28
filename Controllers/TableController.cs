using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Metrica.Controllers
{
    public class TableController : Controller
    {
        public ViewResult basic() => View();
        public ViewResult datatables() => View();
        public ViewResult responsive() => View();
        public ViewResult footable() => View();
        public ViewResult jsgrid() => View();
        public ViewResult dragger() => View();
        public ViewResult editable() => View();
    }
}