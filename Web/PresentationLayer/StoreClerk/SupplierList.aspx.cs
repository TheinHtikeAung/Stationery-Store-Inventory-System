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
    public partial class SupplierList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SupplierController supplierController = new SupplierController();

            if (!Page.IsPostBack)
            {
                ui_supplier_gridview.DataSource = supplierController.actionGetSuppliers();
                ui_supplier_gridview.GridLines = GridLines.None;
                ui_supplier_gridview.DataBind();
            }

        }

        protected void ui_supplier_gridview_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ui_supplier_gridview.SelectedRow != null) {
                int id = Convert.ToInt16(ui_supplier_gridview.SelectedRow.Cells[0].Text);
                Response.Redirect("~/StoreClerk/SupplierDetail.aspx?id=" + id);
            }
        }

        protected void CreateNew_Button_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StoreClerk/SupplierDetail.aspx");
        }

        



        

        
        
    }
}