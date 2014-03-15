using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ApplicationLayer.Controllers;
using ApplicationLayer;

namespace PresentationLayer.StoreSupervisor
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User user = (User)Session["user"];
            if (user == null || ((user.user_type != 5) && (user.user_type != 6)))
            {
                Response.Redirect("~/Login.aspx");
            }

            else
            {
                Response.Redirect("~/StoreSupervisor/DiscrepencyList.aspx");
            }
        }
    }
}