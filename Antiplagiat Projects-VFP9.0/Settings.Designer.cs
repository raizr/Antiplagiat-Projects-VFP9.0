namespace Antiplagiat_Projects_VFP9._0 {
    partial class SettingsDB {
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
            this.dataGridViewKP = new System.Windows.Forms.DataGridView();
            this.buttonOpenDBProjects = new System.Windows.Forms.Button();
            this.buttonAddKP = new System.Windows.Forms.Button();
            this.buttonDelProject = new System.Windows.Forms.Button();
            this.textBoxPathDBKP = new System.Windows.Forms.TextBox();
            this.folderBrowserDialogKP = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKP)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewKP
            // 
            this.dataGridViewKP.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewKP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewKP.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridViewKP.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewKP.Name = "dataGridViewKP";
            this.dataGridViewKP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewKP.Size = new System.Drawing.Size(721, 305);
            this.dataGridViewKP.TabIndex = 1;
            // 
            // buttonOpenDBProjects
            // 
            this.buttonOpenDBProjects.Location = new System.Drawing.Point(217, 324);
            this.buttonOpenDBProjects.Name = "buttonOpenDBProjects";
            this.buttonOpenDBProjects.Size = new System.Drawing.Size(110, 23);
            this.buttonOpenDBProjects.TabIndex = 2;
            this.buttonOpenDBProjects.Text = "Путь к папкам КП";
            this.buttonOpenDBProjects.UseVisualStyleBackColor = true;
            this.buttonOpenDBProjects.Click += new System.EventHandler(this.buttonOpenDBProjects_Click);
            // 
            // buttonAddKP
            // 
            this.buttonAddKP.Location = new System.Drawing.Point(12, 353);
            this.buttonAddKP.Name = "buttonAddKP";
            this.buttonAddKP.Size = new System.Drawing.Size(115, 23);
            this.buttonAddKP.TabIndex = 3;
            this.buttonAddKP.Text = "Добавить КП в БД";
            this.buttonAddKP.UseVisualStyleBackColor = true;
            this.buttonAddKP.Click += new System.EventHandler(this.buttonAddKP_Click);
            // 
            // buttonDelProject
            // 
            this.buttonDelProject.Location = new System.Drawing.Point(651, 324);
            this.buttonDelProject.Name = "buttonDelProject";
            this.buttonDelProject.Size = new System.Drawing.Size(82, 23);
            this.buttonDelProject.TabIndex = 4;
            this.buttonDelProject.Text = "Удалить КП";
            this.buttonDelProject.UseVisualStyleBackColor = true;
            this.buttonDelProject.Click += new System.EventHandler(this.buttonDelProject_Click);
            // 
            // textBoxPathDBKP
            // 
            this.textBoxPathDBKP.Location = new System.Drawing.Point(12, 327);
            this.textBoxPathDBKP.Name = "textBoxPathDBKP";
            this.textBoxPathDBKP.Size = new System.Drawing.Size(199, 20);
            this.textBoxPathDBKP.TabIndex = 5;
            // 
            // SettingsDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 413);
            this.Controls.Add(this.textBoxPathDBKP);
            this.Controls.Add(this.buttonDelProject);
            this.Controls.Add(this.buttonAddKP);
            this.Controls.Add(this.buttonOpenDBProjects);
            this.Controls.Add(this.dataGridViewKP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SettingsDB";
            this.ShowIcon = false;
            this.Text = "База курсовых проектов";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Settings_FormClosed);
            this.Shown += new System.EventHandler(this.Settings_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridViewKP;
        private System.Windows.Forms.Button buttonOpenDBProjects;
        private System.Windows.Forms.Button buttonAddKP;
        private System.Windows.Forms.Button buttonDelProject;
        private System.Windows.Forms.TextBox textBoxPathDBKP;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogKP;
    }
}