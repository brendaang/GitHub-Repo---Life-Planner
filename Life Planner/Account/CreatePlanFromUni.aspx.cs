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
    public partial class CreatePlanFromUni : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            //to uncomment after done
            //if (Session["newChildPlanPrimary"] != null) //can get newchildnric from Session["newChildPlanKindergarten"].ToString();
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
            btnUniSubmit.Visible = true;
            string uniSchName = (string)uniTable.DataKeys[uniTable.SelectedIndex].Value;
            Session["uniName"] = uniSchName;
        }

        protected void btnUniSubmitPlan(object sender, EventArgs e)
        {
            string priSchID="", secSchID="", jcID="", iteID="", polyID="", uniID="";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
            {
                string sql2 = "SELECT id FROM Schools WHERE school_name=@schoolname;";
                SqlCommand cmd2 = new SqlCommand(sql2, con);
                cmd2.Parameters.AddWithValue("@schoolname", Session["priSchName"].ToString());

                con.Open();
                priSchID = cmd2.ExecuteScalar().ToString();
                con.Close();
            }

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
            {
                string sql3 = "SELECT id FROM Schools WHERE school_name=@schoolname;";
                SqlCommand cmd3 = new SqlCommand(sql3, con);
                cmd3.Parameters.AddWithValue("@schoolname", Session["secSchName"].ToString());

                con.Open();
                secSchID = cmd3.ExecuteScalar().ToString();
                con.Close();
            }


            if (Session["JCName"] != null)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
                {
                    string sql4 = "SELECT id FROM Schools WHERE school_name=@schoolname;";
                    SqlCommand cmd4 = new SqlCommand(sql4, con);
                    cmd4.Parameters.AddWithValue("@schoolname", Session["JCName"]);

                    con.Open();
                    jcID = cmd4.ExecuteScalar().ToString();
                    con.Close();
                }
            }
            else if (Session["JCName"] == null)
                jcID = "";

            if (Session["POLYName"] != null)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
                {
                    string sql5 = "SELECT id FROM Schools WHERE school_name=@schoolname;";
                    SqlCommand cmd5 = new SqlCommand(sql5, con);
                    cmd5.Parameters.AddWithValue("@schoolname", Session["POLYName"].ToString());

                    con.Open();
                    polyID = cmd5.ExecuteScalar().ToString();
                    con.Close();
                }
            }

            else if (Session["POLYName"] == null)
                polyID = "";


            if (Session["ITEName"] != null)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
                {
                    string sql6 = "SELECT id FROM Schools WHERE school_name=@schoolname;";
                    SqlCommand cmd6 = new SqlCommand(sql6, con);
                    cmd6.Parameters.AddWithValue("@schoolname", Session["ITEName"].ToString());

                    con.Open();
                    iteID = cmd6.ExecuteScalar().ToString();
                    con.Close();
                }
            }

            else if (Session["ITEName"] == null)
            {
                iteID = "";
            }


            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
            {
                string sql7 = "SELECT id FROM Schools WHERE school_name=@schoolname;";
                SqlCommand cmd7 = new SqlCommand(sql7, con);
                cmd7.Parameters.AddWithValue("@schoolname", Session["uniName"].ToString());

                con.Open();
                uniID = cmd7.ExecuteScalar().ToString();
                con.Close();
            }

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
            {
                string sql = "INSERT INTO dbo.PathPlan(NRIC, priSchID, secSchID, jcID, polyID, ITEID, uniID, accountID) VALUES (@NRIC, @priSchID, @secSchID, @jcID, @polyID, @ITEID, @uniID, @accountID);";
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@NRIC", Session["newChild"].ToString());
                cmd.Parameters.AddWithValue("@secSchID", secSchID == "" ? "" : secSchID);
                cmd.Parameters.AddWithValue("@priSchID", priSchID == "" ? "" : priSchID);
                cmd.Parameters.AddWithValue("@jcID", jcID == "" ? "" : jcID);
                cmd.Parameters.AddWithValue("@polyID", polyID == "" ? "" : polyID);
                cmd.Parameters.AddWithValue("@ITEID", iteID == ""? "":iteID);
                cmd.Parameters.AddWithValue("@uniID", uniID);

                cmd.Parameters.AddWithValue("@accountID", Session["accountID"].ToString());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            //redirect to view plan
            Response.Redirect("~/Account/ViewOwnPlan.aspx");
        }
    }
}