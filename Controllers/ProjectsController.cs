using System.Web.Mvc;

namespace Metrica.Controllers
{
    [Route("projects")]
    public class ProjectsController : Controller
    {
        public ViewResult clients() => View();
        public ViewResult team() => View();
        public ViewResult project() => View();
        public ViewResult task() => View();

        [ActionName("kanban-board")]
        public ViewResult kanbanboard() => View();
        public ViewResult chat() => View();
        public ViewResult users() => View();
        public ViewResult create() => View();
    }
}