using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Antiplagiat_Projects_VFP9._0 {
   public class CManager {

        private string PathConf;
        private Stream ConfFile;
        public CVFPProject Project;
        public CDateBase DBase;

        public CManager() {
            PathConf = Properties.Settings.Default.BDPath;
            Project = new CVFPProject();
            DBase = new CDateBase();
        }

        public string OpenProject(string PathPr) {
            string[] TablesFullName = Directory.GetFiles(PathPr,
                                                         "*.dbf",
                                                         SearchOption.AllDirectories);
            string[] FormsFullName = Directory.GetFiles(PathPr,
                                                     "*.scx",
                                                     SearchOption.AllDirectories);
            string[] NameProject = Directory.GetFiles(PathPr,
                                                     "*.pjx",
                                                     SearchOption.AllDirectories);
            Project.Name = Path.GetFileName(NameProject[0]);
            Project.Open(TablesFullName, FormsFullName);
            return Project.Name;
        }

        public void SaveOpenProjectToBD() {
            DBase.SaveProject(Project, Project.Name);
        }


    }
}
