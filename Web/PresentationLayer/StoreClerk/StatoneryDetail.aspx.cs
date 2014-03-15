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
    public partial class StatoneryDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            StationeryController stationeryController = new StationeryController();

            List<Supplier> suppliers = new SupplierController().actionGetSuppliers();
            

            
            int stationery_id;

            if (int.TryParse(Request.QueryString["id"], out stationery_id))
            {
                stationery_id = Convert.ToInt16(Request.QueryString["id"].ToString());
                if (stationery_id < 1)
                {
                    Response.Redirect("~/StoreClerk/StationeryList.aspx");
                }

                if (!Page.IsPostBack)
                {
                    
                    Stationery stationery = stationeryController.actionGetStationeryByID(stationery_id);

                    ui_id_hiddenfield.Value = stationery.id.ToString();
                    ui_code_textbox.Text = stationery.code.ToString();
                    ui_name_textbox.Text = stationery.stationery_name;

                    ui_category_dropdown.DataSource = stationeryController.actionGetStationeryCategories();
                    ui_category_dropdown.DataTextField = "category_name";
                    ui_category_dropdown.DataValueField = "id";
                    ui_category_dropdown.DataBind();
                    ui_category_dropdown.SelectedValue = stationery.category.ToString();
                    
                    
                    ui_price_textbox.Text = stationery.price.ToString();
                    ui_reorderlevel_textbox.Text = stationery.reorder_level.ToString(); ;
                    ui_reorderquantity_textbox.Text = stationery.reorder_quantity.ToString();
                    ui_quantityinstock_label.Text = stationery.quantity_in_stock.ToString();
                    ui_pendingquantity_label.Text = stationery.pending_quantity_to_distribute.ToString();
                    ui_uom_textbox.Text = stationery.unit_of_measure.ToString();
                    ui_binnum_textbox.Text = stationery.bin.ToString();

                    ui_firstsupplier_dropdown.DataSource = suppliers;
                    ui_secondsupplier_dropdown.DataSource = suppliers;
                    ui_thirdsupplier_dropdown.DataSource = suppliers;
                    ui_firstsupplier_dropdown.DataTextField = "supplier_name";
                    ui_firstsupplier_dropdown.DataValueField = "id";
                    ui_secondsupplier_dropdown.DataTextField = "supplier_name";
                    ui_secondsupplier_dropdown.DataValueField = "id";
                    ui_thirdsupplier_dropdown.DataTextField = "supplier_name";
                    ui_thirdsupplier_dropdown.DataValueField = "id";
                    
                    ui_firstsupplier_dropdown.DataBind();
                    ui_secondsupplier_dropdown.DataBind();
                    ui_thirdsupplier_dropdown.DataBind();
                    ui_firstsupplier_dropdown.SelectedValue = stationery.first_supplier.ToString();
                    ui_secondsupplier_dropdown.SelectedValue = stationery.second_supplier.ToString();
                    ui_thirdsupplier_dropdown.SelectedValue = stationery.third_supplier.ToString();
                    
                    ui_content_title_label.Text = stationery.stationery_name.ToString();
                }
            }
            else
            {
                String tmp_s1 = ui_firstsupplier_dropdown.SelectedValue;
                String tmp_s2 = ui_secondsupplier_dropdown.SelectedValue;
                String tmp_s3 = ui_thirdsupplier_dropdown.SelectedValue;
                String tmp_cate = ui_category_dropdown.SelectedValue;

                ui_category_dropdown.DataSource = stationeryController.actionGetStationeryCategories();
                ui_category_dropdown.DataTextField = "category_name";
                ui_category_dropdown.DataValueField = "id";
                ui_category_dropdown.DataBind();

                ui_firstsupplier_dropdown.DataSource = suppliers;
                ui_secondsupplier_dropdown.DataSource = suppliers;
                ui_thirdsupplier_dropdown.DataSource = suppliers;
                ui_firstsupplier_dropdown.DataTextField = "supplier_name";
                ui_firstsupplier_dropdown.DataValueField = "id";
                ui_secondsupplier_dropdown.DataTextField = "supplier_name";
                ui_secondsupplier_dropdown.DataValueField = "id";
                ui_thirdsupplier_dropdown.DataTextField = "supplier_name";
                ui_thirdsupplier_dropdown.DataValueField = "id";
                
                ui_firstsupplier_dropdown.DataBind();
                ui_secondsupplier_dropdown.DataBind();
                ui_thirdsupplier_dropdown.DataBind();

                ui_category_dropdown.SelectedValue = tmp_cate;
                ui_firstsupplier_dropdown.SelectedValue = tmp_s1;
                ui_secondsupplier_dropdown.SelectedValue = tmp_s2;
                ui_thirdsupplier_dropdown.SelectedValue = tmp_s3;
            }
        }

        protected void ui_save_button_Click(object sender, EventArgs e)
        {
            
            StationeryController sationeryController = new StationeryController();

            Stationery stationery = new Stationery();
            int stationery_id;

            //getting hidden field id
            if (int.TryParse(ui_id_hiddenfield.Value, out stationery_id))
            {
                stationery.id = Convert.ToInt32(ui_id_hiddenfield.Value);
                stationery = sationeryController.actionGetStationeryByID(stationery.id);
            }

            //fields validation
            if (String.IsNullOrEmpty(ui_code_textbox.Text) ||
                String.IsNullOrEmpty(ui_name_textbox.Text) ||
                String.IsNullOrEmpty(ui_reorderlevel_textbox.Text) ||
                String.IsNullOrEmpty(ui_reorderquantity_textbox.Text) ||
                String.IsNullOrEmpty(ui_uom_textbox.Text) ||
                String.IsNullOrEmpty(ui_price_textbox.Text)
                )
            {
                Response.Cookies.Add(new HttpCookie("flash_message", "Field(s) marked with * can't be empty.") { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-error") { Path = "/" });
                return;
                //String idstring = (supplier.id > 0) ? ("?id="+supplier.id) : ("");
                //Response.Redirect("~/StoreClerk/SupplierDetail.aspx"+idstring);
            }
           
            try
            {
                stationery.reorder_level = Convert.ToInt32(ui_reorderlevel_textbox.Text);
                stationery.reorder_quantity = Convert.ToInt32(ui_reorderquantity_textbox.Text);
                stationery.price = Convert.ToDouble(ui_price_textbox.Text);
            }
            catch (Exception ex)
            {
                Response.Cookies.Add(new HttpCookie("flash_message", "Price Reorder Level and Reorder Quantity only allow numbers." ) { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-error") { Path = "/" });
                return;        
            }   
            
            //setting up object
            //setting up object
            stationery.code = ui_code_textbox.Text;
            stationery.stationery_name = ui_name_textbox.Text;
            stationery.category = Convert.ToInt32(ui_category_dropdown.SelectedValue);
            stationery.reorder_level = Convert.ToInt32(ui_reorderlevel_textbox.Text);
            stationery.reorder_quantity = Convert.ToInt32(ui_reorderquantity_textbox.Text);
            stationery.unit_of_measure = ui_uom_textbox.Text;
            stationery.bin = ui_binnum_textbox.Text;

            //stationery.quantity_in_stock = Convert.ToInt32(ui_quantityinstock_label.Text);
            //stationery.pending_quantity_to_distribute = Convert.ToInt32(ui_pendingquantity_label.Text);

            if (Page.IsPostBack) {
               
            }

            stationery.first_supplier = Convert.ToInt32(ui_firstsupplier_dropdown.SelectedItem.Value);
            stationery.second_supplier = Convert.ToInt32(ui_secondsupplier_dropdown.SelectedItem.Value);
            stationery.third_supplier = Convert.ToInt32(ui_thirdsupplier_dropdown.SelectedItem.Value);

            //stationery.first_supplier = 5;
            //stationery.second_supplier = 4;
            //stationery.third_supplier = 1;
            
            //updating db;
            Message message;
            if (stationery.id > 0)
            {
                message = sationeryController.actionUpdateStationery(stationery);
            }
            else
            {
                stationery.quantity_in_stock = 0;
                stationery.pending_quantity_to_distribute = 0;
                message = sationeryController.actionCreateStationery(stationery);
            }

            //redirecting
            if (message.condition)
            {
                Response.Cookies.Add(new HttpCookie("flash_message", "Successfully Saved.") { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-success") { Path = "/" });
                Response.Redirect("~/StoreClerk/StatoneryDetail.aspx?id=" + stationery.id);
            }
            else
            {
                Response.Cookies.Add(new HttpCookie("flash_message", message.message) { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-error") { Path = "/" });
                Response.Redirect("~/StoreClerk/StatoneryDetail.aspx?id=" + stationery.id);
            }
            
        }

        protected void ui_cancle_button_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StoreClerk/StationeryList.aspx");
        }
    }
}