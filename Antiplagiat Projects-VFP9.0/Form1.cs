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
using System.Reflection;

namespace Antiplagiat_Projects_VFP9._0 {
    public partial class MainForm : Form {
        public MainForm(CManager ptr) {
            InitializeComponent();
            Console.SetOut(new Logger(richTextLogger));
            manager = ptr;
            manager.DBase.Load();
        }
        private CManager manager;
        private void button1_Click(object sender, EventArgs e) {
            OpenProject();
        }

        private void OpenProject() {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                toolStripStatusLabel.Text = "Открытие проекта";
                toolStripProgressBar.Minimum = 0;
                string[] TablesFullName = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.dbf",
                                                         SearchOption.AllDirectories);
                string[] FormsFullName = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.scx",
                                                         SearchOption.AllDirectories);
                toolStripProgressBar.Step = 1;
                toolStripProgressBar.Maximum = TablesFullName.Length + FormsFullName.Length;
                manager.InspectProject.onCount += ChangeProgressBar;
                string ProjectName = manager.OpenProject(folderBrowserDialog1.SelectedPath);
                richTextLogger.ScrollToCaret();
                toolStripStatusLabel.Text = "Проект открыт";
                toolStripProgressBar.Value = 0;
                if (ProjectName == null) {
                    MessageBox.Show("Файл проекта не найден");
                } else {
                    if (ProjectName == "null") {
                        MessageBox.Show("Ошибка открытия проекта из БД КП\n Проверьте БД");
                    } else {
                        splitContainer3.Panel2.Controls.Clear();
                        treeViewProject.Nodes[0].Text = Path.GetFileName(ProjectName);
                        string[] TablesName = manager.InspectProject.TablesFullName.Select(file => Path.GetFileNameWithoutExtension(file)).ToArray();
                        string[] FormsName = manager.InspectProject.FormsFullName.Select(file => Path.GetFileNameWithoutExtension(file)).ToArray();
                        
                        treeViewProject.Nodes[0].Nodes[0].Nodes.Clear();
                        treeViewProject.Nodes[0].Nodes[1].Nodes.Clear();
                        for (int i = 0; i < TablesName.Length; i++) {
                            treeViewProject.Nodes[0].Nodes[0].Nodes.Add(TablesName[i].ToString());
                        }
                        
                        for (int i = 0; i < FormsName.Length; i++) {
                            treeViewProject.Nodes[0].Nodes[1].Nodes.Add(FormsName[i].ToString());
                        }
                        treeViewProject.Nodes[0].Expand();
                        treeViewProject.Nodes[0].Nodes[0].Expand();
                        treeViewProject.Nodes[0].Nodes[1].Expand();
                        SetColorNodes(manager.CheckColumnList);
                    }
                }
            }
        }

        public void ChangeProgressBar() {
            toolStripProgressBar.PerformStep();
        }

        private void SetColorNodes(List<SCheckElementInfo> OpenInfo) {
            if(OpenInfo != null) {
                for (int i = 0; i < OpenInfo.Count; i++) {
                    if (OpenInfo[i].Type == 'R' || OpenInfo[i].Type == 'C') {
                        treeViewProject.Nodes[0].Nodes[0].Nodes[OpenInfo[i].EqualNum].BackColor = Color.Red;
                        treeViewProject.Nodes[0].Nodes[0].Nodes[OpenInfo[i].EqualNum].ToolTipText =
                            OpenInfo[i].StudentName + "\n" + Path.GetFileNameWithoutExtension(OpenInfo[i].ProjectName) +
                            "\n" + OpenInfo[i].FileName;
                    } else {
                        treeViewProject.Nodes[0].Nodes[1].Nodes[OpenInfo[i].EqualNum].BackColor = Color.Red;
                        treeViewProject.Nodes[0].Nodes[1].Nodes[OpenInfo[i].EqualNum].ToolTipText =
                            OpenInfo[i].StudentName + "\n" + Path.GetFileNameWithoutExtension(OpenInfo[i].ProjectName) +
                            "\n" + OpenInfo[i].FileName;
                    }
                }
            }
        }

        private void ShowView(TreeNode Node, bool IsMDI) {
            this.splitContainer3.Panel2.Controls.Clear();
            if (Node.Parent.Name == treeViewProject.Nodes[0].Nodes[0].Name) {
                    TableViewForm TVForm = new TableViewForm(manager, Node.Index);
                    //TVForm.MdiParent = this;
                    //dataGridView1.Columns.Clear();
                    if (IsMDI) {
                        TVForm.ShowInTaskbar = false;
                        TVForm.FormBorderStyle = FormBorderStyle.FixedSingle;
                        TVForm.ControlBox = false;
                        TVForm.TopLevel = false;
                        TVForm.Text = "";
                        TVForm.Visible = true;
                        TVForm.Dock = System.Windows.Forms.DockStyle.Fill;
                        this.splitContainer3.Panel2.Controls.Add(TVForm);
                    } else {
                        TVForm.Show();
                    }
                    //Console.WriteLine(e.Node.Text);
                    TVForm.Text = "Просмотр заимствований в таблице " + Node.Text;
                }
                if (Node.Parent.Name == treeViewProject.Nodes[0].Nodes[1].Name) {
                    //dataGridView1.Columns.Clear();
                    FormView ViewForm = new FormView(manager, Node.Index);
                    if (IsMDI) {
                        ViewForm.ShowInTaskbar = false;
                        ViewForm.FormBorderStyle = FormBorderStyle.FixedSingle;
                        ViewForm.ControlBox = false;
                        ViewForm.TopLevel = false;
                        ViewForm.Visible = true;
                        ViewForm.Dock = System.Windows.Forms.DockStyle.Fill;
                        this.splitContainer3.Panel2.Controls.Add(ViewForm);
                        ViewForm.Dock = System.Windows.Forms.DockStyle.Fill;
                    } else {
                        ViewForm.Show();
                    }
                ViewForm.Text = "Просмотр заимствований формы " + Node.Text;
                //ViewForm.listViewObjects.Items.Add()
            }
            
        }

        private void treeViewProject_AfterSelect(object sender, TreeViewEventArgs e) {
            if (e.Node.Parent != null) {
                ShowView(e.Node, true);
            }
        }

        /*private void listView1_SelectedIndexChanged(object sender, EventArgs e) {
            if (listView1.SelectedIndices.Count > 0) {
                richTextBox1.Text = listView1.Items[listView1.SelectedIndices[0]].SubItems[1].Text;
            }
        }*/

        /// <summary>
        ///  Сохранение проекта в БД
        ///    </summary>
        private void button2_Click(object sender, EventArgs e) {
            if (manager.SaveOpenProjectToBD() == 1) {
                MessageBox.Show(this, "Для сохранения проекта необходимо открыть проект", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*private void button3_Click(object sender, EventArgs e) {
            dataGridView1.DataSource = manager.DBase.GetProject(0);
            
        }*/

        private void buttonCheck_Click(object sender, EventArgs e) {
            toolStripStatusLabel.Text = "Проверка проекта";
            toolStripProgressBar.Minimum = 0;
            toolStripProgressBar.Maximum = manager.DBase.Projects.Rows.Count;
            manager.Verificator.onCount += ChangeProgressBar;
            if (manager.CheckProject() == null)
                MessageBox.Show(this, "Для начала проверки необходимо открыть проект", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            richTextLogger.ScrollToCaret();
            SetColorNodes(manager.GetListFilesInfo());
            toolStripStatusLabel.Text = "Проект проверен";
            toolStripProgressBar.Value = 0;
            /*for(int i = 0; i< ColumnsColor.Length;i++) {
                for(int j = 0; j < ColumnsColor.Length; j++) {
                    if(ColumnsColor[i][j] == true)
                        Console.WriteLine("true");
                }
            }*/
        }

        private void treeViewProject_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
            if (e.Node.Parent != null) {
                ShowView(e.Node, false);
            }
        }
        
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenProject();
        }

        private void buttonReportOpen_Click(object sender, EventArgs e) {
            List<CRowReport> rows = manager.GetReport();
            if(rows != null) {
                FormReport FormRep = new FormReport();
                FormRep.Show();
                FormRep.cRowReportBindingSource.DataSource = rows;
            } else {
                MessageBox.Show(this, "Для просмотре отчета необходимо открыть и проверить проект",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void базаКПToolStripMenuItem_Click(object sender, EventArgs e) {
            SettingsDB form = new SettingsDB(manager);
            form.Owner = this;
            form.ShowDialog();
        }

        private void подсветкаЭлементовToolStripMenuItem_Click(object sender, EventArgs e) {
            SettingsColor ColorForm = new SettingsColor();
            ColorForm.Show();
        }
    }

    class Logger : System.IO.TextWriter {
        private RichTextBox rtb;
        public Logger(RichTextBox rtb) { this.rtb = rtb; }
        public override Encoding Encoding { get { return null; } }
        public override void Write(char value) {
            if (value != '\r')
                rtb.AppendText(new string(value, 1));
        }
    }
 }
