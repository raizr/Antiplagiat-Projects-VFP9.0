using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Security.Cryptography;

namespace Antiplagiat_Projects_VFP9._0 {
    class CManager {
        public CManager() {

        }
        
        private void CreateSHA1Prj() {
            HashTables = new string[tablesFullName.Length];
            HashForms = new string[formsFullName.Length];
            for (int i = 0; i < tablesFullName.Length; i++) {
                FileStream stream = File.OpenRead(tablesFullName[i]);
                byte[] hash = sha1.ComputeHash(stream);
                //Console.Write(BitConverter.ToString(hash).Replace("-", String.Empty));
                HashTables[i] = BitConverter.ToString(hash).Replace("-", String.Empty);
                //Console.Write(HashTables[i]+"\n");
            }
            for (int i = 0; i < formsFullName.Length; i++) {
                FileStream stream = File.OpenRead(formsFullName[i]);
                byte[] hash = sha1.ComputeHash(stream);
                //Console.Write(BitConverter.ToString(hash).Replace("-", String.Empty));
                HashForms[i] = BitConverter.ToString(hash).Replace("-", String.Empty);
            }

        }
        public void OpenProject(string[] TablesFullName, string[] FormsFullName) {
            tablesFullName = TablesFullName;
            formsFullName = FormsFullName;
            tablesTables = new DataTable[tablesFullName.Length];
            formTables = new DataTable[formsFullName.Length];
            for (int i = 0; i< tablesFullName.Length; i++) {
                OpenTables(i);
            }
            for (int i = 0; i < formsFullName.Length; i++) {
                OpenForms(i);
            }
            CreateSHA1Prj();
            
        }
        public DataTable OpenTables(int i) {
            //string DirectoryName = Path.GetDirectoryName(tablesFullName[0]);
            TablesTables[i] = loaderDb.OpenDB(tablesFullName[i]);
            return TablesTables[i];
        }
        public DataTable OpenForms(int i) {
            //string DirectoryName = Path.GetDirectoryName(tablesFullName[0]);
            FormTables[i] = loaderDb.OpenDB(formsFullName[i]);
            return FormTables[i];
        }

        public string[] GetColumnsName(int TableIndex) {
            columnsCaption = new string[TablesTables[TableIndex].Columns.Count];
            for(int j = 0; j< TablesTables[TableIndex].Columns.Count; j++) {
                columnsCaption[j] = TablesTables[TableIndex].Columns[j].ColumnName;
            }
            return columnsCaption;
        }

        public string[] GetColumnsType(int TableIndex) {
            columnsType = new string[TablesTables[TableIndex].Columns.Count];
            int length = "System.".Length;
            for (int j = 0; j < TablesTables[TableIndex].Columns.Count; j++) {
                columnsType[j] = TablesTables[TableIndex].Columns[j].DataType.ToString();
                //удаление подстроки "System." в DataType
                columnsType[j] = columnsType[j].Substring(length, columnsType[j].Length-length);
            }
            return columnsType;
        }

        public string[] GetFormObjectsType(int FormTableIndex) {
            string[] ObjectsType = new string[FormTables[FormTableIndex].Rows.Count];
            for (int j = 0; j < FormTables[FormTableIndex].Rows.Count; j++) {
                ObjectsType[j] = FormTables[FormTableIndex].Rows[j][6].ToString();
            }
            return ObjectsType;
        }
        ~CManager() {

        }
        private string[] tablesFullName;
        public string[] TablesFullName
        {
            set
            {
                tablesFullName = value;
            }
            get
            {
                return tablesFullName;
            }
        }
        private string[] formsFullName;
        public string[] FormsFullName
        {
            set
            {
                formsFullName = value;
            }
        }
        private SHA1 sha1 = new SHA1Managed();
        private string[] HashTables;
        private string[] HashForms;
        private CLoaderDB loaderDb = new CLoaderDB();
        private DataTable[] tablesTables;
        public DataTable[] TablesTables
        {
            get
            {
                return tablesTables;
            }
        }
        private DataTable[] formTables;
        public DataTable[] FormTables
        {
            get
            {
                return formTables;
            }
        }
        private string[] columnsCaption;
        private string[] ColumnsCaption
        {
            get
            {
                return columnsCaption;
            }
        }
        private string[] columnsType;
        public string[] ColumnsType
        {
            get
            {
                return ColumnsType;
            }
        }
    }
}
