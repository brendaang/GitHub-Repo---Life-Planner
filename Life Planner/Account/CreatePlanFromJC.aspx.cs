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
    public partial class CreatePlanFromJC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            //to uncomment after done
            //if (Session["newChildPlanSecondary"] != null) //can get newchildnric from Session["newChildPlanKindergarten"].ToString();
            //{
            DataTable ViewSecSchTable = new DataTable();
            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE school_name LIKE '%JUNIOR COLLEGE%';";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ViewSecSchTable);
            JCTable.DataSource = ViewSecSchTable;
            JCTable.DataBind();
            con.Close();
            //}
        }

        protected void btn_JCNorth(object sender, EventArgs e)
        {
            string area = "North";
            DataTable ViewJCTable = new DataTable();
            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE zone_code=@area AND school_name LIKE '%JUNIOR COLLEGE%';";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@area", area);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ViewJCTable);
            JCTable.DataSource = ViewJCTable;
            JCTable.DataBind();
            con.Close();
        }

        protected void btn_JCSouth(object sender, EventArgs e)
        {
            string area = "South";
            DataTable ViewJCTable = new DataTable();
            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE zone_code=@area AND school_name LIKE '%JUNIOR COLLEGE%';";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@area", area);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ViewJCTable);
            JCTable.DataSource = ViewJCTable;
            JCTable.DataBind();
            con.Close();
        }

        protected void btn_JCEast(object sender, EventArgs e)
        {
            string area = "East";
            DataTable ViewJCTable = new DataTable();
            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE zone_code=@area AND school_name LIKE '%JUNIOR COLLEGE%';";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@area", area);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ViewJCTable);
            JCTable.DataSource = ViewJCTable;
            JCTable.DataBind();
            con.Close();
        }

        protected void btn_JCWest(object sender, EventArgs e)
        {
            string area = "West";
            DataTable ViewJCTable = new DataTable();
            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE zone_code=@area AND school_name LIKE '%JUNIOR COLLEGE%';";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@area", area);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ViewJCTable);
            JCTable.DataSource = ViewJCTable;
            JCTable.DataBind();
            con.Close();
        }

        protected void JCGridView_SelectedIndexChanging(object sender, EventArgs e)
        {

            string JCName = (string)JCTable.DataKeys[JCTable.SelectedIndex].Value;
            Session["JCName"] = JCName;
           // Response.Redirect("test2.aspx"); //
            //string threadID = intThreadID.ToString();
            //  lblThreadID.Text = threadID;
            //int index = Convert.ToInt16(priSchTable.SelectedDataKey.Value);
        }
    }
}