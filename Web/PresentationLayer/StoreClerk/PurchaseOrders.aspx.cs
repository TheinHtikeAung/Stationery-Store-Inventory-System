using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationLayer;
using ApplicationLayer.Controllers;
namespace PresentationLayer.StoreClerk
{
    public partial class PurchaseOrders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public List<PurchaseOrderView> actionGetWaitingPurchaseOrders()
        {
            return new PurchaseOrderController().actionGetWaitingPurchaseOrderViews();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in ui_pos.Rows)
            {
                CheckBox cb = (CheckBox)row.FindControl("selector");
                if (cb.Checked)
                {
                    int id = Convert.ToInt32(row.Cells[1].Text);
                    new PurchaseOrderController().actionPurchaseOrderArrive(id);
                }
            }
            Response.Cookies.Add(new HttpCookie("flash_message", "Successfully Saved.") { Path = "/" });
            Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-success") { Path = "/" });
            Response.Redirect(Request.RawUrl);
        }

    }
}