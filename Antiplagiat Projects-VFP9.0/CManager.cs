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
            string[] NameProject = Directory.GetFiles(PathPr, "*.pjx",
                                                     SearchOption.AllDirectories);
            Console.WriteLine("Открытие проекта\n");
            InspectProject.Open(PathPr);
            if (NameProject.Length == 0) {
                Console.Write("Файл проекта отсутствует");
                return null;
            } else {
                //генерация файлов (имя файла, хэш-сумма)
                DataTable e = DBase.GenerateProjectTable(InspectProject);
                CheckColumnList = Verificator.OpenCheck(e, DBase.Projects);
                Console.WriteLine("Заимствованных таблиц: " + Verificator.PerOpenTables + "\n" +
                 "Заимствованных таблиц: " + Verificator.PerOpenForms+ "\n\n");
                if (CheckColumnList != null)
                    return InspectProject.Name;
                else
                    return "null";
            }
            
        }

        public bool[][] CheckProject() {
            Console.WriteLine("Проверка проекта начата...");
            if (InspectProject.Name != null) {
                EqualColumnsInfo = Verificator.CheckTables(InspectProject, DBase.Projects);
                Console.WriteLine("Проверено таблиц: " + InspectProject.TablesTables.Length + "\n" +
                    "Заимствованых полей в таблицах: "+Verificator.PerTablesColumns+" из "+
                    Verificator.AllTablesColumns + "\n"+
                    "Заимствованых записей в таблицах: " + Verificator.PerTablesCells + " из " +
                    Verificator.AllTablesCells);
                if (Verificator.ListCellInfo != null) {
                    CheckCellList = Verificator.ListCellInfo;
                }
                Verificator.CheckForms(InspectProject, DBase.Projects);
                Console.WriteLine("Проверено форм: " + InspectProject.Forms.Count + "\n" +
                    "Заимствованых объектов форм: " + Verificator.PerForms + " из "+
                    InspectProject.AllObjectsCounter+ "\n\n");
                return EqualColumnsInfo;
            }
            else
                return null;
        }

        public List<SCheckColumnInfo> GetListFilesInfo() {
            return Verificator.GetListFilesInfo();
        }

        public void SaveOpenProjectToBD() {
            DBase.SaveProject(InspectProject, InspectProject.Name);
        }
    }
}
