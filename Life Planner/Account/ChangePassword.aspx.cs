using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace Life_Planner.Account
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        validateUser vu = new validateUser();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btn_Change_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //get username
                string username = vu.getUsername(Session["accountID"].ToString());

                //retrieve salt of user
                string salt = vu.RetrieveSalt(username);

                //check for current password
                if (vu.AuthenticatePassword(tb_CurrentPassword.Text, salt))
                {
                    //valid

                    //check for consistant new password
                    if (tb_NewPassword.Text.Trim().Equals(tb_ConfirmPassword.Text.Trim()))
                    {
                        if (vu.updatePasswordByAccID(tb_ConfirmPassword.Text, Session["accountID"].ToString(), salt))
                        {
                            alert_placeholder.Visible = true;
                            alert_placeholder.Attributes["class"] = "alert alert-success alert-dismissable";
                            alertText.Text = "Successfully updated! Redirecting to login page...";
                            FormsAuthentication.SignOut();
                            Session.RemoveAll();
                            Response.AddHeader("REFRESH", "3;URL=Login.aspx");

                        }
                        else
                        {
                            Response.Redirect("~/Error.aspx");
                        }
                    }
                    else
                    {
                        alert_placeholder.Visible = true;
                        alert_placeholder.Attributes["class"] = "alert alert-danger alert-dismissable";
                        alertText.Text = "Inconsistent new password";
                    }
                }
                else
                {
                    alert_placeholder.Visible = true;
                    alert_placeholder.Attributes["class"] = "alert alert-danger alert-dismissable";
                    alertText.Text = "Invalid Password";

                }
            }

            //clear
            tb_CurrentPassword.Text = string.Empty;
            tb_NewPassword.Text = string.Empty;
            tb_ConfirmPassword.Text = string.Empty;
        }
    }
}