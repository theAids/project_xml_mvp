using Project_XML.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Project_XML.Models.DbManager
{
    public class DbConnManager
    {
        protected SqlConnection GetDbConnection(string connName)
        {
            try
            {
                string connString = System.Configuration.ConfigurationManager.ConnectionStrings[connName].ToString();
                return new SqlConnection(connString);
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine("DB CONNECTION ERROR: " + e.Message);
                Debug.WriteLine(e.StackTrace);
                return null;
            }

        }

        public string GetConnectionString(string connName)
        {
            try
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings[connName].ToString();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Get Connection String Error:" + e.Message);
                return null;
            }
        }
        public static List<ConnectionStringModel> GetConnectionStrings()
        {
            List<ConnectionStringModel> cStrings = new List<ConnectionStringModel>();

            // get the web config of the project
            string configPath = "~/Web.config";

            // get the Web application configuration object.
            Configuration config = WebConfigurationManager.OpenWebConfiguration(configPath);


            // get the conectionStrings section.
            ConnectionStringsSection csSection = config.ConnectionStrings;

            // connection string [1] is user accounts database
            for (int i = 2; i < ConfigurationManager.ConnectionStrings.Count; i++)
            {
                ConnectionStringSettings cs = csSection.ConnectionStrings[i];

                ConnectionStringModel cModel = new ConnectionStringModel();
                cModel.index = i - 1;

                cModel.name = cs.Name;

                // get connectionString part and get its attributes
                var input = cs.ConnectionString.Split(';').Select(pair => pair.Split('=')).Where(pair => pair.Length == 2)
                    .ToDictionary(keyvalue => keyvalue[0].Trim(), keyvalue => keyvalue[1].Trim());

                cModel.servername = input["Data Source"];

                cModel.dbname = input["Initial Catalog"];

                cStrings.Add(cModel);
            }
            return cStrings;
        }
    }
}