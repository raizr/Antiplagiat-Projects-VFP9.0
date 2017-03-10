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
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                string [] TablesFullName = Directory.GetFiles(folderBrowserDialog1.SelectedPath,
                                                         "*.dbf",
                                                         SearchOption.AllDirectories);
                string[] FormsFullName = Directory.GetFiles(folderBrowserDialog1.SelectedPath,
                                                         "*.scx",
                                                         SearchOption.AllDirectories);
                manager.OpenProject(TablesFullName, FormsFullName);
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
        private CManager manager = new CManager();

        private void treeViewProject_AfterSelect(object sender, TreeViewEventArgs e) {
            if(e.Node.Parent != null) {
                listView1.Items.Clear();
                if (e.Node.Parent.Name == treeViewProject.Nodes[0].Name) {
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = manager.TablesTables[e.Node.Index];
                    string[] columns = manager.GetColumnsName(e.Node.Index);
                    string[] coltype = manager.GetColumnsType(e.Node.Index);
                    for(int i = 0; i < columns.Count(); i++) {
                        listView1.Items.Add(columns[i]);
                        listView1.Items[i].SubItems.Add(coltype[i]);
                    }
                }
                if (e.Node.Parent.Name == treeViewProject.Nodes[1].Name) {
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = manager.FormTables[e.Node.Index];
                    string[] columns = manager.GetFormObjectsType(e.Node.Index);
                    for (int i = 0; i < columns.Count(); i++)
                        listView1.Items.Add(columns[i]);
                }
            }
            

        }
    }
}
