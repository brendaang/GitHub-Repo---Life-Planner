<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
=======
﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
>>>>>>> f6e423c137678bfc045b63a33542ac7b34eee90a
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
<<<<<<< HEAD
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Life_Planner.Data;

namespace Life_Planner
{
    public partial class Test : System.Web.UI.Page
    {
        public class SecSch {
            public string fax_no { get; set; }
            public string gifted_ind { get; set; }
            public string mothertongue3_code { get; set; }
            public string fifth_vp_name { get; set; }
            public string postal_code { get; set; }
            public string type_code { get; set; }
            public string second_vp_name { get; set; }
            public string first_vp_name { get; set; }
            public string mainlevel_code { get; set; }
            public string email_address { get; set; }
            public string sap_ind { get; set; }
            public string cluster_code { get; set; }
            public string telephone_no_2 { get; set; }
            public string philosophy_culture_ethos { get; set; }
            public string mrt_desc { get; set; }
            public string bus_desc { get; set; }
            public string third_vp_name { get; set; }
            public string telephone_no { get; set; }
            public string ip_ind { get; set; }
            public string special_sdp_offered { get; set; }
            public string principal_name { get; set; }
            public string mothertongue1_code { get; set; }
            public string nature_code { get; set; }
            public string visionstatement_desc { get; set; }
            public string fourth_vp_name { get; set; }
            public string missionstatement_desc { get; set; }
            public string autonomous_ind { get; set; }
            public string session_code { get; set; }
            public string school_name { get; set; }
            public string dgp_code { get; set; }
            public string address { get; set; }
            public string mothertongue2_code { get; set; }
            public string fax_no_2 { get; set; }
            public string zone_code { get; set; }
            public int _id { get; set; }
            public string url_address { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e) {

            System.Net.WebClient client = new System.Net.WebClient();
            String downloadedString = client.DownloadString("https://data.gov.sg/api/action/datastore_search?resource_id=ede26d32-01af-4228-b1ed-f05c45a1d8ee&limit=362");

            Newtonsoft.Json.JsonConvert.SerializeObject(downloadedString);

            dynamic stuff = Newtonsoft.Json.JsonConvert.DeserializeObject(downloadedString);
            dynamic a = stuff.First;


            //S1.Text = Convert.ToString(stuff);
        }

