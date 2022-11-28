using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Metrica.Controllers
{
    [Route("crm")]
    public class CRMController : Controller
    {
        public ActionResult contacts() => View();
        public ActionResult opportunities() => View();
        public ActionResult leads() => View();   
        public ActionResult customers() => View();

    }
}