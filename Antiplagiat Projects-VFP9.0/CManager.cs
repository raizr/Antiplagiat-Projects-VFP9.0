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
        public CVerification Verificator;
        public DataTable ViewTable;
        public List<SCheckColumnInfo> CheckColumnList;
        public bool[][] EqualColumnsInfo;
        public List<SCheckCellInfo> CheckCellList;

        public CManager() {
            PathConf = Properties.Settings.Default.BDPath;
            InspectProject = new CVFPProject();
            ReferenceProject = new CVFPProject();
            DBase = new CDateBase();
            Verificator = new CVerification();
        }

        public string OpenProject(string PathPr) {
            /*string[] TablesFullName = Directory.GetFiles(PathPr, "*.dbf",
                                                         SearchOption.AllDirectories);
            string[] FormsFullName = Directory.GetFiles(PathPr, "*.scx",
                                                     SearchOption.AllDirectories);*/
            string[] NameProject = Directory.GetFiles(PathPr, "*.pjx",
                                                     SearchOption.AllDirectories);
            InspectProject.Open(PathPr);
            if (NameProject.Length == 0) {
                Console.Write("Файл проекта отсутствует");
                return null;
            } else {
                /*InspectProject.Name = NameProject[0];
                InspectProject.Open(TablesFullName, FormsFullName);*/
                //генерация файлов (имя файла, хэш-сумма)
                DataTable e = DBase.GenerateProjectTable(InspectProject);
                CheckColumnList = Verificator.OpenCheck(e,
                                        DBase.Projects);
                if (CheckColumnList != null)
                    return InspectProject.Name;
                else
                    return "null";
            }
            
        }

        public bool[][] CheckProject() {
            if (InspectProject.Name != null) {
                EqualColumnsInfo = Verificator.CheckTables(InspectProject, DBase.Projects);
                if (Verificator.ListCellInfo != null) {
                    CheckCellList = Verificator.ListCellInfo;
                    //Console.WriteLine(InspectProject.TablesTables.Length);
                }
                return EqualColumnsInfo;
            }
            else
                return null;
        }

        public void SaveOpenProjectToBD() {
            DBase.SaveProject(InspectProject, InspectProject.Name);
        }
    }
}
