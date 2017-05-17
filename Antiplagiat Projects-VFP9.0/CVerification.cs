using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Reflection;

namespace Antiplagiat_Projects_VFP9._0 {
    public struct SCheckColumnInfo {
        public int EqualNum;
        public bool IsTable;
        public string ProjectName;
        public string StudentName;
        public string FileName;
        public string Hash;
    }

    public struct SCheckCellInfo {
        public int TableIndex;
        public int rowIndex;
        public string ColumnIndex;
        public int RefrowIndex;
        public string RefColumnIndex;
        public string ProjectName;
        public string StudentName;
        public string FileName;
    }

    public class CVerification {
        private DataTable ReferenceTable;
        private CVFPProject ReferenceProject;
        private List<SCheckColumnInfo> ListColumnInfo;
        public List<SCheckCellInfo> ListCellInfo;
        public delegate void MethodContainer();
        public int PerOpenTables = 0;
        public int PerOpenForms = 0;
        public int PerTables = 0;
        public int AllTablesColumns = 0;
        public int PerTablesColumns = 0;
        public int AllTablesCells = 0;
        public int PerTablesCells = 0;
        public int PerForms = 0;
        public int AllObjects = 0;
        public int PerObjects = 0;
        public event MethodContainer onCount = delegate { };
        public CVerification() {
            ListColumnInfo = new List<SCheckColumnInfo>();
            ListCellInfo = new List<SCheckCellInfo>();
        }
        //Проверка при открытии по именам файлов и их хэш-суммам
        public List<SCheckColumnInfo> OpenCheck(DataTable InspectProject,
                                     DataTable Projects) {
            DataColumn column;
            column = new DataColumn();
            column = new DataColumn();
            ReferenceTable = new DataTable("Project Hash File");
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "FileName";
            ReferenceTable.Columns.Add(column);
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Hash";
            ReferenceTable.Columns.Add(column);
            ListColumnInfo.Clear();
            PerOpenTables = 0;
            PerOpenForms = 0;
            try {
                for (int i = 0; i < Projects.Rows.Count; i++) {
                    string cell = Projects.Rows[i][1].ToString(); // Путь к проекту
                    ReferenceTable.ReadXml(cell.Substring(0,
                                             cell.Length - 4) + ".xml");
                    for (int j = 0, t = 0, f = 0; j < ReferenceTable.Rows.Count; j++) {
                        for (int k = 0; k < InspectProject.Rows.Count; k++) {
                            // Сравнение по имени файла и ...
                            if (ReferenceTable.Rows[j][0].ToString() == InspectProject.Rows[k][0].ToString() ||
                                // ... по хэш-сумме
                                ReferenceTable.Rows[j][1].ToString() == InspectProject.Rows[k][1].ToString()) {
                                List<SCheckColumnInfo> FindTable = ListColumnInfo.FindAll(x => (x.EqualNum == t && x.IsTable ==true));
                                List<SCheckColumnInfo> FindForm = ListColumnInfo.FindAll(x => (x.EqualNum == f && x.IsTable == false));
                                if (FindTable.Count == 0 || FindForm.Count == 0) {
                                    SCheckColumnInfo Info = new SCheckColumnInfo();
                                    string pname = InspectProject.Rows[k][0].ToString();
                                    if (pname.Substring(pname.Length - 3, 3) == "dbf") {
                                        if (FindTable.Count == 0) {
                                            Info.EqualNum = t++;
                                            Info.IsTable = true;
                                            Info.ProjectName = cell;
                                            Info.StudentName = Projects.Rows[i][0].ToString();
                                            Info.FileName = ReferenceTable.Rows[j][0].ToString();
                                            Info.Hash = ReferenceTable.Rows[j][1].ToString();
                                            ListColumnInfo.Add(Info);
                                            PerOpenTables++;
                                        }
                                    } else {
                                        if (pname.Substring(pname.Length - 3, 3) == "scx") {
                                            if (FindForm.Count == 0) {
                                                //Console.WriteLine("№Формы: " + f);
                                                Info.EqualNum = f++;
                                                Info.IsTable = false;
                                                Info.ProjectName = cell;
                                                Info.StudentName = Projects.Rows[i][0].ToString();
                                                Info.FileName = ReferenceTable.Rows[j][0].ToString();
                                                Info.Hash = ReferenceTable.Rows[j][1].ToString();
                                                ListColumnInfo.Add(Info);
                                                PerOpenForms++;
                                            }
                                        }
                                    }
                                    
                                }
                            }
                        }
                    }
                    ReferenceTable.Clear();
                }
            }
            catch (System.IO.FileNotFoundException e) {
                Console.WriteLine("\"" + e.FileName + "\" не найден");
                return null;
            }

            return ListColumnInfo;
        }

