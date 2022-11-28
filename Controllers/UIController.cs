using System.Web.Mvc;

namespace Metrica.Controllers
{
    public class UIController : Controller
    {
        public ActionResult bootstrap() => View();
        public ActionResult animation() => View();
        public ActionResult avatar() => View();
        public ActionResult clipboard() => View();
        public ActionResult files() => View();
        public ActionResult ribbons() => View();
        public ActionResult dragula() => View();

        [ActionName("check-radio")]
        public ActionResult CheckRadio() => View();


        public ActionResult rangeslider() => View();
        public ActionResult sweetalerts() => View();
        public ActionResult nestable() => View();
        public ActionResult ratings() => View();
        public ActionResult highlight() => View();
        public ActionResult session() => View();

        [ActionName("idle-timer")]
        public ActionResult IdleTimer() => View();
    }
}