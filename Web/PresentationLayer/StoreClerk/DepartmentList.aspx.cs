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
    public partial class DepartmentList : System.Web.UI.Page
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {
            //DepartmentController department = new DepartmentController();
            //List<GetDepartmentInfo> list = new List<GetDepartmentInfo>();
            //list = department.actionGetDepartmentInfo();
            //GridView1.DataSource = list;
            //GridView1.DataBind();
        }

        //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    int row = Convert.ToInt32(e.CommandArgument);
        //    String id = GridView1.Rows[row].Cells[1].Text;
        //    String code = GridView1.Rows[row].Cells[2].Text;
        //    String department_name = GridView1.Rows[row].Cells[3].Text;
        //    String contact_name = GridView1.Rows[row].Cells[4].Text;
        //    String phone = GridView1.Rows[row].Cells[5].Text;
        //    String department_head = GridView1.Rows[row].Cells[6].Text;
        //    String department_head_name = GridView1.Rows[row].Cells[7].Text;
        //    String collection_point = GridView1.Rows[row].Cells[8].Text;
        //    String collection_point_name = GridView1.Rows[row].Cells[9].Text;
        //    String representative_name = GridView1.Rows[row].Cells[10].Text;
            
        //    Session["id"] = id;
        //    Session["code"] = code;
        //    Session["department_name"] = department_name;
        //    Session["contact_name"] = contact_name;
        //    Session["phone"] = phone;
        //    Session["department_head"] = department_head;
        //    Session["collection_point"] = collection_point;
        //    Session["representative_name"] = representative_name;
        //    Session["department_head_name"] = department_head_name;
        //    Session["collection_point_name"] = collection_point_name;
        //    Response.Redirect("MaintainDepartment.aspx");
            

           
        //}

        public List<GetDepartmentInfo> actionGetDepartments()
        {
            return new DepartmentController().actionGetDepartmentInfo();
        }

        protected void ui_supplier_gridview_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ui_departments_gridview.SelectedRow != null)
            {
                int id = Convert.ToInt16(ui_departments_gridview.SelectedRow.Cells[0].Text);
                Response.Redirect("~/StoreClerk/DepartmentDetails.aspx?id=" + id);
            }
        }

        protected void CreateNew_Button_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StoreClerk/DepartmentDetails.aspx");
        }

        
    }
}