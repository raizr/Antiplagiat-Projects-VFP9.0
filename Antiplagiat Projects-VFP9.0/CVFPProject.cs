﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Security.Cryptography;

namespace Antiplagiat_Projects_VFP9._0 {
    public struct SForm {
        public string Name;
        public List<SObject> form;
        public List<SObject> commandbutton;
        public List<SObject> header;
        public List<SObject> textbox;
        public List<SObject> grid;
        public List<SObject> label;
        public List<SObject> pageframe;
        public List<SObject> editbox;
        public List<SObject> spinner;
    }
    

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

        public List<SForm> Forms;
        

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
            //FormTables[i] = loaderDb.OpenDB(formsFullName[i]);
            DataTable FormTable = loaderDb.OpenDB(formsFullName[i]);
            FormTables[i] = FormTable;
            SForm form = new SForm();
            form.Name = formsFullName[i];
            form.form = new List<SObject>(); form.commandbutton = new List<SObject>();
            form.header = new List<SObject>(); form.textbox = new List<SObject>();
            form.grid = new List<SObject>(); form.label = new List<SObject>();
            form.pageframe = new List<SObject>(); form.editbox = new List<SObject>();
            form.spinner = new List<SObject>();
            for (int j = 0; j < FormTable.Rows.Count; j++) {
                SObject obj = new SObject();
                // получение полей формы
                obj.classname = FormTable.Rows[j]["baseclass"].ToString(); // имя класса
                obj.objname = FormTable.Rows[j]["objname"].ToString(); // имя объекта
                obj.methods = FormTable.Rows[j]["methods"].ToString(); // методы объекта
                string properties = FormTable.Rows[j]["properties"].ToString(); //свойства
                //преобразование свойства из строки в пары (свойство - значение свойства)
                // разделение одной строки свойств на строки с одним свойством
                    string[] prop = properties.Split('\n'); 
                    obj.properties = new Dictionary<string, string>();
                    for (int k = 0; k < prop.Length-1; k++) {
                        prop[k] = prop[k].Replace("\r", "");
                    //разделение одного свойства на имя и значение
                        string[] PropAndValue = prop[k].Split('=');
                        if(PropAndValue.Length > 1) {
                            obj.properties.Add(PropAndValue[0], PropAndValue[1]);
                        }
                }
                if (obj.classname.Length > 0 && obj.objname.Length > 0 &&
                    obj.properties.Count > 0) {
                    // запись в структуру объекта формы в зависимости от класса объекта
                    switch (obj.classname) {
                        case "form": form.form.Add(obj);
                                     break;
                        case "commandbutton": form.commandbutton.Add(obj);
                                     break;
                        case "header": form.header.Add(obj);
                                     break;
                        case "textbox": form.textbox.Add(obj);
                                     break;
                        case "grid": form.grid.Add(obj);
                                     break;
                        case "label": form.label.Add(obj);
                                     break;
                        case "pageframe": form.pageframe.Add(obj);
                                     break;
                        case "editbox": form.editbox.Add(obj);
                                     break;
                        case "spinner": form.spinner.Add(obj);
                                     break;
                    }
                }
            }
            Forms.Add(form);
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
            /*Console.WriteLine("Хэши таблиц: " + hashTables.Length + " " + tablesFullName.Length);
            Console.WriteLine("Хэши форм: " + hashForms.Length + " " + formsFullName.Length);
            */
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
