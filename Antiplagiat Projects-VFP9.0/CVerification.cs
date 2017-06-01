using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Reflection;

namespace Antiplagiat_Projects_VFP9._0 {
    public struct SCheckElementInfo {
        public int EqualNum;
        public char Type;
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
        private List<SCheckElementInfo> ListColumnInfo;
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
            ListColumnInfo = new List<SCheckElementInfo>();
            ListCellInfo = new List<SCheckCellInfo>();
        }
        //Проверка при открытии по именам файлов и их хэш-суммам
        public List<SCheckElementInfo> OpenCheck(DataTable InspectProject,
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
                                List<SCheckElementInfo> FindTable = ListColumnInfo.FindAll(x => (x.EqualNum == t && x.Type == 'R'));
                                List<SCheckElementInfo> FindForm = ListColumnInfo.FindAll(x => (x.EqualNum == f && x.Type == 'O'));
                                if (FindTable.Count == 0 || FindForm.Count == 0) {
                                    SCheckElementInfo Info = new SCheckElementInfo();
                                    string pname = InspectProject.Rows[k][0].ToString();
                                    if (pname.Substring(pname.Length - 3, 3) == "dbf") {
                                        if (FindTable.Count == 0) {
                                            Info.EqualNum = t++;
                                            Info.Type = 'R';
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
                                                Info.Type = 'O';
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
            PerTablesColumns = 0; 
            PerTablesCells = 0; 
            for (int i = 0; i < Projects.Rows.Count; i++) {
                ReferenceProject.OpenTables(Path.GetDirectoryName(Projects.Rows[i][1].ToString()));
                onCount();
                for (int j = 0; j < ReferenceProject.TablesTables.Length; j++) {
                    AllTablesColumns = 0; AllTablesCells = 0;
                    for (int k = 0; k < InspectProject.TablesTables.Length; k++) {
                        DataTable RefTable = ReferenceProject.TablesTables[j];
                        DataTable InsTable = InspectProject.TablesTables[k];
                        AllTablesColumns += InsTable.Columns.Count;
                        foreach (DataColumn RefColumn in RefTable.Columns) {
                            for (int n = 0; n < InsTable.Columns.Count; n++) {
                                //IsEqual[k][n] = false;
                                if (RefColumn.ColumnName == InsTable.Columns[n].ColumnName &&
                                    RefColumn.DataType == InsTable.Columns[n].DataType) {
                                    SCheckCellInfo CheckCell = new SCheckCellInfo();
                                    List<SCheckCellInfo> results = ListCellInfo.FindAll(x => (x.rowIndex == -1 && x.ColumnIndex == InsTable.Columns[n].ColumnName));
                                    if (results.Count == 0) {
                                        CheckCell.TableIndex = k;
                                        CheckCell.rowIndex = -1;
                                        CheckCell.ColumnIndex = InsTable.Columns[n].ColumnName;
                                        CheckCell.FileName = ReferenceProject.TablesFullName[j];
                                        CheckCell.ProjectName = Projects.Rows[i][1].ToString();
                                        CheckCell.StudentName = Projects.Rows[i][0].ToString();
                                        CheckCell.RefrowIndex = -1;
                                        CheckCell.RefColumnIndex = RefColumn.ColumnName;
                                        ListCellInfo.Add(CheckCell);
                                        PerTablesColumns++;
                                        List<SCheckElementInfo> FindTable =
                                            ListColumnInfo.FindAll(x => (x.EqualNum == k && x.Type == 'C'));
                                        if (FindTable.Count == 0) {
                                            SCheckElementInfo Info = new SCheckElementInfo();
                                            Info.EqualNum = k;
                                            Info.Type = 'C';
                                            Info.ProjectName = Projects.Rows[i][1].ToString();
                                            Info.StudentName = Projects.Rows[i][0].ToString();
                                            Info.FileName = ReferenceProject.TablesFullName[j];
                                            //Info.Hash = ReferenceTable.Rows[j][1].ToString();
                                            ListColumnInfo.Add(Info);

                                        }
                                    } //Console.WriteLine("true");
                                }
                            }
                        }
                        AllTablesCells += InsTable.Rows.Count*InsTable.Columns.Count;
                        for (int m = 0; m < RefTable.Rows.Count; m++) {
                            for (int b = 0; b < InsTable.Rows.Count; b++) {
                                foreach (DataColumn colRef in RefTable.Columns) {
                                    foreach (DataColumn colIn in InsTable.Columns) {
                                        if (Equals(RefTable.Rows[m][colRef.ColumnName],
                                                    InsTable.Rows[b][colIn.ColumnName])) {
                                            SCheckCellInfo CheckCell = new SCheckCellInfo();
                                            List<SCheckCellInfo> results = ListCellInfo.FindAll(x => (x.rowIndex == b && x.ColumnIndex == colIn.ColumnName));
                                            if (results.Count == 0) {
                                                PerTablesCells++;
                                                CheckCell.TableIndex = k;
                                                CheckCell.rowIndex = b;
                                                CheckCell.ColumnIndex = colIn.ColumnName;
                                                CheckCell.FileName = ReferenceProject.TablesFullName[j];
                                                CheckCell.ProjectName = Projects.Rows[i][1].ToString();
                                                CheckCell.StudentName = Projects.Rows[i][0].ToString();
                                                CheckCell.RefrowIndex = m;
                                                CheckCell.RefColumnIndex = colRef.ColumnName;
                                                ListCellInfo.Add(CheckCell);
                                                List<SCheckElementInfo> FindTable =
                                                ListColumnInfo.FindAll(x => (x.EqualNum == k && x.Type == 'R'));
                                                if (FindTable.Count == 0) {
                                                    SCheckElementInfo Info = new SCheckElementInfo();
                                                    Info.EqualNum = k;
                                                    Info.Type = 'R';
                                                    Info.ProjectName = Projects.Rows[i][1].ToString();
                                                    Info.StudentName = Projects.Rows[i][0].ToString();
                                                    Info.FileName = ReferenceProject.TablesFullName[j];
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
            return IsEqual;
        }

        public void CheckForms(CVFPProject InspectProject, DataTable Projects) {
            ReferenceProject = new CVFPProject();
            PerForms = 0;
            for (int i = 0; i < Projects.Rows.Count; i++) {
                onCount();
                ReferenceProject.OpenForms(Path.GetDirectoryName(Projects.Rows[i][1].ToString()));
                for (int j = 0; j < ReferenceProject.Forms.Count; j++) {
                    SForm RefFormStruct = ReferenceProject.Forms[j];
                    Type Reftype = typeof(SForm);
                    var Reffields = Reftype.GetFields();
                    AllObjects = 0;
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
                                    //AllObjects += Instlist.Count;
                                    for (int n = 0; n < Reftlist.Count; n++, RefIndexCount++) {
                                        for (int m = 0; m < Instlist.Count; m++) {
                                            if (Instlist[m] == Reftlist[n] && Instlist[m].IsPlagiarism == false) {
                                                PerForms++;
                                                SObject obj = new SObject();
                                                for (int t = 0; t < Instlist[m].methods.Count; t++) {
                                                    if (Enumerable.SequenceEqual(Instlist[m].methods[t].Hash, Reftlist[n].methods[t].Hash)) {
                                                        SMethod method = new SMethod();
                                                        method.Method = Instlist[m].methods[t].Method;
                                                        method.Hash = Instlist[m].methods[t].Hash;
                                                        method.RefMethod = Reftlist[m].methods[t].Method;
                                                        //obj.methods = new List<SMethod>();
                                                        obj.methods = Instlist[m].methods;
                                                        obj.methods[t] = method;
                                                    }
                                                }
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
                                                List<SCheckElementInfo> FindTable =
                                                 ListColumnInfo.FindAll(x => (x.EqualNum == k && x.Type == 'O'));
                                                if (FindTable.Count == 0) {
                                                    SCheckElementInfo Info = new SCheckElementInfo();
                                                    Info.EqualNum = k;
                                                    Info.Type = 'O';
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

        public List<SCheckElementInfo> GetListFilesInfo() {
            return ListColumnInfo;
        }
    }
}
