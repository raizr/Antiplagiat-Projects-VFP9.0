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

        public TableViewForm(CManager ptr) {
            InitializeComponent();
            manager = ptr;
        }

        private void dataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e) {
            CLoaderDB loader = new CLoaderDB();
            if(e.RowIndex >= 0 && manager.CheckCellList != null)
                dataGridViewRef.DataSource = loader.OpenDB(manager.CheckCellList[e.RowIndex].FileName); ;
        }
    }
}
