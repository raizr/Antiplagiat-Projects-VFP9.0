using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;

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
        public string ProjectName;
        public string StudentName;
        public string FileName;
        public string Hash;
    }

    public class CVerification {
        private DataTable ReferenceTable;
        private CVFPProject ReferenceProject;
        private List<SCheckColumnInfo> ListColumnInfo;
        public List<SCheckCellInfo> ListCellInfo;

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
            ListColumnInfo = new List<SCheckColumnInfo>();
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
                                SCheckColumnInfo Info = new SCheckColumnInfo();
                                string pname = InspectProject.Rows[k][0].ToString();
                                if (pname.Substring(pname.Length - 3, 3) == "dbf") {
                                    Info.EqualNum = t++;
                                    Info.IsTable = true;
                                } else {
                                    if(pname.Substring(pname.Length - 3, 3) == "scx") {
                                        Info.EqualNum = f++;
                                        Info.IsTable = false;
                                    }
                                    
                                }
                                Info.ProjectName = cell;
                                Info.StudentName = Projects.Rows[i][0].ToString();
                                Info.FileName = ReferenceTable.Rows[j][0].ToString();
                                Info.Hash = ReferenceTable.Rows[j][1].ToString();
                                ListColumnInfo.Add(Info);
                                
                            }
                        }
                    }
                    ReferenceTable.Clear();
                }
            }
            catch (System.IO.FileNotFoundException e) {
                Console.WriteLine("\""+e.FileName + "\" не найден");
                return null;
            }

            return ListColumnInfo;
        }

        public bool[][] CheckTables(CVFPProject InspectProject, DataTable Projects) {
            ReferenceProject = new CVFPProject();
            bool[][] IsEqual = new bool[InspectProject.TablesTables.Length][];
            for(int i = 0; i < InspectProject.TablesTables.Length; i++) {
                    IsEqual[i] = new bool[InspectProject.TablesTables[i].Columns.Count];
            }
            ListCellInfo = new List<SCheckCellInfo>();
            for (int i = 0; i < Projects.Rows.Count; i++) {
                ReferenceProject.Open(Path.GetDirectoryName(Projects.Rows[i][1].ToString()));
                for(int j = 0; j < ReferenceProject.TablesTables.Length; j++) {
                    for (int k = 0; k < InspectProject.TablesTables.Length; k++) {
                        DataTable RefTable = ReferenceProject.TablesTables[j];
                        DataTable InsTable = InspectProject.TablesTables[k];
                        foreach (DataColumn RefColumn in RefTable.Columns) {
                            for(int n = 0; n < InsTable.Columns.Count;n++) {
                                //IsEqual[k][n] = false;
                                if (RefColumn.ColumnName == InsTable.Columns[n].ColumnName &&
                                    RefColumn.DataType == InsTable.Columns[n].DataType) {
                                    IsEqual[k][n] = true;
                                    //Console.WriteLine("true");
                                }
                            }
                        }

                        for(int m = 0; m < RefTable.Rows.Count; m++) {
                            for (int b = 0; b < InsTable.Rows.Count; b++) {
                                foreach (DataColumn colRef in RefTable.Columns) {
                                    foreach (DataColumn colIn in InsTable.Columns) {
                                        if (Equals(RefTable.Rows[m][colRef.ColumnName],
                                                    InsTable.Rows[b][colIn.ColumnName])) {
                                            SCheckCellInfo CheckCell = new SCheckCellInfo();
                                            CheckCell.TableIndex = k;
                                            CheckCell.rowIndex = b;
                                            CheckCell.ColumnIndex = colIn.ColumnName;
                                            CheckCell.FileName = ReferenceProject.TablesFullName[j];
                                            CheckCell.ProjectName = Projects.Rows[i][1].ToString();
                                            CheckCell.StudentName = Projects.Rows[i][0].ToString();
                                            ListCellInfo.Add(CheckCell);
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
    }
}
