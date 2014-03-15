using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationLayer;
using ApplicationLayer.Controllers;
using ApplicationLayer.Utils;
using System.Threading;
using System.Globalization;



namespace PresentationLayer.StoreClerk
{
    public partial class DelegateUser : System.Web.UI.Page
    {
        static int loginUserId;

        protected void Page_Load(object sender, EventArgs e)
        {
            User sUser = (User) Session["user"];
            if(sUser == null || ((sUser.user_type != 3) && sUser.user_type != 2))
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!Page.IsPostBack) {
                UserController userController = new UserController();
                User user = userController.actionGetUserByID(sUser.id);

                ui_emplyoees_dropdown.DataSource = new UserController().actionGetEmployeesByDepartmentID(Convert.ToInt32(user.department));
                ui_emplyoees_dropdown.DataTextField = "username";
                ui_emplyoees_dropdown.DataValueField = "id";
                ui_emplyoees_dropdown.DataBind();

                ui_delegated_dropdown.DataSource = new UserController().actionGetHeadDelegatedUsersByDepartmentID(Convert.ToInt32(user.department));
                ui_delegated_dropdown.DataTextField = "username";
                ui_delegated_dropdown.DataValueField = "id";
                ui_delegated_dropdown.DataBind();
            }
            
            
            //get from session
            //loginUserId = 15;
            //BindGridView(loginUserId);
        }

        protected void ui_delegate_button_Click(object sender, EventArgs e)
        {
            User currentUser = (User)Session["user"];
            int user_id = Convert.ToInt32(ui_emplyoees_dropdown.SelectedValue.ToString());
            UserController userController = new UserController();
            Message msg = userController.actionGiveHeadPermissionToAnEmployee(user_id, Convert.ToInt32(currentUser.department));
            if (msg.condition)
            {
                Response.Cookies.Add(new HttpCookie("flash_message", "Successfully Saved.") { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-success") { Path = "/" });
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                Response.Cookies.Add(new HttpCookie("flash_message", msg.message) { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-error") { Path = "/" });
            }

        }

        protected void ui_removedelegated_button_Click(object sender, EventArgs e)
        {
            User currentUser = (User)Session["user"];
            int user_id = Convert.ToInt32(ui_delegated_dropdown.SelectedValue.ToString());
            UserController userController = new UserController();
            Message msg = userController.actionRemoveHeadPermissionFromAnEmployee(user_id, Convert.ToInt32(currentUser.department));

            Session["user"] = userController.actionGetUserByID(currentUser.id);

            if (msg.condition)
            {
                Response.Cookies.Add(new HttpCookie("flash_message", "Successfully Removed.") { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-success") { Path = "/" });
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                Response.Cookies.Add(new HttpCookie("flash_message", msg.message) { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-error") { Path = "/" });
                Response.Redirect(Request.RawUrl);
            }
        }

        /*

        public List<CustomUserDelegate> getCustomUserDelegateList(int loginUserId)
        {
            return new UserDelegateController().getCustomUserDelegateListByLoginUserId(loginUserId);
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(Validation())
            {
                int existingId;
                bool existing = int.TryParse(hfId.Value.ToString(),out existingId);

                if (existing)
                {
                    new UserDelegateController().actionUpdateUserDelegate(existingId, txtUserTo.Text, txtFromDate.Text, txtToDate.Text);
                    btnNew_Click(sender, e);
                    showStatus("Successfully updated.");
                }
                else
                {
                    new UserDelegateController().actionSaveUserDelegate(txtUserTo.Text, loginUserId, txtFromDate.Text, txtToDate.Text);
                    btnNew_Click(sender, e);
                    showStatus("New record successfully created.");
                }

            
                BindGridView(loginUserId);
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id=Convert.ToInt32(GridView1.SelectedRow.Cells[1].Text);

            CustomUserDelegate c = new UserDelegateController().getCustomerUserDelegateById(id);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            txtUserTo.Text = c.user_to;
            txtFromDate.Text = c.date_from.ToString("d");
            txtToDate.Text = c.date_to.ToString("d");
            hfId.Value =c.id.ToString();

            BindGridView(loginUserId);
            hideStatus();
        }

        private void BindGridView(int loginUserId)
        {
            GridView1.DataSource = getCustomUserDelegateList(loginUserId);
            GridView1.DataBind();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            GridView1.SelectedIndex = -1; //Deselect Gridview1

            hfId.Value = null;
            txtUserTo.Text = null;
            txtFromDate.Text = null;
            txtToDate.Text=null;
            hideStatus();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int existingId;
            bool existing = int.TryParse(hfId.Value.ToString(), out existingId);

            if (existing)
            {
                new UserDelegateController().actionDeleteUserDelegate(existingId);

                btnNew_Click(sender, e);
                BindGridView(loginUserId);
                showStatus("Successfully deleted.");
            }
        }

        private void showStatus(string msg)
        {
            lblStatus.Visible = true;
            lblStatus.Text = msg;
        }

        private void hideStatus()
        {
            lblStatus.Visible = false;
            lblStatus.Text = string.Empty;
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.DataSource = getCustomUserDelegateList(loginUserId);
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }

        // Why check ? For condition diable javaScript
        private bool Validation()
        {
            if(RequiredFieldValidator1.IsValid==RequiredFieldValidator1.IsValid==
                RequiredFieldValidator1.IsValid==true)
            {
                return true;
            }

            return false;
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataContext db = new DataContext();
            
            int deptId= new UserDelegateController().getUserDepartmentId(loginUserId);

            string[] stringUser=(from u in db.Users where u.department==deptId && u.user_type!=2 && u.username.StartsWith(prefixText) select u.username).Take(count).ToArray();

            return stringUser;
        }
        */
        
    }
}