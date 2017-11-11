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
    public partial class EditSecSch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
           
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
            //}
        }

        protected void btn_SecNorth(object sender, EventArgs e)
        {
            string area = "North";
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

        protected void btn_SecSouth(object sender, EventArgs e)
        {
            string area = "South";
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

        protected void btn_SecEast(object sender, EventArgs e)
        {
            string area = "East";
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

        protected void btn_SecWest(object sender, EventArgs e)
        {
            string area = "West";
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

        protected void secSchGridView_SelectedIndexChanging(object sender, EventArgs e)
        {
            btn_updateSecondary.Visible = true;
            string secSchName = (string)secSchTable.DataKeys[secSchTable.SelectedIndex].Value;
            Session["secSchName"] = secSchName;
        }

        protected void btn_updateSecondary_Click(object sender, EventArgs e)
        {
            EditPlanDAO ep = new EditPlanDAO();
            string schID = ep.getSchID(Session["secSchName"].ToString());
            SqlConnection con = new DBManager().getConnection();
            string sql = "UPDATE dbo.PathPlan SET secSchID=@secSchID WHERE accountID=@accountID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@secSchID", schID);
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
        }

    }
}