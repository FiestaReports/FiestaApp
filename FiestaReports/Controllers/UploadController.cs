using FiestaReports.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace FiestaReports.Controllers
{
    public class UploadController : Controller
    {

        FiestaZohoDatabaseEntities fze;
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UploadExcel()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Upload")]
        public ActionResult UploadExcel(String nameReport)
        {
            /*******************************************************************
  Author      : Gopi
  Date        : 04/07/2017
  Description : Imports excel file data into specific reports table
 *******************************************************************/
            try
            {
                ViewData["Message"] = "";
                int i = Request.Files.Count;
                HttpPostedFileBase uploadFile = Request.Files[0];
                if ((uploadFile.ContentLength > 0) && (uploadFile != null))
                {
                    if (uploadFile.ContentType == "application/vnd.ms-excel" || uploadFile.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        if (nameReport == "RP")
                        {
                            TempData["RP_Message"] = "";
                            string filename = Path.GetFileName(uploadFile.FileName);                            
                            
                            string extn = filename.Split('.').ElementAt(1);
                            extn = '.' + extn;
                            Guid id = Guid.NewGuid();
                            string file = id + extn;
                            uploadFile.SaveAs(Server.MapPath("~/UploadedFiles/RP/") + file);
                            using (fze = new FiestaZohoDatabaseEntities())
                            {
                                fze.InsertFileUploads(filename, Convert.ToString(Session["UserEmail"]),nameReport, id);
                            }
                                TempData["RP_Message"] = "file has been uploaded";
                        }
                        if (nameReport == "AR")
                        {
                            TempData["AR_Message"] = "";
                            string filename = Path.GetFileName(uploadFile.FileName);
                            string extn = filename.Split('.').ElementAt(1);
                            extn = '.' + extn;
                            Guid id = Guid.NewGuid();                            
                            string file = id + extn;
                            uploadFile.SaveAs(Server.MapPath("~/UploadedFiles/AR/") + file);
                            using (fze = new FiestaZohoDatabaseEntities())
                            {
                                fze.InsertFileUploads(filename, Convert.ToString(Session["UserEmail"]), nameReport,id);
                            }
                            TempData["AR_Message"] = "file has been uploaded";

                        }
                        if (nameReport == "EFT")
                        {
                            TempData["EFT_Message"] = "";
                            string filename = Path.GetFileName(uploadFile.FileName);
                            string extn = filename.Split('.').ElementAt(1);
                            extn = '.' + extn;
                            Guid id = Guid.NewGuid();
                            string file = id + extn;
                            uploadFile.SaveAs(Server.MapPath("~/UploadedFiles/EFT/") + file);
                            using (fze = new FiestaZohoDatabaseEntities())
                            {
                                fze.InsertFileUploads(filename, Convert.ToString(Session["UserEmail"]), nameReport, id);
                            }
                            TempData["EFT_Message"] = "file has been uploaded";

                        }
                        if (nameReport == "RECON")
                        {
                            TempData["Recon_Message"] = "";
                            string filename = Path.GetFileName(uploadFile.FileName);
                            string extn = filename.Split('.').ElementAt(1);
                            extn = '.' + extn;
                            Guid id = Guid.NewGuid();
                            string file = id + extn;
                            uploadFile.SaveAs(Server.MapPath("~/UploadedFiles/RECON/") + file);
                            using (fze = new FiestaZohoDatabaseEntities())
                            {
                                fze.InsertFileUploads(filename, Convert.ToString(Session["UserEmail"]), nameReport, id);
                            }
                            TempData["Recon_Message"] = "file has been uploaded";

                        }
                        if (nameReport == "EOD")
                        {
                            TempData["DD_Message"] = "";
                            string filename = Path.GetFileName(uploadFile.FileName);
                            string extn = filename.Split('.').ElementAt(1);
                            extn = '.' + extn;
                            Guid id = Guid.NewGuid();
                            string file = id + extn;
                            uploadFile.SaveAs(Server.MapPath("~/UploadedFiles/DD/") + file);
                            using (fze = new FiestaZohoDatabaseEntities())
                            {
                                fze.InsertFileUploads(filename, Convert.ToString(Session["UserEmail"]), nameReport,id);
                            }
                            TempData["DD_Message"] = "file has been uploaded";

                        }
                        if (nameReport == "COMMISSION")
                        {
                            TempData["Commission_Message"] = "";
                            string filename = Path.GetFileName(uploadFile.FileName);
                            string extn = filename.Split('.').ElementAt(1);
                            extn = '.' + extn;
                            Guid id = Guid.NewGuid();
                            string file = id + extn;
                            uploadFile.SaveAs(Server.MapPath("~/UploadedFiles/COMMISSION/") + file);
                            using (fze = new FiestaZohoDatabaseEntities())
                            {
                                fze.InsertFileUploads(filename, Convert.ToString(Session["UserEmail"]), nameReport,id);
                            }
                            TempData["Commission_Message"] = "file has been uploaded";

                        }

                    }
                    else {
                        TempData["ExcelMissing_Message"] = "";
                        TempData["ExcelMissing_Message"] = "must select an Excel file to upload";
                    }

                }
                else
                {
                    TempData["ExcelMissing_Message"] = "";
                    TempData["ExcelMissing_Message"] = "must select an Excel file to upload";
                }
            }
             catch (Exception ex)
            {
                TempData["MessageException"] = "The following error occured: " + ex.Message;                

            }
            return View("UploadExcel");
        }

    }
}