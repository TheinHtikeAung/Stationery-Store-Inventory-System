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
    public partial class AddNewDepartment : System.Web.UI.Page
    {
        DepartmentController depController = new DepartmentController();
        Department newDep = new Department();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                List<CollectionPoint> pointList = depController.actionGetAllCollectionPoint();
                DropDownList1.DataSource = pointList;
                DropDownList1.DataTextField = "name";
                DropDownList1.DataValueField = "id";
                DropDownList1.DataBind();

            }

        }

      

        protected void Button1_Click1(object sender, EventArgs e)
        {

            newDep.code = TextBox1.Text;
            newDep.phone = TextBox2.Text;
            newDep.department_name = TextBox3.Text;
            newDep.contact_name = TextBox4.Text;

            newDep.department_head = Convert.ToInt32(TextBox5.Text);
            newDep.collection_point = Convert.ToInt32(DropDownList1.SelectedValue);


            if (TextBox7.Text != null)
            {
                newDep.representative_name = TextBox7.Text;
            }

            depController.actionCreateDepartment(newDep);
            Response.Redirect("DepartmentList.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            TextBox1.Text = string.Empty;
            TextBox2.Text = string.Empty;
            TextBox3.Text = string.Empty;
            TextBox4.Text = string.Empty;
            TextBox5.Text = string.Empty;
        }

        
    }
}