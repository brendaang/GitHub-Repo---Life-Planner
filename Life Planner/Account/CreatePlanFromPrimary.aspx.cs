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
            //if (Session["newChildPlanKindergarten"] != null) //can get newchildnric from Session["newChildPlanKindergarten"].ToString();
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

        protected void btn_PriNorth(object sender, EventArgs e)
        {
            string area = "North";
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

        protected void btn_PriSouth(object sender, EventArgs e)
        {
            string area = "South";
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

        protected void btn_PriEast(object sender, EventArgs e)
        {
            string area = "East";
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

        protected void btn_PriWest(object sender, EventArgs e)
        {
            string area = "West";
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

        protected void priSchGridView_SelectedIndexChanging(object sender, EventArgs e)
        {

            string priSchName = (string)priSchTable.DataKeys[priSchTable.SelectedIndex].Value;
            Session["priSchName"] = priSchName;
            Response.Redirect("test2.aspx"); //
            //string threadID = intThreadID.ToString();
            //  lblThreadID.Text = threadID;
            //int index = Convert.ToInt16(priSchTable.SelectedDataKey.Value);
        }
    }
}