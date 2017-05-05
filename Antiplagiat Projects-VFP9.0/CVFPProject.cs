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
    public class CVFPProject {

        public string Name;
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
            get
            {
                return formsFullName;
            }
        }
        private SHA1 sha1 = new SHA1Managed();
        private string[] hashTables;
        public string[] HashTables
        {
            get
            {
                return hashTables;
            }
        }
        private string[] hashForms;
        public string[] HashForms
        {
            get
            {
                return hashForms;
            }
        }
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

        public CVFPProject() {

        }
        
        public void Open(string Path) {
            string[] TablesFullName = Directory.GetFiles(Path, "*.dbf",
                                                         SearchOption.AllDirectories);
            string[] FormsFullName = Directory.GetFiles(Path, "*.scx",
                                                     SearchOption.AllDirectories);
            string[] NameProject = Directory.GetFiles(Path, "*.pjx",
                                                     SearchOption.AllDirectories);
            Name = NameProject[0];
            tablesFullName = TablesFullName;
            formsFullName = FormsFullName;
            tablesTables = new DataTable[tablesFullName.Length];
            formTables = new DataTable[formsFullName.Length];

            for (int i = 0; i < tablesFullName.Length; i++) {
                OpenTables(i);
            }
            for (int i = 0; i < formsFullName.Length; i++) {
                OpenForms(i);
            }
            CreateSHA1Prj();
        }

        public void Open(string[] TablesFullName, string[] FormsFullName) {
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

        private void CreateSHA1Prj() {
            hashTables = new string[tablesFullName.Length];
            hashForms = new string[formsFullName.Length];
            for (int i = 0; i < tablesFullName.Length; i++) {
                FileStream stream = File.OpenRead(tablesFullName[i]);
                byte[] hash = sha1.ComputeHash(stream);
                //Console.Write(BitConverter.ToString(hash).Replace("-", String.Empty));
                hashTables[i] = BitConverter.ToString(hash).Replace("-", String.Empty);
                //Console.Write(HashTables[i]+"\n");
            }
            for (int i = 0; i < formsFullName.Length; i++) {
                FileStream stream = File.OpenRead(formsFullName[i]);
                byte[] hash = sha1.ComputeHash(stream);
                //Console.Write(BitConverter.ToString(hash).Replace("-", String.Empty));
                hashForms[i] = BitConverter.ToString(hash).Replace("-", String.Empty);
            }
            Console.WriteLine("Хэши таблиц: " + hashTables.Length + " " + tablesFullName.Length);
            Console.WriteLine("Хэши форм: " + hashForms.Length + " " + formsFullName.Length);

        }

        public string[] GetFormObjectsType(int FormTableIndex) {
            string[] ObjectsType = new string[FormTables[FormTableIndex].Rows.Count];
            for (int j = 0; j < FormTables[FormTableIndex].Rows.Count; j++) {
                ObjectsType[j] = FormTables[FormTableIndex].Rows[j]["Baseclass"].ToString();
            }
            return ObjectsType;
        }

        public string[] GetFormObjectsProperties(int FormTableIndex) {
            string[] ObjectsProperties = new string[FormTables[FormTableIndex].Rows.Count];
            for (int j = 0; j < FormTables[FormTableIndex].Rows.Count; j++) {
                ObjectsProperties[j] = FormTables[FormTableIndex].Rows[j]["Properties"].ToString();
            }
            return ObjectsProperties;
        }
    }
}
