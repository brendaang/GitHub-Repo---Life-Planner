using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Life_Planner.Account
{
    public partial class ViewOwnPlan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if no plan created, inform user they have no plan and redirect them to create plan page
        }

        protected void btn_editPlan_Click(object sender, EventArgs e)
        {
            //redirect to edit plan page (not created) or just code here
        }

        protected void btn_deletePlan_Click(object sender, EventArgs e)
        {
            //delete plan logic here
        }
    }
}