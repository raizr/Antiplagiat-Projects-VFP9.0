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
       /* private string[] columnsCaption;
        public string[] ColumnsCaption
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
        */
        public List<SForm> Forms;
        public int AllObjectsCounter = 0;
        public delegate void MethodContainer();
        public event MethodContainer onCount = delegate { };
        public CVFPProject() {
            Forms = new List<SForm>();
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
            Forms.Clear();
            AllObjectsCounter = 0;
            for (int i = 0; i < tablesFullName.Length; i++) {
                OpenTable(i);
                onCount();
            }
            for (int i = 0; i < formsFullName.Length; i++) {
                OpenForm(i);
                onCount();
            }
            CreateSHA1Prj();
        }

        public void Open(string[] TablesFullName, string[] FormsFullName) {
            tablesFullName = TablesFullName;
            formsFullName = FormsFullName;
            tablesTables = new DataTable[tablesFullName.Length];
            formTables = new DataTable[formsFullName.Length];
            Forms.Clear();
            AllObjectsCounter = 0;
            for (int i = 0; i< tablesFullName.Length; i++) {
                OpenTable(i);
                onCount();
            }
            for (int i = 0; i < formsFullName.Length; i++) {
                OpenForm(i);
                onCount();
            }
            CreateSHA1Prj();
        }

        public int OpenTables(string Path) {
            string[] TablesFullName = Directory.GetFiles(Path, "*.dbf",
                                                         SearchOption.AllDirectories);
            string[] NameProject = Directory.GetFiles(Path, "*.pjx",
                                                     SearchOption.AllDirectories);
            Name = NameProject[0];
            tablesFullName = TablesFullName;
            tablesTables = new DataTable[tablesFullName.Length];
            for (int i = 0; i < tablesFullName.Length; i++) {
                OpenTable(i);
            }
            return 0;
        }

        public DataTable OpenTable(int i) {
            //string DirectoryName = Path.GetDirectoryName(tablesFullName[0]);
            TablesTables[i] = loaderDb.OpenDB(tablesFullName[i]);
            return TablesTables[i];
        }
        private SForm FormOpen(string formsFullName, DataTable FormTable) {
            SForm form = new SForm();
            form.Name = formsFullName;
            form.form = new List<SObject>(); form.commandbutton = new List<SObject>();
            form.header = new List<SObject>(); form.textbox = new List<SObject>();
            form.grid = new List<SObject>(); form.label = new List<SObject>();
            form.pageframe = new List<SObject>(); form.editbox = new List<SObject>();
            form.spinner = new List<SObject>(); form.optiongroup = new List<SObject>();
            form.checkbox = new List<SObject>(); form.combobox = new List<SObject>();
            form.listbox = new List<SObject>();
            for (int j = 0; j < FormTable.Rows.Count; j++) {
                SObject obj = new SObject();
                // получение полей формы
                obj.IsPlagiarism = false; obj.RefObjectIndex = -1;
                obj.classname = FormTable.Rows[j]["baseclass"].ToString(); // имя класса
                obj.objname = FormTable.Rows[j]["objname"].ToString(); // имя объекта
                string methods = FormTable.Rows[j]["methods"].ToString(); // методы объекта
                obj.methods = new List<string>();
                string[] strMethods = methods.Split('\n');
                string MethodList = "";
                
                for (int k = 0; k < strMethods.Length; k++) {
                    strMethods[k] = strMethods[k].Trim('\r');
                    string[] method = strMethods[k].Split(' ');
                    for (int i = 0; i < method.Length; i++) {
                        if(method[i] != "ENDPROC") {
                            MethodList += method[i]+" ";
                        } else {
                            MethodList += method[i];
                            obj.methods.Add(MethodList);
                            MethodList = "";
                        }
                    }
                    MethodList += "\n";
                }
                    //obj.methods = 
                string properties = FormTable.Rows[j]["properties"].ToString(); //свойства
                //преобразование свойства из строки в пары (свойство - значение свойства)
                // разделение одной строки свойств на строки с одним свойством
                string[] prop = properties.Split('\n');
                obj.properties = new Dictionary<string, string>();
                for (int k = 0; k < prop.Length - 1; k++) {
                    prop[k] = prop[k].Replace("\r", "");
                    //разделение одного свойства на имя и значение
                    string[] PropAndValue = prop[k].Split('=');
                    if (PropAndValue.Length > 1) {
                        obj.properties.Add(PropAndValue[0], PropAndValue[1]);
                    }
                }
                if (obj.classname.Length > 0 && obj.objname.Length > 0 &&
                    obj.properties.Count > 0) {
                    
                    // запись в структуру объекта формы в зависимости от класса объекта
                    switch (obj.classname) {
                        case "form":
                            form.form.Add(obj); AllObjectsCounter++;
                            break;
                        case "commandbutton":
                            form.commandbutton.Add(obj); AllObjectsCounter++;
                            break;
                        case "header":
                            form.header.Add(obj); AllObjectsCounter++;
                            break;
                        case "textbox":
                            form.textbox.Add(obj); AllObjectsCounter++;
                            break;
                        case "grid":
                            form.grid.Add(obj); AllObjectsCounter++;
                            break;
                        case "label":
                            form.label.Add(obj); AllObjectsCounter++;
                            break;
                        case "pageframe":
                            form.pageframe.Add(obj); AllObjectsCounter++;
                            break;
                        case "editbox":
                            form.editbox.Add(obj); AllObjectsCounter++;
                            break;
                        case "spinner":
                            form.spinner.Add(obj); AllObjectsCounter++;
                            break;
                        case "optiongroup":
                            form.optiongroup.Add(obj); AllObjectsCounter++;
                            break;
                        case "checkbox":
                            form.checkbox.Add(obj); AllObjectsCounter++;
                            break;
                        case "combobox":
                            form.combobox.Add(obj); AllObjectsCounter++;
                            break;
                        case "listbox":
                            form.listbox.Add(obj); AllObjectsCounter++;
                            break;
                    }
                }
            }
            return form;
        }

        public DataTable OpenForm(int i) {
            DataTable FormTable = loaderDb.OpenDB(formsFullName[i]);
            FormTables[i] = FormTable;
            Forms.Add(FormOpen(formsFullName[i], FormTable));
            return FormTables[i];
        }

        public SForm OpenForm(string FormFullName) {
            DataTable FormTable = loaderDb.OpenDB(FormFullName);
            return FormOpen(FormFullName, FormTable);
        }

        public int OpenForms(string Path) {
            string[] FormsFullName = Directory.GetFiles(Path, "*.scx",
                                                     SearchOption.AllDirectories);
            string[] NameProject = Directory.GetFiles(Path, "*.pjx",
                                                     SearchOption.AllDirectories);
            Name = NameProject[0];
            formsFullName = FormsFullName;
            formTables = new DataTable[formsFullName.Length];
            Forms.Clear();
            AllObjectsCounter = 0;
            for (int i = 0; i < formsFullName.Length; i++) {
                OpenForm(i);
                onCount();
            }
            return 0;
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
        }
        /*
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
        */

        /*
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
        }*/
    }
}
