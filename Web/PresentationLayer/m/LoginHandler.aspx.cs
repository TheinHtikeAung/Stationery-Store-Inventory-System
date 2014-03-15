using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ApplicationLayer;
using ApplicationLayer.Controllers;

namespace PresentationLayer.m
{
    public partial class LoginHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String username = Request.Form["username"];
            String password = Request.Form["password"];

            AuthenticationController authController = new AuthenticationController();
            User user = authController.actionCheckLoginData(username, password);
            if (user == null || (user.user_type != 1 && user.user_type != 2))
            {
                Response.Cookies.Add(new HttpCookie("flash_message", "Invilid Login.") { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-error") { Path = "/" });
                Response.Redirect("~/m/Login.aspx");
                return;
            }

            Session["user"] = null;
            Session["user"] = user;

            redirectByUserType(user.user_type);

        }

        public void redirectByUserType(int user_type)
        {
            switch (user_type)
            {
                case 1:
                    Response.Redirect("~/m/DepartmentEmployee/");
                    break;
                case 2:
                    Response.Redirect("~/m/DepartmentEmployee/");
                    break;
                case 3:
                    Response.Redirect("~/m/DepartmentHead/");
                    break;
                case 4:
                    Response.Redirect("~/m/StoreClerk/");
                    break;
                case 5:
                    Response.Redirect("~/m/StoreSupervisor/");
                    break;
                case 6:
                    Response.Redirect("~/m/StoreSupervisor/");
                    break;
                default:
                    Response.Redirect("~/m/Login.aspx");
                    break;
            }
        }
    }
}