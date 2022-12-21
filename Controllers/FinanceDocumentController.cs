using Newtonsoft.Json;
using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace NewZapures_V2.Controllers
{
    public class FinanceDocumentController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        // GET: FinanceDocument
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FinanceDocumentUpload(FinancialDocumentBO obj, HttpPostedFileBase Affidavit, HttpPostedFileBase Salary, HttpPostedFileBase Bank, HttpPostedFileBase Annexure, HttpPostedFileBase ESI, HttpPostedFileBase VCI, HttpPostedFileBase Frame, HttpPostedFileBase Admission, HttpPostedFileBase Land, HttpPostedFileBase SubmitAffidavit, HttpPostedFileBase Other)
        {
            byte[] Documentbyte;
            string extension = string.Empty;
            string ContentType = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    #region "Affidavit"
                    if (Affidavit != null)
                    {
                        extension = Path.GetExtension(Affidavit.FileName);
                        ContentType = Affidavit.ContentType;
                        using (Stream inputStream = Affidavit.InputStream)
                        {
                            MemoryStream memoryStream = inputStream as MemoryStream;
                            if (memoryStream == null)
                            {
                                memoryStream = new MemoryStream();
                                inputStream.CopyTo(memoryStream);
                            }
                            Documentbyte = memoryStream.ToArray();
                            obj.Affidavit = Convert.ToBase64String(Documentbyte);
                            obj.AffidavitExtension = extension;
                            obj.AffidavitContentType = ContentType;
                        }
                    }
                    #endregion

                    #region "Salary Payment"
                    if (Salary != null)
                    {
                        extension = Path.GetExtension(Salary.FileName);
                        ContentType = Salary.ContentType;
                        using (Stream inputStream = Salary.InputStream)
                        {
                            MemoryStream memoryStream = inputStream as MemoryStream;
                            if (memoryStream == null)
                            {
                                memoryStream = new MemoryStream();
                                inputStream.CopyTo(memoryStream);
                            }
                            Documentbyte = memoryStream.ToArray();
                            obj.Salary = Convert.ToBase64String(Documentbyte);
                            obj.SalaryExtension = extension;
                            obj.SalaryContentType = ContentType;
                        }
                    }
                    #endregion

                    #region "Bank Statement"
                    if (Bank != null)
                    {
                        extension = Path.GetExtension(Bank.FileName);
                        ContentType = Bank.ContentType;
                        using (Stream inputStream = Bank.InputStream)
                        {
                            MemoryStream memoryStream = inputStream as MemoryStream;
                            if (memoryStream == null)
                            {
                                memoryStream = new MemoryStream();
                                inputStream.CopyTo(memoryStream);
                            }
                            Documentbyte = memoryStream.ToArray();
                            obj.Bank = Convert.ToBase64String(Documentbyte);
                            obj.BankExtension = extension;
                            obj.BankContentType = ContentType;
                        }
                    }
                    #endregion

                    #region "Annexure 16"
                    if (Annexure != null)
                    {
                        extension = Path.GetExtension(Annexure.FileName);
                        ContentType = Annexure.ContentType;
                        using (Stream inputStream = Annexure.InputStream)
                        {
                            MemoryStream memoryStream = inputStream as MemoryStream;
                            if (memoryStream == null)
                            {
                                memoryStream = new MemoryStream();
                                inputStream.CopyTo(memoryStream);
                            }
                            Documentbyte = memoryStream.ToArray();
                            obj.Annexure = Convert.ToBase64String(Documentbyte);
                            obj.AnnexureExtension = extension;
                            obj.AnnexureContentType = ContentType;
                        }
                    }
                    #endregion

                    #region "ESI Deduction"
                    if (ESI != null)
                    {
                        extension = Path.GetExtension(ESI.FileName);
                        ContentType = ESI.ContentType;
                        using (Stream inputStream = ESI.InputStream)
                        {
                            MemoryStream memoryStream = inputStream as MemoryStream;
                            if (memoryStream == null)
                            {
                                memoryStream = new MemoryStream();
                                inputStream.CopyTo(memoryStream);
                            }
                            Documentbyte = memoryStream.ToArray();
                            obj.ESI = Convert.ToBase64String(Documentbyte);
                            obj.ESIExtension = extension;
                            obj.ESIContentType = ContentType;
                        }
                    }
                    #endregion

                    #region "Gol/Supreme Court/Gor/VCI/Concerned"
                    if (VCI != null)
                    {
                        extension = Path.GetExtension(VCI.FileName);
                        ContentType = VCI.ContentType;
                        using (Stream inputStream = VCI.InputStream)
                        {
                            MemoryStream memoryStream = inputStream as MemoryStream;
                            if (memoryStream == null)
                            {
                                memoryStream = new MemoryStream();
                                inputStream.CopyTo(memoryStream);
                            }
                            Documentbyte = memoryStream.ToArray();
                            obj.VCI = Convert.ToBase64String(Documentbyte);
                            obj.VCIExtension = extension;
                            obj.VCIContentType = ContentType;
                        }
                    }
                    #endregion

                    #region "Time Frame"
                    if (Frame != null)
                    {
                        extension = Path.GetExtension(Frame.FileName);
                        ContentType = Frame.ContentType;
                        using (Stream inputStream = Frame.InputStream)
                        {
                            MemoryStream memoryStream = inputStream as MemoryStream;
                            if (memoryStream == null)
                            {
                                memoryStream = new MemoryStream();
                                inputStream.CopyTo(memoryStream);
                            }
                            Documentbyte = memoryStream.ToArray();
                            obj.Frame = Convert.ToBase64String(Documentbyte);
                            obj.FrameExtension = extension;
                            obj.FrameContentType = ContentType;
                        }
                    }
                    #endregion

                    #region "Selection/Admission"
                    if (Admission != null)
                    {
                        extension = Path.GetExtension(Admission.FileName);
                        ContentType = Admission.ContentType;
                        using (Stream inputStream = Admission.InputStream)
                        {
                            MemoryStream memoryStream = inputStream as MemoryStream;
                            if (memoryStream == null)
                            {
                                memoryStream = new MemoryStream();
                                inputStream.CopyTo(memoryStream);
                            }
                            Documentbyte = memoryStream.ToArray();
                            obj.Admission = Convert.ToBase64String(Documentbyte);
                            obj.AdmissionExtension = extension;
                            obj.AdmissionContentType = ContentType;
                        }
                    }
                    #endregion

                    #region "Sufficient Land for development"
                    if (Land != null)
                    {
                        extension = Path.GetExtension(Land.FileName);
                        ContentType = Land.ContentType;
                        using (Stream inputStream = Land.InputStream)
                        {
                            MemoryStream memoryStream = inputStream as MemoryStream;
                            if (memoryStream == null)
                            {
                                memoryStream = new MemoryStream();
                                inputStream.CopyTo(memoryStream);
                            }
                            Documentbyte = memoryStream.ToArray();
                            obj.Land = Convert.ToBase64String(Documentbyte);
                            obj.LandExtension = extension;
                            obj.LandContentType = ContentType;
                        }
                    }
                    #endregion

                    #region "Submit Affidavit"
                    if (SubmitAffidavit != null)
                    {
                        extension = Path.GetExtension(SubmitAffidavit.FileName);
                        ContentType = SubmitAffidavit.ContentType;
                        using (Stream inputStream = SubmitAffidavit.InputStream)
                        {
                            MemoryStream memoryStream = inputStream as MemoryStream;
                            if (memoryStream == null)
                            {
                                memoryStream = new MemoryStream();
                                inputStream.CopyTo(memoryStream);
                            }
                            Documentbyte = memoryStream.ToArray();
                            obj.SubmitAffidavit = Convert.ToBase64String(Documentbyte);
                            obj.SubmitAffidavitExtension = extension;
                            obj.SubmitAffidavitContentType = ContentType;
                        }
                    }
                    #endregion

                    #region "Other Documents"
                    if (Other != null)
                    {
                        extension = Path.GetExtension(Other.FileName);
                        ContentType = Other.ContentType;
                        using (Stream inputStream = Other.InputStream)
                        {
                            MemoryStream memoryStream = inputStream as MemoryStream;
                            if (memoryStream == null)
                            {
                                memoryStream = new MemoryStream();
                                inputStream.CopyTo(memoryStream);
                            }
                            Documentbyte = memoryStream.ToArray();
                            obj.Other = Convert.ToBase64String(Documentbyte);
                            obj.OtherExtension = extension;
                            obj.OtherContentType = ContentType;
                        }
                    }
                    #endregion
                }


                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Finance/addFinanceDocument");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                _JsonSerializer.MaxJsonLength = Int32.MaxValue;
                request.AddParameter("application/json", _JsonSerializer.Serialize(obj), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    var objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);

                    TempData["isSaved"] = 1;
                    TempData["msg"] = " Details Saved...";
                    return RedirectToAction("Index", "FinanceDocument");
                }
                else
                {
                    TempData["isSaved"] = 0;
                    TempData["msg"] = " Details Not Saved...";
                    return RedirectToAction("Index", "FinanceDocument");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index", "FinanceDocument");
        }
    }
}