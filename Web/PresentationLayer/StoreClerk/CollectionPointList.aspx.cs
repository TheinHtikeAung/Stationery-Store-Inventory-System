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
    public partial class CollectionPointList : System.Web.UI.Page
    {
        CollectionPointController collectionPointController = new CollectionPointController();
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!Page.IsPostBack)
            {
                ui_collectionPoint_gridview.DataSource = collectionPointController.actionGetCollectionPoints();
                ui_collectionPoint_gridview.DataBind();
            }
        }
        protected void ui_collectionPoint_gridview_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ui_collectionPoint_gridview.SelectedRow!= null)
            {
                int id = Convert.ToInt16(ui_collectionPoint_gridview.SelectedRow.Cells[0].Text);
                //response.Redirect("~/admin/CollectionPoint/edit?id=" + id);
                Response.Redirect("~/StoreClerk/CollectionPointDetail.aspx/edit?id=" + id);
            }
        }
        protected void CreateNew_Button_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StoreClerk/CollectionPointDetail.aspx");
        }

        
        }
}