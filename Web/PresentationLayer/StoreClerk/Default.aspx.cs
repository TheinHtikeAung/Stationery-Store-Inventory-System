using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationLayer.Controllers;
using ApplicationLayer;


namespace PresentationLayer.StoreClerk
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //auth check
            User user = (User)Session["user"];
            if (user == null || user.user_type != 4)
            {
                Response.Redirect("~/Login.aspx");
            }

            ui_welcome_text.Text = "" + user.firstname + " " + user.lastname + "";

            StatusCheckingController statusController = new StatusCheckingController();

            int zero_stationeries = statusController.actionGetZeroCountStationeriesCount();

            ui_inventorystats_label.Text = statusController.actionGetReorderLevelStationeriesCount().ToString()+" stationeries reaching to re-order level.";

            if (zero_stationeries > 0) {
                ui_0items_label.Text = zero_stationeries.ToString() + " stationeries are totally shortage in the inventory.";
            }
            
            
            ui_pendingrequests_label.Text = statusController.actionGetApprovedRequsitionsCount().ToString() + " requests are waiting to process.";

            ui_departments_count.Text = statusController.actionGetDepartmentCount().ToString();
            ui_stationeries_count.Text = statusController.actionGetStationeriesCount().ToString();
            ui_customers_count.Text = statusController.actionGetEmplyoeeCount().ToString();
            ui_disbursemetn_count.Text = statusController.actionGetDisbursementTimesCount().ToString();
        }
    }
}