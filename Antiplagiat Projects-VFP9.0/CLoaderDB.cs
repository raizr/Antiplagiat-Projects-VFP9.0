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
            /*Console.WriteLine("ServerVersion: {0} \nDatabase: {1}",
                              oleDbConnection.ServerVersion, oleDbConnection.DataSource);
*/
            if (oleDbConnection.State == ConnectionState.Open) {
                OleDbDataAdapter DataAdapter = new OleDbDataAdapter("select * from " +
                    Path.GetFileName(FileName), oleDbConnection);
                DataSet ds = new DataSet();
                DataAdapter.Fill(ds);
                AllTables = ds.Tables[0];
                oleDbConnection.Close();
            }
        }
        
        public DataTable OpenDB(string FileName) {
            oleDbConnection = new OleDbConnection("Provider=VFPOLEDB.1;" +
                                                 "Data Source=" + FileName);
            oleDbConnection.Open();

            if (oleDbConnection.State == ConnectionState.Open) {
                 OleDbDataAdapter DataAdapter = new OleDbDataAdapter();
                if (FileName.Substring(FileName.Length-3, 3) == "scx") {
                    DataAdapter = new OleDbDataAdapter("select Baseclass, Objname, Properties, Methods from " +
                    Path.GetFileName(FileName), oleDbConnection);
                } else {
                    DataAdapter = new OleDbDataAdapter("select * from " +
                    Path.GetFileName(FileName), oleDbConnection);
                }
                DataSet ds = new DataSet();
                DataAdapter.Fill(ds);
                oleDbConnection.Close();
                if (ds.Tables[0].Columns[0].Caption == "uniqueid")
                    return null;
                else
                    return ds.Tables[0];
            } else {
                return null;
            }
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
