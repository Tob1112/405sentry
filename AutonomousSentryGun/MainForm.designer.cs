namespace AutonomousSentryGun
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
      this.gunTriggerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.gunAimAndShootToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.setupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.calibrateGunToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.motionDetectionThresholdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.gunTriggerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.dataTransmissionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.servosRangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.menuStrip1.SuspendLayout();
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
      this.menuStrip1.Size = new System.Drawing.Size(784, 24);
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
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "File";
      // 
      // loadSetupFileToolStripMenuItem
      // 
      this.loadSetupFileToolStripMenuItem.Name = "loadSetupFileToolStripMenuItem";
      this.loadSetupFileToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
      this.loadSetupFileToolStripMenuItem.Text = "Load Setup File...";
      // 
      // saveSetupFileToolStripMenuItem
      // 
      this.saveSetupFileToolStripMenuItem.Name = "saveSetupFileToolStripMenuItem";
      this.saveSetupFileToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
      this.saveSetupFileToolStripMenuItem.Text = "Save Setup File...";
      // 
      // resetToDefaultToolStripMenuItem
      // 
      this.resetToDefaultToolStripMenuItem.Name = "resetToDefaultToolStripMenuItem";
      this.resetToDefaultToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
      this.resetToDefaultToolStripMenuItem.Text = "Reset to Default";
      this.resetToDefaultToolStripMenuItem.Click += new System.EventHandler(this.resetToDefaultToolStripMenuItem_Click);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
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
      this.testToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
      this.testToolStripMenuItem.Text = "Test";
      // 
      // transmitPositionToolStripMenuItem
      // 
      this.transmitPositionToolStripMenuItem.Name = "transmitPositionToolStripMenuItem";
      this.transmitPositionToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
      this.transmitPositionToolStripMenuItem.Text = "Transmit Position";
      this.transmitPositionToolStripMenuItem.Click += new System.EventHandler(this.transmitPositionToolStripMenuItem_Click);
      // 
      // cameraFeedToolStripMenuItem
      // 
      this.cameraFeedToolStripMenuItem.Name = "cameraFeedToolStripMenuItem";
      this.cameraFeedToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
      this.cameraFeedToolStripMenuItem.Text = "Camera Feed";
      // 
      // motionDetectionToolStripMenuItem
      // 
      this.motionDetectionToolStripMenuItem.Name = "motionDetectionToolStripMenuItem";
      this.motionDetectionToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
      this.motionDetectionToolStripMenuItem.Text = "Motion Detection";
      // 
      // gunTriggerToolStripMenuItem
      // 
      this.gunTriggerToolStripMenuItem.Name = "gunTriggerToolStripMenuItem";
      this.gunTriggerToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
      this.gunTriggerToolStripMenuItem.Text = "Gun Trigger";
      // 
      // gunAimAndShootToolStripMenuItem
      // 
      this.gunAimAndShootToolStripMenuItem.Name = "gunAimAndShootToolStripMenuItem";
      this.gunAimAndShootToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
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
      this.setupToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
      this.setupToolStripMenuItem.Text = "Setup";
      // 
      // calibrateGunToolStripMenuItem
      // 
      this.calibrateGunToolStripMenuItem.Name = "calibrateGunToolStripMenuItem";
      this.calibrateGunToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
      this.calibrateGunToolStripMenuItem.Text = "Calibrate Gun";
      // 
      // motionDetectionThresholdToolStripMenuItem
      // 
      this.motionDetectionThresholdToolStripMenuItem.Name = "motionDetectionThresholdToolStripMenuItem";
      this.motionDetectionThresholdToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
      this.motionDetectionThresholdToolStripMenuItem.Text = "Motion Detection Threshold";
      // 
      // gunTriggerToolStripMenuItem1
      // 
      this.gunTriggerToolStripMenuItem1.Name = "gunTriggerToolStripMenuItem1";
      this.gunTriggerToolStripMenuItem1.Size = new System.Drawing.Size(223, 22);
      this.gunTriggerToolStripMenuItem1.Text = "Gun Trigger";
      // 
      // dataTransmissionToolStripMenuItem
      // 
      this.dataTransmissionToolStripMenuItem.Name = "dataTransmissionToolStripMenuItem";
      this.dataTransmissionToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
      this.dataTransmissionToolStripMenuItem.Text = "Data Transmission";
      // 
      // servosRangeToolStripMenuItem
      // 
      this.servosRangeToolStripMenuItem.Name = "servosRangeToolStripMenuItem";
      this.servosRangeToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
      this.servosRangeToolStripMenuItem.Text = "Servos Range";
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(784, 564);
      this.Controls.Add(this.menuStrip1);
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "MainForm";
      this.Text = "Autonomous Sentry Gun";
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
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
  }
}