        protected void Button1_Click(object sender, EventArgs e) {
            System.Net.WebClient client = new System.Net.WebClient();
            String downloadedString = client.DownloadString("https://data.gov.sg/api/action/datastore_search?resource_id=ede26d32-01af-4228-b1ed-f05c45a1d8ee&limit=362");

            Newtonsoft.Json.JsonConvert.SerializeObject(downloadedString);

            dynamic stuff = Newtonsoft.Json.JsonConvert.DeserializeObject(downloadedString);
            dynamic a = stuff.First;
            //S1.Text = Convert.ToString(stuff);

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString)) {
                string sql = "INSERT INTO dbo.SecSch(school_name, lName, email, birthdate, role) VALUES (@fName, @lName, @email, @date, 0);";
                //sql = "INSERT INTO [dbo].[SecSch] ([school_name] ,[url_address] ,[address] ,[postal_code] ,[telephone_no] ,[telephone_no_2] ,[fax_no] ,[fax_no_2] ,[email_address] ,[mrt_desc] ,[bus_desc] ,[principal_name] ,[first_vp_name] ,[second_vp_name] ,[third_vp_name] ,[fourth_vp_name] ,[fifth_vp_name] ,[visionstatement_desc] ,[missionstatement_desc] ,[philosophy_culture_ethos] ,[dgp_code] ,[zone_code] ,[cluster_code] ,[type_code] ,[nature_code] ,[session_code] ,[mainlevel_code] ,[sap_ind] ,[autonomous_ind] ,[gifted_ind] ,[ip_ind] ,[mothertongue1_code] ,[mothertongue2_code] ,[mothertongue3_code] ,[special_sdp_offered]) VALUES (<school_name, varchar(50),> ,<url_address, varchar(50),> ,<address, text,> ,<postal_code, char(10),> ,<telephone_no, char(10),> ,<telephone_no_2, char(10),> ,<fax_no, char(10),> ,<fax_no_2, char(10),> ,<email_address, varchar(50),> ,<mrt_desc, text,> ,<bus_desc, text,> ,<principal_name, varchar(50),> ,<first_vp_name, varchar(50),> ,<second_vp_name, varchar(50),> ,<third_vp_name, varchar(50),> ,<fourth_vp_name, varchar(50),> ,<fifth_vp_name, varchar(50),> ,<visionstatement_desc, text,> ,<missionstatement_desc, text,> ,<philosophy_culture_ethos, text,> ,<dgp_code, char(10),> ,<zone_code, char(10),> ,<cluster_code, char(10),> ,<type_code, char(10),> ,<nature_code, char(10),> ,<session_code, char(10),> ,<mainlevel_code, char(10),> ,<sap_ind, char(10),> ,<autonomous_ind, char(10),> ,<gifted_ind, char(10),> ,<ip_ind, char(10),> ,<mothertongue1_code, char(10),> ,<mothertongue2_code, char(10),> ,<mothertongue3_code, char(10),> ,<special_sdp_offered, char(10),>)";

                sql = "INSERT INTO " +
                    "dbo.SecSch(school_name, url_address, address, postal_code, telephone_no, telephone_no_2, fax_no, fax_no_2, email_address, mrt_desc, bus_desc, principal_name, first_vp_name, second_vp_name, third_vp_name, fourth_vp_name, fifth_vp_name, visionstatement_desc, missionstatement_desc, philosophy_culture_ethos, dgp_code, zone_code, cluster_code, type_code, nature_code, session_code, mainlevel_code, sap_ind, autonomous_ind, gifted_ind, ip_ind, mothertongue1_code, mothertongue2_code, mothertongue3_code, special_sdp_offered) " +

                    "VALUES " +
                    "(@school_name, @url_address, @address, @postal_code, @telephone_no, @telephone_no_2, @fax_no, @fax_no_2, @email_address, @mrt_desc, @bus_desc, @principal_name, @first_vp_name, @second_vp_name, @third_vp_name, @fourth_vp_name, @fifth_vp_name, @visionstatement_desc, @missionstatement_desc, @philosophy_culture_ethos, @dgp_code, @zone_code, @cluster_code, @type_code, @nature_code, @session_code, @mainlevel_code, @sap_ind, @autonomous_ind, @gifted_ind, @ip_ind, @mothertongue1_code, @mothertongue2_code, @mothertongue3_code, @special_sdp_offered)";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@school_name", "Whitley Secondary School");
                cmd.Parameters.AddWithValue("@url_address", "http://www.whitleysec.moe.edu.sg/");
                cmd.Parameters.AddWithValue("@address", "30 BISHAN STREET 24");
                cmd.Parameters.AddWithValue("@postal_code", "579747");
                cmd.Parameters.AddWithValue("@telephone_no", "64561336");
                cmd.Parameters.AddWithValue("@telephone_no_2", "na");
                cmd.Parameters.AddWithValue("@fax_no", "64591330");
                cmd.Parameters.AddWithValue("@fax_no_2", "na");
                cmd.Parameters.AddWithValue("@email_address", "WHITLEY_SS@MOE.EDU.SG".ToLower());
                cmd.Parameters.AddWithValue("@mrt_desc", "BISHAN MRT; MARYMOUNT MRT");
                cmd.Parameters.AddWithValue("@bus_desc", "13, 52, 54, 74, 74E, 88, 162, 162M,588, 410, 851, 852");
                cmd.Parameters.AddWithValue("@principal_name", "MDM TAN YANG FERN");
                cmd.Parameters.AddWithValue("@first_vp_name", "MR KEITH TAN");
                cmd.Parameters.AddWithValue("@second_vp_name", "MR TYEBALLY AZIZ");
                cmd.Parameters.AddWithValue("@third_vp_name", "123");
                cmd.Parameters.AddWithValue("@fourth_vp_name", "123");
                cmd.Parameters.AddWithValue("@fifth_vp_name", "123");
                cmd.Parameters.AddWithValue("@visionstatement_desc", "123");
                cmd.Parameters.AddWithValue("@missionstatement_desc", "123");
                cmd.Parameters.AddWithValue("@philosophy_culture_ethos", "123");
                cmd.Parameters.AddWithValue("@dgp_code", "123");
                cmd.Parameters.AddWithValue("@zone_code", "123");
                cmd.Parameters.AddWithValue("@cluster_code", "123");
                cmd.Parameters.AddWithValue("@type_code", "123");
                cmd.Parameters.AddWithValue("@nature_code", "123");
                cmd.Parameters.AddWithValue("@session_code", "123");
                cmd.Parameters.AddWithValue("@mainlevel_code", "123");
                cmd.Parameters.AddWithValue("@sap_ind", "123");
                cmd.Parameters.AddWithValue("@autonomous_ind", "123");
                cmd.Parameters.AddWithValue("@gifted_ind", "123");
                cmd.Parameters.AddWithValue("@ip_ind", "123");
                cmd.Parameters.AddWithValue("@mothertongue1_code", "123");
                cmd.Parameters.AddWithValue("@mothertongue2_code", "123");
                cmd.Parameters.AddWithValue("@mothertongue3_code", "123");
                cmd.Parameters.AddWithValue("@special_sdp_offered", "123");
                //cmd.Parameters.AddWithValue("@", "");
                //cmd.Parameters.AddWithValue("@date", tb_datepicker.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                // Generate random salt
                /*RandomNumberGenerator rng = new RNGCryptoServiceProvider();
                byte[] tokenData = new byte[4];
                rng.GetBytes(tokenData);
                string salt = Convert.ToBase64String(tokenData);

                // Generate hash from salted password
                var hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(salt + tb_password.Text));
                string hashedPwd = Convert.ToBase64String(hash);

                string sql2 = "INSERT INTO dbo.AccCreds(username, passwordSalt, passwordHash, accountID) VALUES (@Username, @PasswordSalt, @PasswordHash, @accountID);";
                cmd = new SqlCommand(sql2, con);

                cmd.Parameters.AddWithValue("@Username", tb_username.Text);
                cmd.Parameters.AddWithValue("@PasswordSalt", salt);
                cmd.Parameters.AddWithValue("@PasswordHash", hashedPwd);
                cmd.Parameters.AddWithValue("@accountID", retrieveAccID(tb_email.Text));

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();
                cmd.Dispose();

                // Resets all form fields
                tb_username.Text = String.Empty;
                tb_fName.Text = String.Empty;
                tb_lName.Text = String.Empty;
                tb_email.Text = String.Empty;
                tb_password.Text = String.Empty;
                tb_rePassword.Text = String.Empty;
                tb_datepicker.Text = String.Empty;

                alert_placeholder.Visible = true;
                alert_placeholder.Attributes["class"] = "alert alert-success alert-dismissable";
                alertText.Text = "User account successfully created! You will be redirected to the login page shortly.";

                Response.AddHeader("REFRESH", "3;URL=/Account/Login.aspx");*/
            }


        }
    }
