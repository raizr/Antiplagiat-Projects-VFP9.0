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
        public List<SCheckFormInfo> CheckFormList;

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
                //генерация файлов (имя файла, хэш-сумма)
                DataTable e = DBase.GenerateProjectTable(InspectProject);
                CheckColumnList = Verificator.OpenCheck(e,
                                        DBase.Projects);
                /*for(int i = 0; i < InspectProject.Forms.Count; i++) {
                    Console.WriteLine(InspectProject.Forms[i].form[0].objname + " " +
                                        InspectProject.Forms[i].editbox[0].objname + " " +
                                        InspectProject.Forms[i].textbox[0].objname);
                }*/
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
                }
                Verificator.CheckForms(InspectProject, DBase.Projects);
                if (Verificator.ListFormInfo != null) {
                    CheckFormList = Verificator.ListFormInfo;
                    CheckFormList = Verificator.ListCommandbutton;
                    Console.Write("\n\n");
                    for(int i = 0;i < CheckFormList.Count; i++) {
                        Console.WriteLine(CheckFormList[i].FormIndex + " " +
                            CheckFormList[i].RefFormIndex + " "+ CheckFormList[i].FileName);
                    }
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
