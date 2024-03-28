namespace FugasDetectionSystem.SQLGeneratorTools
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            treeView1 = new TreeView();
            splitContainer2 = new SplitContainer();
            DGVColumnas = new DataGridView();
            tabControl1 = new TabControl();
            tabInsert = new TabPage();
            RTBInsert = new RichTextBox();
            tabUpdate = new TabPage();
            RTBUpdate = new RichTextBox();
            tabDelete = new TabPage();
            RTBDelete = new RichTextBox();
            tabAll = new TabPage();
            RTBAll = new RichTextBox();
            tabById = new TabPage();
            RTBById = new RichTextBox();
            menuStrip1 = new MenuStrip();
            playToolStripMenuItem = new ToolStripMenuItem();
            refreshToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DGVColumnas).BeginInit();
            tabControl1.SuspendLayout();
            tabInsert.SuspendLayout();
            tabUpdate.SuspendLayout();
            tabDelete.SuspendLayout();
            tabAll.SuspendLayout();
            tabById.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 24);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(treeView1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Size = new Size(691, 343);
            splitContainer1.SplitterDistance = 229;
            splitContainer1.TabIndex = 0;
            // 
            // treeView1
            // 
            treeView1.Dock = DockStyle.Fill;
            treeView1.Location = new Point(0, 0);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(229, 343);
            treeView1.TabIndex = 0;
            treeView1.AfterSelect += treeView1_AfterSelect;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(DGVColumnas);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(tabControl1);
            splitContainer2.Size = new Size(458, 343);
            splitContainer2.SplitterDistance = 133;
            splitContainer2.TabIndex = 0;
            // 
            // DGVColumnas
            // 
            DGVColumnas.AllowUserToAddRows = false;
            DGVColumnas.AllowUserToDeleteRows = false;
            DGVColumnas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DGVColumnas.Dock = DockStyle.Fill;
            DGVColumnas.Location = new Point(0, 0);
            DGVColumnas.Name = "DGVColumnas";
            DGVColumnas.ReadOnly = true;
            DGVColumnas.Size = new Size(458, 133);
            DGVColumnas.TabIndex = 0;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabInsert);
            tabControl1.Controls.Add(tabUpdate);
            tabControl1.Controls.Add(tabDelete);
            tabControl1.Controls.Add(tabAll);
            tabControl1.Controls.Add(tabById);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(458, 206);
            tabControl1.TabIndex = 0;
            // 
            // tabInsert
            // 
            tabInsert.Controls.Add(RTBInsert);
            tabInsert.Location = new Point(4, 24);
            tabInsert.Name = "tabInsert";
            tabInsert.Padding = new Padding(3);
            tabInsert.Size = new Size(450, 178);
            tabInsert.TabIndex = 0;
            tabInsert.Text = "Insert";
            tabInsert.UseVisualStyleBackColor = true;
            // 
            // RTBInsert
            // 
            RTBInsert.Dock = DockStyle.Fill;
            RTBInsert.Location = new Point(3, 3);
            RTBInsert.Name = "RTBInsert";
            RTBInsert.Size = new Size(444, 172);
            RTBInsert.TabIndex = 0;
            RTBInsert.Text = "";
            // 
            // tabUpdate
            // 
            tabUpdate.Controls.Add(RTBUpdate);
            tabUpdate.Location = new Point(4, 24);
            tabUpdate.Name = "tabUpdate";
            tabUpdate.Padding = new Padding(3);
            tabUpdate.Size = new Size(450, 178);
            tabUpdate.TabIndex = 1;
            tabUpdate.Text = "Update";
            tabUpdate.UseVisualStyleBackColor = true;
            // 
            // RTBUpdate
            // 
            RTBUpdate.Dock = DockStyle.Fill;
            RTBUpdate.Location = new Point(3, 3);
            RTBUpdate.Name = "RTBUpdate";
            RTBUpdate.Size = new Size(444, 172);
            RTBUpdate.TabIndex = 0;
            RTBUpdate.Text = "";
            // 
            // tabDelete
            // 
            tabDelete.Controls.Add(RTBDelete);
            tabDelete.Location = new Point(4, 24);
            tabDelete.Name = "tabDelete";
            tabDelete.Size = new Size(450, 178);
            tabDelete.TabIndex = 2;
            tabDelete.Text = "Delete";
            tabDelete.UseVisualStyleBackColor = true;
            // 
            // RTBDelete
            // 
            RTBDelete.Dock = DockStyle.Fill;
            RTBDelete.Location = new Point(0, 0);
            RTBDelete.Name = "RTBDelete";
            RTBDelete.Size = new Size(450, 178);
            RTBDelete.TabIndex = 0;
            RTBDelete.Text = "";
            // 
            // tabAll
            // 
            tabAll.Controls.Add(RTBAll);
            tabAll.Location = new Point(4, 24);
            tabAll.Name = "tabAll";
            tabAll.Size = new Size(450, 178);
            tabAll.TabIndex = 3;
            tabAll.Text = "All";
            tabAll.UseVisualStyleBackColor = true;
            // 
            // RTBAll
            // 
            RTBAll.Dock = DockStyle.Fill;
            RTBAll.Location = new Point(0, 0);
            RTBAll.Name = "RTBAll";
            RTBAll.Size = new Size(450, 178);
            RTBAll.TabIndex = 0;
            RTBAll.Text = "";
            // 
            // tabById
            // 
            tabById.Controls.Add(RTBById);
            tabById.Location = new Point(4, 24);
            tabById.Name = "tabById";
            tabById.Size = new Size(450, 178);
            tabById.TabIndex = 4;
            tabById.Text = "ById";
            tabById.UseVisualStyleBackColor = true;
            // 
            // RTBById
            // 
            RTBById.Dock = DockStyle.Fill;
            RTBById.Location = new Point(0, 0);
            RTBById.Name = "RTBById";
            RTBById.Size = new Size(450, 178);
            RTBById.TabIndex = 0;
            RTBById.Text = "";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { playToolStripMenuItem, refreshToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(691, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // playToolStripMenuItem
            // 
            playToolStripMenuItem.Name = "playToolStripMenuItem";
            playToolStripMenuItem.Size = new Size(41, 20);
            playToolStripMenuItem.Text = "Play";
            // 
            // refreshToolStripMenuItem
            // 
            refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            refreshToolStripMenuItem.Size = new Size(58, 20);
            refreshToolStripMenuItem.Text = "Refresh";
            refreshToolStripMenuItem.Click += refreshToolStripMenuItem_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(691, 367);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            MinimumSize = new Size(700, 400);
            Name = "FormMain";
            Text = "FormMain";
            Load += FormMain_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DGVColumnas).EndInit();
            tabControl1.ResumeLayout(false);
            tabInsert.ResumeLayout(false);
            tabUpdate.ResumeLayout(false);
            tabDelete.ResumeLayout(false);
            tabAll.ResumeLayout(false);
            tabById.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private SplitContainer splitContainer1;
        private TreeView treeView1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem playToolStripMenuItem;
        private ToolStripMenuItem refreshToolStripMenuItem;
        private SplitContainer splitContainer2;
        private DataGridView DGVColumnas;
        private DataGridViewCheckBoxColumn IsIdentity;
        private DataGridViewTextBoxColumn ColumnName;
        private DataGridViewTextBoxColumn DataType;
        private DataGridViewCheckBoxColumn AllowNulls;
        private TabControl tabControl1;
        private TabPage tabInsert;
        private TabPage tabUpdate;
        private TabPage tabDelete;
        private TabPage tabAll;
        private TabPage tabById;
        private RichTextBox RTBInsert;
        private RichTextBox RTBUpdate;
        private RichTextBox RTBDelete;
        private RichTextBox RTBAll;
        private RichTextBox RTBById;
    }
}