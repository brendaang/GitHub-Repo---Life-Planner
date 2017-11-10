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
    public partial class CreatePlanFromPrimary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            //to uncomment after done
            //if (Session["newChildPlanKindergarten"] != null) //can get newchildnric from Session["newChild"].ToString();
            //{
            DataTable ViewPriSchTable = new DataTable();
            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE school_name LIKE '%PRIMARY SCHOOL%';";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ViewPriSchTable);
            priSchTable.DataSource = ViewPriSchTable;
            priSchTable.DataBind();
            con.Close();
            //}

        }

        protected void btnPriLocation(string area)
        {
            DataTable ViewPriSchTable = new DataTable();
            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE zone_code=@area AND school_name LIKE '%PRIMARY SCHOOL%';";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@area", area);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ViewPriSchTable);
            priSchTable.DataSource = ViewPriSchTable;
            priSchTable.DataBind();
            con.Close();
        }

        protected void btn_PriNorth(object sender, EventArgs e)
        {
            string area = "North";
            btnPriLocation(area);
        }

        protected void btn_PriSouth(object sender, EventArgs e)
        {
            string area = "South";
            btnPriLocation(area);
        }

        protected void btn_PriEast(object sender, EventArgs e)
        {
            string area = "East";
            btnPriLocation(area);
        }

        protected void btn_PriWest(object sender, EventArgs e)
        {
            string area = "West";
            btnPriLocation(area);
        }

        protected void priSchGridView_SelectedIndexChanging(object sender, EventArgs e)
        {
            btnPriCont.Visible = true;
            btnPriSubmit.Visible = true;
            string priSchName = (string)priSchTable.DataKeys[priSchTable.SelectedIndex].Value;
            Session["priSchName"] = priSchName;
           
        }


       

        protected void btnPriContinuePlanning(object sender, EventArgs e)
        {
            Response.Redirect("CreatePlanFromSecondary.aspx");
        }

        protected void btnPriSubmitPlan(object sender, EventArgs e)
        {
            int priSchID;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
            {
                string sql2 = "SELECT id FROM Schools WHERE school_name=@schoolname;";
                SqlCommand cmd2 = new SqlCommand(sql2,con);
                cmd2.Parameters.AddWithValue("@schoolname", Session["priSchName"].ToString());

                con.Open();
                priSchID = (int)cmd2.ExecuteScalar();
                con.Close();
            }

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
            {     
                string sql = "INSERT INTO dbo.PathPlan(NRIC, priSchID, accountID) VALUES (@NRIC, @priSchID, @accountID);";
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@NRIC", Session["newChild"].ToString());
                cmd.Parameters.AddWithValue("@priSchID", priSchID);
                cmd.Parameters.AddWithValue("@accountID", Session["accountID"].ToString());

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            //redirect to view plan
            Response.AddHeader("REFRESH", "3;URL=/Account/ViewOwnPlan.aspx");
        }

        protected void btn_PriNone(object sender, EventArgs e)
        {
            DataTable ViewPriSchTable = new DataTable();
            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT school,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE school_name LIKE '%PRIMARY SCHOOL%';";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ViewPriSchTable);
            priSchTable.DataSource = ViewPriSchTable;
            priSchTable.DataBind();
            con.Close();
        }
    }
}