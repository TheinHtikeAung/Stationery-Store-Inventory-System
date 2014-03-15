using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ApplicationLayer;
using ApplicationLayer.Controllers;
using ApplicationLayer.Utils;

namespace PresentationLayer.DepartmentHead
{
    public partial class AccountEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User sUser = (User)Session["user"];
            if (sUser == null || sUser.user_type != 3)
            {
                if(sUser.user_type == 2){
                    Response.Redirect("~/DepartmentEmployee/AccountEdit.aspx");
                }

                Response.Redirect("~/Login.aspx");
            }

            User dUser = new UserController().actionGetUserByID(sUser.id);

            if (!Page.IsPostBack)
            {
                ui_username_label.Text = dUser.username;
                //ui_password_textbox.Text = dUser.password;
                ui_firstname_textbox.Text = dUser.firstname;
                ui_lastname_textbox.Text = dUser.lastname;
                ui_email_textbox.Text = dUser.email;
                ui_phonenumber_textbox.Text = dUser.phone_number;
            }


        }

        protected void ui_save_button_Click(object sender, EventArgs e)
        {
            User sUser = (User)Session["user"];
            User dUser = new UserController().actionGetUserByID(sUser.id);

            if (String.IsNullOrEmpty(ui_password_textbox.Text) || String.IsNullOrEmpty(ui_email_textbox.Text))
            {
                Response.Cookies.Add(new HttpCookie("flash_message", "Field(s) marked with * can't be empty.") { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-error") { Path = "/" });
                return;
            }

            dUser.password = ui_password_textbox.Text;
            dUser.firstname = ui_firstname_textbox.Text;
            dUser.lastname = ui_lastname_textbox.Text;
            dUser.email = ui_email_textbox.Text;
            dUser.phone_number = ui_phonenumber_textbox.Text;

            Message msg = new UserController().actionUpdateUser(dUser);
            if (!msg.condition)
            {
                Response.Cookies.Add(new HttpCookie("flash_message", msg.message) { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-error") { Path = "/" });
                return;
            }

            Response.Cookies.Add(new HttpCookie("flash_message", "Successfully Saved.") { Path = "/" });
            Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-success") { Path = "/" });
            Session["user"] = new UserController().actionGetUserByID(sUser.id);
            Response.Redirect("AccountEdit.aspx");
        }

        protected void ui_cancle_button_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/");
        }
    }
}