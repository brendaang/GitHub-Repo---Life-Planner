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
    public partial class CreatePlanFromJCPOLY1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            DataTable ViewPolySchTable = new DataTable();
            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE school_name LIKE '%POLYTECHNIC%';";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ViewPolySchTable);
            POLYTable.DataSource = ViewPolySchTable;
            POLYTable.DataBind();
            con.Close();
        }

        protected void btnPOLYLocation(string area)
        {
            DataTable ViewPOLYTable = new DataTable();
            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE zone_code=@area AND school_name LIKE '%POLYTECHNIC%';";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@area", area);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ViewPOLYTable);
            POLYTable.DataSource = ViewPOLYTable;
            POLYTable.DataBind();
            con.Close();
        }

        protected void btn_POLYNone(object sender, EventArgs e)
        {
            DataTable ViewPolySchTable = new DataTable();
            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE school_name LIKE '%POLYTECHNIC%';";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ViewPolySchTable);
            POLYTable.DataSource = ViewPolySchTable;
            POLYTable.DataBind();
            con.Close();
        }

        protected void btn_POLYNorth(object sender, EventArgs e)
        {
            string area = "North";
            btnPOLYLocation(area);
        }

        protected void btn_POLYSouth(object sender, EventArgs e)
        {
            string area = "South";
            btnPOLYLocation(area);
        }

        protected void btn_POLYEast(object sender, EventArgs e)
        {
            string area = "East";
            btnPOLYLocation(area);
        }

        protected void btn_POLYWest(object sender, EventArgs e)
        {
            string area = "West";
            btnPOLYLocation(area);
        }

        protected void POLYGridView_SelectedIndexChanging(object sender, EventArgs e)
        {
            btnPOLYSubmit2.Visible = true;
            btnPOLYCont2.Visible = true;

            PolyCoursesTable.Visible = true;

            DataTable PolyTable = new DataTable();
            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT DISTINCT OLevelCOP.school, OLevelCOP.course_name, Min(OLevelCOP.gceo_cut_off) AS gceo_cut_off FROM [CZ2006 - Life Planner].[dbo].[OLevelCOP] GROUP BY OLevelCOP.school, OLevelCOP.course_name;";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(PolyTable);
            PolyCoursesTable.DataSource = PolyTable;
            PolyCoursesTable.DataBind();
            con.Close();

            string POLYName = (string)POLYTable.DataKeys[POLYTable.SelectedIndex].Value;
            Session["POLYName"] = POLYName;
        }

        protected void POLYCourseGridView_SelectedIndexChanging(object sender, EventArgs e)
        {
            btnPOLYSubmit2.Visible = true;
            btnPOLYCont2.Visible = true;

            string polyCourse = (string)PolyCoursesTable.DataKeys[PolyCoursesTable.SelectedIndex].Value;
            Session["PolyCourse"] = polyCourse;

        }

        protected void btnPOLYSubmitPlan2(object sender, EventArgs e)
        {
            string priSchID1 = "", secSchID1 = "", polyID = "", polyCourse = "";
            if (Session["priSchName"] != null)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
                {
                    string sql2 = "SELECT id FROM Schools WHERE school_name=@schoolname;";
                    SqlCommand cmd2 = new SqlCommand(sql2, con);
                    cmd2.Parameters.AddWithValue("@schoolname", Session["priSchName"].ToString());

                    con.Open();
                    priSchID1 = cmd2.ExecuteScalar().ToString();
                    con.Close();
                }
            }
            else if (Session["priSchName"] == null)
                priSchID1 = "";

            if (Session["secSchName"] != null)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
                {
                    string sql3 = "SELECT id FROM Schools WHERE school_name=@schoolname;";
                    SqlCommand cmd3 = new SqlCommand(sql3, con);
                    cmd3.Parameters.AddWithValue("@schoolname", Session["secSchName"].ToString());

                    con.Open();
                    secSchID1 = cmd3.ExecuteScalar().ToString();
                    con.Close();
                }
            }
            else if (Session["secSchName"] == null)
                secSchID1 = "";

            if (Session["POLYName"] != null)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
                {
                    string sql4 = "SELECT id FROM Schools WHERE school_name=@schoolname;";
                    SqlCommand cmd4 = new SqlCommand(sql4, con);
                    cmd4.Parameters.AddWithValue("@schoolname", Session["POLYName"].ToString());

                    con.Open();
                    polyID = cmd4.ExecuteScalar().ToString();
                    con.Close();
                }
            }

            else if (Session["POLYName"] == null)
                polyID = "";


            if (Session["PolyCourse"] == null)
            {
                polyCourse = "";
            }
            else
            {
                polyCourse = Session["PolyCourse"].ToString();
            }

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
            {
                //string sql = "INSERT INTO dbo.PathPlan(NRIC, priSchID, secSchID, jcID, polyID, ITEID, uniID, accountID) VALUES (@NRIC, @priSchID, @secSchID, @jcID, @polyID, @ITEID, @uniID, @accountID);";
                string sql = "";
                sql += "INSERT INTO dbo.PathPlan(NRIC, priSchID, secSchID, polyID, polyCourse, accountID) VALUES (@NRIC, ";

                if (priSchID1 == "")
                    sql += "NULL, ";
                else
                    sql += "@priSchID, ";


                if (secSchID1 == "")
                    sql += "NULL, ";
                else
                    sql += "@secSchID, ";

                if (polyID == "")
                    sql += "NULL, ";
                else
                    sql += "@polyID, ";

                if (polyCourse == "")
                    sql += "NULL, ";
                else
                    sql += "@polyCourse, ";


                sql += "@accountID);";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@NRIC", Session["newChild"]);
                cmd.Parameters.AddWithValue("@priSchID", priSchID1);
                cmd.Parameters.AddWithValue("@secSchID", secSchID1);
                cmd.Parameters.AddWithValue("@polyID", polyID);
                cmd.Parameters.AddWithValue("@polyCourse", Session["PolyCourse"].ToString());
                cmd.Parameters.AddWithValue("@accountID", Session["accountID"].ToString());



                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            Response.Redirect("~/Account/ViewOwnPlan.aspx");

        }

        protected void btnPOLYContinuePlanning2(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/CreatePlanFromUni.aspx");
        }
    }
}