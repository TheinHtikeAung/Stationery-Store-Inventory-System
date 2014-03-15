using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ApplicationLayer;
using ApplicationLayer.Controllers;

namespace PresentationLayer.TemplatesWeb
{
    public partial class StoreSupervisor_Template : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User user = (User)Session["user"];
            if (user == null || user.user_type != 5 && user.user_type != 6)
            {
                Response.Redirect("~/Login.aspx");
            }
            
        }
    }
}