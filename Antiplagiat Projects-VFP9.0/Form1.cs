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
            Console.SetOut(new Logger(richTextBox2));
            manager = ptr;
            manager.DBase.Load();
        }
        private CManager manager;
        private bool[][] ColumnsColor;
        private void button1_Click(object sender, EventArgs e) {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                string ProjectName = manager.OpenProject(folderBrowserDialog1.SelectedPath);
                if(ProjectName == null) {
                    MessageBox.Show("Файл проекта не найден");
                } else {
                    if(ProjectName == "null") {
                        MessageBox.Show("Ошибка открытия проекта из БД КП\n Проверьте БД");
                    }else {
                        label1.Text = Path.GetFileName(ProjectName);
                        string[] TablesName = manager.InspectProject.TablesFullName.Select(file => Path.GetFileNameWithoutExtension(file)).ToArray();
                        string[] FormsName = manager.InspectProject.FormsFullName.Select(file => Path.GetFileNameWithoutExtension(file)).ToArray();
                        treeViewProject.Nodes[0].Nodes.Clear();
                        treeViewProject.Nodes[1].Nodes.Clear();
                        for (int i = 0; i < TablesName.Length; i++) {
                            treeViewProject.Nodes[0].Nodes.Add(TablesName[i].ToString());
                        }
                        treeViewProject.Nodes[0].Expand();
                        for (int i = 0; i < FormsName.Length; i++) {
                            treeViewProject.Nodes[1].Nodes.Add(FormsName[i].ToString());
                        }
                        treeViewProject.Nodes[1].Expand();

                        List<SCheckColumnInfo> OpenInfo = manager.CheckColumnList;
                        for (int i = 0; i < OpenInfo.Count; i++) {
                            if (OpenInfo[i].IsTable) {
                                treeViewProject.Nodes[0].Nodes[OpenInfo[i].EqualNum].BackColor = Color.Red;
                                treeViewProject.Nodes[0].Nodes[OpenInfo[i].EqualNum].ToolTipText =
                                    OpenInfo[i].StudentName + "\n" + Path.GetFileNameWithoutExtension(OpenInfo[i].ProjectName) +
                                    "\n" + OpenInfo[i].FileName;
                            } else {
                                treeViewProject.Nodes[1].Nodes[OpenInfo[i].EqualNum].BackColor = Color.Red;
                                treeViewProject.Nodes[1].Nodes[OpenInfo[i].EqualNum].ToolTipText =
                                    OpenInfo[i].StudentName + "\n" + Path.GetFileNameWithoutExtension(OpenInfo[i].ProjectName) +
                                    "\n" + OpenInfo[i].FileName;
                            }
                        }
                    
                    }
                }   
            }
        }

        private void treeViewProject_AfterSelect(object sender, TreeViewEventArgs e) {
            if(e.Node.Parent != null) {
                listView1.Items.Clear();
                if (e.Node.Parent.Name == treeViewProject.Nodes[0].Name) {
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = manager.InspectProject.TablesTables[e.Node.Index];
                    Console.WriteLine(e.Node.Index);
                    if (ColumnsColor != null) {
                        for(int i = 0; i < ColumnsColor[e.Node.Index].Length; i++) {
                            if (ColumnsColor[e.Node.Index][i]) {
                                e.Node.BackColor = Color.Red;
                                dataGridView1.Columns[i].HeaderCell.Style.BackColor = Color.Red;
                            }
                        }
                    }
                    //dataGridView1.Columns[0]
                    string[] columns = manager.InspectProject.GetColumnsName(e.Node.Index);
                    string[] coltype = manager.InspectProject.GetColumnsType(e.Node.Index);
                    for(int i = 0; i < columns.Count(); i++) {
                        listView1.Items.Add(columns[i]);
                        listView1.Items[i].SubItems.Add(coltype[i]);
                    }
                }
                if (e.Node.Parent.Name == treeViewProject.Nodes[1].Name) {
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = manager.InspectProject.FormTables[e.Node.Index];
                    string[] columns = manager.InspectProject.GetFormObjectsType(e.Node.Index);
                    string[] properties = manager.InspectProject.GetFormObjectsProperties(e.Node.Index);
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

        private void buttonCheck_Click(object sender, EventArgs e) {
            ColumnsColor = manager.CheckProject();
            /*for(int i = 0; i< ColumnsColor.Length;i++) {
                for(int j = 0; j < ColumnsColor.Length; j++) {
                    if(ColumnsColor[i][j] == true)
                        Console.WriteLine("true");
                }
            }*/
        }

        private void treeViewProject_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
            if (e.Node.Parent != null) {
                listView1.Items.Clear();
                if (e.Node.Parent.Name == treeViewProject.Nodes[0].Name) {
                    TableViewForm TVForm = new TableViewForm(manager);
                    //dataGridView1.Columns.Clear();
                    TVForm.dataGridView.DataSource = manager.InspectProject.TablesTables[e.Node.Index];
                    if (ColumnsColor != null) {
                        for (int i = 0; i < ColumnsColor[e.Node.Index].Length; i++) {
                            if (ColumnsColor[e.Node.Index][i]) {
                                e.Node.BackColor = Color.Red;
                                TVForm.dataGridView.Columns[i].HeaderCell.Style.BackColor = Color.Red;
                            }
                        }
                    }
                    TVForm.Show();
                    Console.WriteLine(e.Node.Text);
                    TVForm.Text = "Просмотр заимствований в таблице " + e.Node.Text;
                    if (manager.CheckCellList != null) {
                        for (int i = 0; i < manager.CheckCellList.Count; i++) {
                            if (manager.CheckCellList[i].TableIndex == e.Node.Index) {
                                TVForm.dataGridView.Rows[manager.CheckCellList[i].rowIndex].
                                    Cells[manager.CheckCellList[i].ColumnIndex].
                                    Style.BackColor = Color.Red;
                                TVForm.dataGridView.Rows[manager.CheckCellList[i].rowIndex].
                                    Cells[manager.CheckCellList[i].ColumnIndex].ToolTipText =
                                    manager.CheckCellList[i].StudentName + "\n" +
                                    manager.CheckCellList[i].ProjectName + "\n" +
                                    manager.CheckCellList[i].FileName;
                            }
                        }
                    }
                }
            }
        }
    }

    class Logger : System.IO.TextWriter {
        private RichTextBox rtb;
        public Logger(RichTextBox rtb) { this.rtb = rtb; }
        public override Encoding Encoding { get { return null; } }
        public override void Write(char value) {
            if (value != '\r') rtb.AppendText(new string(value, 1));
        }
    }
 }
