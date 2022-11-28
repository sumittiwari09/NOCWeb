using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using NewZapures_V2.Helper;
using NewZapures_V2.Models;
using PagedList;

namespace NewZapures_V2.Controllers
{
    [NoDirectAccess]
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index(int page=1)
        {
            int PageIndex = 1;
            PageIndex = page;
            HttpClient client = new HttpClient();

            HttpResponseMessage response = client.GetAsync("https://localhost:44327/CategoryMasters/GetAll").Result;
            var obj = JsonConvert.DeserializeObject<Response>(response.Content.ReadAsStringAsync().Result);
            var objData = JsonConvert.DeserializeObject<List<CategoryMaster>>(obj.Data.ToString());
           // var objData = JsonConvert.DeserializeObject(obj.Data.ToString());
            
            var ListDepartments = new List<FillDropDown>()
            {
                new FillDropDown() {Id = 1, Text="Company" },
                new FillDropDown() {Id = 2, Text="Distributor" },
                new FillDropDown() {Id = 3, Text="Retail" },
            };
            // Retrieve departments and build SelectList
            ViewBag.FillDropDown = ListDepartments;
            var lst = new List<string>() { "india", "Chine" };
            ViewBag.list = lst;
            List<CategoryMaster> lstcategory = new List<CategoryMaster>();
            List<CategoryMaster> lsts = new List<CategoryMaster>();
            foreach(var item in (List<CategoryMaster>)objData)
            {
                lsts.Add(item);
            }
            ViewBag.TotalRecords = obj.Total;
            return View(lsts.ToList().ToPagedList(PageIndex, 2));
        }
    }
}