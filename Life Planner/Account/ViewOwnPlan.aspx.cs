﻿using System;
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
    public partial class ViewOwnPlan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string accID = Session["accountID"].ToString();
            SqlConnection con = new DBManager().getConnection();
            string sql = "SELECT * FROM dbo.PathPlan WHERE accountID=@accID";
            string[] info = new string[7];

            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@accID", accID);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                info[0] = dr["priSchID"].ToString();
                info[1] = dr["secSchID"].ToString();
                info[2] = dr["jcID"].ToString();
                info[3] = dr["polyID"].ToString();
                if (!dr.IsDBNull(dr.GetOrdinal("polyCourse")))
                    info[4] =(string)dr["polyCourse"];
                //info[5] = dr["ITEID"].ToString();
                info[5] = dr["uniID"].ToString();
                if(!dr.IsDBNull(dr.GetOrdinal("uniCourse")))
                    info[6] = (string)dr["uniCourse"];
            }
            dr.Close();
            con.Close();
            con.Dispose();

            //display of plan info
            displayPlan(info);
            //if no plan created, inform user they have no plan and redirect them to create plan page
            checkFields();
        }

        protected void btn_editPlan_Click(object sender, EventArgs e)
        {
            btn_editPri.Visible = true;
            btn_editSec.Visible = true;
            btn_editTertiary.Visible = true;
            btn_doneEdit.Visible = true;
            btn_editPlan.Visible = false;
        }

        protected void btn_deletePlan_Click(object sender, EventArgs e)
        {
            //delete plan logic here
            SqlConnection con = new DBManager().getConnection();
            string sql = "DELETE FROM dbo.PathPlan WHERE accountID=@accountID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@accountID", Session["accountID"].ToString());
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            setNA();
            alert_placeholder.Visible = true;
            alert_placeholder.Attributes["class"] = "alert alert-success alert-dismissable";
            alertText.Text = "Successfully Deleted Plan! Redirecting to create plan page..";
            Response.AddHeader("REFRESH", "3;URL=CreatePlan.aspx");


        }

        protected void displayPlan(string[] info)
        {
            //display in form
            tb_priName.Text = getSchName(info[0]);
            tb_secName.Text = getSchName(info[1]);
            tb_jcName.Text = getSchName(info[2]);
            string[] polyinfo = getPolyInfo(info[3]);
            tb_polyName.Text = polyinfo[0];
            tb_polyCourse.Text = polyinfo[1];
            //tb_uniName.Text = info[5];
            //tb_uniCourse.Text = info[6];
            tb_uniName.Text = tb_uniCourse.Text = "Not Available"; //no uni data in db
        }

        protected string getSchName(string priID)
        {
            if(!string.IsNullOrEmpty(priID))
            {
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT school_name FROM dbo.Schools WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", priID);

                con.Open();
                string schName = cmd.ExecuteScalar().ToString();
                con.Close();
                con.Dispose();
                return schName;
            }
            return "Not Available";
        }

        protected string[] getPolyInfo(string polyID)
        {
            string[] polyinfo = new string[2];
            if (!string.IsNullOrEmpty(polyID))
            {
                SqlConnection con = new DBManager().getConnection();
                string sql = "SELECT * FROM dbo.OLevelCOP WHERE id=@id";
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", polyID);
                SqlDataReader dr = cmd.ExecuteReader();
                
                while (dr.Read())
                {
                    polyinfo[0] = (string)dr["school"];
                    polyinfo[1] = (string)dr["course_name"];
                }
                con.Close();
                con.Dispose();
                return polyinfo;
            }
            polyinfo[0] = polyinfo[1] = "Not Available";
            return polyinfo;
        }

        protected void checkFields()
        {
            if(tb_priName.Text == tb_secName.Text)
                if(tb_secName.Text == tb_jcName.Text)
                    if(tb_jcName.Text == tb_polyName.Text)
                        if(tb_polyName.Text == tb_polyCourse.Text)
                            if(tb_polyCourse.Text == "Not Available")
                            { //no plans
                                btn_deletePlan.Visible = false;
                                btn_editPlan.Visible = false;
                                alert_placeholder.Visible = true;
                                alert_placeholder.Attributes["class"] = "alert alert-warning alert-dismissable";
                                alertText.Text = "No Existing Plan! Redirecting to Create Plan page...";
                                Response.AddHeader("REFRESH", "3;URL=CreatePlan.aspx");
                            }
        }

        protected void btn_editPri_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditPrimarySch.aspx");
        }

        protected void btn_editSec_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditSecSch.aspx");
        }

        protected void btn_editTertiary_Click(object sender, EventArgs e)
        {

        }

        protected void btn_doneEdit_Click(object sender, EventArgs e)
        {
            btn_editPri.Visible = false;
            btn_editSec.Visible = false;
            btn_editTertiary.Visible = false;
            btn_doneEdit.Visible = false;
            btn_editPlan.Visible = true;
        }

        protected void setNA()
        {
            tb_priName.Text = "";
            tb_secName.Text = "";
            tb_jcName.Text = "";
            tb_polyName.Text = "";
            tb_polyCourse.Text = "";
            tb_uniName.Text = "";
            tb_uniCourse.Text = "";
        }
    }
}