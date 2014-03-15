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
    public partial class DepartmentDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DepartmentController departmentController = new DepartmentController();
            CollectionPointController collectionPointController = new CollectionPointController();

            int department_id;
            if (int.TryParse(Request.QueryString["id"], out department_id))
            {
                department_id = Convert.ToInt16(Request.QueryString["id"].ToString());
                if (department_id < 1)
                {
                    Response.Redirect("~/StoreClerk/DepartmentList.aspx");
                }

                Department department = departmentController.actionGetDepartmentByID(department_id);
                if (!Page.IsPostBack)
                {
                    ui_id_hiddenfield.Value = department.id.ToString();
                    ui_deptcode_textbox.Text = department.code;
                    ui_deptname_textbox.Text = department.department_name;
                    ui_contactname_textbox.Text = department.contact_name;
                    ui_phone_textbox.Text = department.phone;
                    ui_representativename_textbox.Text = department.representative_name;

                    ui_depthead_dropdown.DataSource = departmentController.actionGetUsersOfADepartment(department_id);
                    ui_depthead_dropdown.DataTextField = "username";
                    ui_depthead_dropdown.DataValueField = "id";
                    ui_depthead_dropdown.DataBind();
                    ui_depthead_dropdown.SelectedValue = department.department_head.ToString();

                    ui_collectionpoint_dropdown.DataSource = collectionPointController.actionGetCollectionPoints();
                    ui_collectionpoint_dropdown.DataTextField = "name";
                    ui_collectionpoint_dropdown.DataValueField = "id";
                    ui_collectionpoint_dropdown.DataBind();
                    ui_collectionpoint_dropdown.SelectedValue = department.collection_point.ToString();

                }
            }
            else 
            {
                String tmp_depthead = ui_depthead_dropdown.SelectedValue;
                String tmp_colpoint = ui_collectionpoint_dropdown.SelectedValue;
                ui_depthead_dropdown.DataSource = departmentController.actionGetUsersOfADepartment(department_id);
                ui_depthead_dropdown.DataTextField = "username";
                ui_depthead_dropdown.DataValueField = "id";
                ui_depthead_dropdown.DataBind();
                
                ui_collectionpoint_dropdown.DataSource = collectionPointController.actionGetCollectionPoints();
                ui_collectionpoint_dropdown.DataTextField = "name";
                ui_collectionpoint_dropdown.DataValueField = "id";
                ui_collectionpoint_dropdown.DataBind();

                ui_depthead_dropdown.SelectedValue = tmp_depthead;
                ui_collectionpoint_dropdown.SelectedValue = tmp_colpoint;
            }
        }

        protected void ui_save_button_Click(object sender, EventArgs e)
        {
            DepartmentController departmentController = new DepartmentController();
            
            Department department = new Department();
            int department_id;

            //getting hidden field id
            if (int.TryParse(ui_id_hiddenfield.Value, out department_id))
            {
                department.id = Convert.ToInt32(ui_id_hiddenfield.Value);
            }
            if (department.id > 0)
            {
                department = departmentController.actionGetDepartmentByID(department.id);
            }
            //fields validation
            if (String.IsNullOrEmpty(ui_deptcode_textbox.Text) || String.IsNullOrEmpty(ui_deptname_textbox.Text)
                || (Convert.ToInt32(ui_collectionpoint_dropdown.SelectedValue) < 1))
            {
                Response.Cookies.Add(new HttpCookie("flash_message", "Field(s) marked with * can't be empty.") { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-error") { Path = "/" });
                return;
            }

            //setting up object
            //user.username = ui_username_textbox.Text;
            
            department.code = ui_deptcode_textbox.Text;
            department.department_name = ui_deptname_textbox.Text;
            department.contact_name = ui_contactname_textbox.Text;
            department.phone = ui_phone_textbox.Text;
            department.representative_name = ui_representativename_textbox.Text;
            try {
                int head_id = Convert.ToInt32(ui_depthead_dropdown.SelectedValue);
                if (head_id > 0)
                {
                    department.department_head = head_id;
                }
            } catch (Exception ex){
                department.department_head = null;
            }
            
            

            int collection_point = Convert.ToInt32(ui_collectionpoint_dropdown.SelectedValue);
            if (collection_point > 0)
            {
                department.collection_point = collection_point;
            }

            //updating db;
            Message message;
            if (department.id > 0)
            {
                message = departmentController.actionUpdateDepartment(department);
            }
            else
            {
                message = departmentController.actionCreateDepartment(department);
            }

            //redirecting
            if (message.condition)
            {
                Response.Cookies.Add(new HttpCookie("flash_message", "Successfully Saved.") { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-success") { Path = "/" });
                Response.Redirect("~/StoreClerk/DepartmentDetails.aspx?id=" + department.id);
            }
            else
            {
                Response.Cookies.Add(new HttpCookie("flash_message", message.message) { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-error") { Path = "/" });
                Response.Redirect("~/StoreClerk/DepartmentDetails.aspx?id=" + department.id);
            }
        }

        protected void ui_cancle_button_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StoreClerk/DepartmentList.aspx");
        }
    }
}