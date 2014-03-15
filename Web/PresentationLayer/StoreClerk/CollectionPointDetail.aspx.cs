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
    public partial class CollectionPointDetail : System.Web.UI.Page
    {
        CollectionPointController collectionPointController = new CollectionPointController();
        protected void Page_Load(object sender, EventArgs e)
        {
            int CollectionPoint_id;
            if (int.TryParse(Request.QueryString["id"], out CollectionPoint_id))
            {
                CollectionPoint_id = Convert.ToInt32(Request.QueryString["id"].ToString());
                if (CollectionPoint_id < 1)
                {
                    Response.Redirect("~/StoreClerk/CollectionPointList.aspx");
                }
                if (!Page.IsPostBack)
                {

                    CollectionPoint collectionPoint = collectionPointController.actionGetCollectionPointByID(CollectionPoint_id);

                    ui_id_hiddenfield.Value = collectionPoint.id.ToString();
                    ui_collectionPoint_textbox.Text = collectionPoint.name;

                    ui_content_title_label.Text = collectionPoint.name;

                }
            }
            else
            {

            }


        }
        protected void ui_save_button_Click(object sender, EventArgs e)
        {
            CollectionPoint collectionPoint = new CollectionPoint();
            int collectionPoint_id;

            if (int.TryParse(ui_id_hiddenfield.Value, out collectionPoint_id))
            {
                collectionPoint.id = Convert.ToInt32(ui_id_hiddenfield.Value);
            }
            if (String.IsNullOrEmpty(ui_collectionPoint_textbox.Text))
            {
                Response.Cookies.Add(new HttpCookie("flash_message", "Field(s) marked with * can't be empty.") { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-error") { Path = "/" });
                return;
        }
     
            collectionPoint.name = ui_collectionPoint_textbox.Text;

             Message message;
            if (collectionPoint.id > 0)
            {
                message = collectionPointController.actionUpdateCollectionPoint(collectionPoint);
            }
            else {
                message =collectionPointController.actionCreateCollectionPoint(collectionPoint);
            }

            //redirecting
            if (message.condition)
            {
                Response.Cookies.Add(new HttpCookie("flash_message", "Successfully Saved.") { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-success") { Path = "/" });
                Response.Redirect("~/StoreClerk/CollectionPointDetail.aspx?id=" + collectionPoint.id);
            }
            else {
                Response.Cookies.Add(new HttpCookie("flash_message", message.message) { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-error") { Path = "/" });
                Response.Redirect("~/StoreClerk/CollectionPointDetail.aspx?id=" + collectionPoint.id);
            }
            
        }

        protected void ui_cancle_button_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StoreClerk/CollectionPointList.aspx");
        }
    }
    }
