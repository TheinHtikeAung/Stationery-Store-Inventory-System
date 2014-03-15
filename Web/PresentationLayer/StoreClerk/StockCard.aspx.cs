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
    public partial class StockCard : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            ui_content_title_label.Text = "Stock Card";
            Session["data:stock_card:stationery_id"] = null;
            StationeryController stationeryController = new StationeryController();
            SupplierController supplierController = new SupplierController();
           
            int stationery_id;
            if (int.TryParse(Request.QueryString["id"], out stationery_id))
            {
                stationery_id = Convert.ToInt16(Request.QueryString["id"].ToString());
                Session["data:stock_card:stationery_id"] = stationery_id;
                if (!(stationery_id > 0))
                {

                }

                if (!Page.IsPostBack)
                {

                    Stationery stationery = stationeryController.actionGetStationeryByID(stationery_id);
                    ui_stationery_code_label.Text = stationery.code.ToString();
                    ui_stationery_name_label.Text = stationery.stationery_name.ToString();
                    ui_stationery_bin_label.Text = stationery.bin.ToString();
                    ui_stationery_uom_label.Text = stationery.unit_of_measure.ToString();
                    ui_stationery_supplier1_label.Text = supplierController.actionGetSuppierByID(Convert.ToInt16(stationery.first_supplier)).supplier_name.ToString();
                    ui_stationery_supplier2_label.Text = supplierController.actionGetSuppierByID(Convert.ToInt16(stationery.second_supplier)).supplier_name.ToString();
                    ui_stationery_supplier3_label.Text = supplierController.actionGetSuppierByID(Convert.ToInt16(stationery.third_supplier)).supplier_name.ToString();
                    ui_seationerysearch_box.Text = stationery.stationery_name.ToString();
                    StockTransactionController stockContorller = new StockTransactionController();

                    //ui_stockcard_gridview.DataSource = new StockTransactionController().actionGetStockTransactionByStationeryId(2);
                    //ui_stockcard_gridview.DataBind();
                    
                }
                else
                {

                }
            }
            else {
                ui_stationeryinfo_panel.CssClass = "hide";
            }
            
            
        }
        
        public List<StockTransaction> actionGetStockTransactionByStationeryId()
        {
            int id = Convert.ToInt32(Session["data:stock_card:stationery_id"]);
            return new StockTransactionController().actionGetStockTransactionByStationeryId(id);
        }

        protected void ui_stockcard_gridview_PageIndexChanged(object sender, EventArgs e)
        {
            
        }



    }
}