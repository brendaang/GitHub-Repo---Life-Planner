using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Life_Planner
{

    public class Rootobject
    {
        public string help { get; set; }
        public bool success { get; set; }
        public Result1 result { get; set; }
    }

    public class Result1
    {
        public string resource_id { get; set; }
        public List<Field> fields { get; set; }
        public List<Record> records { get; set; }
        public _Links _links { get; set; }
        public int total { get; set; }
     

    }

    public class _Links
    {
        public string start { get; set; }
        public string next { get; set; }
    }

    public class Field1
    {
        public string type { get; set; }
        public string id { get; set; }
    }

    public class Record1
    {
        public string percentage { get; set; }
        public int _id { get; set; }
        public string post_secondary_course { get; set; }
        public string year { get; set; }
    }

    public partial class viewConsolidatedData : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {


            //Load JSON to BUILD SQL QUERY

            //Initialise SQL Query
            string sql = "";
            sql += "INSERT INTO dbo.PostSecondaryData(";

            //Fetch WEBDATA
            System.Net.WebClient webFetcher = new System.Net.WebClient();
   
            string json = webFetcher.DownloadString("https://data.gov.sg/api/action/datastore_search?resource_id=173b95bc-6e8b-4af9-a166-4441789383cd");
            //parse into object
            JObject jObject = JObject.Parse(json);
            JToken jFields = jObject["result"]["fields"];
            //Dynamically enumerating "field" property (ignoring the first field, id) into List field
            List<string> fields = new List<string>();
            for (int i = 1; i < jFields.Count() - 1; i++)
            {
                sql += jFields[i]["id"].ToString() + ", ";
                fields.Add(jFields[i]["id"].ToString());
            }
            //add the last one manually to remove the last ", "
            sql += jFields[jFields.Count() - 1]["id"].ToString();
            fields.Add(jFields[jFields.Count() - 1]["id"].ToString());
            sql += ") VALUES (";
            //sqli
            for (int i = 0; i < fields.Count() - 1; i++)
            {
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
            
            foreach (string field in fields)
            {
                TableHeaderCell tableHeaderCell = new TableHeaderCell();
                tableHeaderCell.Text = field;
                tableHeader.Cells.Add(tableHeaderCell);
            }


            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CZ2006 - Life Planner"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd;

                //DEBUG
                //delete all data in table
                cmd = new SqlCommand("DELETE FROM dbo.PostSecondaryData", con);
                cmd.ExecuteNonQuery();


                //loop through all records
                foreach (JToken record in jRecords)
                {
                    //poupulate data
                    TableRow tableRow = new TableRow();
                    //Table1.Rows.Add(tableRow);

                    cmd = new SqlCommand(sql, con);
                    //populate the values into @placeholders
                    foreach (string field in fields)
                    {
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
                //Response.AddHeader("REFRESH", "1;URL=/Account/Login.aspx");
            }

            //done

        }

    }

    }
