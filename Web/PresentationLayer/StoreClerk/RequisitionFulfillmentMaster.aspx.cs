using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationLayer.Controllers;
using ApplicationLayer.Utils;

namespace PresentationLayer.StoreClerk
{
    public partial class RequisitionFulfillmentMaster : System.Web.UI.Page
    {
        int deptId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //FulfillmentController fc = new FulfillmentController();
                //GridView1.DataSource = fc.actionGetDepartmentFulfillment();

                GridView1.DataSource = new DepartmentController().actionGetDepartmentForFulfillment();
                GridView1.DataBind();
            }

            //lblStatus.Visible = false;
        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            bool b = false;
            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox cb = (CheckBox)row.FindControl("selector");
                if (cb.Checked)
                {
                    deptId = Convert.ToInt32(row.Cells[2].Text);
                    actionChangeAllToFulfillment();
                    b = true;
                    //lblStatus.Visible = true;
                    //lblStatus.Text = "Successfully process the requisition items.";
                }
            }
            if (b) {
                Response.Cookies.Add(new HttpCookie("flash_message", "Successfully Merged.") { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-success") { Path = "/" });
                Response.Redirect(HttpContext.Current.Request.Url.PathAndQuery);
            }
            
        }

        private void actionChangeAllToFulfillment()
        {
            FulfillmentController f = new FulfillmentController();

            if (chkDateTime.Checked)
            {
                f.actionChangeAllToFulfillmentByDepartmentAndDateTime(deptId, Helper.CovertToCustomDateTime(txtDate.Text, txtTime.Text, txtTimeMin.Text,ddlTime.Text));
            }
            else 
            {
                f.actionChangeAllToFulfillmentByDepartment(deptId);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)");
                e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)");

                //e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(GridView1, "Select$" + e.Row.RowIndex.ToString()));
            }
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[2].Visible = false;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridView1.SelectedRow != null)
            {
                String id = GridView1.SelectedRow.Cells[2].Text;

                Response.Redirect("RequisitionFulfillment.aspx?id="+id);
            }
        }

        protected void chkDateTime_CheckedChanged(object sender, EventArgs e)
        {
            txtDate.Visible = chkDateTime.Checked;
            txtTime.Visible = chkDateTime.Checked;
            txtTimeMin.Visible = chkDateTime.Checked;
            ddlTime.Visible = chkDateTime.Checked;

            Label2.Visible = chkDateTime.Checked;
            Label3.Visible = chkDateTime.Checked;
            Label4.Visible = chkDateTime.Checked;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Response.Write("<script language=javascript>child=window.open('default.aspx');</script>");

            //Button1.Attributes.Add("onclick", "window.open('www.microsoft.com'); return false;");
            //Button1.Attributes.Add("onclick", "Popup=window.open('DiscrepancyMaintenance.aspx','Popup','toolbar=no, location=yes,status=no,menubar=no,scrollbars=yes,resizable=no, width=700,height=350,left=430,top=23'); return false;");
        }


    }
}