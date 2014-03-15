using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ApplicationLayer.Controllers;
using ApplicationLayer;

namespace PresentationLayer.TemplatesWeb
{
    public partial class DepartmentHead_Template : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            User user = (User)Session["user"];
            if (user == null || (user.user_type != 2) && (user.user_type != 3))
            {
                Response.Redirect("~/Login.aspx");
            }

            ui_username_label.Text = user.firstname + " " + user.lastname;

        }

    }
}