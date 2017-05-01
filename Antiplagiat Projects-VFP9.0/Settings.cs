using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Antiplagiat_Projects_VFP9._0 {
    public partial class Settings : Form {
        public Settings(CManager ptr) {
            InitializeComponent();
            manager = ptr;
            textBoxPathDBKP.Text = Properties.Settings.Default.BDPath;
            Console.WriteLine(Properties.Settings.Default.BDPath);
        }

        private CManager manager;
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e) {
            if (e.Node.Text == "База КП") {
                dataGridViewKP.Enabled = true;
                buttonAddKP.Enabled = true;
                buttonDelProject.Enabled = true;
                buttonOpenDBProjects.Enabled = true;
                dataGridViewKP.Visible = true;
                buttonAddKP.Visible = true;
                buttonDelProject.Visible = true;
                buttonOpenDBProjects.Visible = true;
                    
                if(dataGridViewKP.DataSource != null)
                    ((DataTable)dataGridViewKP.DataSource).Clear();
                dataGridViewKP.DataSource = manager.DBase.Load();
                dataGridViewKP.AutoResizeColumns(
                    DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            } else {
                manager.DBase.Save();
                dataGridViewKP.Enabled = false;
                buttonAddKP.Enabled = false;
                buttonDelProject.Enabled = false;
                buttonOpenDBProjects.Enabled = false;
                dataGridViewKP.Visible = false;
                buttonAddKP.Visible = false;
                buttonDelProject.Visible = false;
                buttonOpenDBProjects.Visible = false;
            }
        }

        private void dataSetProjectsBindingSource_CurrentChanged(object sender, EventArgs e) {

        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e) {
            
        }

        private void Settings_FormClosed(object sender, FormClosedEventArgs e) {
            manager.DBase.Save();
            Properties.Settings.Default.BDPath = textBoxPathDBKP.Text;
            Properties.Settings.Default.Save();
        }

        private void buttonDelProject_Click(object sender, EventArgs e) {
            if (dataGridViewKP.SelectedRows.Count > 0) {
                if (dataGridViewKP.SelectedRows[0].IsNewRow == false) {
                    dataGridViewKP.Rows.RemoveAt(dataGridViewKP.SelectedRows[0].Index);
                }
            }
            manager.DBase.Save();
        }

        private void buttonOpenDBProjects_Click(object sender, EventArgs e) {
            if (folderBrowserDialogKP.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                textBoxPathDBKP.Text = folderBrowserDialogKP.SelectedPath;
                
            }
        }

        private void buttonAddKP_Click(object sender, EventArgs e) {
            if(textBoxPathDBKP.Text.Length == 0) {
                MessageBox.Show("Укажите путь к папкам КП");
            }else {
                CVFPProject VFProject = new CVFPProject();
                Properties.Settings.Default.Save(); 
                string[] Projects = Directory.GetFiles(folderBrowserDialogKP.SelectedPath,
                                                         "*.pjx",
                                                         SearchOption.AllDirectories);
                string prjpath;
                for (int i = 0; i < Projects.Length; i++) {
                    prjpath = Path.GetDirectoryName(Projects[i]);
                    string[] TablesFullName = Directory.GetFiles(prjpath,
                                                                 "*.dbf",
                                                                 SearchOption.AllDirectories);
                    string[] FormsFullName = Directory.GetFiles(prjpath,
                                                             "*.scx",
                                                             SearchOption.AllDirectories);
                    VFProject.Name = Path.GetFileName(Projects[i]);
                    VFProject.Open(TablesFullName, FormsFullName);
                    manager.DBase.SaveProject(VFProject, Projects[i]);
                }
                dataGridViewKP.Refresh();
            }
        }
    }
}
