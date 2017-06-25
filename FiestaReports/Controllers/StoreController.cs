using FiestaReports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FiestaReports.Controllers
{
    public class StoreController : Controller
    {

        FiestaZohoDatabaseEntities fze;
        // GET: Store
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizationFilter]
        public ActionResult ManageStores()
        {
            return View();
        }

        [AuthorizationFilter]
        public ActionResult AddStore(Store objStore)
        {
            /*******************************************************************
  Author      : Gopi
  Date        : 04/07/2017
  Description : Saves new Store details 
 *******************************************************************/
            var res = string.Empty;
                  using (fze = new FiestaZohoDatabaseEntities())
            {
                 res = fze.AddStore(objStore.StateId,objStore.StoreNumber, objStore.Address, objStore.City, objStore.ZipCode).FirstOrDefault();
            }
                return Json(res, JsonRequestBehavior.AllowGet);
        }

    }
}