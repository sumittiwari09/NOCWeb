using System.Web.Mvc;

namespace Metrica.Controllers
{
    public class AuthenticationController : Controller
    {
        public ActionResult login() => View();

        [ActionName("login-alt")]
        public ActionResult loginAlt() => View();

        public ActionResult register() => View();

        [ActionName("register-alt")]
        public ActionResult registerAlt() => View();

        [ActionName("recover-pw")]
        public ActionResult recoverPw() => View();
        
        [ActionName("recover-pw-new")]
        public ActionResult recoverPwNew() => View();

        [ActionName("recover-pw-alt")]
        public ActionResult recoverAlt() => View();
        
        [ActionName("lock-screen")]
        public ActionResult lockScreen() => View();

        [ActionName("lock-screen-alt")]
        public ActionResult lockScreenAlt() => View();

        [ActionName("404")]
        public ActionResult NotFound() => View();

        [ActionName("404-alt")]
        public ActionResult NotFoundAlt() => View();

        [ActionName("500")]
        public ActionResult InternalServerError() => View();

        [ActionName("500-alt")]
        public ActionResult InternalServerErrorAlt() => View();
    }
}