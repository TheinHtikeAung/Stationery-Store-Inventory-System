using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationLayer;

namespace PresentationLayer.m
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {if ((User)Session["user"] == null)
            {
                Response.Redirect("~/m/Login.aspx");
                return;
            }

            //already login. redirecting...
            User user = (User)Session["user"];
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