using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationLayer;
using ApplicationLayer.Controllers;

namespace PresentationLayer.StoreSupervisor
{
    public partial class ClerkList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public List<UserView> actionGetClerks()
        {
            return new UserController().actionGetClerks();
        }

        protected void ui_supplier_gridview_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ui_user_gridview.SelectedRow != null)
            {
                int id = Convert.ToInt16(ui_user_gridview.SelectedRow.Cells[0].Text);
                Response.Redirect("~/StoreSupervisor/ClerkDetail.aspx?id=" + id);
            }
        }

        protected void CreateNew_Button_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StoreSupervisor/ClerkDetail.aspx");
        }
    }
}