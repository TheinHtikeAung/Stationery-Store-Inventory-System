using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationLayer;

namespace PresentationLayer.m.DepartmentEmployee
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User user = (User)Session["user"];
            if (user == null || ((user.user_type != 1) && (user.user_type != 2)))
            {
                Response.Redirect("~/m/Login.aspx");
            }

            else
            {
                Response.Redirect("~/m/DepartmentEmployee/Stationeries.aspx");
            }
        }
    }
}