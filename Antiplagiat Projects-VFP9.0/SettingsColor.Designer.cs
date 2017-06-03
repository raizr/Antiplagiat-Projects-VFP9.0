namespace Antiplagiat_Projects_VFP9._0 {
    partial class SettingsColor {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.labelOpen = new System.Windows.Forms.Label();
            this.labelCell = new System.Windows.Forms.Label();
            this.labelRefCell = new System.Windows.Forms.Label();
            this.labelRefObject = new System.Windows.Forms.Label();
            this.labelObject = new System.Windows.Forms.Label();
            this.buttonOpenColor = new System.Windows.Forms.Button();
            this.buttonColorCell = new System.Windows.Forms.Button();
            this.buttonFormObjColor = new System.Windows.Forms.Button();
            this.buttonRefColorCell = new System.Windows.Forms.Button();
            this.buttonRefFormObjColor = new System.Windows.Forms.Button();
            this.buttonInsMethodColor = new System.Windows.Forms.Button();
            this.labelMethod = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelOpen
            // 
            this.labelOpen.AutoSize = true;
            this.labelOpen.Location = new System.Drawing.Point(12, 27);
            this.labelOpen.Name = "labelOpen";
            this.labelOpen.Size = new System.Drawing.Size(160, 13);
            this.labelOpen.TabIndex = 0;
            this.labelOpen.Text = "Цвет заимствованного файла";
            // 
            // labelCell
            // 
            this.labelCell.AutoSize = true;
            this.labelCell.Location = new System.Drawing.Point(12, 56);
            this.labelCell.Name = "labelCell";
            this.labelCell.Size = new System.Drawing.Size(223, 13);
            this.labelCell.TabIndex = 1;
            this.labelCell.Text = "Цвет заимствованного элемента таблицы";
            // 
            // labelRefCell
            // 
            this.labelRefCell.AutoSize = true;
            this.labelRefCell.Location = new System.Drawing.Point(12, 83);
            this.labelRefCell.Name = "labelRefCell";
            this.labelRefCell.Size = new System.Drawing.Size(191, 13);
            this.labelRefCell.TabIndex = 2;
            this.labelRefCell.Text = "Цвет эталонного элемента таблицы";
            // 
            // labelRefObject
            // 
            this.labelRefObject.AutoSize = true;
            this.labelRefObject.Location = new System.Drawing.Point(12, 138);
            this.labelRefObject.Name = "labelRefObject";
            this.labelRefObject.Size = new System.Drawing.Size(184, 13);
            this.labelRefObject.TabIndex = 4;
            this.labelRefObject.Text = "Цвет эталонного элемента формы";
            // 
            // labelObject
            // 
            this.labelObject.AutoSize = true;
            this.labelObject.Location = new System.Drawing.Point(12, 111);
            this.labelObject.Name = "labelObject";
            this.labelObject.Size = new System.Drawing.Size(216, 13);
            this.labelObject.TabIndex = 3;
            this.labelObject.Text = "Цвет заимствованного элемента формы";
            // 
            // buttonOpenColor
            // 
            this.buttonOpenColor.BackColor = System.Drawing.Color.Red;
            this.buttonOpenColor.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.buttonOpenColor.FlatAppearance.BorderSize = 0;
            this.buttonOpenColor.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonOpenColor.Location = new System.Drawing.Point(249, 24);
            this.buttonOpenColor.Margin = new System.Windows.Forms.Padding(0);
            this.buttonOpenColor.Name = "buttonOpenColor";
            this.buttonOpenColor.Size = new System.Drawing.Size(18, 18);
            this.buttonOpenColor.TabIndex = 5;
            this.buttonOpenColor.UseVisualStyleBackColor = false;
            this.buttonOpenColor.Click += new System.EventHandler(this.buttonOpenColor_Click);
            // 
            // buttonColorCell
            // 
            this.buttonColorCell.BackColor = System.Drawing.Color.Red;
            this.buttonColorCell.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.buttonColorCell.FlatAppearance.BorderSize = 0;
            this.buttonColorCell.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonColorCell.Location = new System.Drawing.Point(249, 53);
            this.buttonColorCell.Margin = new System.Windows.Forms.Padding(0);
            this.buttonColorCell.Name = "buttonColorCell";
            this.buttonColorCell.Size = new System.Drawing.Size(18, 18);
            this.buttonColorCell.TabIndex = 6;
            this.buttonColorCell.UseVisualStyleBackColor = false;
            this.buttonColorCell.Click += new System.EventHandler(this.buttonColorCell_Click);
            // 
            // buttonFormObjColor
            // 
            this.buttonFormObjColor.BackColor = System.Drawing.Color.Red;
            this.buttonFormObjColor.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.buttonFormObjColor.FlatAppearance.BorderSize = 0;
            this.buttonFormObjColor.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonFormObjColor.Location = new System.Drawing.Point(249, 106);
            this.buttonFormObjColor.Margin = new System.Windows.Forms.Padding(0);
            this.buttonFormObjColor.Name = "buttonFormObjColor";
            this.buttonFormObjColor.Size = new System.Drawing.Size(18, 18);
            this.buttonFormObjColor.TabIndex = 7;
            this.buttonFormObjColor.UseVisualStyleBackColor = false;
            this.buttonFormObjColor.Click += new System.EventHandler(this.buttonFormObjColor_Click);
            // 
            // buttonRefColorCell
            // 
            this.buttonRefColorCell.BackColor = System.Drawing.Color.Gray;
            this.buttonRefColorCell.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.buttonRefColorCell.FlatAppearance.BorderSize = 0;
            this.buttonRefColorCell.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonRefColorCell.Location = new System.Drawing.Point(249, 78);
            this.buttonRefColorCell.Margin = new System.Windows.Forms.Padding(0);
            this.buttonRefColorCell.Name = "buttonRefColorCell";
            this.buttonRefColorCell.Size = new System.Drawing.Size(18, 18);
            this.buttonRefColorCell.TabIndex = 8;
            this.buttonRefColorCell.UseVisualStyleBackColor = false;
            this.buttonRefColorCell.Click += new System.EventHandler(this.buttonRefColorCell_Click);
            // 
            // buttonRefFormObjColor
            // 
            this.buttonRefFormObjColor.BackColor = System.Drawing.Color.Gray;
            this.buttonRefFormObjColor.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.buttonRefFormObjColor.FlatAppearance.BorderSize = 0;
            this.buttonRefFormObjColor.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonRefFormObjColor.Location = new System.Drawing.Point(249, 133);
            this.buttonRefFormObjColor.Margin = new System.Windows.Forms.Padding(0);
            this.buttonRefFormObjColor.Name = "buttonRefFormObjColor";
            this.buttonRefFormObjColor.Size = new System.Drawing.Size(18, 18);
            this.buttonRefFormObjColor.TabIndex = 9;
            this.buttonRefFormObjColor.UseVisualStyleBackColor = false;
            this.buttonRefFormObjColor.Click += new System.EventHandler(this.buttonRefFormObjColor_Click);
            // 
            // buttonInsMethodColor
            // 
            this.buttonInsMethodColor.BackColor = System.Drawing.Color.Red;
            this.buttonInsMethodColor.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.buttonInsMethodColor.FlatAppearance.BorderSize = 0;
            this.buttonInsMethodColor.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonInsMethodColor.Location = new System.Drawing.Point(249, 163);
            this.buttonInsMethodColor.Margin = new System.Windows.Forms.Padding(0);
            this.buttonInsMethodColor.Name = "buttonInsMethodColor";
            this.buttonInsMethodColor.Size = new System.Drawing.Size(18, 18);
            this.buttonInsMethodColor.TabIndex = 11;
            this.buttonInsMethodColor.UseVisualStyleBackColor = false;
            this.buttonInsMethodColor.Click += new System.EventHandler(this.buttonInsMethodColor_Click);
            // 
            // labelMethod
            // 
            this.labelMethod.AutoSize = true;
            this.labelMethod.Location = new System.Drawing.Point(12, 168);
            this.labelMethod.Name = "labelMethod";
            this.labelMethod.Size = new System.Drawing.Size(192, 13);
            this.labelMethod.TabIndex = 10;
            this.labelMethod.Text = "Цвет заимствованных токенов кода";
            // 
            // SettingsColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 192);
            this.Controls.Add(this.buttonInsMethodColor);
            this.Controls.Add(this.labelMethod);
            this.Controls.Add(this.buttonRefFormObjColor);
            this.Controls.Add(this.buttonRefColorCell);
            this.Controls.Add(this.buttonFormObjColor);
            this.Controls.Add(this.buttonColorCell);
            this.Controls.Add(this.buttonOpenColor);
            this.Controls.Add(this.labelRefObject);
            this.Controls.Add(this.labelObject);
            this.Controls.Add(this.labelRefCell);
            this.Controls.Add(this.labelCell);
            this.Controls.Add(this.labelOpen);
            this.Name = "SettingsColor";
            this.ShowIcon = false;
            this.Text = "Настройки цвета";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Label labelOpen;
        private System.Windows.Forms.Label labelCell;
        private System.Windows.Forms.Label labelRefCell;
        private System.Windows.Forms.Label labelRefObject;
        private System.Windows.Forms.Label labelObject;
        private System.Windows.Forms.Button buttonOpenColor;
        private System.Windows.Forms.Button buttonColorCell;
        private System.Windows.Forms.Button buttonFormObjColor;
        private System.Windows.Forms.Button buttonRefColorCell;
        private System.Windows.Forms.Button buttonRefFormObjColor;
        private System.Windows.Forms.Button buttonInsMethodColor;
        private System.Windows.Forms.Label labelMethod;
    }
}