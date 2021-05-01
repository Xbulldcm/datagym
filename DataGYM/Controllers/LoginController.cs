using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataGYM.Models;
using DataAccess.DB;
using BusinessModel;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Web.Security;
using System.Globalization;
using DataGYM.Tools;

namespace DataGYM.Controllers
{
    public class LoginController : Controller
    {
        private USER_TABLE oUser;
        private string rolename;
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string email, string password)
        {
            using (DBContext<USER_TABLE> context = new DBContext<USER_TABLE>(new DataGYMEntities()))
            {
                string Epw = Encripta.Hash(password);
                oUser = context.genericBM.Find(x => x.Email == email && x.Password == Epw).FirstOrDefault();
                if (oUser == null || oUser.IS_ACTIVE == false)
                {
                    ViewBag.MensajeError = "Correo y/o contraseña incorrectos";
                    return View();
                } 
                Session["user"] = oUser;
                Session["username"] = oUser.Name;
                using (DataGYMEntities db = new DataGYMEntities())
                {
                    rolename = (from d in db.ROLE_TABLE
                                where oUser.FKROLE == d.ROLE_ID
                                select d.NAME).FirstOrDefault();
                }
                Session["role"] = rolename;
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            Session["role"] = "";
            return RedirectToAction("Index", "Home");
        }


    }

}