        public bool[][] CheckTables(CVFPProject InspectProject, DataTable Projects) {
            ReferenceProject = new CVFPProject();
            bool[][] IsEqual = new bool[InspectProject.TablesTables.Length][];
            for (int i = 0; i < InspectProject.TablesTables.Length; i++) {
                IsEqual[i] = new bool[InspectProject.TablesTables[i].Columns.Count];
            }
            ListCellInfo.Clear();
            PerTablesColumns = 0; AllTablesColumns = 0;
            PerTablesCells = 0; AllTablesCells = 0;
            for (int i = 0; i < Projects.Rows.Count; i++) {
                ReferenceProject.Open(Path.GetDirectoryName(Projects.Rows[i][1].ToString()));
                onCount();
                for (int j = 0; j < ReferenceProject.TablesTables.Length; j++) {
                    for (int k = 0; k < InspectProject.TablesTables.Length; k++) {
                        DataTable RefTable = ReferenceProject.TablesTables[j];
                        DataTable InsTable = InspectProject.TablesTables[k];
                        AllTablesColumns = InsTable.Columns.Count;
                        foreach (DataColumn RefColumn in RefTable.Columns) {
                            for (int n = 0; n < InsTable.Columns.Count; n++) {
                                //IsEqual[k][n] = false;
                                if (RefColumn.ColumnName == InsTable.Columns[n].ColumnName &&
                                    RefColumn.DataType == InsTable.Columns[n].DataType) {
                                    IsEqual[k][n] = true;
                                    List<SCheckColumnInfo> FindTable = 
                                        ListColumnInfo.FindAll(x => (x.EqualNum == k && x.IsTable == true));
                                    if (FindTable.Count == 0) {
                                        SCheckColumnInfo Info = new SCheckColumnInfo();
                                        Info.EqualNum = k;
                                        Info.IsTable = true;
                                        Info.ProjectName = Projects.Rows[i][1].ToString();
                                        Info.StudentName = Projects.Rows[i][0].ToString();
                                        Info.FileName = ReferenceProject.TablesFullName[j];
                                        //Info.Hash = ReferenceTable.Rows[j][1].ToString();
                                        ListColumnInfo.Add(Info);
                                        PerTablesColumns++;
                                    }
                                        //Console.WriteLine("true");
                                    }
                            }
                        }
                        for (int m = 0; m < RefTable.Rows.Count; m++) {
                            AllTablesCells = InsTable.Rows.Count;
                            for (int b = 0; b < InsTable.Rows.Count; b++) {
                                foreach (DataColumn colRef in RefTable.Columns) {
                                    foreach (DataColumn colIn in InsTable.Columns) {
                                        if (Equals(RefTable.Rows[m][colRef.ColumnName],
                                                    InsTable.Rows[b][colIn.ColumnName])) {
                                            SCheckCellInfo CheckCell = new SCheckCellInfo();
                                            List<SCheckCellInfo> results = ListCellInfo.FindAll(x => (x.rowIndex == b && x.ColumnIndex == colIn.ColumnName));
                                            if (results.Count == 0) {
                                                CheckCell.TableIndex = k;
                                                CheckCell.rowIndex = b;
                                                CheckCell.ColumnIndex = colIn.ColumnName;
                                                CheckCell.FileName = ReferenceProject.TablesFullName[j];
                                                CheckCell.ProjectName = Projects.Rows[i][1].ToString();
                                                CheckCell.StudentName = Projects.Rows[i][0].ToString();
                                                CheckCell.RefrowIndex = m;
                                                CheckCell.RefColumnIndex = colRef.ColumnName;
                                                ListCellInfo.Add(CheckCell);
                                                List<SCheckColumnInfo> FindTable =
                                                ListColumnInfo.FindAll(x => (x.EqualNum == k && x.IsTable == true));
                                                if (FindTable.Count == 0) {
                                                    SCheckColumnInfo Info = new SCheckColumnInfo();
                                                    Info.EqualNum = k;
                                                    Info.IsTable = true;
                                                    Info.ProjectName = Projects.Rows[i][1].ToString();
                                                    Info.StudentName = Projects.Rows[i][0].ToString();
                                                    Info.FileName = ReferenceProject.TablesFullName[j];
                                                    //Info.Hash = ReferenceTable.Rows[j][1].ToString();
                                                    ListColumnInfo.Add(Info);
                                                    PerTablesCells++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return IsEqual;
        }

        public void CheckForms(CVFPProject InspectProject, DataTable Projects) {
            ReferenceProject = new CVFPProject();
            PerForms = 0;
            for (int i = 0; i < Projects.Rows.Count; i++) {
                onCount();
                ReferenceProject.Open(Path.GetDirectoryName(Projects.Rows[i][1].ToString()));
                for (int j = 0; j < ReferenceProject.Forms.Count; j++) {
                    SForm RefFormStruct = ReferenceProject.Forms[j];
                    Type Reftype = typeof(SForm);
                    var Reffields = Reftype.GetFields();
                    for (int k = 0; k < InspectProject.Forms.Count; k++) {
                        SForm InsFormStruct = InspectProject.Forms[k];
                        Type Instype = typeof(SForm);
                        var Insfields = Instype.GetFields();
                        int RefIndexCount = 0;
                        foreach (FieldInfo Rfi in Reffields) {
                            foreach (FieldInfo fi in Reffields) {
                                if (Rfi.Name != "Name" && fi.Name != "Name") {
                                    List<SObject> Reftlist = (List<SObject>)fi.GetValue(RefFormStruct);
                                    List<SObject> Instlist = (List<SObject>)fi.GetValue(InsFormStruct);
                                    for (int n = 0; n < Reftlist.Count; n++, RefIndexCount++) {
                                        for (int m = 0; m < Instlist.Count; m++) {
                                            if (Instlist[m] == Reftlist[n] && Instlist[m].IsPlagiarism == false) {
                                                SObject obj = new SObject();
                                                switch (fi.Name) {
                                                    case "form":
                                                        obj = InspectProject.Forms[k].form[m];
                                                        obj.IsPlagiarism = true;
                                                        obj.RefFormFullName = ReferenceProject.Forms[j].Name;
                                                        obj.RefObjectIndex = RefIndexCount;
                                                        InspectProject.Forms[k].form[m] = obj;
                                                        break;
                                                    case "commandbutton":
                                                        obj = InspectProject.Forms[k].commandbutton[m];
                                                        obj.IsPlagiarism = true;
                                                        obj.RefFormFullName = ReferenceProject.Forms[j].Name;
                                                        obj.RefObjectIndex = RefIndexCount;
                                                        InspectProject.Forms[k].commandbutton[m] = obj;
                                                        break;
                                                    case "header":
                                                        obj = InspectProject.Forms[k].header[m];
                                                        obj.IsPlagiarism = true;
                                                        obj.RefFormFullName = ReferenceProject.Forms[j].Name;
                                                        obj.RefObjectIndex = RefIndexCount;
                                                        InspectProject.Forms[k].header[m] = obj;
                                                        break;
                                                    case "textbox":
                                                        obj = InspectProject.Forms[k].textbox[m];
                                                        obj.IsPlagiarism = true;
                                                        obj.RefFormFullName = ReferenceProject.Forms[j].Name;
                                                        obj.RefObjectIndex = RefIndexCount;
                                                        InspectProject.Forms[k].textbox[m] = obj;
                                                        break;
                                                    case "grid":
                                                        obj = InspectProject.Forms[k].grid[m];
                                                        obj.IsPlagiarism = true;
                                                        obj.RefFormFullName = ReferenceProject.Forms[j].Name;
                                                        obj.RefObjectIndex = RefIndexCount;
                                                        InspectProject.Forms[k].grid[m] = obj;
                                                        break;
                                                    case "label":
                                                        obj = InspectProject.Forms[k].label[m];
                                                        obj.IsPlagiarism = true;
                                                        obj.RefFormFullName = ReferenceProject.Forms[j].Name;
                                                        obj.RefObjectIndex = RefIndexCount;
                                                        InspectProject.Forms[k].label[m] = obj;
                                                        break;
                                                    case "pageframe":
                                                        obj = InspectProject.Forms[k].pageframe[m];
                                                        obj.IsPlagiarism = true;
                                                        obj.RefFormFullName = ReferenceProject.Forms[j].Name;
                                                        obj.RefObjectIndex = RefIndexCount;
                                                        InspectProject.Forms[k].pageframe[m] = obj;
                                                        break;
                                                    case "editbox":
                                                        obj = InspectProject.Forms[k].editbox[m];
                                                        obj.IsPlagiarism = true;
                                                        obj.RefFormFullName = ReferenceProject.Forms[j].Name;
                                                        obj.RefObjectIndex = RefIndexCount;
                                                        InspectProject.Forms[k].editbox[m] = obj;
                                                        break;
                                                    case "spinner":
                                                        obj = InspectProject.Forms[k].spinner[m];
                                                        obj.IsPlagiarism = true;
                                                        obj.RefFormFullName = ReferenceProject.Forms[j].Name;
                                                        obj.RefObjectIndex = RefIndexCount;
                                                        InspectProject.Forms[k].spinner[m] = obj;
                                                        break;
                                                    case "optiongroup":
                                                        obj = InspectProject.Forms[k].optiongroup[m];
                                                        obj.IsPlagiarism = true;
                                                        obj.RefFormFullName = ReferenceProject.Forms[j].Name;
                                                        obj.RefObjectIndex = RefIndexCount;
                                                        InspectProject.Forms[k].optiongroup[m] = obj;
                                                        break;
                                                    case "checkbox":
                                                        obj = InspectProject.Forms[k].checkbox[m];
                                                        obj.IsPlagiarism = true;
                                                        obj.RefFormFullName = ReferenceProject.Forms[j].Name;
                                                        obj.RefObjectIndex = RefIndexCount;
                                                        InspectProject.Forms[k].checkbox[m] = obj;
                                                        break;
                                                    case "combobox":
                                                        obj = InspectProject.Forms[k].combobox[m];
                                                        obj.IsPlagiarism = true;
                                                        obj.RefFormFullName = ReferenceProject.Forms[j].Name;
                                                        obj.RefObjectIndex = RefIndexCount;
                                                        InspectProject.Forms[k].combobox[m] = obj;
                                                        break;
                                                    case "listbox":
                                                        obj = InspectProject.Forms[k].listbox[m];
                                                        obj.IsPlagiarism = true;
                                                        obj.RefFormFullName = ReferenceProject.Forms[j].Name;
                                                        obj.RefObjectIndex = RefIndexCount;
                                                        InspectProject.Forms[k].listbox[m] = obj;
                                                        break;
                                                }
                                                PerForms++;
                                                List<SCheckColumnInfo> FindTable =
                                                 ListColumnInfo.FindAll(x => (x.EqualNum == k && x.IsTable == false));
                                                if (FindTable.Count == 0) {
                                                    SCheckColumnInfo Info = new SCheckColumnInfo();
                                                    Info.EqualNum = k;
                                                    Info.IsTable = false;
                                                    Info.ProjectName = Projects.Rows[i][1].ToString();
                                                    Info.StudentName = Projects.Rows[i][0].ToString();
                                                    Info.FileName = ReferenceProject.Forms[j].Name;
                                                    //Info.Hash = ReferenceTable.Rows[j][1].ToString();
                                                    ListColumnInfo.Add(Info);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public List<SCheckColumnInfo> GetListFilesInfo() {
            return ListColumnInfo;
        }
    }
}
