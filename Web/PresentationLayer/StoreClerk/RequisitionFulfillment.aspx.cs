using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ApplicationLayer.Controllers;
using ApplicationLayer.Utils;
using ApplicationLayer;

namespace PresentationLayer
{
    public partial class RequisitionFulfillment : System.Web.UI.Page
    {
        
        int originalQty, editableQty, deptId, stationeryId;
        bool refreshNeed=false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["id"].ToString() != string.Empty)
                {
                    deptId = Convert.ToInt32(Request.QueryString["id"].ToString());

                    GetDepartmentForFulfillment d = new DepartmentController().actionGetDepartmentForFulfillment(deptId);
                    ui_content_title_label.Text = "Requistions from "+d.department_name;
                    //lblDeptName.Text = d.department_name;
                    lblCollectionPoint.Text = d.name;
                    lblReptName.Text = d.representative_name;
                }

                FulfillmentController fc = new FulfillmentController();
                BindGridView();

            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)");
                e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)");
                //e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(GridView1, "Select$" + e.Row.RowIndex.ToString()));
                //e.Row.Attributes.Add("onclick", String.Format("javascript:__doPostBack('GridView1','Select${0}')", e.Row.RowIndex));
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(2000);
            foreach (GridViewRow row in GridView1.Rows)
            {
                // protect not to fill invalid even in disable javaScript state 
                // lack of check balance in code
                
                if (checkValidation(row))
                {
                    String txtOriginal = row.Cells[6].Text;
                    TextBox txtEditable = (TextBox)row.FindControl("TextBox1");

                    if (checkEdit(txtOriginal, txtEditable.Text))
                    {
                        deptId = Convert.ToInt32(row.Cells[2].Text);
                        stationeryId = Convert.ToInt32(row.Cells[3].Text);

                        FulfillmentController fc = new FulfillmentController();
                        fc.actionUpdateQty(deptId, stationeryId, originalQty, editableQty);

                        refreshNeed = true;
                    }
                }
            }

            showSuccess(refreshNeed, "Successfully Saved.");
        }

        public bool checkEdit(String txtOriginalQty, String txtEditableQty)
        {
            originalQty = Convert.ToInt32(txtOriginalQty);
            editableQty = Convert.ToInt32(txtEditableQty);

            return (originalQty != editableQty ? true : false);
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // why here ?
            // To avoid null in cell, hide in row creation

            //e.Row.Cells[2].Visible = false;
            //e.Row.Cells[3].Visible = false;
        }

        protected void btnDistribute_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (checkValidation(row))
                {
                    CheckBox cb = (CheckBox)row.FindControl("selector");
                    if (cb.Checked)
                    {
                        deptId = Convert.ToInt32(row.Cells[2].Text);
                        stationeryId = Convert.ToInt32(row.Cells[3].Text);

                        new DisbursementController().actionUpdateToDisbursement(deptId, stationeryId);

                        refreshNeed = true;
                    }
                }
            }

            Department department = new DepartmentController().actionGetDepartmentByID(deptId);
            User head = new UserController().actionGetUserByID(Convert.ToInt32(department.department_head));

            String email_title = "Stationery Requesitions Update Information.";
            String email_body = "<p>Hello " + head.firstname + " " + head.lastname + ",</p>" +
                                      "<p>The stationeries requested from "+ department.department_name+ " are distributed " +
                                      "to " + department.representative_name + ".</p>" +
                                      "<br />Thank you,<br/> Logic University.<p>This is system generated mail. Please do not reply.</p>";
            Helper.sendMail(head.email, "no-reply@logic-university.com", email_title, email_body);
            new SMSController().sendSMS(head.phone_number, "Requested stationeries from " + department.department_name + " are distributed.");

            showSuccess(refreshNeed, "Successfully Recorded Distributed Items.");
        }

        private bool checkValidation(GridViewRow row)
        {
            RequiredFieldValidator rfv = (RequiredFieldValidator)row.FindControl("RequiredFieldValidator1");
            CompareValidator cv1 = (CompareValidator)row.FindControl("CompareValidator1");
            RangeValidator rv = (RangeValidator)row.FindControl("RangeValidator1");

            return (rfv.IsValid == cv1.IsValid == rv.IsValid == true ? true : false);
        }

        protected void btnProceed_Click(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(2000);


            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox cb = (CheckBox)row.FindControl("selector");
                if (cb.Checked)
                {
                    deptId = Convert.ToInt32(row.Cells[2].Text);
                    stationeryId = Convert.ToInt32(row.Cells[3].Text);

                    actionChangeAllToFulfillment();

                    // For showing cookie
                    refreshNeed = true;
                }
            }

            showSuccess(refreshNeed, "Successfully Merged.");
        }

        private void actionChangeAllToFulfillment()
        {
            FulfillmentController f = new FulfillmentController();

            if (chkDateTime.Checked)
            {
                DateTime dateTime = Helper.CovertToCustomDateTime(txtDate.Text, txtTime.Text, txtTimeMin.Text, ddlTime.Text);
                f.actionChangeAllToFulfillmentByDepartmentAndStationeryAndTime(deptId, stationeryId, dateTime);
            }
            else
            {
                f.actionChangeAllToFulfillmentByDepartmentAndStationery(deptId, stationeryId); 
            }
        }

        private void BindGridView()
        {
            GridView1.DataSource = new FulfillmentController().actionGetFulfillmentViewByDepartment(deptId);
            GridView1.DataBind();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
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

        protected void btnFulfillQty_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                TextBox txtFulfillQty = (TextBox)row.FindControl("TextBox1");
                int reqQty = Convert.ToInt32(row.Cells[5].Text);
                int stockQty=Convert.ToInt32(row.Cells[9].Text);

                if (reqQty != Convert.ToInt32(txtFulfillQty.Text))
                {
                    if (reqQty <= stockQty)
                        txtFulfillQty.Text = reqQty.ToString();
                    else 
                        txtFulfillQty.Text = stockQty.ToString();
                }
            }
        }

        public void showSuccess(bool flag,string msg)
        {
            if (flag)
            {
                Response.Cookies.Add(new HttpCookie("flash_message", msg) { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-success") { Path = "/" });
                Response.Redirect(Request.RawUrl);
            }

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            
        }
    }
}