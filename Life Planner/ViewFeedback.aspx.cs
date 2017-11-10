using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Life_Planner.Data;

namespace Life_Planner
{
    public partial class ViewFeedback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void feedbackGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            feedbackGridView.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void NumRecordLoaded_SelectedIndexChanged(object sender, EventArgs e)
        {
            feedbackGridView.PageSize = Convert.ToInt32(NumRecordLoaded.SelectedValue);
            BindGrid();
        }

        private void BindGrid()
        {
            DataTable ViewAllFeedbacks = new DataTable();
            SqlConnection con = new DBManager().getConnection();

            string sql = "SELECT t.[feedbackID],t.[feedbackDatetime],t.[feedbackIssue],t.[feedbackContent], t.[submittedBy], t.[feedbackStatus] FROM [CZ2006 - Life Planner].[dbo].[Feedback] t";
            string sql1 = "SELECT COUNT ([feedbackID]) FROM [CZ2006 - Life Planner].[dbo].[Feedback]";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlCommand cmd1 = new SqlCommand(sql1, con);

            con.Open();
            int FbkCount = (int)cmd1.ExecuteScalar();
            countFeedbacks.Text = FbkCount.ToString();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ViewAllFeedbacks);
            feedbackGridView.DataSource = ViewAllFeedbacks;
            feedbackGridView.DataBind();

            con.Close();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            searchFunction();
        }
        protected void searchFunction()
        {
            DataTable ViewAllFeedbacks = new DataTable();
            SqlConnection con = new DBManager().getConnection();

            string sql = "SELECT t.[feedbackID],t.[feedbackDatetime],t.[feedbackIssue],t.[feedbackContent], t.[submittedBy], t.[feedbackStatus] FROM [CZ2006 - Life Planner].[dbo].[Feedback] t WHERE t.[feedbackIssue] LIKE '%' + @pSearch + '%';";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@pSearch", SqlDbType.NVarChar).Value = fbkSearch.Text;
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ViewAllFeedbacks);
            feedbackGridView.DataSource = ViewAllFeedbacks;
            feedbackGridView.DataBind();
            con.Close();
        }

        protected void feedbackGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "viewFbkDetail")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument) % feedbackGridView.PageSize;
                GridViewRow row = feedbackGridView.Rows[rowIndex];

                string feedbackID = row.Cells[0].Text;
                string feedbackStts = row.Cells[1].Text;

                Session["Feedback_ID"] = feedbackID;
                Session["Feedback_Status"] = feedbackStts;
                Response.Redirect("ResolveFeedback.aspx", false);
            }
        }

        //private void BindStatusList(DropDownList fbkStatusID)
        //{
        //    SqlConnection con = new DBManager().getConnection();
        //    string sql = "SELECT feedbackStatus FROM dbo.Feedback WHERE feedbackStatus =  @feedbackStatus";
        //    SqlCommand cmd = new SqlCommand(sql, con);

        //    cmd.Parameters.Add("@feedbackStatus", fbkStatusID.SelectedValue.ToString());
        //    con.Open();
        //    fbkStatusID.DataSource = cmd.ExecuteReader();
        //    fbkStatusID.DataTextField = "feedbackStatus";
        //    fbkStatusID.DataValueField = "feedbackStatus";
        //    fbkStatusID.DataBind();
        //    con.Close();
        //    fbkStatusID.Items.FindByValue(ViewState["test"].ToString()).Selected = true;
        //}

        //protected void fbkStatus_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DropDownList fbkStatusID = (DropDownList)sender;
        //    ViewState["test"] = fbkStatusID.SelectedValue;
        //    this.BindGrid();
        //}
    }
}