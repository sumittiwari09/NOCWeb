using NewZapures_V2.Models;
using System.Web.Mvc;

namespace Metrica.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index() {
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            var Token = Session["Token"];
            if (userdetailsSession != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("SignOut", "Home");
            }
        }

        public ActionResult WelcomeNoc()
        {
            return View();
        }

        //public ActionResult WelcomeNoc()
        //{
        //    return View();
        //}

        [ActionName("crypto-index")]
        public ActionResult CryptoIndex() => View();


        [ActionName("crm-index")]
        public ActionResult CrmIndex() => View();

        [ActionName("project-index")]
        public ActionResult ProjectIndex() => View();


        [ActionName("ecommerce-index")]
        public ActionResult EcommerceIndex() => View();


        [ActionName("helpdesk-index")]
        public ActionResult HelpdeskIndex() => View();

        [ActionName("hospital-index")]
        public ActionResult hospitalIndex() => View();
    }
}