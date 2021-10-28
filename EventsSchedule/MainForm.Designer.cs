namespace EventsSchedule
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.eventsList = new System.Windows.Forms.ListView();
            this.ColumnId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnStartsAt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnFinished = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.addEventButton = new System.Windows.Forms.ToolStripButton();
            this.editEventButton = new System.Windows.Forms.ToolStripButton();
            this.removeEventButton = new System.Windows.Forms.ToolStripButton();
            this.autoRunButton = new System.Windows.Forms.ToolStripButton();
            this.settingsButton = new System.Windows.Forms.ToolStripButton();
            this.eventChecker = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyIconContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.notifyIconContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // eventsList
            // 
            this.eventsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnId,
            this.ColumnName,
            this.ColumnDate,
            this.columnStartsAt,
            this.columnFinished});
            this.eventsList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.eventsList.FullRowSelect = true;
            this.eventsList.HideSelection = false;
            this.eventsList.Location = new System.Drawing.Point(0, 28);
            this.eventsList.Name = "eventsList";
            this.eventsList.Size = new System.Drawing.Size(800, 422);
            this.eventsList.TabIndex = 0;
            this.eventsList.UseCompatibleStateImageBehavior = false;
            this.eventsList.View = System.Windows.Forms.View.Details;
            this.eventsList.DoubleClick += new System.EventHandler(this.eventsList_DoubleClick);
            this.eventsList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.eventsList_KeyDown);
            // 
            // ColumnId
            // 
            this.ColumnId.Text = "ID";
            // 
            // ColumnName
            // 
            this.ColumnName.Text = "Event Name";
            this.ColumnName.Width = 256;
            // 
            // ColumnDate
            // 
            this.ColumnDate.Text = "Event Time";
            this.ColumnDate.Width = 190;
            // 
            // columnStartsAt
            // 
            this.columnStartsAt.Text = "Event Starts At";
            this.columnStartsAt.Width = 173;
            // 
            // columnFinished
            // 
            this.columnFinished.Text = "Is Finished?";
            this.columnFinished.Width = 73;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addEventButton,
            this.editEventButton,
            this.removeEventButton,
            this.autoRunButton,
            this.settingsButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // addEventButton
            // 
            this.addEventButton.Image = ((System.Drawing.Image)(resources.GetObject("addEventButton.Image")));
            this.addEventButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addEventButton.Name = "addEventButton";
            this.addEventButton.Size = new System.Drawing.Size(49, 22);
            this.addEventButton.Text = "Add";
            this.addEventButton.Click += new System.EventHandler(this.addEventButton_Click);
            // 
            // editEventButton
            // 
            this.editEventButton.Image = ((System.Drawing.Image)(resources.GetObject("editEventButton.Image")));
            this.editEventButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editEventButton.Name = "editEventButton";
            this.editEventButton.Size = new System.Drawing.Size(47, 22);
            this.editEventButton.Text = "Edit";
            this.editEventButton.Click += new System.EventHandler(this.editEventButton_Click);
            // 
            // removeEventButton
            // 
            this.removeEventButton.Image = ((System.Drawing.Image)(resources.GetObject("removeEventButton.Image")));
            this.removeEventButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeEventButton.Name = "removeEventButton";
            this.removeEventButton.Size = new System.Drawing.Size(70, 22);
            this.removeEventButton.Text = "Remove";
            this.removeEventButton.Click += new System.EventHandler(this.removeEventButton_Click);
            // 
            // autoRunButton
            // 
            this.autoRunButton.Image = ((System.Drawing.Image)(resources.GetObject("autoRunButton.Image")));
            this.autoRunButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.autoRunButton.Name = "autoRunButton";
            this.autoRunButton.Size = new System.Drawing.Size(129, 22);
            this.autoRunButton.Text = "Start with Windows";
            this.autoRunButton.Click += new System.EventHandler(this.autoRunButton_Click);
            // 
            // settingsButton
            // 
            this.settingsButton.Image = ((System.Drawing.Image)(resources.GetObject("settingsButton.Image")));
            this.settingsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(69, 22);
            this.settingsButton.Text = "Settings";
            this.settingsButton.ToolTipText = "Settings";
            this.settingsButton.Click += new System.EventHandler(this.settingsButtons_Click);
            // 
            // eventChecker
            // 
            this.eventChecker.Enabled = true;
            this.eventChecker.Interval = 1000;
            this.eventChecker.Tick += new System.EventHandler(this.eventChecker_Tick);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "Programm in tray";
            this.notifyIcon.ContextMenuStrip = this.notifyIconContextMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Events Schedule";
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // notifyIconContextMenu
            // 
            this.notifyIconContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitButton});
            this.notifyIconContextMenu.Name = "notifyIconContextMenu";
            this.notifyIconContextMenu.Size = new System.Drawing.Size(94, 26);
            // 
            // exitButton
            // 
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(93, 22);
            this.exitButton.Text = "Exit";
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.eventsList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Events Schedule";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.notifyIconContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ToolStripButton addEventButton;

        private System.Windows.Forms.ToolStrip toolStrip1;

        #endregion

        private System.Windows.Forms.ColumnHeader ColumnId;
        private System.Windows.Forms.ColumnHeader ColumnName;
        private System.Windows.Forms.ColumnHeader ColumnDate;
        private System.Windows.Forms.ListView eventsList;
        private System.Windows.Forms.ToolStripButton editEventButton;
        private System.Windows.Forms.ToolStripButton removeEventButton;
        private System.Windows.Forms.Timer eventChecker;
        private System.Windows.Forms.ColumnHeader columnStartsAt;
        private System.Windows.Forms.ColumnHeader columnFinished;
        private System.Windows.Forms.ToolStripButton autoRunButton;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip notifyIconContextMenu;
        private System.Windows.Forms.ToolStripMenuItem exitButton;
        private System.Windows.Forms.ToolStripButton settingsButton;
    }
}