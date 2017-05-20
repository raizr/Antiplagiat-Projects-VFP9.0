using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                       List<SCheckElementInfo> FilesInfo/*,
                       List<SCheckCellInfo> ListCellInfo,
                       List<SCheckColumnInfo> ListColumnInfo*/) {
            /*this.ListCellInfo = ListCellInfo;
            this.ListColumnInfo = ListColumnInfo;*/
            List<SCheckElementInfo> FindTable = FilesInfo.FindAll(x => x.Type == 'R');
            for (int i = 0; i < TablesName.Length; i++) {
                CRowReport row = new CRowReport(NameProject, TablesName[i],
                                                FindTable[i].StudentName, FindTable[i].FileName,0);
                RowsReport.Add(row);
            }
            List<SCheckElementInfo> FindForms = FilesInfo.FindAll(x => x.Type == 'O');
            for (int i = 0; i < FormsName.Length; i++) {
                CRowReport row = new CRowReport(NameProject, FormsName[i],
                                                FindForms[i].StudentName, FindForms[i].FileName, 0);
                RowsReport.Add(row);
            }
        }

        public List<CRowReport> GetSource() {
            return RowsReport;
        }
    }
}
