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
    public partial class CategoryDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int category_id;
            if (int.TryParse(Request.QueryString["id"], out category_id))
            {
                category_id = Convert.ToInt16(Request.QueryString["id"].ToString());
                if (category_id < 1)
                {
                    Response.Redirect("~/StoreClerk/CategoryList.aspx");
                }

                if (!Page.IsPostBack)
                {
                    StationeryController categoryController = new StationeryController();
                    StationeryCategory category = categoryController.actionGetStationeryCategoryByID(category_id);

                    ui_id_hiddenfield.Value = category.id.ToString();
                    ui_catname_textbox.Text = category.category_name;
                    
                    ui_content_title_label.Text = category.category_name;
                }
            }
            else
            {

            }
        }

        protected void ui_save_button_Click(object sender, EventArgs e)
        {
            StationeryController sationeryController = new StationeryController();

            StationeryCategory category = new StationeryCategory();
            int category_id;

            //getting hidden field id
            if (int.TryParse(ui_id_hiddenfield.Value, out category_id))
            {
                category.id = Convert.ToInt32(ui_id_hiddenfield.Value);
            }

            //fields validation
            if (String.IsNullOrEmpty(ui_catname_textbox.Text))
            {
                Response.Cookies.Add(new HttpCookie("flash_message", "Field(s) marked with * can't be empty.") { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-error") { Path = "/" });
                return;
                //String idstring = (supplier.id > 0) ? ("?id="+supplier.id) : ("");
                //Response.Redirect("~/StoreClerk/SupplierDetail.aspx"+idstring);
            }

            //setting up object
            category.category_name = ui_catname_textbox.Text;

            //updating db;
            Message message;
            if (category.id > 0)
            {
                message = sationeryController.actionUpdateStationeryCategory(category);
            }
            else
            {
                message = sationeryController.actionCreateStationeryCategory(category);
            }

            //redirecting
            if (message.condition)
            {
                Response.Cookies.Add(new HttpCookie("flash_message", "Successfully Saved.") { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-success") { Path = "/" });
                Response.Redirect("~/StoreClerk/CategoryDetail.aspx?id=" + category.id);
            }
            else
            {
                Response.Cookies.Add(new HttpCookie("flash_message", message.message) { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-error") { Path = "/" });
                Response.Redirect("~/StoreClerk/CategoryDetail.aspx?id=" + category.id);
            }

        }

        protected void ui_cancle_button_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StoreClerk/CategoryList.aspx");
        }
    }
}