using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ApplicationLayer.Utils;
using ApplicationLayer;
using ApplicationLayer.Controllers;

namespace PresentationLayer.m
{
    public partial class MakeRequisition : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                StationeryController stationeryController = new StationeryController();

                List<Supplier> suppliers = new SupplierController().actionGetSuppliers();

                int stationery_id;

                if (int.TryParse(Request.QueryString["id"], out stationery_id))
                {
                    stationery_id = Convert.ToInt16(Request.QueryString["id"].ToString());
                    if (stationery_id < 1)
                    {
                        Response.Redirect("~/m/DepartmentEmplyoee/Stationeries.aspx");
                    }

                    if (!Page.IsPostBack)
                    {
                        Stationery stationery = stationeryController.actionGetStationeryByID(stationery_id);
                        StationeryCategory category = stationeryController.actionGetStationeryCategoryByID(stationery.category);
                        ui_stationery_name.Text = stationery.stationery_name;
                        ui_category.Text = category.category_name;
                        ui_id_hidden.Value = stationery.id.ToString();
                        ui_uom.Text = stationery.unit_of_measure;
                        ui_price.Text = stationery.price.ToString();
                        ui_total.Text = stationery.price.ToString();
                        ui_price_hidden_forjscalculation.Value = stationery.price.ToString();
                    }
                }
                else
                {

                }
            }
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            User sUser = (User)Session["user"];
            if (sUser == null || sUser.user_type != 1)
            {
                Response.Redirect("~/m/Login.aspx");
            }

            User dUser = new UserController().actionGetUserByID(sUser.id);

            RequisitionController requisitionController = new RequisitionController();
            ApplicationLayer.Requisition requisition = new ApplicationLayer.Requisition();
            requisition.user_obj = dUser.id;
            requisition.stationery = Convert.ToInt32(ui_id_hidden.Value);
            requisition.quantity = Convert.ToInt32(ui_quantity.Text);
            requisition.department = Convert.ToInt32(dUser.department);
            Message message = requisitionController.actionCreateRequisition(requisition);

            if (message.condition)
            {
                Response.Cookies.Add(new HttpCookie("flash_message", "Successfully Requested.") { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-success") { Path = "/" });
                Response.Redirect("~/m/DepartmentEmployee/Stationeries.aspx");
            }
            else {
                Response.Cookies.Add(new HttpCookie("flash_message", "Requisition Fail. <br />" + message.message) { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-error") { Path = "/" });
                return;
            }
        }
    }
}