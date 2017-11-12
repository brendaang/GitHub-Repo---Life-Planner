using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

namespace Life_Planner.Account
{
    public partial class SetAdmin : System.Web.UI.Page
    {
        validateUser vu = new validateUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["role"].ToString() != "Admin")
            {
                Response.Redirect("~/Error.aspx");
            }
        }

        protected void btn_Change_Click(object sender, EventArgs e)
        {
            Session["tb_SBUsername"] = tb_SBUsername.Text;
            if (vu.AuthenticateUsername(tb_SBUsername.Text))
            {
                //valid username
                //get and show user info

                string accID = vu.getAccountIDByUsername(tb_SBUsername.Text);
                Session["uia"] = accID;

                resetField();
                Response.Redirect("~/Account/ChangeRole.aspx");
            }
            else
            {
                alert_placeholder.Visible = true;
                alert_placeholder.Attributes["class"] = "alert alert-danger alert-dismissable";
                alertText.Text = "Invalid Username.";
            }
        }

        public void resetField()
        {
            tb_SBUsername.Text = "";
        }
    }
}