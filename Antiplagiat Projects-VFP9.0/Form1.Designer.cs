namespace Antiplagiat_Projects_VFP9._0 {
    partial class MainForm {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Файлы таблиц");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Файлы форм");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Имя проекта", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.treeViewProject = new System.Windows.Forms.TreeView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.базаКПToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.подсветкаЭлементовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonSaveToBD = new System.Windows.Forms.Button();
            this.buttonCheck = new System.Windows.Forms.Button();
            this.richTextLogger = new System.Windows.Forms.RichTextBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonReportOpen = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.menuStrip1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "DBC (*.dbc)|*.dbc";
            this.openFileDialog1.Title = "Открыть dbc";
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "DataBase";
            this.folderBrowserDialog1.SelectedPath = "C:\\Users\\Denis\\Dropbox\\VKR\\DateBase";
            // 
            // treeViewProject
            // 
            this.treeViewProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewProject.Location = new System.Drawing.Point(0, 0);
            this.treeViewProject.Name = "treeViewProject";
            treeNode1.Name = "Tables";
            treeNode1.Text = "Файлы таблиц";
            treeNode2.Name = "Forms";
            treeNode2.Text = "Файлы форм";
            treeNode3.Name = "Узел0";
            treeNode3.Text = "Имя проекта";
            this.treeViewProject.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.treeViewProject.ShowNodeToolTips = true;
            this.treeViewProject.Size = new System.Drawing.Size(232, 524);
            this.treeViewProject.TabIndex = 2;
            this.treeViewProject.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewProject_AfterSelect);
            this.treeViewProject.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewProject_NodeMouseDoubleClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.настройкиToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1170, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.O)));
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.открытьToolStripMenuItem.Text = "Открыть";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.базаКПToolStripMenuItem,
            this.подсветкаЭлементовToolStripMenuItem});
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            // 
            // базаКПToolStripMenuItem
            // 
            this.базаКПToolStripMenuItem.Name = "базаКПToolStripMenuItem";
            this.базаКПToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.базаКПToolStripMenuItem.Text = "База КП";
            this.базаКПToolStripMenuItem.Click += new System.EventHandler(this.базаКПToolStripMenuItem_Click);
            // 
            // подсветкаЭлементовToolStripMenuItem
            // 
            this.подсветкаЭлементовToolStripMenuItem.Name = "подсветкаЭлементовToolStripMenuItem";
            this.подсветкаЭлементовToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.подсветкаЭлементовToolStripMenuItem.Text = "Подсветка элементов";
            this.подсветкаЭлементовToolStripMenuItem.Click += new System.EventHandler(this.подсветкаЭлементовToolStripMenuItem_Click);
            // 
            // buttonSaveToBD
            // 
            this.buttonSaveToBD.Location = new System.Drawing.Point(11, 65);
            this.buttonSaveToBD.Name = "buttonSaveToBD";
            this.buttonSaveToBD.Size = new System.Drawing.Size(100, 34);
            this.buttonSaveToBD.TabIndex = 7;
            this.buttonSaveToBD.Text = "Сохранить проект в БД";
            this.buttonSaveToBD.UseVisualStyleBackColor = true;
            this.buttonSaveToBD.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonCheck
            // 
            this.buttonCheck.Location = new System.Drawing.Point(11, 19);
            this.buttonCheck.Name = "buttonCheck";
            this.buttonCheck.Size = new System.Drawing.Size(100, 40);
            this.buttonCheck.TabIndex = 10;
            this.buttonCheck.Text = "Проверить проект";
            this.buttonCheck.UseVisualStyleBackColor = true;
            this.buttonCheck.Click += new System.EventHandler(this.buttonCheck_Click);
            // 
            // richTextLogger
            // 
            this.richTextLogger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextLogger.Location = new System.Drawing.Point(0, 0);
            this.richTextLogger.Name = "richTextLogger";
            this.richTextLogger.Size = new System.Drawing.Size(934, 125);
            this.richTextLogger.TabIndex = 11;
            this.richTextLogger.Text = "";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 548);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1170, 22);
            this.statusStrip.TabIndex = 12;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(200, 16);
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(108, 17);
            this.toolStripStatusLabel.Text = "Открытие проекта";
            // 
            // buttonReportOpen
            // 
            this.buttonReportOpen.Location = new System.Drawing.Point(11, 111);
            this.buttonReportOpen.Name = "buttonReportOpen";
            this.buttonReportOpen.Size = new System.Drawing.Size(100, 23);
            this.buttonReportOpen.TabIndex = 13;
            this.buttonReportOpen.Text = "Открыть отчет";
            this.buttonReportOpen.UseVisualStyleBackColor = true;
            this.buttonReportOpen.Click += new System.EventHandler(this.buttonReportOpen_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewProject);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1170, 524);
            this.splitContainer1.SplitterDistance = 232;
            this.splitContainer1.TabIndex = 14;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.richTextLogger);
            this.splitContainer2.Size = new System.Drawing.Size(934, 524);
            this.splitContainer2.SplitterDistance = 395;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.splitContainer3.Size = new System.Drawing.Size(934, 395);
            this.splitContainer3.SplitterDistance = 116;
            this.splitContainer3.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonCheck);
            this.groupBox1.Controls.Add(this.buttonReportOpen);
            this.groupBox1.Controls.Add(this.buttonSaveToBD);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(116, 395);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Инструменты";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 570);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "ИПК Антиплагиат пректов Баз Данных VFP9.0";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TreeView treeViewProject;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.Button buttonSaveToBD;
        private System.Windows.Forms.Button buttonCheck;
        private System.Windows.Forms.RichTextBox richTextLogger;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.Button buttonReportOpen;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripMenuItem базаКПToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem подсветкаЭлементовToolStripMenuItem;
    }
}

