using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace Antiplagiat_Projects_VFP9._0 {
    public class CRowReport {
        private string nameproject;
        public string NameProject
        {
            get
            {
                return nameproject;
            }
        }
        public string insFileName;
        public string InsFileName {
            get
            {
                return insFileName;
            }
        }
        public string refStudentName;
        public string RefStudentName
        {
            get
            {
                return refStudentName;
            }
        }
        private string refFileName;
        public string RefFileName
        {
            get
            {
                return refFileName;
            }
        }
        private int percentPlagFile;
        public int PercentPlagFile
        {
            get
            {
                return percentPlagFile;
            }
        }

        public CRowReport(string NameProject, string InsFileName, string RefStudentName,
                          string RefFileName, int PercentPlagFile) {
            this.nameproject = NameProject;
            this.insFileName = InsFileName;
            this.refStudentName = RefStudentName;
            this.refFileName = RefFileName;
            this.percentPlagFile = PercentPlagFile;
        }
    }
    
    public  class CReport {

        public List<CRowReport> RowsReport = new List<CRowReport>();

        public CReport(string NameProject, 
                       string[] TablesName,
                       string[] FormsName,
                       List<SCheckElementInfo> FilesInfo,
                       List<SCheckCellInfo> ListCellInfo, List<SForm> Forms) {
            /*this.ListCellInfo = ListCellInfo;
            this.ListColumnInfo = ListColumnInfo;*/
            List<SCheckElementInfo> FindTable = FilesInfo.FindAll(x => x.Type == 'R' || x.Type == 'C');
            for (int i = 0; i < TablesName.Length; i++) {
                CRowReport row = new CRowReport(NameProject, TablesName[i],
                                                FindTable[i].StudentName,
                                                "Файл таблицы "+FindTable[i].FileName,0);
                RowsReport.Add(row);
                List<SCheckCellInfo> FindCell = 
                                    ListCellInfo.FindAll(x => 
                                    Path.GetFileName(x.FileName) == FindTable[i].FileName);
                for(int j = 0; j < FindCell.Count; j++) {
                    string str = FindCell[j].RefrowIndex == -1 ?
                        "Поле таблицы " + FindCell[j].RefColumnIndex :
                        "Ячейка таблицы " + FindCell[j].rowIndex;
                    CRowReport rowCell = new CRowReport("", "",
                                                "", 
                                                str, 0);
                    RowsReport.Add(rowCell);
                }
                
            }
            List<SCheckElementInfo> FindForms = FilesInfo.FindAll(x => x.Type == 'O');
            for (int i = 0; i < FormsName.Length; i++) {
                CRowReport row = new CRowReport(NameProject, FormsName[i],
                                                FindForms[i].StudentName,
                                                "Файл формы "+FindForms[i].FileName, 0);
                RowsReport.Add(row);
                List<SForm> FindObjForms = Forms.FindAll(x =>
                                            Path.GetFileName(x.Name) == FindForms[i].FileName);
                for (int j = 0; j < FindObjForms.Count; j++) {
                    SForm FormStruct = FindObjForms[j];
                    Type type = typeof(SForm);
                    var fields = type.GetFields();
                    foreach (FieldInfo fi in fields) {
                        if (fi.Name != "Name") {
                            List<SObject> list = (List<SObject>)fi.GetValue(FormStruct);
                            for (int k = 0; k < list.Count; k++) {
                                
                                CRowReport ObjRow = new CRowReport("", "",
                                                    "", "Объект "+ list[k].objname, 0);
                                RowsReport.Add(ObjRow);
                            }
                        }
                    }
                                
                    
                }
            }
        }

        public List<CRowReport> GetSource() {
            return RowsReport;
        }
    }
}
