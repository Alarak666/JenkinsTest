namespace FZFarm.MainApp.Forms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            spinContainer = new SplitContainer();
            GroupBoxModul = new GroupBox();
            menuStrip1 = new MenuStrip();
            файлToolStripMenuItem = new ToolStripMenuItem();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)spinContainer).BeginInit();
            spinContainer.Panel1.SuspendLayout();
            spinContainer.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 6F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 99.99999F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 6F));
            tableLayoutPanel1.Controls.Add(spinContainer, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 28);
            tableLayoutPanel1.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 99.99999F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            tableLayoutPanel1.Size = new Size(1297, 619);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // spinContainer
            // 
            spinContainer.Location = new Point(10, 8);
            spinContainer.Margin = new Padding(4, 3, 4, 3);
            spinContainer.Name = "spinContainer";
            // 
            // spinContainer.Panel1
            // 
            spinContainer.Panel1.Controls.Add(GroupBoxModul);
            spinContainer.Panel1.RightToLeft = RightToLeft.No;
            // 
            // spinContainer.Panel2
            // 
            spinContainer.Panel2.RightToLeft = RightToLeft.No;
            spinContainer.Size = new Size(1277, 603);
            spinContainer.SplitterDistance = 410;
            spinContainer.SplitterWidth = 5;
            spinContainer.TabIndex = 1;
            // 
            // groupBox1
            // 
            GroupBoxModul.Dock = DockStyle.Fill;
            GroupBoxModul.Location = new Point(0, 0);
            GroupBoxModul.Margin = new Padding(4, 3, 4, 3);
            GroupBoxModul.Name = "GroupBoxModul";
            GroupBoxModul.Padding = new Padding(4, 3, 4, 3);
            GroupBoxModul.Size = new Size(410, 603);
            GroupBoxModul.TabIndex = 0;
            GroupBoxModul.TabStop = false;
            GroupBoxModul.Text = "Forms";
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { файлToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1297, 28);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new Size(59, 24);
            файлToolStripMenuItem.Text = "Файл";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1297, 647);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(menuStrip1);
            Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Margin = new Padding(4, 3, 4, 3);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FozzyFarm";
            tableLayoutPanel1.ResumeLayout(false);
            spinContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)spinContainer).EndInit();
            spinContainer.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TableLayoutPanel tableLayoutPanel1;
        private SplitContainer spinContainer;
        private GroupBox GroupBoxModul;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem файлToolStripMenuItem;
    }
}
