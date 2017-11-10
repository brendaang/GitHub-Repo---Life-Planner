﻿using System;
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
    public partial class ResolveFeedback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string feedbackID = (string)(Session["Feedback_ID"]);
            string feedbackStatus = (string)(Session["Feedback_Status"]);

            if (feedbackStatus.Equals("Open"))
            {
                SqlConnection con = new DBManager().getConnection();
                String acct_Name = Session["username"].ToString();

                string sql2 = "SELECT feedbackDatetime, feedbackIssue, feedbackContent, submittedBy, feedbackStatus FROM dbo.Feedback WHERE @feedbackID = feedbackID";
                SqlCommand cmd2 = new SqlCommand(sql2, con);

                cmd2.Parameters.AddWithValue("@feedbackID", feedbackID);
                con.Open();
                SqlDataReader reader = cmd2.ExecuteReader();
                reader.Read();
                fbkDatetime.Text = reader["feedbackDatetime"].ToString();
                txtfeedbackIssue.Text = reader["feedbackIssue"].ToString();
                fbkContent.Text = reader["feedbackContent"].ToString();
                resolvedBy.Text = new CommonMethodsForFeedback().getNameOpen(acct_Name);

                string today = DateTime.Now.ToString();
                resolvedOn.Text = today;

                reader.Close();
                con.Close();
            }
            if ((feedbackStatus.Equals("Pending") && (!IsPostBack)) || (feedbackStatus.Equals("Resolved") && (!IsPostBack)))
            {
                string feedback_ID = (string)(Session["Feedback_ID"]);
                SqlConnection con1 = new DBManager().getConnection();

                string sql1 = "SELECT feedbackDatetime, feedbackIssue, feedbackContent, submittedBy, feedbackStatus FROM dbo.Feedback WHERE feedbackID = @feedbackID";
                string sql2 = "SELECT resolvedNotes, resolvedOn FROM dbo.ResolveFeedback WHERE feedbackID = @feedbackID";
                SqlCommand cmd1 = new SqlCommand(sql1, con1);
                SqlCommand cmd2 = new SqlCommand(sql2, con1);

                cmd1.Parameters.AddWithValue("@feedbackID", feedbackID);
                cmd2.Parameters.AddWithValue("@feedbackID", feedbackID);
                cmd2.Parameters.AddWithValue("@feedbackStatus", feedbackStatus);
                con1.Open();
                SqlDataReader reader = cmd1.ExecuteReader();
                reader.Read();
                fbkDatetime.Text = reader["feedbackDatetime"].ToString();
                txtfeedbackIssue.Text = reader["feedbackIssue"].ToString();
                fbkContent.Text = reader["feedbackContent"].ToString();
                reader.Close();

                SqlDataReader reader2 = cmd2.ExecuteReader();
                reader2.Read();
                txtAddnotes.Text = reader2["resolvedNotes"].ToString();
                resolvedBy.Text = new CommonMethodsForFeedback().getName(feedback_ID);
                resolvedOn.Text = reader2["resolvedOn"].ToString();
                reader2.Close();
                con1.Close();
            }
        }

        protected void cancelBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewFeedback.aspx");
        }


        protected void resolvedBtn_Click(object sender, EventArgs e)
        {
            string feedbackStatus = (string)(Session["Feedback_Status"]);
            
            String acctName = Session["username"].ToString();

            SqlConnection con = new DBManager().getConnection();

            if (feedbackStatus.Equals("Open"))
            {
                String resolvedNotes = txtAddnotes.Text;
                string feedback_ID = (string)(Session["Feedback_ID"]);

                string sql0 = "SET IDENTITY_INSERT ID ON";
                string sql3 = "SET XACT_ABORT ON";
                string sql4 = "BEGIN TRANSACTION";
                string sql1 = "INSERT INTO [CZ2006 - Life Planner].[dbo].[ResolveFeedback] (feedbackID, resolvedBy, resolvedOn, feedbackStatus, resolvedNotes) VALUES (@feedbackID, @resolvedBy, @resolvedOn, @feedbackStatus, @resolvedNotes);";
                string sql2 = "UPDATE dbo.Feedback SET feedbackStatus = @feedbackStatus  WHERE feedbackID = @feedbackID";
                string sql5 = "COMMIT TRANSACTION";

                SqlCommand cmd0 = new SqlCommand(sql0, con);
                SqlCommand cmd3 = new SqlCommand(sql3, con);
                SqlCommand cmd4 = new SqlCommand(sql4, con);
                SqlCommand cmd1 = new SqlCommand(sql1, con);
                SqlCommand cmd2 = new SqlCommand(sql2, con);
                SqlCommand cmd5 = new SqlCommand(sql5, con);

                cmd1.Parameters.AddWithValue("@feedbackID", feedback_ID);
                cmd1.Parameters.AddWithValue("@resolvedBy", new CommonMethodsForFeedback().getAccID(acctName));
                cmd1.Parameters.AddWithValue("@resolvedOn", DateTime.Now);
                cmd1.Parameters.AddWithValue("@feedbackStatus", DropDownListStatus.SelectedValue);
                cmd1.Parameters.AddWithValue("@resolvedNotes", resolvedNotes);
                cmd2.Parameters.AddWithValue("@feedbackID", feedback_ID);
                cmd2.Parameters.AddWithValue("@feedbackStatus", DropDownListStatus.SelectedValue);

                con.Open();
                cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                con.Close();
                cmd1.Dispose();
                cmd2.Dispose();
            }
            if ((feedbackStatus.Equals("Pending")) || (feedbackStatus.Equals("Resolved")))
            {
                string feedback_ID = (string)(Session["Feedback_ID"]);
                string sql0 = "SET IDENTITY_INSERT ID ON";
                string sql3 = "SET XACT_ABORT ON";
                string sql4 = "BEGIN TRANSACTION";
                string sql1 = "UPDATE dbo.ResolveFeedback SET resolvedBy = @resolvedBy, resolvedOn = @resolvedOn, feedbackStatus = @feedbackStatus, resolvedNotes = @resolvedNotes WHERE feedbackID = @feedbackID";
                string sql2 = "UPDATE dbo.Feedback SET feedbackStatus = @feedbackStatus  WHERE feedbackID = @feedbackID";
                string sql5 = "COMMIT TRANSACTION";

                SqlCommand cmd0 = new SqlCommand(sql0, con);
                SqlCommand cmd3 = new SqlCommand(sql3, con);
                SqlCommand cmd4 = new SqlCommand(sql4, con);
                SqlCommand cmd1 = new SqlCommand(sql1, con);
                SqlCommand cmd2 = new SqlCommand(sql2, con);
                SqlCommand cmd5 = new SqlCommand(sql5, con);

                cmd1.Parameters.AddWithValue("@feedbackID", feedback_ID);
                cmd1.Parameters.AddWithValue("@resolvedBy", new CommonMethodsForFeedback().getAccID(acctName));
                cmd1.Parameters.AddWithValue("@resolvedOn", DateTime.Now);
                cmd1.Parameters.AddWithValue("@feedbackStatus", DropDownListStatus.SelectedValue);
                cmd1.Parameters.AddWithValue("@resolvedNotes", txtAddnotes.Text);
                cmd2.Parameters.AddWithValue("@feedbackID", feedback_ID);
                cmd2.Parameters.AddWithValue("@feedbackStatus", DropDownListStatus.SelectedValue);

                con.Open();
                cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                resolvedBy.Text = new CommonMethodsForFeedback().getName(feedback_ID);
                string todayResolved = DateTime.Now.ToString();
                resolvedOn.Text = todayResolved;
                con.Close();
            }
            alert_placeholder.Visible = true;
            alert_placeholder.Attributes["class"] = "alert alert-success alert-dismissable";
            resolveACK.Text = "Your resolved to this feedback has been saved.";
        }
    }
}