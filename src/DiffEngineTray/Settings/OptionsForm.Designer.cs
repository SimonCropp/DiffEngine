﻿partial class OptionsForm
{
    System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    void InitializeComponent()
    {
            this.bottomPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.cancel = new System.Windows.Forms.Button();
            this.save = new System.Windows.Forms.Button();
            this.acceptAllHotKey = new HotKeyControl();
            this.startupCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.versionLabel = new System.Windows.Forms.Label();
            this.trayDocsLink = new System.Windows.Forms.LinkLabel();
            this.diffEngineLink = new System.Windows.Forms.LinkLabel();
            this.acceptOpenHotKey = new HotKeyControl();
            this.bottomPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bottomPanel
            // 
            this.bottomPanel.AutoSize = true;
            this.bottomPanel.Controls.Add(this.cancel);
            this.bottomPanel.Controls.Add(this.save);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.bottomPanel.Location = new System.Drawing.Point(9, 767);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(755, 50);
            this.bottomPanel.TabIndex = 1;
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(614, 3);
            this.cancel.Name = "cancel";
            this.cancel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cancel.Size = new System.Drawing.Size(138, 44);
            this.cancel.TabIndex = 0;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(470, 3);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(138, 44);
            this.save.TabIndex = 1;
            this.save.Text = "Apply";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // acceptAllHotKey
            // 
            this.acceptAllHotKey.AutoSize = true;
            this.acceptAllHotKey.Dock = System.Windows.Forms.DockStyle.Top;
            this.acceptAllHotKey.Help = "Accepts all pending deletes and pending moves";
            this.acceptAllHotKey.HotKey = null;
            this.acceptAllHotKey.Label = "Accept all HotKey";
            this.acceptAllHotKey.Location = new System.Drawing.Point(9, 9);
            this.acceptAllHotKey.Margin = new System.Windows.Forms.Padding(4);
            this.acceptAllHotKey.Name = "acceptAllHotKey";
            this.acceptAllHotKey.Size = new System.Drawing.Size(755, 225);
            this.acceptAllHotKey.TabIndex = 2;
            this.acceptAllHotKey.TabStop = false;
            // 
            // startupCheckBox
            // 
            this.startupCheckBox.AutoSize = true;
            this.startupCheckBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.startupCheckBox.Location = new System.Drawing.Point(9, 459);
            this.startupCheckBox.Name = "startupCheckBox";
            this.startupCheckBox.Padding = new System.Windows.Forms.Padding(9);
            this.startupCheckBox.Size = new System.Drawing.Size(755, 52);
            this.startupCheckBox.TabIndex = 3;
            this.startupCheckBox.Text = "Run at startup";
            this.startupCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.versionLabel);
            this.groupBox1.Controls.Add(this.trayDocsLink);
            this.groupBox1.Controls.Add(this.diffEngineLink);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(9, 511);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox1.Size = new System.Drawing.Size(755, 138);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "More information:";
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.versionLabel.Location = new System.Drawing.Point(10, 98);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(81, 30);
            this.versionLabel.TabIndex = 2;
            this.versionLabel.Text = "Version";
            // 
            // trayDocsLink
            // 
            this.trayDocsLink.AutoSize = true;
            this.trayDocsLink.Dock = System.Windows.Forms.DockStyle.Top;
            this.trayDocsLink.Location = new System.Drawing.Point(10, 68);
            this.trayDocsLink.Name = "trayDocsLink";
            this.trayDocsLink.Size = new System.Drawing.Size(297, 30);
            this.trayDocsLink.TabIndex = 1;
            this.trayDocsLink.TabStop = true;
            this.trayDocsLink.Text = "DiffEngineTray Documentation";
            this.trayDocsLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.trayDocsLink_LinkClicked);
            // 
            // diffEngineLink
            // 
            this.diffEngineLink.AutoSize = true;
            this.diffEngineLink.Dock = System.Windows.Forms.DockStyle.Top;
            this.diffEngineLink.Location = new System.Drawing.Point(10, 38);
            this.diffEngineLink.Name = "diffEngineLink";
            this.diffEngineLink.Size = new System.Drawing.Size(183, 30);
            this.diffEngineLink.TabIndex = 0;
            this.diffEngineLink.TabStop = true;
            this.diffEngineLink.Text = "GitHub/DiffEngine";
            this.diffEngineLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.diffEngineLink_LinkClicked);
            // 
            // acceptOpenHotKey
            // 
            this.acceptOpenHotKey.AutoSize = true;
            this.acceptOpenHotKey.Dock = System.Windows.Forms.DockStyle.Top;
            this.acceptOpenHotKey.Help = "Accepts all peding deletes and pending moves with an open diff tool";
            this.acceptOpenHotKey.HotKey = null;
            this.acceptOpenHotKey.Label = "Accept all open HotKey";
            this.acceptOpenHotKey.Location = new System.Drawing.Point(9, 234);
            this.acceptOpenHotKey.Margin = new System.Windows.Forms.Padding(4);
            this.acceptOpenHotKey.Name = "acceptOpenHotKey";
            this.acceptOpenHotKey.Size = new System.Drawing.Size(755, 225);
            this.acceptOpenHotKey.TabIndex = 6;
            this.acceptOpenHotKey.TabStop = false;
            // 
            // OptionsForm
            // 
            this.AcceptButton = this.save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(773, 826);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.startupCheckBox);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.acceptOpenHotKey);
            this.Controls.Add(this.acceptAllHotKey);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Options";
            this.bottomPanel.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }
    private System.Windows.Forms.FlowLayoutPanel bottomPanel;
    private System.Windows.Forms.Button cancel;
    private System.Windows.Forms.Button save;
    private HotKeyControl acceptAllHotKey;
    private System.Windows.Forms.CheckBox startupCheckBox;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.LinkLabel diffEngineLink;
    private System.Windows.Forms.LinkLabel trayDocsLink;
    private System.Windows.Forms.Label versionLabel;
    private HotKeyControl acceptOpenHotKey;
}