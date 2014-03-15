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
    public partial class StationeryList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["data:stock_card:stationery_id"] = null;
            //StationeryController stationeryController = new StationeryController();
            //SupplierController supplierController = new SupplierController();
           
            //int stationery_id;
            //if (int.TryParse(Request.QueryString["id"], out stationery_id))
            //{
            //    stationery_id = Convert.ToInt16(Request.QueryString["id"].ToString());
            //    Session["data:stock_card:stationery_id"] = stationery_id;
            //    if (!(stationery_id > 0))
            //    {

            //    }

            //    if (!Page.IsPostBack)
            //    {

            //    }
            //    else
            //    {

            //    }
            //}
            //else {
                
            //}
            
            
        }

        public List<StationeryView> actionGetStationeries()
        {
            //int id = Convert.ToInt32(Session["data:stock_card:stationery_id"]);
            return new StationeryController().actionGetStationeryViews();
        }

        protected void ui_stockcard_gridview_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ui_stationeries_gridview.SelectedRow != null)
            {
                int id = Convert.ToInt16(ui_stationeries_gridview.SelectedRow.Cells[0].Text);
                Response.Redirect("~/StoreClerk/StatoneryDetail.aspx?id=" + id);
            }
        }
        
    }
}