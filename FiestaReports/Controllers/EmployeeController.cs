using FiestaReports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FiestaReports.Controllers
{
    public class EmployeeController : Controller
    {
        private FiestaZohoDatabaseEntities db = new FiestaZohoDatabaseEntities();

        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult ManageEmployee()
        {
            Employee obj = new Employee();
            obj.LoginUser = Convert.ToString(Session["UserName"]);
            return View(obj);
        }

        /*******************************************************************
            Author      : Gopi
            Date        : 04/07/2017
            Description : Returns Loggedin user Name
           *******************************************************************/
        [AuthorizationFilter]
        public ActionResult RegisterEmployee()
        {
            Employee obj = new Employee();
            obj.LoginUser = Convert.ToString(Session["UserName"]);
            return View(obj);
        }

        [AuthorizationFilter]
        [HttpGet]
        public ActionResult GetAllEmployees()
        {
            var query = (from e in db.Fiesta_Employee
                             join r in db.Fiesta_Role on e.RoleId equals r.RoleId
                             join es in db.Fiesta_EmpStore on e.EmpId equals es.EmpId
                             join s in db.Fiesta_Store on es.StoreNo equals s.StoreNo
                             select new {
                                 EmpId = e.EmpId,
                                 FirstName = e.FirstName,
                                 LastName = e.LastName,
                                 Email = e.EmailAddress,
                                 Role = r.RoleName,
                                 Active = e.IsActive,
                                 EmpStore = s.StoreNo
                             }).ToList();

            var employees = query.GroupBy(cc => cc.EmpId).Select(dd => new { EmpId = dd.Key, EmpStore = string.Join(",", dd.Select(ee => ee.EmpStore).ToList()) });

            var other = (from e in employees
                         join q in query on e.EmpId equals q.EmpId
                         select new {
                             FirstName = q.FirstName,
                             LastName = q.LastName,
                             Email = q.Email,
                             Role = q.Role,
                             EmpStore = e.EmpStore,
                             Active = q.Active
                         }).Distinct().ToList();


            return Json(other, JsonRequestBehavior.AllowGet);
        }
    }
}