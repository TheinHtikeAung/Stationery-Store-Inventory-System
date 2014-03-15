using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ApplicationLayer.Controllers;
using ApplicationLayer.ViewModels;
using ApplicationLayer;

namespace PresentationLayer.StoreSupervisor
{
    public partial class DiscrepencyList : System.Web.UI.Page
    {
        DiscrepencyController discrepancyController;
        int id;
        List<Supervisor_Discrepancy_View> discrepencies;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            discrepancyController = new DiscrepencyController();
            if (!Page.IsPostBack)
            {
                
                User user = (User)Session["user"];
                if (user == null || user.user_type != 5 && user.user_type != 6)
                {
                    Response.Redirect("~/Login.aspx");
                }
                id = user.user_type;

                if (id == 5)
                {
                    discrepencies = discrepancyController.actionGetDiscrepanciesForStoreSupervisor();
                    discrepancyListView.DataSource = discrepencies;
                    discrepancyListView.DataBind();
                }
                else if (id == 6)
                {
                    discrepencies = discrepancyController.actionGetDiscrepanciesForStoreManager();
                    discrepancyListView.DataSource = discrepencies;
                    discrepancyListView.DataBind();
                }
            }
            //discrepancyListView.DataBind();
            
            
        }
       

        //public List<Supervisor_Discrepancy_View> actionGetDiscrepanciesForStoreSupervisor()
        //{
        //    if (id == 4)
        //    {
        //        return discrepancyController.actionGetDiscrepanciesForStoreSupervisor();
        //    }
        //    else if (id == 5)
        //    {
        //        return null;
        //    }
        //    return null;
            
        //}



        // ** Important for rendering grid view without compiler error **//
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        // *** Do not remove this ***
        public override void VerifyRenderingInServerForm(Control control)
        {
            
        }

        protected void discrepancyListView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            discrepancyListView.PageIndex = e.NewPageIndex;

            User user = (User)Session["user"];
            if (user == null || user.user_type != 5 && user.user_type != 6)
            {
                Response.Redirect("~/Login.aspx");
            }
            id = user.user_type;

            if (id == 5)
            {
                discrepencies = discrepancyController.actionGetDiscrepanciesForStoreSupervisor();
                discrepancyListView.DataSource = discrepencies;
                discrepancyListView.DataBind();
            }
            else if (id == 6)
            {
                discrepencies = discrepancyController.actionGetDiscrepanciesForStoreManager();
                discrepancyListView.DataSource = discrepencies;
                discrepancyListView.DataBind();
            } 
            
            //discrepancyListView.DataSource = discrepencies;
            //discrepancyListView.DataBind();

        }

        protected void ApproveAll_Button_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in discrepancyListView.Rows)
            {
                CheckBox cb = (CheckBox)row.FindControl("selector");
                if (cb.Checked)
                {
                    id = Convert.ToInt32(row.Cells[1].Text);
                    discrepancyController.actionApproveDiscrepancies(id);
                }
            }
            Response.Cookies.Add(new HttpCookie("flash_message", "Approved.") { Path = "/" });
            Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-success") { Path = "/" });
            Response.Redirect(Request.RawUrl);
        }

                    

        protected void discrepancyListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        

    }
}