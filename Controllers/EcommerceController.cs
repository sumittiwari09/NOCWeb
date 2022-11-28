using System.Web.Mvc;

namespace Metrica.Controllers
{
    public class EcommerceController : Controller
    {
        public ViewResult index() => View();
        public ViewResult cart() => View();
        public ViewResult checkout() => View();

        [ActionName("product-detail")]
        public ViewResult ProductDetail() => View();

        [ActionName("product-list")]
        public ViewResult ProductList() => View();
        public ViewResult products() => View();
    }
}