=======

namespace Life_Planner {

	public class Field {
		public string type { get; set; }
		public string id { get; set; }
	}

	public class Record {
		public string fax_no { get; set; }
		public string gifted_ind { get; set; }
		public string mothertongue3_code { get; set; }
		public string fifth_vp_name { get; set; }
		public string postal_code { get; set; }
		public string type_code { get; set; }
		public string second_vp_name { get; set; }
		public string first_vp_name { get; set; }
		public string mainlevel_code { get; set; }
		public string email_address { get; set; }
		public string sap_ind { get; set; }
		public string cluster_code { get; set; }
		public string telephone_no_2 { get; set; }
		public string philosophy_culture_ethos { get; set; }
		public string mrt_desc { get; set; }
		public string bus_desc { get; set; }
		public string third_vp_name { get; set; }
		public string telephone_no { get; set; }
		public string ip_ind { get; set; }
		public string special_sdp_offered { get; set; }
		public string principal_name { get; set; }
		public string mothertongue1_code { get; set; }
		public string nature_code { get; set; }
		public string visionstatement_desc { get; set; }
		public string fourth_vp_name { get; set; }
		public string missionstatement_desc { get; set; }
		public string autonomous_ind { get; set; }
		public string session_code { get; set; }
		public string school_name { get; set; }
		public string dgp_code { get; set; }
		public string address { get; set; }
		public string mothertongue2_code { get; set; }
		public string fax_no_2 { get; set; }
		public string zone_code { get; set; }
		public int _id { get; set; }
		public string url_address { get; set; }
	}

