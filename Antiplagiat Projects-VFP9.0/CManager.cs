using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace Antiplagiat_Projects_VFP9._0 {
   public class CManager {

        private string PathConf;
        public CVFPProject InspectProject;
        public CVFPProject ReferenceProject;
        public CDateBase DBase;
        private CVerification Verificator;
        public DataTable ViewTable;
        public List<SCheckInfo> CheckOpenList;
        public CManager() {
            PathConf = Properties.Settings.Default.BDPath;
            InspectProject = new CVFPProject();
            ReferenceProject = new CVFPProject();
            DBase = new CDateBase();
            Verificator = new CVerification();
        }

        public string OpenProject(string PathPr) {
            string[] TablesFullName = Directory.GetFiles(PathPr, "*.dbf",
                                                         SearchOption.AllDirectories);
            string[] FormsFullName = Directory.GetFiles(PathPr, "*.scx",
                                                     SearchOption.AllDirectories);
            string[] NameProject = Directory.GetFiles(PathPr, "*.pjx",
                                                     SearchOption.AllDirectories);
            InspectProject.Name = NameProject[0];
            InspectProject.Open(TablesFullName, FormsFullName);
            DataTable e = DBase.GenerateProjectTable(InspectProject);
            CheckOpenList = Verificator.OpenCheck(e,
                                    DBase.Projects);
            foreach(SCheckInfo i in CheckOpenList) {
                Console.WriteLine("№"+i.EqualNum+" "+i.FileName + " " + i.ProjectName+" "+i.StudentName);
            }
            return InspectProject.Name;
        }

        public void CheckProject() {
            
        }

        public void SaveOpenProjectToBD() {
            DBase.SaveProject(InspectProject, InspectProject.Name);
        }


    }
}
