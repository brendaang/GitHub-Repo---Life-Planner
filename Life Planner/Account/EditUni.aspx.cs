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
    public partial class EditUni : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            
            {
                DataTable ViewUniTable = new DataTable();
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE school_name LIKE '%UNIVERSITY%';";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ViewUniTable);
                uniTable.DataSource = ViewUniTable;
                uniTable.DataBind();
                con.Close();
            }
        }

        protected void btnUniLocation(string area)
        {
            DataTable ViewUniTable = new DataTable();
            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE zone_code=@area AND school_name LIKE '%UNIVERSITY%';";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@area", area);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ViewUniTable);
            uniTable.DataSource = ViewUniTable;
            uniTable.DataBind();
            con.Close();
        }

        protected void btn_UniNorth(object sender, EventArgs e)
        {
            string area = "North";
            btnUniLocation(area);
        }

        protected void btn_UniSouth(object sender, EventArgs e)
        {
            string area = "South";
            btnUniLocation(area);
        }

        protected void btn_UniEast(object sender, EventArgs e)
        {
            string area = "East";
            btnUniLocation(area);
        }

        protected void btn_UniWest(object sender, EventArgs e)
        {
            string area = "West";
            btnUniLocation(area);
        }

        protected void btn_UniNone(object sender, EventArgs e)
        {
            DataTable ViewUniTable = new DataTable();
            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE school_name LIKE '%UNIVERSITY%';";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ViewUniTable);
            uniTable.DataSource = ViewUniTable;
            uniTable.DataBind();
            con.Close();
        }

        protected void uniGridView_SelectedIndexChanging(object sender, EventArgs e)
        {
            btn_updateUni.Visible = true;
            string uniSchName = (string)uniTable.DataKeys[uniTable.SelectedIndex].Value;
            Session["uniName"] = uniSchName;
        }

        protected void btn_updateUni_Click(object sender, EventArgs e)
        {
            EditPlanDAO ep = new EditPlanDAO();
            SqlConnection con = new DBManager().getConnection();
            int count;
            string id = ep.getSchID(Session["uniName"].ToString());
            string sql = "UPDATE dbo.PathPlan SET uniID=@uniID WHERE accountID=@accountID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@uniID", id);
            cmd.Parameters.AddWithValue("@accountID", Session["accountID"].ToString());
            con.Open();
            count = Convert.ToInt32(cmd.ExecuteScalar());
            //update: count == 0
            if (count == 0)
            {
                alert_placeholder.Visible = true;
                alert_placeholder.Attributes["class"] = "alert alert-success alert-dismissable";
                alertText.Text = "Successfully updated! Redirecting to View Own Plan page...";
                Response.AddHeader("REFRESH", "2;URL=ViewOwnPlan.aspx");
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