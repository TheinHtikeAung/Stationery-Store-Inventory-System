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
    public partial class DiscrepancyList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public List<Discrepancy_View> actionGetPendingDiscrepencyViews() {
            return new DiscrepencyController().actionGetPendingDiscrepancyViews();

        }

        public List<Discrepancy_View> actionGetApprovedDiscrepencyViews()
        {
            return new DiscrepencyController().actionGetApprovedDiscrepancyViews();

        }

        protected void ui_supplier_gridview_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        
        
    }
}