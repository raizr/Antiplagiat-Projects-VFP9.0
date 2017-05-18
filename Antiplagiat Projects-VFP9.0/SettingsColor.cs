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
    public partial class SettingsColor : Form {
        public SettingsColor() {
            InitializeComponent();
            buttonOpenColor.BackColor = Properties.Settings.Default.OpenColor;
            buttonColorCell.BackColor = Properties.Settings.Default.ColorCell;
            buttonRefColorCell.BackColor = Properties.Settings.Default.RefColorCell;
            buttonFormObjColor.BackColor = Properties.Settings.Default.FormObjColor;
            buttonRefFormObjColor.BackColor = Properties.Settings.Default.RefFormObjColor;
        }

        private void buttonOpenColor_Click(object sender, EventArgs e) {
            if (colorDialog.ShowDialog() == DialogResult.OK) {
                buttonOpenColor.BackColor = Properties.Settings.Default.OpenColor = colorDialog.Color;
                Properties.Settings.Default.Save();
            }
        }

        private void buttonColorCell_Click(object sender, EventArgs e) {
            if (colorDialog.ShowDialog() == DialogResult.OK) {
                buttonColorCell.BackColor = Properties.Settings.Default.ColorCell = colorDialog.Color;
                Properties.Settings.Default.Save();
            }
        }

        private void buttonRefColorCell_Click(object sender, EventArgs e) {
            if (colorDialog.ShowDialog() == DialogResult.OK) {
                buttonRefColorCell.BackColor = Properties.Settings.Default.RefColorCell = colorDialog.Color;
                Properties.Settings.Default.Save();
            }
        }

        private void buttonFormObjColor_Click(object sender, EventArgs e) {
            if (colorDialog.ShowDialog() == DialogResult.OK) {
                buttonFormObjColor.BackColor = Properties.Settings.Default.FormObjColor = colorDialog.Color;
                Properties.Settings.Default.Save();
            }
        }

        private void buttonRefFormObjColor_Click(object sender, EventArgs e) {
            if (colorDialog.ShowDialog() == DialogResult.OK) {
                buttonRefFormObjColor.BackColor = Properties.Settings.Default.RefFormObjColor = colorDialog.Color;
                Properties.Settings.Default.Save();
            }
        }
    }
}
