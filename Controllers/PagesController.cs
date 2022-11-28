using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Metrica.Controllers
{
    public class PagesController : Controller
    {
        public ActionResult profile() => View();
        public ActionResult tour() => View();
        public ActionResult timeline() => View();
        public ActionResult treeview() => View();
        public ActionResult starter() => View();
        public ActionResult pricing() => View();
        public ActionResult blogs() => View();
        public ActionResult faq() => View();
        public ActionResult gallery() => View();

    }
}