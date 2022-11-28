using System.Web.Mvc;

namespace Metrica.Controllers
{
    [Route("crypto")]
    public class CryptoController : Controller
    {
        [ActionName("exchange")]
        public ViewResult Exchange() => View();

        [ActionName("wallet")]
        public ViewResult Wallet() => View();

        [ActionName("news")]
        public ViewResult News() => View();

        [ActionName("ico")]
        public ViewResult Ico() => View();

        [ActionName("settings")]
        public ViewResult Settings() => View();
    }
}