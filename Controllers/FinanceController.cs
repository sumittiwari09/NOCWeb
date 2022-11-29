using Newtonsoft.Json;
using NewZapures_V2.Helper;
using NewZapures_V2.Models;
using RestSharp;
using System.Configuration;
using System.Web.Mvc;

namespace NewZapures_V2.Controllers
{
    public class FinanceController : Controller
    {
        //ResponseData objResponse;
        public ActionResult Index()
        {
            
            return View();
        }
    }
}