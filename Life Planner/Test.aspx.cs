using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
			sql += "INSERT INTO dbo.Schools(";

			//Fetch WEBDATA
			System.Net.WebClient webFetcher = new System.Net.WebClient();
			string jsonforSecSch = "https://data.gov.sg/api/action/datastore_search?resource_id=ede26d32-01af-4228-b1ed-f05c45a1d8ee&limit=362";
			string json = webFetcher.DownloadString("https://data.gov.sg/api/action/datastore_search?resource_id=ede26d32-01af-4228-b1ed-f05c45a1d8ee&limit=362");
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
				cmd = new SqlCommand("DELETE FROM dbo.Schools", con);
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
				//Response.AddHeader("REFRESH", "0;URL=/Account/Login.aspx");
			}

			//done
			
		}
	}
}