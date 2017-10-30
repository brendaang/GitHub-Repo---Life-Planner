using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Life_Planner.Account
{
    public partial class ChangeRole : System.Web.UI.Page
    {
        validateUser vu = new validateUser();
        string[] userinfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            tb_accId.Text = (string)Session["uia"];
            userinfo = vu.getUserInfoByID(tb_accId.Text);
            
            tb_fname.Text = userinfo[0];
            tb_lname.Text = userinfo[1];
            tb_role.Text = userinfo[2];
            if (userinfo[2] == "0")
            {
                tb_role.Text = "User";
            }
            else
            {
                tb_role.Text = "Admin";
            }

        }

        protected void btn_ChangeRole_Click(object sender, EventArgs e)
        {
            //change role
            vu.changeRoleByID(tb_accId.Text);
            Response.Redirect(Request.RawUrl);
        }

    }
}