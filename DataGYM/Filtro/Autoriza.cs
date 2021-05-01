using BusinessModel;
using DataAccess.DB;
using DataGYM.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace DataGYM.Filtro
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =true)]
    public class Autoriza:AuthorizeAttribute
    {
        private USER_TABLE oUser;
        private string rol_name;
        private string assigned;
        public Autoriza(string rol)
        {
            this.rol_name = rol;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                oUser = (USER_TABLE)HttpContext.Current.Session["user"];
                if (oUser == null)
                {
                    filterContext.Result = new RedirectResult("~/Login");
                }
                else
                {
                    using (DataGYMEntities db = new DataGYMEntities())
                    {

                        assigned = (from d in db.ROLE_TABLE
                                    where oUser.FKROLE == d.ROLE_ID
                                    select d.NAME).FirstOrDefault();
                    }
                    if (!rol_name.Contains(assigned))
                    {
                        filterContext.Result = new RedirectResult("/Error/NoAuthorized");
                    }
                }

            }
            catch (Exception e)
            {
                filterContext.Result = new RedirectResult("~/Login");
            }
        }
    }
}