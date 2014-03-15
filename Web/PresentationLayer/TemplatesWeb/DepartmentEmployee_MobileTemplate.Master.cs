﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationLayer;
namespace PresentationLayer.TemplatesWeb
{
    public partial class DepartmentEmployee_MobileTemplate : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User user = (User)Session["user"];
            if (user == null || (user.user_type != 1) && (user.user_type != 2))
            {
                Response.Redirect("~/m/Login.aspx");
            }
        }
    }
}