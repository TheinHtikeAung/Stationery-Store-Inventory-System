using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using ApplicationLayer;
using ApplicationLayer.Controllers;
using ApplicationLayer.Utils;

namespace PresentationLayer.StoreClerk
{
    public partial class SupplierDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int supplier_id;
            if (int.TryParse(Request.QueryString["id"], out supplier_id))
            {
                supplier_id = Convert.ToInt16(Request.QueryString["id"].ToString());
                if (supplier_id < 1)
                {
                    Response.Redirect("~/StoreClerk/SupplierList.aspx");
                }

                if (!Page.IsPostBack)
                {
                    SupplierController supplierController = new SupplierController();
                    Supplier supplier = supplierController.actionGetSuppierByID(supplier_id);

                    ui_id_hiddenfield.Value = supplier.id.ToString();
                    ui_code_textbox.Text = supplier.code;
                    ui_name_textbox.Text = supplier.supplier_name;
                    ui_contactname_textbox.Text = supplier.contact_name;
                    ui_phonenumber_textbox.Text = supplier.phone_number;
                    ui_faxnumber_textbox.Text = supplier.fax_number;
                    ui_email_textbox.Text = supplier.email;
                    ui_address_textbox.Text = supplier.address;

                    ui_content_title_label.Text = supplier.supplier_name;
                }
            }
            else 
            {
                
            }
        }

        protected void ui_save_button_Click(object sender, EventArgs e)
        {
            SupplierController supplierController = new SupplierController();
            
            Supplier supplier = new Supplier();
            int supplier_id;

            //getting hidden field id
            if (int.TryParse(ui_id_hiddenfield.Value, out supplier_id))
            {
                supplier.id = Convert.ToInt32(ui_id_hiddenfield.Value);
            }

            //fields validation
            if (String.IsNullOrEmpty(ui_name_textbox.Text) || String.IsNullOrEmpty(ui_code_textbox.Text))
            {
                Response.Cookies.Add(new HttpCookie("flash_message", "Field(s) marked with * can't be empty.") { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-error") { Path = "/" });
                return;
                //String idstring = (supplier.id > 0) ? ("?id="+supplier.id) : ("");
                //Response.Redirect("~/StoreClerk/SupplierDetail.aspx"+idstring);
            }
            
            //setting up object
            supplier.code = ui_code_textbox.Text;
            supplier.supplier_name = ui_name_textbox.Text;
            supplier.contact_name = ui_contactname_textbox.Text;
            supplier.phone_number = ui_phonenumber_textbox.Text;
            supplier.fax_number = ui_faxnumber_textbox.Text;
            supplier.email = ui_email_textbox.Text;
            supplier.address = ui_address_textbox.Text;
            
            //updating db;
            Message message;
            if (supplier.id > 0)
            {
                message = supplierController.actionUpdateSupplier(supplier);
            }
            else {
                message = supplierController.actionCreateSupplier(supplier);
            }

            //redirecting
            if (message.condition)
            {
                Response.Cookies.Add(new HttpCookie("flash_message", "Successfully Saved.") { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-success") { Path = "/" });
                Response.Redirect("~/StoreClerk/SupplierDetail.aspx?id=" + supplier.id);
            }
            else {
                Response.Cookies.Add(new HttpCookie("flash_message", message.message) { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-error") { Path = "/" });
                Response.Redirect("~/StoreClerk/SupplierDetail.aspx?id=" + supplier.id);
            }
            
        }

        protected void ui_cancle_button_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StoreClerk/SupplierList.aspx");
        }
    }
}