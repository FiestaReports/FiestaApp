﻿using FiestaReports.Models;
using FiestaReports.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            var userRole = int.Parse(Session["UserRole"].ToString());
            var employeeId = int.Parse(Session["UserID"].ToString());
            var stores = StoresByUser();
            var roles = VisibleRoles();

            var query = (from r in roles
                            join e in db.Fiesta_Employee on r.RoleId equals e.RoleId 
                             join es in db.Fiesta_EmpStore on e.EmpId equals es.EmpId 
                             join s in stores on es.StoreNo equals s.StoreNo
                         where e.RoleId >= userRole
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
                             EmpId = e.EmpId,
                             FirstName = q.FirstName,
                             LastName = q.LastName,
                             Email = q.Email,
                             Role = q.Role,
                             EmpStore = e.EmpStore,
                             Active = q.Active
                         }).Distinct().ToList();

            if(userRole == (int)UserTypeEnum.National)
            {
                var nationals = (from r in db.Fiesta_Role
                                 join e in db.Fiesta_Employee on r.RoleId equals e.RoleId
                                 where e.RoleId == userRole
                                 select new
                                 {
                                     EmpId = e.EmpId,
                                     FirstName = e.FirstName,
                                     LastName = e.LastName,
                                     Email = e.EmailAddress,
                                     Role = r.RoleName,
                                     EmpStore = "ALL",
                                     Active = e.IsActive

                                 }).ToList();
                other.AddRange(nationals);
            }

            return Json(other.OrderBy(x=> x.FirstName).ThenBy(x=> x.LastName), JsonRequestBehavior.AllowGet);
        }

        [AuthorizationFilter]
        [HttpGet]
        public PartialViewResult _Edit(int id)
        {
            var model = db.Fiesta_Employee.FirstOrDefault(x => x.EmpId == id);
            if (model == null) { 
                model = new Fiesta_Employee();
                ViewBag.Message = "No data available";
            }

            return PartialView(model);
        }

        [AuthorizationFilter]
        [HttpPost]
        public ActionResult _Edit(Fiesta_Employee model)
        {
            bool modelIsValid = true;
            if (string.IsNullOrEmpty(model.FirstName))
            {
                ModelState.AddModelError("FirstName", "Required");
                modelIsValid = false;
            }
            if(string.IsNullOrEmpty(model.LastName))
            {
                ModelState.AddModelError("LastName", "Required");
                modelIsValid = false;
            }
                
            if (modelIsValid)
            {
                var original = db.Fiesta_Employee.FirstOrDefault(x => x.EmpId == model.EmpId);
                original.FirstName = model.FirstName;
                original.LastName = model.LastName;
                original.EmailAddress = model.EmailAddress;
                original.IsActive = model.IsActive;
                original.ModifiedDate = DateTime.Now;
                db.SaveChanges();

                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            
            return PartialView(model);
        }
        
        [AuthorizationFilter]
        [HttpPost]
        public ActionResult _EditPassword(string pass, int empId)
        {
            string encryptedPassword = Convert.ToBase64String(System.Security.Cryptography.SHA256.Create()
                                    .ComputeHash(Encoding.UTF8.GetBytes(pass)));

            var original = db.Fiesta_Employee.FirstOrDefault(x => x.EmpId == empId);
            original.Password = encryptedPassword;
            original.ModifiedDate = DateTime.Now;
            db.SaveChanges();
            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        [AuthorizationFilter]
        [HttpGet]
        public ActionResult _EmployeeReports(int id)
        {
            var model = db.Fiesta_Employee.FirstOrDefault(x => x.EmpId == id);
            if (model == null)
            {
                model = new Fiesta_Employee();
                ViewBag.Message = "No data available";
            }

            return View();
        }


        private IQueryable<Fiesta_Store> StoresByUser()
        {
            var userRole = int.Parse(Session["UserRole"].ToString());
            var employeeId = int.Parse(Session["UserID"].ToString());

            if(userRole == (int)UserTypeEnum.National)
            {
                return db.Fiesta_Store;
            }

            return (from s in db.Fiesta_Store
                         join es in db.Fiesta_EmpStore on s.StoreNo equals es.StoreNo
                         where es.EmpId == employeeId
                         select s);
        }

        private IQueryable<Fiesta_Role> VisibleRoles()
        {
            var userRole = int.Parse(Session["UserRole"].ToString());

            if(userRole == (int)UserTypeEnum.Manager)
            {
                return db.Fiesta_Role.Where(x => x.RoleId == (int)UserTypeEnum.Agent);
            }

            return db.Fiesta_Role;
        }
        
    }
}