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
    public partial class InventoryStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                StatusCheckingController ic = new StatusCheckingController();

                ui_reorderlevel_label.Text = ic.actionGetReorderLevelStationeriesCount().ToString();
                ui_zerolevel_lable.Text = ic.actionGetZeroCountStationeriesCount().ToString();
            }
        }

        public List<StationeryView> actionGetReorderLevelStationeryViews()
        {
            //int id = Convert.ToInt32(Session["data:stock_card:stationery_id"]);
            return new StatusCheckingController().actionGetReorderLevelStationeryViews();
        }

        protected void ui_inventory_gridview_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ui_inventory_gridview.SelectedRow != null)
            {
                int id = Convert.ToInt16(ui_inventory_gridview.SelectedRow.Cells[0].Text);
                Response.Redirect("~/StoreClerk/PurchaseOrder.aspx?id="+id);
            }
            
        }
    }
}