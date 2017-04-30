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
    public partial class Form1 : Form {
        public Form1(CManager ptr) {
            InitializeComponent();
            manager = ptr;
        }
        private CManager manager;
        private CVFPProject VFProject = new CVFPProject();
        public CDateBase DBase = new CDateBase();
        private string[] NameProject;
        private void button1_Click(object sender, EventArgs e) {
            
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                manager.OpenProject(folderBrowserDialog1.SelectedPath);
                string [] TablesFullName = Directory.GetFiles(folderBrowserDialog1.SelectedPath,
                                                         "*.dbf",
                                                         SearchOption.AllDirectories);
                string[] FormsFullName = Directory.GetFiles(folderBrowserDialog1.SelectedPath,
                                                         "*.scx",
                                                         SearchOption.AllDirectories);
                NameProject = Directory.GetFiles(folderBrowserDialog1.SelectedPath,
                                                         "*.pjx",
                                                         SearchOption.AllDirectories);
                VFProject.Name = Path.GetFileName(NameProject[0]); 
                label1.Text = VFProject.Name;
                VFProject.Open(TablesFullName, FormsFullName);
                /*manager.TablesFullName = TablesFullName;
                manager.FormsFullName = FormsFullName;*/
                string[] TablesName = TablesFullName.Select(file => Path.GetFileNameWithoutExtension(file)).ToArray();
                string[] FormsName = FormsFullName.Select(file => Path.GetFileNameWithoutExtension(file)).ToArray();
                treeViewProject.Nodes[0].Nodes.Clear();
                treeViewProject.Nodes[1].Nodes.Clear();
                for (int i = 0; i< TablesName.Length; i++) {
                    treeViewProject.Nodes[0].Nodes.Add(TablesName[i].ToString());
                }
                treeViewProject.Nodes[0].Expand();
                for (int i = 0; i < FormsName.Length; i++) {
                    treeViewProject.Nodes[1].Nodes.Add(FormsName[i].ToString());
                }
                treeViewProject.Nodes[1].Expand();
                
                //CLoaderDB loadDB = new CLoaderDB(openFileDialog1.FileName);
                ////dataGridView1.Columns.Clear();
                //DataTable dt = loadDB.allTables;
                ////dataGridView1.DataSource = dt;
                //foreach (DataRow row in dt.Rows) {
                //    treeViewProject.Nodes[0].Nodes.Add(row["TABLE_NAME"].ToString());

                //}
            }

        }

        private void treeViewProject_AfterSelect(object sender, TreeViewEventArgs e) {
            if(e.Node.Parent != null) {
                listView1.Items.Clear();
                if (e.Node.Parent.Name == treeViewProject.Nodes[0].Name) {
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = VFProject.TablesTables[e.Node.Index];
                    string[] columns = VFProject.GetColumnsName(e.Node.Index);
                    string[] coltype = VFProject.GetColumnsType(e.Node.Index);
                    for(int i = 0; i < columns.Count(); i++) {
                        listView1.Items.Add(columns[i]);
                        listView1.Items[i].SubItems.Add(coltype[i]);
                    }
                }
                if (e.Node.Parent.Name == treeViewProject.Nodes[1].Name) {
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = VFProject.FormTables[e.Node.Index];
                    string[] columns = VFProject.GetFormObjectsType(e.Node.Index);
                    string[] properties = VFProject.GetFormObjectsProperties(e.Node.Index);
                    for (int i = 0; i < columns.Count(); i++) {
                        listView1.Items.Add(columns[i]);
                        listView1.Items[i].SubItems.Add(properties[i]);
                    }
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e) {
            if (listView1.SelectedIndices.Count > 0) {
                richTextBox1.Text = listView1.Items[listView1.SelectedIndices[0]].SubItems[1].Text;
            }
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e) {
            //DBase.OpenBase();
            Settings form = new Settings();
            form.Owner = this;
            form.ShowDialog();


        }

        private void button2_Click(object sender, EventArgs e) {
            DBase.SaveProject(VFProject, NameProject[0]);
        }

        private void button3_Click(object sender, EventArgs e) {
            dataGridView1.DataSource = DBase.GetProject(0);
            
        }
    }
}
