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
    public partial class CreatePlanFromSecondary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            //to uncomment after done
            //if (Session["newChildPlanPrimary"] != null) //can get newchildnric from Session["newChildPlanKindergarten"].ToString();
            {
                DataTable ViewSecSchTable = new DataTable();
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE school_name LIKE '%SECONDARY SCHOOL%';";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ViewSecSchTable);
                secSchTable.DataSource = ViewSecSchTable;
                secSchTable.DataBind();
                con.Close();
            }
        }

        protected void btnSecLocation(string area)
        {
            DataTable ViewSecSchTable = new DataTable();
            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE zone_code=@area AND school_name LIKE '%SECONDARY SCHOOL%';";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@area", area);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ViewSecSchTable);
            secSchTable.DataSource = ViewSecSchTable;
            secSchTable.DataBind();
            con.Close();
        }

        protected void btn_SecNorth(object sender, EventArgs e)
        {
            string area = "North";
            btnSecLocation(area);
        }

        protected void btn_SecSouth(object sender, EventArgs e)
        {
            string area = "South";
            btnSecLocation(area);
        }

        protected void btn_SecEast(object sender, EventArgs e)
        {
            string area = "East";
            btnSecLocation(area);
        }

        protected void btn_SecWest(object sender, EventArgs e)
        {
            string area = "West";
            btnSecLocation(area);
        }

        protected void secSchGridView_SelectedIndexChanging(object sender, EventArgs e)
        {
            btnSecCont.Visible = true;
            btnSecSubmit.Visible = true;
            string secSchName = (string)secSchTable.DataKeys[secSchTable.SelectedIndex].Value;
            Session["secSchName"] = secSchName;
        }

        protected void btn_SecNone(object sender, EventArgs e)
        {
            DataTable ViewSecSchTable = new DataTable();
            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE school_name LIKE '%SECONDARY SCHOOL%';";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ViewSecSchTable);
            secSchTable.DataSource = ViewSecSchTable;
            secSchTable.DataBind();
            con.Close();
        }

        protected void btnSecContinuePlanning(object sender, EventArgs e)
        {
            Response.Redirect("CreatePlanFromITEJCPOLY.aspx");
        }

        protected void btnSecSubmitPlan(object sender, EventArgs e)
        {
            int priSchID, secSchID;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
            {
                string sql2 = "SELECT id FROM Schools WHERE school_name=@schoolname;";
                SqlCommand cmd2 = new SqlCommand(sql2, con);
                cmd2.Parameters.AddWithValue("@schoolname", Session["priSchName"].ToString());

                con.Open();
                priSchID = (int)cmd2.ExecuteScalar();
                con.Close();
            }

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
            {
                string sql3 = "SELECT id FROM Schools WHERE school_name=@schoolname;";
                SqlCommand cmd3 = new SqlCommand(sql3, con);
                cmd3.Parameters.AddWithValue("@schoolname", Session["secSchName"].ToString());

                con.Open();
                secSchID = (int)cmd3.ExecuteScalar();
                con.Close();
            }

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
            {
                string sql = "INSERT INTO dbo.PathPlan(NRIC, priSchID, secSchID, accountID) VALUES (@NRIC, @priSchID, @secSchID, @accountID);";
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@NRIC", Session["newChild"].ToString());
                cmd.Parameters.AddWithValue("@secSchID", secSchID);
                cmd.Parameters.AddWithValue("@priSchID", priSchID);
                cmd.Parameters.AddWithValue("@accountID", Session["accountID"].ToString());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            //redirect to view plan
            Response.AddHeader("REFRESH", "3;URL=/Account/ChangePassword.aspx");
        }

        
    }
}