using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ApplicationLayer;
using ApplicationLayer.Controllers;

namespace PresentationLayer
{
    public partial class login : System.Web.UI.Page
    {
        private static string[] mobileDevices = new string[] {"iphone","ppc",
                                                      "windows ce","blackberry",
                                                      "opera mini","mobile","palm",
                                                      "portable","opera mobi", "nexus" };

        public static bool IsMobileDevice(string userAgent)
        {
            // TODO: null check
            userAgent = userAgent.ToLower();
            return mobileDevices.Any(x => userAgent.Contains(x));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (IsMobileDevice(Request.UserAgent)) {
            //    Response.Redirect("~/m/");
            //}

            if ((User)Session["user"] == null) {
                return;
            }
            
            //already login. redirecting...
            User user = (User)Session["user"];
            redirectByUserType(user.user_type);
            
        }

        protected void actionLoginButtonClicked(object sender, EventArgs e)
        {
            AuthenticationController authController = new AuthenticationController();
            User user = authController.actionCheckLoginData(ui_username_textbox.Text, ui_password_textbox.Text);
            if (user == null)
            {
                Response.Cookies.Add(new HttpCookie("flash_message", "Username or Password is incorrect.") { Path = "/" });
                Response.Cookies.Add(new HttpCookie("flash_css", "alert alert-error") { Path = "/" });
                //Response.Redirect("~");
                return;
            }

            Session["user"] = null;
            Session["user"] = user;

            redirectByUserType(user.user_type);
                        
        }

        public void redirectByUserType(int user_type) {
            switch (user_type)
            {
                case 1:
                    Response.Redirect("~/DepartmentEmployee/");
                    break;
                case 2:
                    Response.Redirect("~/DepartmentEmployee/");
                    break;
                case 3:
                    Response.Redirect("~/DepartmentHead/");
                    break;
                case 4:
                    Response.Redirect("~/StoreClerk/");
                    break;
                case 5:
                    Response.Redirect("~/StoreSupervisor/");
                    break;
                case 6:
                    Response.Redirect("~/StoreSupervisor/");
                    break;
                default:
                    Response.Redirect("~/Login.aspx");
                    break;
            }
        }

        
        
    }
}