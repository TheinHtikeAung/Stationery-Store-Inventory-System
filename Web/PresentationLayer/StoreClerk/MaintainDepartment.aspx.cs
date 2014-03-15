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
    public partial class MaintainDepartment : System.Web.UI.Page
    {

        Department department = new Department();
        DepartmentController departmentController = new DepartmentController();
        protected void Update_Click(object sender, EventArgs e)
        {
            department = departmentController.actionGetDepartmentByID(Convert.ToInt32((Session["id"])));
            department.code = TextBox1.Text;
            department.phone = TextBox2.Text;
            department.department_name = TextBox3.Text;
            if (TextBox4.Text != string.Empty)
            {
                department.contact_name = TextBox4.Text;
            }
            department.department_head = Convert.ToInt32(TextBox5.Text);
            department.collection_point = Convert.ToInt32(DropDownList1.SelectedValue);
            department.representative_name = TextBox7.Text;
            departmentController.actionUpdateDepartment(department);
            Response.Redirect("DepartmentList.aspx");
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            TextBox1.Text = string.Empty;
            TextBox2.Text = string.Empty;
            TextBox3.Text = string.Empty;
            TextBox4.Text = string.Empty;
            TextBox5.Text = string.Empty;
            TextBox7.Text = string.Empty;
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Session["id"]);
            departmentController.actionDeleteDepartmentByID(id);
            Response.Redirect("DepartmentList.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                TextBox1.Text = Convert.ToString(Session["code"]);
                TextBox2.Text = Convert.ToString(Session["phone"]);
                TextBox3.Text = Convert.ToString(Session["department_name"]);
                TextBox4.Text = Convert.ToString(Session["contact_name"]);
                TextBox5.Text = Convert.ToString(Session["department_head"]);
                TextBox7.Text = Convert.ToString(Session["representative_name"]);

                List<CollectionPoint> pointList = departmentController.actionGetAllCollectionPoint();
                DropDownList1.DataSource = pointList;
                DropDownList1.DataTextField = "name";
                DropDownList1.DataValueField = "id";
                DropDownList1.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("DepartmentList.aspx");
        }
    }
}