using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationLayer.Controllers;

namespace PresentationLayer.StoreClerk
{
    public partial class RetrievalForm : System.Web.UI.Page
    {
        RequisitionFulfillmentController rfc;

        protected void Page_Load(object sender, EventArgs e)
        {
            rfc = new RequisitionFulfillmentController();
            if (!Page.IsPostBack)
            {
                retrievalGridview_Master.DataSource = rfc.actionGetAllRequisitionFulfillmentsForMaster();
                retrievalGridview_Master.DataBind();
            }

        }

        protected void retrievalGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Details")
            {
                int RowIndex = Convert.ToInt32((e.CommandArgument).ToString());
                Button btn = (Button)retrievalGridview_Master.Rows[RowIndex].FindControl("btn_Show");
                // Label lblID = (Label)GridView1.Rows[RowIndex - 1].FindControl("Label1");
                // int row = Convert.ToInt32(lblID.Text);
                if (btn.Text == "Details >>")
                {

                    //  Label lblID = (Label)gvDiscMaster.Rows[RowIndex].FindControl("lblID");
                    GridView gv =(GridView)retrievalGridview_Master.Rows[RowIndex].FindControl("retrievalGridview_Details");
                    int stationeryId = Convert.ToInt32(retrievalGridview_Master.Rows[RowIndex].Cells[0].Text);
                    gv.DataSource = rfc.actionGetAllRequisitionFulfillmentsForDetails(stationeryId);
                    gv.DataBind();
                    gv.Visible = true;
                    //btn.Text = "Hide<<";
                    //btn.Visible = false;
                   
                    // Edited
                    btn.Text = "Hide <<";

                }
                else if (btn.Text == "Hide <<")
                {
                    GridView gv = (GridView)retrievalGridview_Master.Rows[RowIndex].FindControl("retrievalGridview_Details");
                    //int stationeryId = Convert.ToInt32(retrievalGridview_Master.Rows[RowIndex].Cells[0].Text);
                    gv.DataSource = null;
                    gv.DataBind();
                    gv.Visible = false;
                    btn.Text = "Details >>";
                }
            }
        }
    }
}