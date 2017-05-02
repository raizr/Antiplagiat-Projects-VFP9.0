using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Antiplagiat_Projects_VFP9._0 {
    public struct SCheckInfo {
        public int EqualNum;
        public bool IsTable;
        public string ProjectName;
        public string StudentName;
        public string FileName;
        public string Hash;
    }

    public class CVerification {
        private int rate = 0;
        private DataTable ReferenceProject;
        private List<SCheckInfo> EqualsInfo;
        //Проверка при открытии по именам файлов и их хэш-суммам
        public List<SCheckInfo> OpenCheck(DataTable InspectProject,
                                     DataTable Projects) {
            DataColumn column;
            column = new DataColumn();
            column = new DataColumn();
            ReferenceProject = new DataTable("Project Hash File");
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "FileName";
            ReferenceProject.Columns.Add(column);
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Hash";
            ReferenceProject.Columns.Add(column);
            EqualsInfo = new List<SCheckInfo>();
            for (int i = 0; i < Projects.Rows.Count; i++) {
                string cell = Projects.Rows[i][1].ToString(); // Путь к проекту
                ReferenceProject.ReadXml(cell.Substring(0,
                                         cell.Length - 4) + ".xml");
                for (int j = 0, t = 0, f = 0; j < ReferenceProject.Rows.Count; j++) {
                    for(int k = 0; k < InspectProject.Rows.Count; k++) {
                        // Сравнение по имени файла и ...
                        if (ReferenceProject.Rows[j][0].ToString() == InspectProject.Rows[k][0].ToString() ||
                            // ... по хэш-сумме
                            ReferenceProject.Rows[j][1].ToString() == InspectProject.Rows[k][1].ToString()) {
                            SCheckInfo Info = new SCheckInfo();
                            string pname = InspectProject.Rows[k][0].ToString();
                            if(pname.Substring(pname.Length - 3, 3) == "dbf") {
                                Info.EqualNum = t++;
                                Info.IsTable = true;
                            } else {
                                Info.EqualNum = f++;
                                Info.IsTable = false;
                            }
                            Info.ProjectName = cell;
                            Info.StudentName = Projects.Rows[i][0].ToString();
                            Info.FileName = ReferenceProject.Rows[j][0].ToString();
                            Info.Hash = ReferenceProject.Rows[j][1].ToString();
                            EqualsInfo.Add(Info);
                        }
                    }
                }
                ReferenceProject.Clear();
            }
            return EqualsInfo;
        }

        public int CheckFileName(string[] InspectName, string[] ReferenceName) {

            for(int i = 0; i< InspectName.Length; i++) {

            }


            return rate;
        }
    }
}
