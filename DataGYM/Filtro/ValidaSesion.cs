using DataAccess.DB;
using DataGYM.Controllers;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataGYM.Filtro
{
    public class ValidaSesion: ActionFilterAttribute
    {
        private USER_TABLE oUser;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            oUser = (USER_TABLE)HttpContext.Current.Session["user"];
            if (oUser == null)
            {
                if (filterContext.Controller is LoginController == false)
                {
                    filterContext.HttpContext.Response.Redirect("~/Login");
                }
                
            }
            else
            {
                if (filterContext.Controller is LoginController)
                {
                    filterContext.HttpContext.Response.Redirect("~/Home");
                }
            }

        }
    }
}