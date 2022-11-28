using System.Web.Mvc;

namespace Metrica.Controllers
{
    [Route("analytics")]
    public class AnalyticsController : Controller
    {

        [ActionName("customers")]
        public ViewResult Customers() => View();

        [ActionName("reports")]
        public ViewResult Reports() => View();

    } 
}