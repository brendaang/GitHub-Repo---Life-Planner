using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Life_Planner.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace Life_Planner.Account
{
    public partial class Login : Page
    {
        validateUser vu = new validateUser();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string salt = vu.RetrieveSalt(tb_username.Text);
                if (salt.Equals("-1"))
                {
                    alert_placeholder.Visible = true;
                    alert_placeholder.Attributes["class"] = "alert alert-danger alert-dismissable";
                    alertText.Text = "Username or password is incorrect.";
                }
                else
                {
                    if (vu.AuthenticateUser(tb_username.Text, tb_password.Text, salt))
                    {
                        // Perform a redirect to Home page
                        Session["username"] = tb_username.Text;
                        Session["accountID"] = vu.GetAccountID(tb_username.Text, tb_password.Text, salt);
                        FormsAuthentication.RedirectFromLoginPage(tb_username.Text, true);
                        //Response.Redirect("~404.aspx");
                        //Response.Redirect("~/ViewFeedback.aspx");

                        //alert_placeholder.Visible = true;
                        //alert_placeholder.Attributes["class"] = "alert alert-success alert-dismissable";
                        //alertText.Text = "Login successful! Account ID is " + Session["accountID"].ToString();
                    }
                    else
                    {
                        alert_placeholder.Visible = true;
                        alert_placeholder.Attributes["class"] = "alert alert-danger alert-dismissable";
                        alertText.Text = "Username or password is incorrect.";
                    }
                }
            }
        }

        protected void btn_clear_Click(object sender, EventArgs e)
        {
            tb_username.Text = string.Empty;
            tb_password.Text = string.Empty;
        }
    }
}
    