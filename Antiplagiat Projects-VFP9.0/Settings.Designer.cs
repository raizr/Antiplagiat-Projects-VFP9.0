namespace Antiplagiat_Projects_VFP9._0 {
    partial class Settings {
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
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("База КП");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Подсветка заимствованных элементов");
            this.treeViewSettings = new System.Windows.Forms.TreeView();
            this.dataGridViewKP = new System.Windows.Forms.DataGridView();
            this.buttonOpenDBProjects = new System.Windows.Forms.Button();
            this.buttonAddKP = new System.Windows.Forms.Button();
            this.buttonDelProject = new System.Windows.Forms.Button();
            this.textBoxPathDBKP = new System.Windows.Forms.TextBox();
            this.folderBrowserDialogKP = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKP)).BeginInit();
            this.SuspendLayout();
            // 
            // treeViewSettings
            // 
            this.treeViewSettings.Location = new System.Drawing.Point(12, 12);
            this.treeViewSettings.Name = "treeViewSettings";
            treeNode5.Name = "Узел0";
            treeNode5.Text = "База КП";
            treeNode6.Name = "Узел1";
            treeNode6.Text = "Подсветка заимствованных элементов";
            this.treeViewSettings.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6});
            this.treeViewSettings.Size = new System.Drawing.Size(190, 389);
            this.treeViewSettings.TabIndex = 0;
            this.treeViewSettings.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // dataGridViewKP
            // 
            this.dataGridViewKP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewKP.Enabled = false;
            this.dataGridViewKP.Location = new System.Drawing.Point(208, 12);
            this.dataGridViewKP.Name = "dataGridViewKP";
            this.dataGridViewKP.Size = new System.Drawing.Size(525, 305);
            this.dataGridViewKP.TabIndex = 1;
            this.dataGridViewKP.Visible = false;
            // 
            // buttonOpenDBProjects
            // 
            this.buttonOpenDBProjects.Enabled = false;
            this.buttonOpenDBProjects.Location = new System.Drawing.Point(414, 321);
            this.buttonOpenDBProjects.Name = "buttonOpenDBProjects";
            this.buttonOpenDBProjects.Size = new System.Drawing.Size(110, 23);
            this.buttonOpenDBProjects.TabIndex = 2;
            this.buttonOpenDBProjects.Text = "Путь к папкам КП";
            this.buttonOpenDBProjects.UseVisualStyleBackColor = true;
            this.buttonOpenDBProjects.Visible = false;
            this.buttonOpenDBProjects.Click += new System.EventHandler(this.buttonOpenDBProjects_Click);
            // 
            // buttonAddKP
            // 
            this.buttonAddKP.Enabled = false;
            this.buttonAddKP.Location = new System.Drawing.Point(209, 350);
            this.buttonAddKP.Name = "buttonAddKP";
            this.buttonAddKP.Size = new System.Drawing.Size(115, 23);
            this.buttonAddKP.TabIndex = 3;
            this.buttonAddKP.Text = "Добавить КП в БД";
            this.buttonAddKP.UseVisualStyleBackColor = true;
            this.buttonAddKP.Visible = false;
            this.buttonAddKP.Click += new System.EventHandler(this.buttonAddKP_Click);
            // 
            // buttonDelProject
            // 
            this.buttonDelProject.Enabled = false;
            this.buttonDelProject.Location = new System.Drawing.Point(651, 324);
            this.buttonDelProject.Name = "buttonDelProject";
            this.buttonDelProject.Size = new System.Drawing.Size(82, 23);
            this.buttonDelProject.TabIndex = 4;
            this.buttonDelProject.Text = "Удалить КП";
            this.buttonDelProject.UseVisualStyleBackColor = true;
            this.buttonDelProject.Visible = false;
            this.buttonDelProject.Click += new System.EventHandler(this.buttonDelProject_Click);
            // 
            // textBoxPathDBKP
            // 
            this.textBoxPathDBKP.Location = new System.Drawing.Point(209, 324);
            this.textBoxPathDBKP.Name = "textBoxPathDBKP";
            this.textBoxPathDBKP.Size = new System.Drawing.Size(199, 20);
            this.textBoxPathDBKP.TabIndex = 5;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 413);
            this.Controls.Add(this.textBoxPathDBKP);
            this.Controls.Add(this.buttonDelProject);
            this.Controls.Add(this.buttonAddKP);
            this.Controls.Add(this.buttonOpenDBProjects);
            this.Controls.Add(this.dataGridViewKP);
            this.Controls.Add(this.treeViewSettings);
            this.Name = "Settings";
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Settings_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewSettings;
        private System.Windows.Forms.DataGridView dataGridViewKP;
        private System.Windows.Forms.Button buttonOpenDBProjects;
        private System.Windows.Forms.Button buttonAddKP;
        private System.Windows.Forms.Button buttonDelProject;
        private System.Windows.Forms.TextBox textBoxPathDBKP;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogKP;
    }
}