using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NewZapures_V2.Helper
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NoDirectAccessAttribute : ActionFilterAttribute
    {
        //TODO: This is not working with SSO
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //SendToLog("NoDirectAccessAttribute:OnActionExecuting");
            ///SendToLog("URL Referrer : " + filterContext.HttpContext.Request.UrlReferrer);
            //SendToLog("Request.Url.Host : " + filterContext.HttpContext.Request.Url.Host);
            ////SendToLog("Request.UrlReferrer.Host : " + filterContext.HttpContext.Request.UrlReferrer.Host);
            if (filterContext.HttpContext.Request.UrlReferrer == null ||
     filterContext.HttpContext.Request.Url.Host != filterContext.HttpContext.Request.UrlReferrer.Host)
            {

                var descriptor = filterContext.ActionDescriptor;
                var actionName = descriptor.ActionName;
                var controllerName = descriptor.ControllerDescriptor.ControllerName;

                filterContext.Result = new RedirectToRouteResult(new
                                          RouteValueDictionary(new { controller = "Welcome", action = "NoDirectAccess" }));
            }
            // SendToLog("NoDirectAccessAttribute:End");
        }
        //public void SendToLog(string Message)
        //{
        //    string sPathName = System.Web.Hosting.HostingEnvironment.MapPath("~/LogFile/Error");
        //    string sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";
        //    string sYear = DateTime.Now.Year.ToString();
        //    string sMonth = DateTime.Now.Month.ToString();
        //    string sDay = DateTime.Now.Day.ToString();
        //    string sErrorTime = sYear + sMonth + sDay;
        //    if (!System.IO.Directory.Exists(sPathName + sErrorTime))
        //    {
        //        System.IO.Directory.CreateDirectory(sPathName + sErrorTime);
        //    }
        //    System.IO.File.AppendAllText(sPathName + sErrorTime + "\\Log.txt", sLogFormat + "===================  SSO Log================================="
        //        + Environment.NewLine + Message + Environment.NewLine
        //          + "====================================================END" + Environment.NewLine);
        //}
    }
}