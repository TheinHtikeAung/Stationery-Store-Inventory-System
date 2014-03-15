using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationLayer.Controllers;
using ApplicationLayer;
using ApplicationLayer.Utils;


namespace PresentationLayer.DepartmentHead
{
    public partial class Preference : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            User user = (User)Session["user"];
            if (user == null || ((user.user_type != 3) && user.user_type != 2))
            {
                Response.Redirect("~/Login.aspx");
            }

            UserController userController = new UserController();
            User dUser = userController.actionGetUserByID(user.id);

            if (!IsPostBack)
            {
                Department department = new DepartmentController().actionGetDepartmentByID(Convert.ToInt32(dUser.department));

                ddlCollectionPoint.DataSource = new CollectionPointController().actionGetCollectionPoints();
                ddlCollectionPoint.DataValueField = "id";
                ddlCollectionPoint.DataTextField = "name";
                ddlCollectionPoint.DataBind();
                ddlCollectionPoint.Items.FindByValue(department.collection_point.ToString()).Selected = true;

                txtRepresentative.Text = department.representative_name;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int department_id = Convert.ToInt32(((User)Session["user"]).department);

            Message msg = new DepartmentController().actionChangeCollectionPointAndReptName(department_id, Convert.ToInt32(ddlCollectionPoint.SelectedValue), txtRepresentative.Text);

            if (msg.condition)
            {
                Response.Cookies.Add(new HttpCookie("flash_message", "Successfully Saved.") { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-success") { Path = "/" });
                Response.Redirect("~/DepartmentHead/Preference.aspx?id=" + department_id);
            }
            else {
                Response.Cookies.Add(new HttpCookie("flash_message", msg.message) { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-error") { Path = "/" });
                Response.Redirect(Request.RawUrl);
            }
            
            
            //lblStatus.Visible = true;

            //lblStatus.Text = "Successfully Updated";
        }
    }
}