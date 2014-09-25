namespace AudioRenamer
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.labelRuntime = new System.Windows.Forms.Label();
            this.numericUpDownHours = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMinutes = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSeconds = new System.Windows.Forms.NumericUpDown();
            this.textBoxSeconds = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanelFiles = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxSourceFiles = new System.Windows.Forms.GroupBox();
            this.groupBoxNewNames = new System.Windows.Forms.GroupBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonRename = new System.Windows.Forms.Button();
            this.tableLayoutPanelIgnoredFiles = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxIgnored = new System.Windows.Forms.GroupBox();
            this.groupBoxIgnoredWhy = new System.Windows.Forms.GroupBox();
            this.listBoxIgnored = new BetterListBox();
            this.listBoxIgnoredWhy = new BetterListBox();
            this.listBoxSource = new BetterListBox();
            this.listBoxNewName = new BetterListBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeconds)).BeginInit();
            this.tableLayoutPanelFiles.SuspendLayout();
            this.groupBoxSourceFiles.SuspendLayout();
            this.groupBoxNewNames.SuspendLayout();
            this.tableLayoutPanelIgnoredFiles.SuspendLayout();
            this.groupBoxIgnored.SuspendLayout();
            this.groupBoxIgnoredWhy.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelRuntime
            // 
            this.labelRuntime.AutoSize = true;
            this.labelRuntime.Location = new System.Drawing.Point(12, 14);
            this.labelRuntime.Name = "labelRuntime";
            this.labelRuntime.Size = new System.Drawing.Size(46, 13);
            this.labelRuntime.TabIndex = 0;
            this.labelRuntime.Text = "Runtime";
            // 
            // numericUpDownHours
            // 
            this.numericUpDownHours.Location = new System.Drawing.Point(64, 12);
            this.numericUpDownHours.Name = "numericUpDownHours";
            this.numericUpDownHours.Size = new System.Drawing.Size(40, 20);
            this.numericUpDownHours.TabIndex = 1;
            this.toolTip1.SetToolTip(this.numericUpDownHours, "Hours");
            this.numericUpDownHours.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDownHours.Enter += new System.EventHandler(this.numericUpDown_SelectAll);
            // 
            // numericUpDownMinutes
            // 
            this.numericUpDownMinutes.Location = new System.Drawing.Point(110, 12);
            this.numericUpDownMinutes.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownMinutes.Name = "numericUpDownMinutes";
            this.numericUpDownMinutes.Size = new System.Drawing.Size(40, 20);
            this.numericUpDownMinutes.TabIndex = 2;
            this.toolTip1.SetToolTip(this.numericUpDownMinutes, "Minutes");
            this.numericUpDownMinutes.ValueChanged += new System.EventHandler(this.numericUpDownMinutes_ValueChanged);
            this.numericUpDownMinutes.Enter += new System.EventHandler(this.numericUpDown_SelectAll);
            // 
            // numericUpDownSeconds
            // 
            this.numericUpDownSeconds.Location = new System.Drawing.Point(156, 12);
            this.numericUpDownSeconds.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownSeconds.Name = "numericUpDownSeconds";
            this.numericUpDownSeconds.Size = new System.Drawing.Size(40, 20);
            this.numericUpDownSeconds.TabIndex = 3;
            this.toolTip1.SetToolTip(this.numericUpDownSeconds, "Seconds");
            this.numericUpDownSeconds.ValueChanged += new System.EventHandler(this.numericUpDownSeconds_ValueChanged);
            this.numericUpDownSeconds.Enter += new System.EventHandler(this.numericUpDown_SelectAll);
            // 
            // textBoxSeconds
            // 
            this.textBoxSeconds.Location = new System.Drawing.Point(202, 12);
            this.textBoxSeconds.Name = "textBoxSeconds";
            this.textBoxSeconds.ReadOnly = true;
            this.textBoxSeconds.Size = new System.Drawing.Size(100, 20);
            this.textBoxSeconds.TabIndex = 4;
            this.textBoxSeconds.TabStop = false;
            this.toolTip1.SetToolTip(this.textBoxSeconds, "Total Runtime (in seconds)");
            // 
            // tableLayoutPanelFiles
            // 
            this.tableLayoutPanelFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelFiles.ColumnCount = 2;
            this.tableLayoutPanelFiles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelFiles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelFiles.Controls.Add(this.groupBoxSourceFiles, 0, 0);
            this.tableLayoutPanelFiles.Controls.Add(this.groupBoxNewNames, 1, 0);
            this.tableLayoutPanelFiles.Controls.Add(this.buttonClear, 0, 1);
            this.tableLayoutPanelFiles.Controls.Add(this.buttonRename, 1, 1);
            this.tableLayoutPanelFiles.Location = new System.Drawing.Point(12, 38);
            this.tableLayoutPanelFiles.Name = "tableLayoutPanelFiles";
            this.tableLayoutPanelFiles.RowCount = 2;
            this.tableLayoutPanelFiles.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelFiles.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelFiles.Size = new System.Drawing.Size(702, 397);
            this.tableLayoutPanelFiles.TabIndex = 5;
            // 
            // groupBoxSourceFiles
            // 
            this.groupBoxSourceFiles.Controls.Add(this.listBoxSource);
            this.groupBoxSourceFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSourceFiles.Location = new System.Drawing.Point(3, 3);
            this.groupBoxSourceFiles.Name = "groupBoxSourceFiles";
            this.groupBoxSourceFiles.Size = new System.Drawing.Size(345, 362);
            this.groupBoxSourceFiles.TabIndex = 0;
            this.groupBoxSourceFiles.TabStop = false;
            this.groupBoxSourceFiles.Text = "Source (Old Names)";
            // 
            // groupBoxNewNames
            // 
            this.groupBoxNewNames.Controls.Add(this.listBoxNewName);
            this.groupBoxNewNames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxNewNames.Location = new System.Drawing.Point(354, 3);
            this.groupBoxNewNames.Name = "groupBoxNewNames";
            this.groupBoxNewNames.Size = new System.Drawing.Size(345, 362);
            this.groupBoxNewNames.TabIndex = 1;
            this.groupBoxNewNames.TabStop = false;
            this.groupBoxNewNames.Text = "New Names";
            // 
            // buttonClear
            // 
            this.buttonClear.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonClear.Location = new System.Drawing.Point(3, 371);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(345, 23);
            this.buttonClear.TabIndex = 2;
            this.buttonClear.Text = "Clear All";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonRename
            // 
            this.buttonRename.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonRename.Location = new System.Drawing.Point(354, 371);
            this.buttonRename.Name = "buttonRename";
            this.buttonRename.Size = new System.Drawing.Size(345, 23);
            this.buttonRename.TabIndex = 3;
            this.buttonRename.Text = "Rename";
            this.buttonRename.UseVisualStyleBackColor = true;
            this.buttonRename.Click += new System.EventHandler(this.buttonRename_Click);
            // 
            // tableLayoutPanelIgnoredFiles
            // 
            this.tableLayoutPanelIgnoredFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelIgnoredFiles.ColumnCount = 2;
            this.tableLayoutPanelIgnoredFiles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelIgnoredFiles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelIgnoredFiles.Controls.Add(this.groupBoxIgnored, 0, 0);
            this.tableLayoutPanelIgnoredFiles.Controls.Add(this.groupBoxIgnoredWhy, 1, 0);
            this.tableLayoutPanelIgnoredFiles.Location = new System.Drawing.Point(12, 441);
            this.tableLayoutPanelIgnoredFiles.Name = "tableLayoutPanelIgnoredFiles";
            this.tableLayoutPanelIgnoredFiles.RowCount = 1;
            this.tableLayoutPanelIgnoredFiles.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelIgnoredFiles.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanelIgnoredFiles.Size = new System.Drawing.Size(702, 100);
            this.tableLayoutPanelIgnoredFiles.TabIndex = 6;
            // 
            // groupBoxIgnored
            // 
            this.groupBoxIgnored.Controls.Add(this.listBoxIgnored);
            this.groupBoxIgnored.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxIgnored.Location = new System.Drawing.Point(3, 3);
            this.groupBoxIgnored.Name = "groupBoxIgnored";
            this.groupBoxIgnored.Size = new System.Drawing.Size(345, 94);
            this.groupBoxIgnored.TabIndex = 0;
            this.groupBoxIgnored.TabStop = false;
            this.groupBoxIgnored.Text = "Ignored Files";
            // 
            // groupBoxIgnoredWhy
            // 
            this.groupBoxIgnoredWhy.Controls.Add(this.listBoxIgnoredWhy);
            this.groupBoxIgnoredWhy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxIgnoredWhy.Location = new System.Drawing.Point(354, 3);
            this.groupBoxIgnoredWhy.Name = "groupBoxIgnoredWhy";
            this.groupBoxIgnoredWhy.Size = new System.Drawing.Size(345, 94);
            this.groupBoxIgnoredWhy.TabIndex = 1;
            this.groupBoxIgnoredWhy.TabStop = false;
            this.groupBoxIgnoredWhy.Text = "Why?";
            // 
            // listBoxIgnored
            // 
            this.listBoxIgnored.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxIgnored.FormattingEnabled = true;
            this.listBoxIgnored.HorizontalScrollbar = true;
            this.listBoxIgnored.Location = new System.Drawing.Point(3, 16);
            this.listBoxIgnored.Name = "listBoxIgnored";
            this.listBoxIgnored.Size = new System.Drawing.Size(339, 75);
            this.listBoxIgnored.TabIndex = 0;
            this.listBoxIgnored.Scroll += new BetterListBox.BetterListBoxScrollDelegate(this.listBoxIgnored_Scroll);
            this.listBoxIgnored.SelectedIndexChanged += new System.EventHandler(this.listBoxIgnored_SelectedIndexChanged);
            // 
            // listBoxIgnoredWhy
            // 
            this.listBoxIgnoredWhy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxIgnoredWhy.Enabled = false;
            this.listBoxIgnoredWhy.FormattingEnabled = true;
            this.listBoxIgnoredWhy.Location = new System.Drawing.Point(3, 16);
            this.listBoxIgnoredWhy.Name = "listBoxIgnoredWhy";
            this.listBoxIgnoredWhy.Size = new System.Drawing.Size(339, 75);
            this.listBoxIgnoredWhy.TabIndex = 0;
            // 
            // listBoxSource
            // 
            this.listBoxSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxSource.FormattingEnabled = true;
            this.listBoxSource.HorizontalScrollbar = true;
            this.listBoxSource.Location = new System.Drawing.Point(3, 16);
            this.listBoxSource.Name = "listBoxSource";
            this.listBoxSource.Size = new System.Drawing.Size(339, 343);
            this.listBoxSource.TabIndex = 0;
            this.listBoxSource.Scroll += new BetterListBox.BetterListBoxScrollDelegate(this.listBoxSource_Scroll);
            this.listBoxSource.SelectedIndexChanged += new System.EventHandler(this.listBoxSource_SelectedIndexChanged);
            // 
            // listBoxNewName
            // 
            this.listBoxNewName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxNewName.Enabled = false;
            this.listBoxNewName.FormattingEnabled = true;
            this.listBoxNewName.Location = new System.Drawing.Point(3, 16);
            this.listBoxNewName.Name = "listBoxNewName";
            this.listBoxNewName.Size = new System.Drawing.Size(339, 343);
            this.listBoxNewName.TabIndex = 0;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 553);
            this.Controls.Add(this.tableLayoutPanelIgnoredFiles);
            this.Controls.Add(this.tableLayoutPanelFiles);
            this.Controls.Add(this.textBoxSeconds);
            this.Controls.Add(this.numericUpDownSeconds);
            this.Controls.Add(this.numericUpDownMinutes);
            this.Controls.Add(this.numericUpDownHours);
            this.Controls.Add(this.labelRuntime);
            this.Name = "Form1";
            this.Text = "Audio Renamer (Bitrate Calculator)";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeconds)).EndInit();
            this.tableLayoutPanelFiles.ResumeLayout(false);
            this.groupBoxSourceFiles.ResumeLayout(false);
            this.groupBoxNewNames.ResumeLayout(false);
            this.tableLayoutPanelIgnoredFiles.ResumeLayout(false);
            this.groupBoxIgnored.ResumeLayout(false);
            this.groupBoxIgnoredWhy.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelRuntime;
        private System.Windows.Forms.NumericUpDown numericUpDownHours;
        private System.Windows.Forms.NumericUpDown numericUpDownMinutes;
        private System.Windows.Forms.NumericUpDown numericUpDownSeconds;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox textBoxSeconds;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFiles;
        private System.Windows.Forms.GroupBox groupBoxSourceFiles;
        private System.Windows.Forms.GroupBox groupBoxNewNames;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonRename;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelIgnoredFiles;
        private BetterListBox listBoxSource;
        private BetterListBox listBoxNewName;
        private System.Windows.Forms.GroupBox groupBoxIgnored;
        private BetterListBox listBoxIgnored;
        private System.Windows.Forms.GroupBox groupBoxIgnoredWhy;
        private BetterListBox listBoxIgnoredWhy;
    }
}

