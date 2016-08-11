using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using Newtonsoft;
using System.Data;
using RazorSample.Models;
using System.Text.RegularExpressions;

namespace RazorSample.Controllers
{
    public class AccountController : Controller
    {
        BusinessManager bm;

        [HttpGet]
        public ActionResult Register()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegistrationModel Model)
        {
            if (!ModelState.IsValid)
                return View();
            
            bm = new BusinessManager();
            DataSet res = bm.AddStaffInfo(
                StaffName : Model.Name,
                Gender : Model.Gender,
                DOB : Model.DOB,
                Phone : Model.Phone,
                Email : Model.Email,
                Languages : Model.Languages,
                Country : Model.Country,
                Username : Model.Username,
                Password : Model.Password
                );
            if (res != null && res.Tables.Count > 0 && res.Tables[0].Rows.Count > 0 && Convert.ToInt32(res.Tables[0].Rows[0]["InsertCount"]) > 0)
            {
                ViewBag.Message = "Successful";
                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewBag.Message = "Error Occured";
                return View("Register", Model);
            }        
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel Model)
        {
            DataSet UserDetails;
            if (!ModelState.IsValid)
                return View();
            bm = new BusinessManager();
            ViewBag.StaffID = bm.ValidateUser(Model.Username,Model.Password);
            if (ViewBag.StaffID > 0)
            {
                Session["StaffID"] = ViewBag.StaffID;
                UserDetails = new DataSet();
                UserDetails = bm.getStaffInfo(ViewBag.StaffID);
                if (UserDetails.Tables[0].Rows.Count > 0)
                {
                    Session["StaffName"] = UserDetails.Tables[0].Rows[0]["Name"];
                    Session["StaffEmail"] = UserDetails.Tables[0].Rows[0]["Email"];
                }                
                return RedirectToAction("AccountHome");
            }                
            else
            {
                ModelState.AddModelError("Username", "Incorrect Username or Password!");
                return View();
            }           
        }
        
        public ActionResult AccountHome(AccountHomeModel Model)
        {
            if (Session == null || Session["StaffID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            DataSet UserDetails = new DataSet();
            bm = new BusinessManager();
            UserDetails = bm.getStaffInfo(Convert.ToInt32(Session["StaffID"]));
            if (UserDetails.Tables[0].Rows.Count > 0)
            {
                ViewBag.StaffID = UserDetails.Tables[0].Rows[0]["StaffID"].ToString(); ;
                ViewBag.StaffName = UserDetails.Tables[0].Rows[0]["Name"].ToString();
                ViewBag.StaffEmail = UserDetails.Tables[0].Rows[0]["Email"].ToString();
                ViewBag.Gender = UserDetails.Tables[0].Rows[0]["Gender"].ToString();
                ViewBag.GenderPrefix = ViewBag.Gender == "Male"?"Mr.":"Ms";
            }                
            return View();
        }

        public ActionResult StaffListing()
        {
            bm = new BusinessManager();
            ViewBag.staffInfo = bm.getStaffInfo();
            return View();
        }

        public string GetStaffInfo(int draw, int start,int length)
        {
            bm = new BusinessManager();
            DataSet ds = bm.getStaffInfo();
    
            DataTable resultTable = new DataTable();
            resultTable.Columns.Add("draw", typeof(Int32));
            resultTable.Columns.Add("recordsTotal", typeof(Int32));
            resultTable.Columns.Add("recordsFiltered", typeof(Int32));
            resultTable.Columns.Add("data", typeof(DataTable));
            resultTable.Rows.Add(draw, ds.Tables[0].Rows.Count, ds.Tables[0].Rows.Count, ds.Tables[0]);
            var resultJson = Newtonsoft.Json.JsonConvert.SerializeObject(resultTable);
            resultJson = Regex.Replace(resultJson, @"^\[+", "");
            resultJson = Regex.Replace(resultJson, @"\]+$", "");
            return resultJson;
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index","Home");
        }

    }
}
