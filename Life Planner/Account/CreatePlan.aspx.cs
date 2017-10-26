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

namespace Life_Planner.Account
{
    public partial class CreatePlan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack)
                return;

            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT moduleID, moduleName FROM [CZ2006 - Life Planner].[dbo].[Module];";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();

            con.Open();

            dataAdapter.Fill(dataSet, "Module");

            con.Close();

            //to insert moduleName values retrieved from Module table (from database) into dropdownlist
            ddlCreatePlanChildCurrentEdLevel.DataSource = dataSet.Tables["Module"];
            ddlCreatePlanChildCurrentEdLevel.DataTextField = "moduleName";
            ddlCreatePlanChildCurrentEdLevel.DataBind();
        }

        protected void clearBtn_Click(object sender, EventArgs e)
        {
            txtCreatePlanfName.Text = string.Empty;
            txtCreatePlanlName.Text = string.Empty;
            txtCreatePlanNRIC.Text = string.Empty;
            txtCreatePlanEmail.Text = string.Empty;
            txtCreatePlanDOB.Text = string.Empty;
            radioCreatePlanGender.SelectedIndex = 0;
            ddlCreatePlanChildCurrentEdLevel.SelectedIndex = 0;
            alert_placeholder.Visible = false;
        }

        protected void submitFeedback_Click(object sender, EventArgs e)
        {

            //Response.Write(emailValid);
            //Response.Write(nricValid);
            //Response.Write(ddlCreatePlanChildCurrentEdLevel.Text);

            if (Page.IsValid)
            {
                int emailValid = EmailValid(txtCreatePlanEmail.Text);
                int nricValid = NRICValid(txtCreatePlanNRIC.Text);

                if (emailValid == 1 && nricValid == 1)
                {
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
                    {

                        string sql = "INSERT INTO dbo.Child(NRIC, fName, lName, email, birthdate, gender, currentEdLevel) VALUES (@NRIC, @fName, @lName, @email, @birthdate, @gender, @currentEdLevel);";

                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.Parameters.AddWithValue("@NRIC", txtCreatePlanNRIC.Text);
                        cmd.Parameters.AddWithValue("@fName", txtCreatePlanfName.Text);
                        cmd.Parameters.AddWithValue("@lName", txtCreatePlanlName.Text);
                        cmd.Parameters.AddWithValue("@email", txtCreatePlanEmail.Text);
                        cmd.Parameters.AddWithValue("@birthdate", txtCreatePlanDOB.Text);
                        cmd.Parameters.AddWithValue("@gender", radioCreatePlanGender.Text);
                        cmd.Parameters.AddWithValue("@currentEdLevel", ddlCreatePlanChildCurrentEdLevel.Text);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }            

                    string edLevel = ddlCreatePlanChildCurrentEdLevel.Text;
                    if (edLevel == "Kindergarten")
                    {
                        Response.AddHeader("REFRESH", "3;URL=/Account/CreatePlanFromPrimary.aspx");
                    }


                }

                else if (emailValid==0 && nricValid==1)
                {
                    alert_placeholder.Visible = true;
                    alert_placeholder.Attributes["class"] = "alert alert-danger alert-dismissable";
                    alertText.Text = "Email already exists. Please use an alternative Email.";
                }

                else if (emailValid == 1 && nricValid == 0)
                {
                    alert_placeholder.Visible = true;
                    alert_placeholder.Attributes["class"] = "alert alert-danger alert-dismissable";
                    alertText.Text = "NRIC already exists. Please use an alternative NRIC.";
                }

                else if (emailValid == 0 && nricValid == 0)
                {
                    alert_placeholder.Visible = true;
                    alert_placeholder.Attributes["class"] = "alert alert-danger alert-dismissable";
                    alertText.Text = "Email and NRIC already exists. Please use an alternative Email and NRIC.";
                }
            }
        }

        private static int EmailValid(string email)
        {
            int valid = 0;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.Child WHERE email=@email", con);
                cmd.Parameters.AddWithValue("@email", email);
                con.Open();

                int result = int.Parse(cmd.ExecuteScalar().ToString());

                if (result == 0)
                    valid = 1;
                else
                    valid = 0;

                con.Close();
                con.Dispose();
                cmd.Dispose();
            }
            return valid;
        }

        private static int NRICValid(string nric)
        {
            int valid = 0;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.Child WHERE NRIC=@nric", con);
                cmd.Parameters.AddWithValue("@nric", nric);
                con.Open();

                int result = int.Parse(cmd.ExecuteScalar().ToString());

                if (result == 0)
                    valid = 1;
                else
                    valid = 0;

                con.Close();
                con.Dispose();
                cmd.Dispose();
            }
            return valid;
        }
    }
}