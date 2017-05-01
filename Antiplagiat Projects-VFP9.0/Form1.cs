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
        private void button1_Click(object sender, EventArgs e) {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                label1.Text = Path.GetFileName(manager.OpenProject(folderBrowserDialog1.SelectedPath));
                string[] TablesName = manager.Project.TablesFullName.Select(file => Path.GetFileNameWithoutExtension(file)).ToArray();
                string[] FormsName = manager.Project.FormsFullName.Select(file => Path.GetFileNameWithoutExtension(file)).ToArray();
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
            }

        }

        private void treeViewProject_AfterSelect(object sender, TreeViewEventArgs e) {
            if(e.Node.Parent != null) {
                listView1.Items.Clear();
                if (e.Node.Parent.Name == treeViewProject.Nodes[0].Name) {
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = manager.Project.TablesTables[e.Node.Index];
                    string[] columns = manager.Project.GetColumnsName(e.Node.Index);
                    string[] coltype = manager.Project.GetColumnsType(e.Node.Index);
                    for(int i = 0; i < columns.Count(); i++) {
                        listView1.Items.Add(columns[i]);
                        listView1.Items[i].SubItems.Add(coltype[i]);
                    }
                }
                if (e.Node.Parent.Name == treeViewProject.Nodes[1].Name) {
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = manager.Project.FormTables[e.Node.Index];
                    string[] columns = manager.Project.GetFormObjectsType(e.Node.Index);
                    string[] properties = manager.Project.GetFormObjectsProperties(e.Node.Index);
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
            Settings form = new Settings(manager);
            form.Owner = this;
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e) {
            manager.SaveOpenProjectToBD();
        }

        private void button3_Click(object sender, EventArgs e) {
            dataGridView1.DataSource = manager.DBase.GetProject(0);
            
        }
    }
}
