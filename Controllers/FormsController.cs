using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Metrica.Controllers
{
    public class FormsController : Controller
    {
        public ActionResult elements() => View();
        public ActionResult advanced() => View();
        public ActionResult validation() => View();
        public ActionResult wizard() => View();
        public ActionResult editors() => View();
        public ActionResult repeater() => View();
        
        [ActionName("x-editable")]
        public ActionResult XEditable() => View();
        public ActionResult uploads() => View();

        [ActionName("img-crop")]
        public ActionResult ImageCrop() => View();
    }
}