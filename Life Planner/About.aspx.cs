using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Life_Planner
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if((string)Session["role"] == "User")
            {
                Response.Redirect("~/Account/ViewOwnPlan");
            }
            else if ((string)Session["role"] == "Admin")
            {
                Response.Redirect("~/Account/SetAdmin.aspx");
            }
            else
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }
    }
}