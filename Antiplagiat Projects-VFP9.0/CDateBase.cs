using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
namespace Antiplagiat_Projects_VFP9._0 {
    public class CDateBase {

        public DataTable Projects; // таблица всех проектов
        private DataTable FilesProjectHash; // таблиц файлов одного проекта с их хэшами
        public CDateBase() {
            Projects = new DataTable("Projects Path");

            DataColumn column;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ФИО студента, группа";
            Projects.Columns.Add(column);
            
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Путь к проекту";
            Projects.Columns.Add(column);

            column = new DataColumn();
            FilesProjectHash = new DataTable("Project Hash File");
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "FileName";
            FilesProjectHash.Columns.Add(column);
            
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Hash";
            FilesProjectHash.Columns.Add(column);
        }

        public void Save() {
            Projects.WriteXml("projects.xml");
        }

        public DataTable Load() {
            Projects.Clear();
            if (File.Exists("projects.xml")) {
                Projects.ReadXml("projects.xml");
            } else {
                File.Create("projects.xml");
                Projects.ReadXml("projects.xml");
            }
            return Projects;
        }

        public DataTable GetProject(int index) {
            if(Projects.Rows.Count > 0) {
                DataRow row = Projects.Rows[index];
                string s = row["Путь к проекту"].ToString();
                FilesProjectHash.ReadXml(s.Substring(0, s.Length - 4) + ".xml");
            } else {
                Load();
                DataRow row = Projects.Rows[index];
                string s = row["Путь к проекту"].ToString();
                FilesProjectHash.ReadXml(s.Substring(0, s.Length - 4) + ".xml");
            }
            return FilesProjectHash;
        }

        public bool CheckProject(string Name) {
            for (int i = 0; i < Projects.Rows.Count; i++) {
                if (Projects.Rows[i][1].ToString() == Name) {
                    return false;
                }
            }
            return true;
        }

        public DataTable GenerateProjectTable(CVFPProject project) {
            DataRow newRow;
            FilesProjectHash.Clear();
            // Сохранение путей файлов таблиц и их хэшей
            for (int i = 0; i < project.TablesFullName.Length; i++) {
                newRow = FilesProjectHash.NewRow();
                newRow["FileName"] = Path.GetFileName(project.TablesFullName[i]); 
                newRow["Hash"] = project.HashTables[i];
                FilesProjectHash.Rows.Add(newRow);
            }
            // Сохранение путей файлов форм и их хэшей
            for (int i = 0; i < project.FormsFullName.Length; i++) {
                newRow = FilesProjectHash.NewRow();
                newRow["FileName"] = Path.GetFileName(project.FormsFullName[i]);
                newRow["Hash"] = project.HashForms[i];
                FilesProjectHash.Rows.Add(newRow);
            }
            FilesProjectHash.WriteXml(project.Name.Substring(0, project.Name.Length - 4) + ".xml");
            return FilesProjectHash;
        }

        public void SaveProject(CVFPProject project, string path) {
            DataRow newRow;
            Console.WriteLine(path);
            if (CheckProject(path)) {
                FilesProjectHash.Clear();
                newRow = Projects.NewRow();
                newRow[0] = "";
                newRow[1] = path;
                Projects.Rows.Add(newRow);
                
                // Сохранение путей файлов таблиц и их хэшей
                for (int i = 0; i < project.TablesFullName.Length; i++) {
                    newRow = FilesProjectHash.NewRow();
                    newRow["FileName"] = Path.GetFileName(project.TablesFullName[i]);
                    newRow["Hash"] = project.HashTables[i];
                    FilesProjectHash.Rows.Add(newRow);
                }
                // Сохранение путей файлов форм и их хэшей
                for (int i = 0; i < project.FormsFullName.Length; i++) {
                    newRow = FilesProjectHash.NewRow();
                    newRow["FileName"] = Path.GetFileName(project.FormsFullName[i]);
                    newRow["Hash"] = project.HashForms[i];
                    FilesProjectHash.Rows.Add(newRow);
                }
                FilesProjectHash.WriteXml(path.Substring(0, path.Length - 4) + ".xml");
                FilesProjectHash.Clear();
                Save();
            } else {
                Console.WriteLine("Этот проект уже сущесвует");
            }
            
        }
    }
}
