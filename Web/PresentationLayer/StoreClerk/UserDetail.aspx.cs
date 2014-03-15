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
    public partial class UserDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            int user_id;
            if (int.TryParse(Request.QueryString["id"], out user_id))
            {
                user_id = Convert.ToInt16(Request.QueryString["id"].ToString());
                if (user_id < 1)
                {
                    Response.Redirect("~/StoreClerk/UserList.aspx");
                }

                if (!Page.IsPostBack)
                {
                    UserController userController = new UserController();
                    UserView user = userController.actionGetUserViewByUserID(user_id);

                    ui_id_hiddenfield.Value = user.id.ToString();
                    ui_username_textbox.Text = user.username;
                    ui_password_textbox.Text = "";
                    ui_firstname_textbox.Text = user.firstname;
                    ui_lastname_textbox.Text = user.lastname;
                    ui_email_textbox.Text = user.email;
                    ui_phonenumber_textbox.Text = user.phone_number;
                    ui_usertype_label.Text = user.type;


                    List<Department> departments = new DepartmentController().actionGetDepartments();
                    ui_department_dropdown.DataSource = departments;
                    ui_department_dropdown.DataTextField = "department_name";
                    ui_department_dropdown.DataValueField = "id";
                    if (user.department > 0) {
                        ui_department_dropdown.SelectedValue = user.department.ToString();
                    }
                    
                    ui_department_dropdown.DataBind();
                    
                    ui_content_title_label.Text = user.firstname + " " + user.lastname;
                }
            }
            else
            {
                String tmp_dept = ui_department_dropdown.SelectedValue;
                ui_usertype_label.Text = "(Default user type will be emplyoee)";

                List<Department> departments = new DepartmentController().actionGetDepartments();
                ui_department_dropdown.DataSource = departments;
                ui_department_dropdown.DataTextField = "department_name";
                ui_department_dropdown.DataValueField = "id";
                ui_department_dropdown.DataBind();

                ui_department_dropdown.SelectedValue = tmp_dept;
            }

            
        }


        protected void ui_save_button_Click(object sender, EventArgs e)
        {
            UserController userController = new UserController();

            User user = new User();
            int user_id;

            //getting hidden field id
            if (int.TryParse(ui_id_hiddenfield.Value, out user_id))
            {
                user.id = Convert.ToInt32(ui_id_hiddenfield.Value);
            }
            if (user.id > 0) {
                user = userController.actionGetUserByID(user.id);
            }
            //fields validation
            if (String.IsNullOrEmpty(ui_email_textbox.Text) || String.IsNullOrEmpty(ui_password_textbox.Text) || (Convert.ToInt32(ui_department_dropdown.SelectedValue) < 1))
            {
                Response.Cookies.Add(new HttpCookie("flash_message", "Field(s) marked with * can't be empty.") { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-error") { Path = "/" });
                return;
            }

            //setting up object
            user.username = ui_username_textbox.Text;
            user.password = ui_password_textbox.Text;
            user.firstname = ui_firstname_textbox.Text;
            user.lastname = ui_lastname_textbox.Text;
            user.phone_number = ui_phonenumber_textbox.Text;
            user.email = ui_email_textbox.Text;
            user.department = Convert.ToInt32(ui_department_dropdown.SelectedValue);
           
            //updating db;
            Message message;
            if (user.id > 0)
            {
                message = userController.actionUpdateUser(user);
            }
            else
            {
                //Default user creation is 1//Employee
                user.user_type = 1;
                message = userController.actionCreateUser(user);
            }

            //redirecting
            if (message.condition)
            {
                Response.Cookies.Add(new HttpCookie("flash_message", "Successfully Saved.") { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-success") { Path = "/" });
                Response.Redirect("~/StoreClerk/UserDetail.aspx?id=" + user.id);
            }
            else
            {
                Response.Cookies.Add(new HttpCookie("flash_message", message.message) { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-error") { Path = "/" });
                Response.Redirect("~/StoreClerk/UserDetail.aspx?id=" + user.id);
            }
           

        }

        protected void ui_cancle_button_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StoreClerk/UserList.aspx");
        }

    }
}