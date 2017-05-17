using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Antiplagiat_Projects_VFP9._0 {
    public partial class TableViewForm : Form {
        private CManager manager;
        private int TableIndex;
        private List<SCheckColumnInfo> RefInfoList;
        private List<SCheckCellInfo> RefCellInfo;
        public TableViewForm(CManager ptr, int TableIndex) {
            InitializeComponent();
            manager = ptr;
            this.TableIndex = TableIndex;
            RefInfoList = manager.GetListFilesInfo().FindAll(x => x.IsTable == true);
            if (manager.CheckCellList != null)
                RefCellInfo = manager.CheckCellList.FindAll(x => x.TableIndex == TableIndex);
            dataGridView.DataSource = manager.InspectProject.TablesTables[TableIndex];
            if (manager.EqualColumnsInfo != null) {
                bool[][] ColumnsColor = manager.EqualColumnsInfo;
                for (int i = 0; i < ColumnsColor[TableIndex].Length; i++) {
                    if (ColumnsColor[TableIndex][i]) {
                        dataGridView.Columns[i].HeaderCell.Style.BackColor = Color.Red;
                    }
                }
            }
        }

        private void dataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e) {
            if (RefCellInfo != null && e.RowIndex >= 0) {
                CLoaderDB loader = new CLoaderDB();
                string columnIndex = dataGridView.Columns[e.ColumnIndex].Name;
                SCheckCellInfo cellinfo = RefCellInfo.Find(x => x.rowIndex == e.RowIndex &&
                                                             x.ColumnIndex == columnIndex);
                    dataGridViewRef.DataSource = loader.OpenDB(cellinfo.FileName);
                dataGridViewRef.Rows[cellinfo.RefrowIndex].
                            Cells[cellinfo.RefColumnIndex].
                            Style.BackColor = Color.Gray;
                SCheckColumnInfo info = RefInfoList.Find(x => x.EqualNum == TableIndex);
                    richTextBoxRefInfo.Text = "Имя проекта: " + info.ProjectName + "\n" +
                        "Студента: " + info.StudentName + "\n" +
                        "Название формы:" + cellinfo.FileName;
            }
        }

        private void TableViewForm_Shown(object sender, EventArgs e) {
            if (RefCellInfo != null) {
                for (int i = 0; i < RefCellInfo.Count; i++) {
                    dataGridView.Rows[RefCellInfo[i].rowIndex].
                        Cells[RefCellInfo[i].ColumnIndex].
                        Style.BackColor = Color.Red;
                    dataGridView.Rows[RefCellInfo[i].rowIndex].
                        Cells[RefCellInfo[i].ColumnIndex].ToolTipText =
                        RefCellInfo[i].StudentName + "\n" +
                        RefCellInfo[i].ProjectName + "\n" +
                        RefCellInfo[i].FileName;
                }
            } 
        }
    }
}
