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
    public partial class CreatePlanFromJCPOLY : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
        }

        protected void radioSelectPolyJC_SelectedIndexChanged(object sender, EventArgs e)
        {
            // to load secsch gridview with sec schools after setting it to visible
            //*************to store priSchName into db

            if (radioSelectPolyJC.SelectedItem.Text == "Junior College")
            {
                DataTable ViewJCSchTable = new DataTable();
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE school_name LIKE '%JUNIOR COLLEGE%';";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ViewJCSchTable);
                JCPOLYTable.DataSource = ViewJCSchTable;
                JCPOLYTable.DataBind();
                con.Close();
            }

            else if (radioSelectPolyJC.SelectedItem.Text == "Polytechnic")
            {
                DataTable ViewPolySchTable = new DataTable();
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE school_name LIKE '%POLYTECHNIC%';";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ViewPolySchTable);
                JCPOLYTable.DataSource = ViewPolySchTable;
                JCPOLYTable.DataBind();
                con.Close();
            }
        }

        protected void btnJCPOLYLocation(string area)
        {
            if (radioSelectPolyJC.SelectedItem.Text == "Junior College")
            {
                DataTable ViewJCPOLYTable = new DataTable();
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE zone_code=@area AND school_name LIKE '%JUNIOR COLLEGE%';";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@area", area);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ViewJCPOLYTable);
                JCPOLYTable.DataSource = ViewJCPOLYTable;
                JCPOLYTable.DataBind();
                con.Close();
            }

            else if (radioSelectPolyJC.SelectedItem.Text == "Polytechnic")
            {
                DataTable ViewJCPOLYTable = new DataTable();
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE zone_code=@area AND school_name LIKE '%POLYTECHNIC%';";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@area", area);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ViewJCPOLYTable);
                JCPOLYTable.DataSource = ViewJCPOLYTable;
                JCPOLYTable.DataBind();
                con.Close();
            }

        }

        protected void btn_JCPOLYNorth(object sender, EventArgs e)
        {
            string area = "North";
            btnJCPOLYLocation(area);
        }

        protected void btn_JCPOLYSouth(object sender, EventArgs e)
        {
            string area = "South";
            btnJCPOLYLocation(area);
        }

        protected void btn_JCPOLYEast(object sender, EventArgs e)
        {
            string area = "East";
            btnJCPOLYLocation(area);
        }

        protected void btn_JCPOLYWest(object sender, EventArgs e)
        {
            string area = "West";
            btnJCPOLYLocation(area);
        }

        protected void JCPOLYGridView_SelectedIndexChanging(object sender, EventArgs e)
        {

            string JCPOLYName = (string)JCPOLYTable.DataKeys[JCPOLYTable.SelectedIndex].Value;
            Session["JCPOLYName"] = JCPOLYName;
        }

        protected void btnJCPOLYSubmitPlan(object sender, EventArgs e)
        {
            
        }

        protected void btnJCPOLYContinuePlanning(object sender, EventArgs e)
        {
            //Response.Redirect("CreatePlanFromUniversity.aspx");
        }

        protected void btn_JCPOLYNone(object sender, EventArgs e)
        {

        }
    }
}