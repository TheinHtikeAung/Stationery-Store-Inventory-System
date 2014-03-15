using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ApplicationLayer;
using ApplicationLayer.Controllers;
using ApplicationLayer.Utils;

namespace PresentationLayer.StoreClerk
{
    public partial class UserList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserController userController = new UserController();
            //ui_user_gridview.DataSource = userController.actionGetUserViews();
            //ui_user_gridview.GridLines = GridLines.None;
            //ui_user_gridview.DataBind();
        }

        protected void ui_supplier_gridview_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ui_user_gridview.SelectedRow != null)
            {
                int id = Convert.ToInt16(ui_user_gridview.SelectedRow.Cells[0].Text);
                Response.Redirect("~/StoreClerk/UserDetail.aspx?id=" + id);
            }
        }

        protected void CreateNew_Button_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StoreClerk/UserDetail.aspx");
        }

        public List<UserView> actionGetDepartmentSideUserViews()
        {
            return new UserController().actionGetDepartmentSideUserViews();
        }
    }
}