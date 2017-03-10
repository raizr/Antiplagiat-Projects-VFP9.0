using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.IO;

namespace Antiplagiat_Projects_VFP9._0 {
    class CLoaderDB {
        public CLoaderDB() {
            
        }
        public CLoaderDB(string FileName) {
            oleDbConnection = new OleDbConnection("Provider=VFPOLEDB.1;"+
                                                  "Data Source=" + FileName);
            oleDbConnection.Open();
            Console.WriteLine("ServerVersion: {0} \nDatabase: {1}",
                              oleDbConnection.ServerVersion, oleDbConnection.DataSource);

            if (oleDbConnection.State == ConnectionState.Open) {
                //AllTables = oleDbConnection.GetSchema("Tables", new string[] { null, null, null, "TABLE" });
                
                
                OleDbDataAdapter DataAdapter = new OleDbDataAdapter("select * from " +
                    Path.GetFileName(FileName), oleDbConnection);
                DataSet ds = new DataSet();
                DataAdapter.Fill(ds);
                AllTables = ds.Tables[0];
                //string mySQL = "USE ?";
                //OleDbCommand query = new OleDbCommand(mySQL, oleDbConnection);

                //DA.SelectCommand = query;

                //DA.Fill(AllTables);

                oleDbConnection.Close();
            }
        }

        public DataTable OpenDB(string FileName) {
            oleDbConnection = new OleDbConnection("Provider=VFPOLEDB.1;" +
                                                 "Data Source=" + FileName);
            oleDbConnection.Open();
            Console.WriteLine("ServerVersion: {0} \nDatabase: {1}",
                              oleDbConnection.ServerVersion, oleDbConnection.DataSource);

            if (oleDbConnection.State == ConnectionState.Open) {
                //AllTables = oleDbConnection.GetSchema("Tables", new string[] { null, null, null, "TABLE" });


                OleDbDataAdapter DataAdapter = new OleDbDataAdapter("select * from " +
                    Path.GetFileName(FileName), oleDbConnection);
                DataSet ds = new DataSet();
                DataAdapter.Fill(ds);
                oleDbConnection.Close();
                return ds.Tables[0];
                //string mySQL = "USE ?";
                //OleDbCommand query = new OleDbCommand(mySQL, oleDbConnection);

                //DA.SelectCommand = query;

                //DA.Fill(AllTables);


            }else {
                return null;
            }
        }

        ~CLoaderDB() {

        }

        private OleDbConnection oleDbConnection;
        private DataTable AllTables = new DataTable();
        public DataTable Table
        {
            get
            {
                return AllTables;
            }
        }
    }
}
