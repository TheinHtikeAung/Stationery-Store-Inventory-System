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
    public partial class CategoryList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StationeryController stationeryController = new StationeryController();
            ui_category_gridview.DataSource = stationeryController.actionGetStationeryCategories();
            ui_category_gridview.GridLines = GridLines.None;
            ui_category_gridview.DataBind();
        }

        
        protected void CreateNew_Button_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StoreClerk/CategoryDetail.aspx");
        }

        protected void ui_category_gridview_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ui_category_gridview.SelectedRow != null)
            {
                int id = Convert.ToInt16(ui_category_gridview.SelectedRow.Cells[0].Text);
                Response.Redirect("~/StoreClerk/CategoryDetail.aspx?id=" + id);
            }
        }
    }
}