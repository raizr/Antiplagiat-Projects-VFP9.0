using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Antiplagiat_Projects_VFP9._0 {
    public partial class TableViewForm : Form {
        private CManager manager;
        private int TableIndex;
        private int RColumnIndex = 0;
        private List<SCheckElementInfo> RefInfoList;
        private List<SCheckCellInfo> RefCellInfo;
        public TableViewForm(CManager ptr, int TableIndex) {
            InitializeComponent();
            manager = ptr;
            this.TableIndex = TableIndex;
            RefInfoList = manager.GetListFilesInfo().FindAll(x => x.Type == 'R');
            if (manager.CheckCellList != null)
                RefCellInfo = manager.CheckCellList.FindAll(x => x.TableIndex == TableIndex);
            dataGridView.DataSource = manager.InspectProject.TablesTables[TableIndex];
            /*if (manager.EqualColumnsInfo != null) {
                bool[][] ColumnsColor = manager.EqualColumnsInfo;
                for (int i = 0; i < ColumnsColor[TableIndex].Length; i++) {
                    if (ColumnsColor[TableIndex][i]) {
                        dataGridView.Columns[i].HeaderCell.Style.BackColor = Color.Red;
                    }
                }
            }*/
            foreach (DataGridViewColumn column in dataGridView.Columns) {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void dataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e) {
            if (RefCellInfo != null && e.RowIndex >= 0) {
                CLoaderDB loader = new CLoaderDB();
                string columnIndex = dataGridView.Columns[e.ColumnIndex].Name;
                SCheckCellInfo cellinfo = RefCellInfo.Find(x => x.rowIndex == e.RowIndex &&
                                                             x.ColumnIndex == columnIndex);
                if(cellinfo.FileName != null) {
                    dataGridViewRef.DataSource = loader.OpenDB(cellinfo.FileName);
                    // устанавливаем цвет по умолчанию для предыдущего выделенного column
                    if(RColumnIndex < dataGridViewRef.Columns.Count)
                        dataGridViewRef.Columns[RColumnIndex].
                                       HeaderCell.Style.BackColor = Color.White;
                    //устанавливаем цвет заимствованного cell в эталонной таблице
                    dataGridViewRef.Rows[cellinfo.RefrowIndex].
                                Cells[cellinfo.RefColumnIndex].
                                Style.BackColor = Color.Gray;
                    SCheckElementInfo info = RefInfoList.Find(x => x.EqualNum == TableIndex && x.Type == 'R');
                    richTextBoxRefInfo.Text = "Имя проекта: " + Path.GetFileNameWithoutExtension(info.ProjectName) + "\n" +
                        "Студента: " + info.StudentName + "\n" +
                        "Название формы:" + Path.GetFileNameWithoutExtension(cellinfo.FileName);
                    foreach (DataGridViewColumn column in dataGridViewRef.Columns) {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
            }
        }

        private void TableViewForm_Shown(object sender, EventArgs e) {
            if (RefCellInfo != null) {
                for (int i = 0; i < RefCellInfo.Count; i++) {
                    if(RefCellInfo[i].rowIndex >= 0) {
                        dataGridView.Rows[RefCellInfo[i].rowIndex].
                        Cells[RefCellInfo[i].ColumnIndex].
                        Style.BackColor = Properties.Settings.Default.ColorCell;
                        dataGridView.Rows[RefCellInfo[i].rowIndex].
                            Cells[RefCellInfo[i].ColumnIndex].ToolTipText =
                            RefCellInfo[i].StudentName + "\n" +
                            Path.GetFileNameWithoutExtension(RefCellInfo[i].ProjectName) + "\n" +
                            Path.GetFileNameWithoutExtension(RefCellInfo[i].FileName);
                    }else {
                        dataGridView.Columns[RefCellInfo[i].ColumnIndex].
                            HeaderCell.Style.BackColor = Properties.Settings.Default.ColorCell;
                        dataGridView.Columns[RefCellInfo[i].ColumnIndex].ToolTipText =
                            RefCellInfo[i].StudentName + "\n" +
                            Path.GetFileNameWithoutExtension(RefCellInfo[i].ProjectName) + "\n" +
                            Path.GetFileNameWithoutExtension(RefCellInfo[i].FileName);
                    }
                }
            } 
        }

        private void dataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
            //if ( e.RowIndex >= 0) {
                CLoaderDB loader = new CLoaderDB();
                string columnIndex = dataGridView.Columns[e.ColumnIndex].Name;
                //richTextBoxRefInfo.Text = columnIndex;
                SCheckCellInfo cellinfo = RefCellInfo.Find(x => x.rowIndex == -1 &&
                                                             x.ColumnIndex == columnIndex);
                if (cellinfo.FileName != null) {
                    dataGridViewRef.DataSource = loader.OpenDB(cellinfo.FileName);
                    // устанавливаем цвет по умолчанию для предыдущего выделенного column
                    dataGridViewRef.Columns[RColumnIndex].
                                   HeaderCell.Style.BackColor = Color.White;
                //устанавливаем цвет заимствованного column в эталонной таблице
                dataGridViewRef.Columns[cellinfo.RefColumnIndex].
                                    HeaderCell.Style.BackColor = Properties.Settings.Default.RefColorCell;
                // запоминаем закрашенный column
                RColumnIndex = e.ColumnIndex;
                    richTextBoxRefInfo.Text = "Имя проекта: " + Path.GetFileNameWithoutExtension(cellinfo.ProjectName) + "\n" +
                        "Студента: " + cellinfo.StudentName + "\n" +
                        "Название формы:" + Path.GetFileNameWithoutExtension(cellinfo.FileName);
                }
           // }
        }
    }
}
