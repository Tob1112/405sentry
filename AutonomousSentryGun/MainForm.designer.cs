﻿namespace AutonomousSentryGun
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
        this.menuStrip1 = new System.Windows.Forms.MenuStrip();
        this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.loadSetupFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.saveSetupFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.resetToDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.transmitPositionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.cameraFeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.motionDetectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.onOffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.gunTriggerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.gunAimAndShootToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.setupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.calibrateGunToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.motionDetectionThresholdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.gunTriggerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        this.dataTransmissionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.servosRangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.panel1 = new System.Windows.Forms.Panel();
        this.statusBar = new System.Windows.Forms.StatusStrip();
        this.fpsLabel = new System.Windows.Forms.ToolStripStatusLabel();
        this.objectsCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
        this.timer = new System.Windows.Forms.Timer(this.components);
        this.cameraWindow1 = new AutonomousSentryGun.CameraWindow();
        this.menuStrip1.SuspendLayout();
        this.panel1.SuspendLayout();
        this.statusBar.SuspendLayout();
        this.SuspendLayout();
        // 
        // menuStrip1
        // 
        this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.testToolStripMenuItem,
            this.setupToolStripMenuItem});
        this.menuStrip1.Location = new System.Drawing.Point(0, 0);
        this.menuStrip1.Name = "menuStrip1";
        this.menuStrip1.Size = new System.Drawing.Size(432, 24);
        this.menuStrip1.TabIndex = 0;
        this.menuStrip1.Text = "menuStrip1";
        // 
        // fileToolStripMenuItem
        // 
        this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadSetupFileToolStripMenuItem,
            this.saveSetupFileToolStripMenuItem,
            this.resetToDefaultToolStripMenuItem,
            this.exitToolStripMenuItem});
        this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
        this.fileToolStripMenuItem.Text = "File";
        // 
        // loadSetupFileToolStripMenuItem
        // 
        this.loadSetupFileToolStripMenuItem.Name = "loadSetupFileToolStripMenuItem";
        this.loadSetupFileToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
        this.loadSetupFileToolStripMenuItem.Text = "Load Setup File...";
        // 
        // saveSetupFileToolStripMenuItem
        // 
        this.saveSetupFileToolStripMenuItem.Name = "saveSetupFileToolStripMenuItem";
        this.saveSetupFileToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
        this.saveSetupFileToolStripMenuItem.Text = "Save Setup File...";
        // 
        // resetToDefaultToolStripMenuItem
        // 
        this.resetToDefaultToolStripMenuItem.Name = "resetToDefaultToolStripMenuItem";
        this.resetToDefaultToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
        this.resetToDefaultToolStripMenuItem.Text = "Reset to Default";
        this.resetToDefaultToolStripMenuItem.Click += new System.EventHandler(this.resetToDefaultToolStripMenuItem_Click);
        // 
        // exitToolStripMenuItem
        // 
        this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
        this.exitToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
        this.exitToolStripMenuItem.Text = "Exit";
        this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
        // 
        // testToolStripMenuItem
        // 
        this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.transmitPositionToolStripMenuItem,
            this.cameraFeedToolStripMenuItem,
            this.motionDetectionToolStripMenuItem,
            this.gunTriggerToolStripMenuItem,
            this.gunAimAndShootToolStripMenuItem});
        this.testToolStripMenuItem.Name = "testToolStripMenuItem";
        this.testToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
        this.testToolStripMenuItem.Text = "Test";
        // 
        // transmitPositionToolStripMenuItem
        // 
        this.transmitPositionToolStripMenuItem.Name = "transmitPositionToolStripMenuItem";
        this.transmitPositionToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
        this.transmitPositionToolStripMenuItem.Text = "Transmit Position";
        this.transmitPositionToolStripMenuItem.Click += new System.EventHandler(this.transmitPositionToolStripMenuItem_Click);
        // 
        // cameraFeedToolStripMenuItem
        // 
        this.cameraFeedToolStripMenuItem.Name = "cameraFeedToolStripMenuItem";
        this.cameraFeedToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
        this.cameraFeedToolStripMenuItem.Text = "Camera Feed";
        this.cameraFeedToolStripMenuItem.Click += new System.EventHandler(this.cameraFeedToolStripMenuItem_Click);
        // 
        // motionDetectionToolStripMenuItem
        // 
        this.motionDetectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onOffToolStripMenuItem});
        this.motionDetectionToolStripMenuItem.Name = "motionDetectionToolStripMenuItem";
        this.motionDetectionToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
        this.motionDetectionToolStripMenuItem.Text = "Motion Detection";
        // 
        // onOffToolStripMenuItem
        // 
        this.onOffToolStripMenuItem.Name = "onOffToolStripMenuItem";
        this.onOffToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
        this.onOffToolStripMenuItem.Text = "On/Off";
        this.onOffToolStripMenuItem.Click += new System.EventHandler(this.onOffToolStripMenuItem_Click);
        // 
        // gunTriggerToolStripMenuItem
        // 
        this.gunTriggerToolStripMenuItem.Name = "gunTriggerToolStripMenuItem";
        this.gunTriggerToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
        this.gunTriggerToolStripMenuItem.Text = "Gun Trigger";
        // 
        // gunAimAndShootToolStripMenuItem
        // 
        this.gunAimAndShootToolStripMenuItem.Name = "gunAimAndShootToolStripMenuItem";
        this.gunAimAndShootToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
        this.gunAimAndShootToolStripMenuItem.Text = "Gun Aim and Shoot";
        // 
        // setupToolStripMenuItem
        // 
        this.setupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.calibrateGunToolStripMenuItem,
            this.motionDetectionThresholdToolStripMenuItem,
            this.gunTriggerToolStripMenuItem1,
            this.dataTransmissionToolStripMenuItem,
            this.servosRangeToolStripMenuItem});
        this.setupToolStripMenuItem.Name = "setupToolStripMenuItem";
        this.setupToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
        this.setupToolStripMenuItem.Text = "Setup";
        // 
        // calibrateGunToolStripMenuItem
        // 
        this.calibrateGunToolStripMenuItem.Name = "calibrateGunToolStripMenuItem";
        this.calibrateGunToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
        this.calibrateGunToolStripMenuItem.Text = "Calibrate Gun";
        // 
        // motionDetectionThresholdToolStripMenuItem
        // 
        this.motionDetectionThresholdToolStripMenuItem.Name = "motionDetectionThresholdToolStripMenuItem";
        this.motionDetectionThresholdToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
        this.motionDetectionThresholdToolStripMenuItem.Text = "Motion Detection Threshold";
        // 
        // gunTriggerToolStripMenuItem1
        // 
        this.gunTriggerToolStripMenuItem1.Name = "gunTriggerToolStripMenuItem1";
        this.gunTriggerToolStripMenuItem1.Size = new System.Drawing.Size(216, 22);
        this.gunTriggerToolStripMenuItem1.Text = "Gun Trigger";
        // 
        // dataTransmissionToolStripMenuItem
        // 
        this.dataTransmissionToolStripMenuItem.Name = "dataTransmissionToolStripMenuItem";
        this.dataTransmissionToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
        this.dataTransmissionToolStripMenuItem.Text = "Data Transmission";
        // 
        // servosRangeToolStripMenuItem
        // 
        this.servosRangeToolStripMenuItem.Name = "servosRangeToolStripMenuItem";
        this.servosRangeToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
        this.servosRangeToolStripMenuItem.Text = "Servos Range";
        // 
        // panel1
        // 
        this.panel1.Controls.Add(this.cameraWindow1);
        this.panel1.Location = new System.Drawing.Point(0, 27);
        this.panel1.Name = "panel1";
        this.panel1.Size = new System.Drawing.Size(432, 300);
        this.panel1.TabIndex = 2;
        // 
        // statusBar
        // 
        this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fpsLabel,
            this.objectsCountLabel});
        this.statusBar.Location = new System.Drawing.Point(0, 327);
        this.statusBar.Name = "statusBar";
        this.statusBar.Size = new System.Drawing.Size(432, 22);
        this.statusBar.TabIndex = 4;
        // 
        // fpsLabel
        // 
        this.fpsLabel.AutoSize = false;
        this.fpsLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                    | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                    | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
        this.fpsLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
        this.fpsLabel.Name = "fpsLabel";
        this.fpsLabel.Size = new System.Drawing.Size(150, 17);
        this.fpsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // objectsCountLabel
        // 
        this.objectsCountLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                    | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                    | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
        this.objectsCountLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
        this.objectsCountLabel.Name = "objectsCountLabel";
        this.objectsCountLabel.Size = new System.Drawing.Size(267, 17);
        this.objectsCountLabel.Spring = true;
        this.objectsCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // timer
        // 
        this.timer.Interval = 1000;
        this.timer.Tick += new System.EventHandler(this.timer_Tick);
        // 
        // cameraWindow1
        // 
        this.cameraWindow1.AutoSizeControl = true;
        this.cameraWindow1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
        this.cameraWindow1.Camera = null;
        this.cameraWindow1.Location = new System.Drawing.Point(55, 29);
        this.cameraWindow1.Name = "cameraWindow1";
        this.cameraWindow1.Size = new System.Drawing.Size(322, 242);
        this.cameraWindow1.TabIndex = 1;
        this.cameraWindow1.Text = "cameraWindow1";
        // 
        // MainForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(432, 349);
        this.Controls.Add(this.statusBar);
        this.Controls.Add(this.menuStrip1);
        this.Controls.Add(this.panel1);
        this.MainMenuStrip = this.menuStrip1;
        this.Name = "MainForm";
        this.Text = "Autonomous Sentry Gun";
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
        this.menuStrip1.ResumeLayout(false);
        this.menuStrip1.PerformLayout();
        this.panel1.ResumeLayout(false);
        this.statusBar.ResumeLayout(false);
        this.statusBar.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem loadSetupFileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveSetupFileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem resetToDefaultToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem transmitPositionToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem setupToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem cameraFeedToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem motionDetectionToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem gunTriggerToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem gunAimAndShootToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem calibrateGunToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem motionDetectionThresholdToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem gunTriggerToolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem dataTransmissionToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem servosRangeToolStripMenuItem;
    private CameraWindow cameraWindow1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.StatusStrip statusBar;
    private System.Windows.Forms.ToolStripStatusLabel fpsLabel;
    private System.Windows.Forms.ToolStripStatusLabel objectsCountLabel;
    private System.Windows.Forms.Timer timer;
    private System.Windows.Forms.ToolStripMenuItem onOffToolStripMenuItem;
  }
}
