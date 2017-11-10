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

        protected void radioSelectITEPolyJC_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnITEJCPOLYEast.Visible = true;
            btnITEJCPOLYNone.Visible = true;
            btnITEJCPOLYNorth.Visible = true;
            btnITEJCPOLYSouth.Visible = true;
            btnITEJCPOLYWest.Visible = true;
            lblITEJCPOLYSchFilterByLoc.Visible = true;

            if (radioSelectITEPolyJC.SelectedItem.Text == "Junior College")
            {
                PolyCoursesTable.Visible = false;

                DataTable ViewITEJCSchTable = new DataTable();
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE school_name LIKE '%JUNIOR COLLEGE%';";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ViewITEJCSchTable);
                ITEJCPOLYTable.DataSource = ViewITEJCSchTable;
                ITEJCPOLYTable.DataBind();
                con.Close();
            }

            else if (radioSelectITEPolyJC.SelectedItem.Text == "Polytechnic")
            {
                PolyCoursesTable.Visible = true;
                btnITEJCPOLYCont.Visible = false;
                btnITEJCPOLYSubmit.Visible = false;

                DataTable ViewITEPolySchTable = new DataTable();
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE school_name LIKE '%POLYTECHNIC%';";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ViewITEPolySchTable);
                ITEJCPOLYTable.DataSource = ViewITEPolySchTable;
                ITEJCPOLYTable.DataBind();
                con.Close();
            }

            else if (radioSelectITEPolyJC.SelectedItem.Text == "ITE")
            {
                PolyCoursesTable.Visible = false;

                DataTable ViewITEPolySchTable = new DataTable();
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE school_name LIKE '%ITE COLLEGE%';";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ViewITEPolySchTable);
                ITEJCPOLYTable.DataSource = ViewITEPolySchTable;
                ITEJCPOLYTable.DataBind();
                con.Close();
            }

        }

        protected void btnITEJCPOLYLocation(string area)
        {
            if (radioSelectITEPolyJC.SelectedItem.Text == "Junior College")
            {
                DataTable ViewJCPOLYTable = new DataTable();
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE zone_code=@area AND school_name LIKE '%JUNIOR COLLEGE%';";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@area", area);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ViewJCPOLYTable);
                ITEJCPOLYTable.DataSource = ViewJCPOLYTable;
                ITEJCPOLYTable.DataBind();
                con.Close();
            }

            else if (radioSelectITEPolyJC.SelectedItem.Text == "Polytechnic")
            {

                DataTable ViewITEJCPOLYTable = new DataTable();
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE zone_code=@area AND school_name LIKE '%POLYTECHNIC%';";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@area", area);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ViewITEJCPOLYTable);
                ITEJCPOLYTable.DataSource = ViewITEJCPOLYTable;
                ITEJCPOLYTable.DataBind();
                con.Close();
            }

            else if (radioSelectITEPolyJC.SelectedItem.Text == "ITE")
            {
                DataTable ViewITEJCPOLYTable = new DataTable();
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE zone_code=@area AND school_name LIKE '%ITE COLLEGE%';";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@area", area);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ViewITEJCPOLYTable);
                ITEJCPOLYTable.DataSource = ViewITEJCPOLYTable;
                ITEJCPOLYTable.DataBind();
                con.Close();
            }

        }

        protected void btn_ITEJCPOLYNone(object sender, EventArgs e)
        {
            if (radioSelectITEPolyJC.SelectedItem.Text == "Junior College")
            {
                DataTable ViewITEJCSchTable = new DataTable();
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE school_name LIKE '%JUNIOR COLLEGE%';";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ViewITEJCSchTable);
                ITEJCPOLYTable.DataSource = ViewITEJCSchTable;
                ITEJCPOLYTable.DataBind();
                con.Close();
            }

            else if (radioSelectITEPolyJC.SelectedItem.Text == "Polytechnic")
            {
                DataTable ViewITEPolySchTable = new DataTable();
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE school_name LIKE '%POLYTECHNIC%';";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ViewITEPolySchTable);
                ITEJCPOLYTable.DataSource = ViewITEPolySchTable;
                ITEJCPOLYTable.DataBind();
                con.Close();
            }

            else if (radioSelectITEPolyJC.SelectedItem.Text == "ITE")
            {
                DataTable ViewITEPolySchTable = new DataTable();
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT school_name,zone_code, dgp_code,url_address FROM [CZ2006 - Life Planner].[dbo].[Schools] WHERE school_name LIKE '%ITE COLLEGE%';";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ViewITEPolySchTable);
                ITEJCPOLYTable.DataSource = ViewITEPolySchTable;
                ITEJCPOLYTable.DataBind();
                con.Close();
            }
        }

        protected void btn_ITEJCPOLYNorth(object sender, EventArgs e)
        {
            string area = "North";
            btnITEJCPOLYLocation(area);
        }

        protected void btn_ITEJCPOLYSouth(object sender, EventArgs e)
        {
            string area = "South";
            btnITEJCPOLYLocation(area);
        }

        protected void btn_ITEJCPOLYEast(object sender, EventArgs e)
        {
            string area = "East";
            btnITEJCPOLYLocation(area);
        }

        protected void btn_ITEJCPOLYWest(object sender, EventArgs e)
        {
            string area = "West";
            btnITEJCPOLYLocation(area);
        }

        protected void ITEJCPOLYGridView_SelectedIndexChanging(object sender, EventArgs e)
        {
            btnITEJCPOLYSubmit.Visible = true;
            btnITEJCPOLYCont.Visible = true;

            if (radioSelectITEPolyJC.SelectedItem.Text == "ITE" || radioSelectITEPolyJC.SelectedItem.Text == "Junior College")
            {
                PolyCoursesTable.Visible = false;
            }

            if (radioSelectITEPolyJC.SelectedItem.Text == "Polytechnic")
            {
                PolyCoursesTable.Visible = true;
                btnITEJCPOLYCont.Visible = false;
                btnITEJCPOLYSubmit.Visible = false;

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
            }

            string ITEJCPOLYName = (string)ITEJCPOLYTable.DataKeys[ITEJCPOLYTable.SelectedIndex].Value;

            if (radioSelectITEPolyJC.SelectedItem.Text == "Junior College")
                Session["JCName"] = ITEJCPOLYName;
            if (radioSelectITEPolyJC.SelectedItem.Text == "ITE")
                Session["ITEName"] = ITEJCPOLYName;
            if (radioSelectITEPolyJC.SelectedItem.Text == "Polytechnic")
                Session["POLYName"] = ITEJCPOLYName;
        }

        protected void btnITEJCPOLYSubmitPlan(object sender, EventArgs e)
        {
            if (radioSelectITEPolyJC.SelectedItem.Text == "Junior College")
            {
                string priSchID1 = "", secSchID1 = "", jcID = "";

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


                if (Session["JCName"] != null)
                {
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
                    {
                        string sql4 = "SELECT id FROM Schools WHERE school_name=@schoolname;";
                        SqlCommand cmd4 = new SqlCommand(sql4, con);
                        cmd4.Parameters.AddWithValue("@schoolname", Session["JCName"].ToString());

                        con.Open();
                        jcID = cmd4.ExecuteScalar().ToString();
                        con.Close();
                    }
                }
                else if (Session["JCName"] == null)
                    jcID = "";

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
                {
                    string sql = "";
                    sql += "INSERT INTO dbo.PathPlan(NRIC, priSchID, secSchID, jcID, accountID) VALUES (@NRIC, ";

                    if (priSchID1 == "")
                        sql += "NULL, ";
                    else
                        sql += "@priSchID, ";


                    if (secSchID1 == "")
                        sql += "NULL, ";
                    else
                        sql += "@secSchID, ";

                    if (jcID == "")
                        sql += "NULL, ";
                    else
                        sql += "@jcID, ";


                    sql += "@accountID);";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@NRIC", Session["newChild"]);
                    cmd.Parameters.AddWithValue("@priSchID", priSchID1);
                    cmd.Parameters.AddWithValue("@secSchID", secSchID1);
                    cmd.Parameters.AddWithValue("@jcID", jcID);
                    cmd.Parameters.AddWithValue("@accountID", Session["accountID"].ToString());



                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }



                Session["ITEName"] = null;
                Session["POLYName"] = null;

            }



            if (radioSelectITEPolyJC.SelectedItem.Text == "ITE")
            {
                string priSchID1 = "", secSchID1 = "", iteID = "";
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

                if (Session["ITEName"] != null)
                {
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
                    {
                        string sql4 = "SELECT id FROM Schools WHERE school_name=@schoolname;";
                        SqlCommand cmd4 = new SqlCommand(sql4, con);
                        cmd4.Parameters.AddWithValue("@schoolname", Session["ITEName"].ToString());

                        con.Open();
                        iteID = cmd4.ExecuteScalar().ToString();
                        con.Close();
                    }
                }

                else if (Session["ITEName"] == null)
                {
                    iteID = "";
                }

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
                {
                    //string sql = "INSERT INTO dbo.PathPlan(NRIC, priSchID, secSchID, jcID, polyID, ITEID, uniID, accountID) VALUES (@NRIC, @priSchID, @secSchID, @jcID, @polyID, @ITEID, @uniID, @accountID);";
                    string sql = "";
                    sql += "INSERT INTO dbo.PathPlan(NRIC, priSchID, secSchID, ITEID, accountID) VALUES (@NRIC, ";

                    if (priSchID1 == "")
                        sql += "NULL, ";
                    else
                        sql += "@priSchID, ";


                    if (secSchID1 == "")
                        sql += "NULL, ";
                    else
                        sql += "@secSchID, ";

                    if (iteID == "")
                        sql += "NULL, ";
                    else
                        sql += "@ITEID, ";


                    sql += "@accountID);";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@NRIC", Session["newChild"]);
                    cmd.Parameters.AddWithValue("@priSchID", priSchID1);
                    cmd.Parameters.AddWithValue("@secSchID", secSchID1);
                    cmd.Parameters.AddWithValue("@ITEID", iteID);
                    cmd.Parameters.AddWithValue("@accountID", Session["accountID"].ToString());



                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                Session["JCName"] = null;
                Session["POLYName"] = null;
            }

            //redirect to view plan
            Response.Redirect("~/Account/ViewOwnPlan.aspx");
        }

        protected void btnITEJCPOLYContinuePlanning(object sender, EventArgs e)
        {
            if (radioSelectITEPolyJC.SelectedItem.Text == "ITE")
                Response.Redirect("~/Account/CreatePlanFromPOLY.aspx");
            else
                Response.Redirect("~/Account/CreatePlanFromUni.aspx");
        }

        protected void POLYCourseGridView_SelectedIndexChanging(object sender, EventArgs e)
        {
            btnITEJCPOLYSubmit2.Visible = true;
            btnITEJCPOLYCont2.Visible = true;

            string polyCourse = (string)PolyCoursesTable.DataKeys[PolyCoursesTable.SelectedIndex].Value;
            Session["PolyCourse"] = polyCourse;

        }

        protected void btnITEJCPOLYSubmitPlan2(object sender, EventArgs e)
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

            Session["JCName"] = null;
            Session["ITEName"] = null;

            Response.Redirect("~/Account/ViewOwnPlan.aspx");

        }

        protected void btnITEJCPOLYContinuePlanning2(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/CreatePlanFromUni.aspx");
        }
    }
}