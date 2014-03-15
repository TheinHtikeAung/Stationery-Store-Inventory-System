using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApplicationLayer.Controllers;
using ApplicationLayer;
using ApplicationLayer.ViewModels;
using ApplicationLayer.Utils;

using System.Web.UI;
using System.Web.UI.WebControls;



namespace PresentationLayer.StoreClerk
{
    public partial class OrderArrivalCreation : System.Web.UI.Page
    {
        Stationery stationeryNew = new Stationery();
        SupplierController supController = new SupplierController();
        StationeryController statController = new StationeryController();
        StockTransactionController stockController = new StockTransactionController();
        Message message = new Message();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                List<Supplier> sList = supController.actionGetSuppliers();

                DropDownList2.DataSource = sList;
                DropDownList2.DataTextField = "supplier_name";
                DropDownList2.DataValueField = "id";
                DropDownList2.DataBind();
         
            }
        }

        protected void Save_Click(object sender, EventArgs e)
        {

            if (TextBox1.Text != string.Empty && TextBox2.Text != string.Empty)
            {

                try
                {
                    
                    String name = TextBox1.Text;


                    int supplierId = Convert.ToInt32(DropDownList2.SelectedValue);
                    int quantityAdd = Convert.ToInt32(TextBox2.Text);

                    String des;
                    if (TextBox3.Text == "")
                    {
                        des = "Supplier " + DropDownList2.SelectedItem.Text;
                    }
                    else {
                        des = TextBox3.Text + " (Supplier " + DropDownList2.SelectedItem.Text + ")"; 
                    }
 
                    String stationeryid = statController.actionGetStatIdbyItemName(name);
                    int stationeryId = Convert.ToInt32(stationeryid);
                    int quantityStocked = Convert.ToInt32(statController.actionGetStationeryQuantityStocked(stationeryId));
                    int bal = quantityStocked + quantityAdd;
                    stockController.actionCreateStockTransaction(stationeryId, bal, des, "+"+TextBox2.Text);

                    statController.actionUpdateQtyInStock(stationeryId, quantityAdd);

                    Response.Cookies.Add(new HttpCookie("flash_message", "Successfully Saved.") { Path = "/" });
                    Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-success") { Path = "/" });
                    Response.Redirect(Request.RawUrl);



                    // update the stationery quantity of stock after add new items from supplier
                    //lblStatus.Visible = true;
                    //lblStatus.Text = "Successfully Saved.";
                    
                }
                catch 
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('please input correctly')", true);
                }
            }
            else
            {
                Response.Cookies.Add(new HttpCookie("flash_message", "Field(s) marked with * can't be empty.") { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-error") { Path = "/" });
                Response.Redirect("~/StoreClerk/OrderArrivalCreation.aspx");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('please input item and quantity')", true);
               
            }
            //TextBox1.Text = "";
            //TextBox2.Text = "";
            //TextBox3.Text = "";
            //DropDownList2.DataSource = null;
            //DropDownList2.DataBind();
            
           
        }

   


        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {

            string[] stringDept = new StationeryController().getStatoineryString(prefixText, count);

            return stringDept;
        }



        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            

            List<Supplier> supplierList = new List<Supplier>();
            supplierList = statController.actionGetSuppliersByItem(TextBox1.Text);

            DropDownList2.DataSource = supplierList;
            DropDownList2.DataTextField = "supplier_name";
            DropDownList2.DataValueField = "id";
            DropDownList2.DataBind();
            
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StoreClerk/");
        }

       
        
    }

    
}