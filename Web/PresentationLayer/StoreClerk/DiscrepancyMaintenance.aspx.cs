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
    public partial class DiscrepancyMaintenance : System.Web.UI.Page
    {
        DiscrepencyController dc;
        StationeryController sc;
        StockTransactionController stc;
        string id;

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            /*
                dc = new DiscrepencyController();
                sc = new StationeryController();
                stc = new StockTransactionController();
                if (Request.QueryString["id"] != null)
                {
                    id = Request.QueryString["id"];
                    Discrepancy discrepancy = dc.actionGetDiscrepancy(Convert.ToInt32(id));
                    Stationery stationery = sc.actionGetStationeryByID(discrepancy.stationery);
                    stryTxt.Text = stationery.stationery_name;
                    qtyTxt.Text = discrepancy.quantity.ToString();
                    if (discrepancy.remark != null)
                        remarkTxt.Text = discrepancy.remark;

                    dateTxt.Visible = true;
                    lblDate.Visible = true;
                    stryTxt.ReadOnly = true;
                    qtyTxt.ReadOnly = true;
                    remarkTxt.ReadOnly = true;
                    dateTxt.ReadOnly = true;
                    saveBtn.Visible = false;

                    dateTxt.Text = String.Format("{0: d MMM, yyyy}", discrepancy.datetime_reported);

                    saveBtn.Visible = false;
                }
                else if (Request.QueryString["id"] == null)
                {
                    //remarkTxt.Text = "";
                    dateTxt.Text = DateTime.Now.ToShortDateString();
                    dateTxt.Enabled = false;
                    delBtn.Enabled = false;
                }*/
            //}
        }

        protected void cancelBtn_Click(object sender, EventArgs e)
        {
            //Response.Redirect("DiscrepancyList.aspx");
        }

        protected void saveBtn_Click(object sender, EventArgs e)
        {
            /*
            int stationeryId = sc.actionGetStationeryIdByName(stryTxt.Text);
            int quantityToUpdate = Convert.ToInt32(qtyTxt.Text);
            sc.actionUpdateDiscrepancyQuantityInStock(stationeryId, quantityToUpdate*(-1));
            dc.actionCreateNewDiscrepancy(stationeryId, quantityToUpdate, remarkTxt.Text);
            int quantityInStock=sc.actionGetQtyInStock(stationeryId);
            stc.actionCreateStockTransaction(stationeryId, quantityInStock , "", "-" + qtyTxt.Text);
            clearAllText();

            Response.Redirect("DiscrepancyList.aspx");
             */ 
        }

        protected void delBtn_Click(object sender, EventArgs e)
        {
            //int stationeryId = sc.actionGetStationeryIdByName(stryTxt.Text);
            //int quantityToUpdate = Convert.ToInt32(qtyTxt.Text);
            //sc.actionUpdateDiscrepancyQuantityInStock(stationeryId, quantityToUpdate);
            //int id = Convert.ToInt32(Request.QueryString["id"]);
            //dc.actionDeleteDiscrepancy(id);
            //int quantityInStock = sc.actionGetQtyInStock(stationeryId);
            //stc.actionCreateStockTransaction(stationeryId, quantityInStock, "", "+" + qtyTxt.Text);
            //dc.actionDeleteDiscrepancy(Convert.ToInt32(id));
            //clearAllText();

            //Response.Redirect("DiscrepancyList.aspx");
        }

        private void clearAllText()
        {
            //stryTxt.Text = "";
            //qtyTxt.Text = "";
            //dateTxt.Text = "";
            //remarkTxt.Text = "";
            
        }

        protected void ui_save_button_Click(object sender, EventArgs e)
        {
            DiscrepencyController discrepancyController = new DiscrepencyController();
            Discrepancy discrepancy = new Discrepancy();
            if (String.IsNullOrEmpty(ui_stationery_hidden.Value) || String.IsNullOrEmpty(ui_quantity_textbox.Text)) {
                Response.Cookies.Add(new HttpCookie("flash_message", "Field(s) marked with * can't be empty.") { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-error") { Path = "/" });
                return;
            }

            discrepancy.stationery = Convert.ToInt32(ui_stationery_hidden.Value);
            try
            {
                discrepancy.quantity = Convert.ToInt32(ui_quantity_textbox.Text);
                if (discrepancy.quantity < 0) {
                    throw new Exception();
                }
            }
            catch (Exception ex) {
                Response.Cookies.Add(new HttpCookie("flash_message", "Quantity should be only number.") { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-error") { Path = "/" });
                Response.Redirect(Request.RawUrl);
            }
            discrepancy.remark = ui_remark_textboxt.Text;

            discrepancyController.actionCreateNewDiscrepancy(discrepancy);

            Response.Redirect("DiscrepancyList.aspx");
        }

        protected void ui_cancle_button_Click(object sender, EventArgs e)
        {
            Response.Redirect("DiscrepancyList.aspx");
        }
        //protected void updateBtn_Click(object sender, EventArgs e)
        //{
            
        //}
    }
}