	public class Links {
		public string start { get; set; }
		public string next { get; set; }
	}

	public class Result {
		public string resource_id { get; set; }
		public List<Field> fields { get; set; }
		public List<Record> records { get; set; }
		public Links _links { get; set; }
		public int limit { get; set; }
		public int total { get; set; }
	}

	public class RootObject {
		public string help { get; set; }
		public bool success { get; set; }
		public Result result { get; set; }
	}

	public partial class Test : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {

			//Load JSON to BUILD SQL QUERY

			//Initialise SQL Query
			string sql = "";
			sql += "INSERT INTO dbo.SecSch(";

			//Fetch WEBDATA
			System.Net.WebClient webFetcher = new System.Net.WebClient();
			string jsonforSecSch = "https://data.gov.sg/api/action/datastore_search?resource_id=ede26d32-01af-4228-b1ed-f05c45a1d8ee&limit=362";
			string json = webFetcher.DownloadString("https://data.gov.sg/api/action/datastore_search?resource_id=ede26d32-01af-4228-b1ed-f05c45a1d8ee");
			//parse into object
			JObject jObject = JObject.Parse(json);
			JToken jFields = jObject["result"]["fields"];
			//Dynamically enumerating "field" property (ignoring the first field, id) into List field
			List<string> fields = new List<string>();
			for (int i = 1; i < jFields.Count() - 1; i++) {
				sql += jFields[i]["id"].ToString() + ", ";
				fields.Add(jFields[i]["id"].ToString());
			}
			//add the last one manually to remove the last ", "
			sql += jFields[jFields.Count()-1]["id"].ToString();
			fields.Add(jFields[jFields.Count() - 1]["id"].ToString());
			sql += ") VALUES (";
			//sqli
			for (int i = 0; i < fields.Count() - 1; i++) {
				sql += "@" + fields[i] + ", ";
			}
			sql += "@" + fields[fields.Count() - 1];
			sql += ");";
			//sql query now built, TODO: run for every record

			//get all records
			JToken jRecords = jObject["result"]["records"];
			List<string[]> secsch = new List<string[]>();

			//to populate data on table
			//table headers
			TableHeaderRow tableHeader = new TableHeaderRow();
			Table1.Rows.Add(tableHeader);
			foreach(string field in fields) {
				TableHeaderCell tableHeaderCell = new TableHeaderCell();
				tableHeaderCell.Text = field;
				tableHeader.Cells.Add(tableHeaderCell);
			}


			using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString)) {
				con.Open();
				SqlCommand cmd;
				
				//DEBUG
				//delete all data in table
				cmd = new SqlCommand("DELETE FROM dbo.SecSch", con);
				cmd.ExecuteNonQuery();


				//loop through all records
				foreach (JToken record in jRecords) {
					//poupulate data
					TableRow tableRow = new TableRow();
					Table1.Rows.Add(tableRow);

					cmd = new SqlCommand(sql, con);
					//populate the values into @placeholders
					foreach (string field in fields) {
						cmd.Parameters.AddWithValue("@" + field, record[field].ToString());

						//for <table>
						TableCell tableCell = new TableCell();
						tableCell.Text = (record[field].ToString().Length > 50) ? record[field].ToString().Substring(0, 47) + "..." : record[field].ToString();
						tableCell.Height = 12;
						tableRow.Cells.Add(tableCell);
					}
					//execute
					cmd.ExecuteNonQuery();
				}
				con.Close();
				//Response.AddHeader("REFRESH", "3;URL=/Account/Login.aspx");
			}

			//done
			
		}
	}
>>>>>>> f6e423c137678bfc045b63a33542ac7b34eee90a
}