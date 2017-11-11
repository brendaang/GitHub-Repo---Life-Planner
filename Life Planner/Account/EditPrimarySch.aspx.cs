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
    public partial class EditPrimarySch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            
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
            btn_updatePrimary.Visible = true;
            string priSchName = (string)priSchTable.DataKeys[priSchTable.SelectedIndex].Value;
            Session["priSchName"] = priSchName;
        }

        protected void btn_updatePrimary_Click(object sender, EventArgs e)
        {
            EditPlanDAO ep = new EditPlanDAO();
            string schID = ep.getSchID(Session["priSchName"].ToString());
            SqlConnection con = new DBManager().getConnection();
            string sql = "UPDATE dbo.PathPlan SET priSchID=@priSchID WHERE accountID=@accountID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@priSchID", schID);
            cmd.Parameters.AddWithValue("@accountID", Session["accountID"].ToString());
            con.Open();
            int count;
            count = Convert.ToInt32(cmd.ExecuteScalar());
            // updated: count == 0
            if (count == 0)
            {
                alert_placeholder.Visible = true;
                alert_placeholder.Attributes["class"] = "alert alert-success alert-dismissable";
                alertText.Text = "Successfully updated! Redirecting to View Own Plan page...";
                Response.AddHeader("REFRESH", "1;URL=ViewOwnPlan.aspx");
                con.Close();
                con.Dispose();
            }
            else
            {
                con.Close();
                con.Dispose();
                Response.Redirect("Error.aspx");

            }

            //Response.Redirect("ViewOwnPlan.aspx");
        }

    }
}