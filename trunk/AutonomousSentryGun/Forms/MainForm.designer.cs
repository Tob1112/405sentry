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
        this.components = new System.ComponentModel.Container();
        this.menuStrip1 = new System.Windows.Forms.MenuStrip();
        this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.loadSetupFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.saveSetupFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.resetToDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.transmitPositionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.remoteAimToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.fPSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.setupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.motionDetectionSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.dataTransmissionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.servosSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.soundSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.onOffSoundMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.panel1 = new System.Windows.Forms.Panel();
        this.groupBox5 = new System.Windows.Forms.GroupBox();
        this.AllOffButton = new System.Windows.Forms.Button();
        this.groupBox4 = new System.Windows.Forms.GroupBox();
        this.GunFireButton = new System.Windows.Forms.Button();
        this.groupBox3 = new System.Windows.Forms.GroupBox();
        this.GunTrackButton = new System.Windows.Forms.Button();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.MotionDetectionButton = new System.Windows.Forms.Button();
        this.aimDot = new System.Windows.Forms.PictureBox();
        this.cameraWindow1 = new AutonomousSentryGun.CameraWindow();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.CameraFeedButton = new System.Windows.Forms.Button();
        this.statusBar = new System.Windows.Forms.StatusStrip();
        this.fpsLabel = new System.Windows.Forms.ToolStripStatusLabel();
        this.objectsCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
        this.timer = new System.Windows.Forms.Timer(this.components);
        this.TrackingTimer = new System.Windows.Forms.Timer(this.components);
        this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
        this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
        this.menuStrip1.SuspendLayout();
        this.panel1.SuspendLayout();
        this.groupBox5.SuspendLayout();
        this.groupBox4.SuspendLayout();
        this.groupBox3.SuspendLayout();
        this.groupBox2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.aimDot)).BeginInit();
        this.groupBox1.SuspendLayout();
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
        this.menuStrip1.Size = new System.Drawing.Size(858, 24);
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
        this.loadSetupFileToolStripMenuItem.Click += new System.EventHandler(this.loadSetupFileToolStripMenuItem_Click);
        // 
        // saveSetupFileToolStripMenuItem
        // 
        this.saveSetupFileToolStripMenuItem.Name = "saveSetupFileToolStripMenuItem";
        this.saveSetupFileToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
        this.saveSetupFileToolStripMenuItem.Text = "Save Setup File...";
        this.saveSetupFileToolStripMenuItem.Click += new System.EventHandler(this.saveSetupFileToolStripMenuItem_Click);
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
            this.remoteAimToolStripMenuItem,
            this.fPSToolStripMenuItem});
        this.testToolStripMenuItem.Name = "testToolStripMenuItem";
        this.testToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
        this.testToolStripMenuItem.Text = "Test";
        // 
        // transmitPositionToolStripMenuItem
        // 
        this.transmitPositionToolStripMenuItem.Name = "transmitPositionToolStripMenuItem";
        this.transmitPositionToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
        this.transmitPositionToolStripMenuItem.Text = "Transmit Position";
        this.transmitPositionToolStripMenuItem.Click += new System.EventHandler(this.transmitPositionToolStripMenuItem_Click);
        // 
        // remoteAimToolStripMenuItem
        // 
        this.remoteAimToolStripMenuItem.Name = "remoteAimToolStripMenuItem";
        this.remoteAimToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
        this.remoteAimToolStripMenuItem.Text = "Remote Aim";
        this.remoteAimToolStripMenuItem.Click += new System.EventHandler(this.remoteAimToolStripMenuItem_Click);
        // 
        // fPSToolStripMenuItem
        // 
        this.fPSToolStripMenuItem.Name = "fPSToolStripMenuItem";
        this.fPSToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
        this.fPSToolStripMenuItem.Text = "FPS Aim";
        this.fPSToolStripMenuItem.Click += new System.EventHandler(this.fPSToolStripMenuItem_Click);
        // 
        // setupToolStripMenuItem
        // 
        this.setupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.motionDetectionSettingsToolStripMenuItem,
            this.dataTransmissionToolStripMenuItem,
            this.servosSettingsToolStripMenuItem,
            this.soundSettingsToolStripMenuItem});
        this.setupToolStripMenuItem.Name = "setupToolStripMenuItem";
        this.setupToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
        this.setupToolStripMenuItem.Text = "Setup";
        // 
        // motionDetectionSettingsToolStripMenuItem
        // 
        this.motionDetectionSettingsToolStripMenuItem.Name = "motionDetectionSettingsToolStripMenuItem";
        this.motionDetectionSettingsToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
        this.motionDetectionSettingsToolStripMenuItem.Text = "Motion Detection Settings";
        this.motionDetectionSettingsToolStripMenuItem.Click += new System.EventHandler(this.motionDetectionSettingsToolStripMenuItem_Click);
        // 
        // dataTransmissionToolStripMenuItem
        // 
        this.dataTransmissionToolStripMenuItem.Name = "dataTransmissionToolStripMenuItem";
        this.dataTransmissionToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
        this.dataTransmissionToolStripMenuItem.Text = "Data Transmission";
        this.dataTransmissionToolStripMenuItem.Click += new System.EventHandler(this.dataTransmissionToolStripMenuItem_Click);
        // 
        // servosSettingsToolStripMenuItem
        // 
        this.servosSettingsToolStripMenuItem.Name = "servosSettingsToolStripMenuItem";
        this.servosSettingsToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
        this.servosSettingsToolStripMenuItem.Text = "Servos Settings";
        // 
        // soundSettingsToolStripMenuItem
        // 
        this.soundSettingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onOffSoundMenuItem});
        this.soundSettingsToolStripMenuItem.Name = "soundSettingsToolStripMenuItem";
        this.soundSettingsToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
        this.soundSettingsToolStripMenuItem.Text = "Sound Settings";
        // 
        // onOffSoundMenuItem
        // 
        this.onOffSoundMenuItem.Name = "onOffSoundMenuItem";
        this.onOffSoundMenuItem.Size = new System.Drawing.Size(119, 22);
        this.onOffSoundMenuItem.Text = "On/Off";
        this.onOffSoundMenuItem.Click += new System.EventHandler(this.onOffSoundMenuItem_Click);
        // 
        // panel1
        // 
        this.panel1.Controls.Add(this.groupBox5);
        this.panel1.Controls.Add(this.groupBox4);
        this.panel1.Controls.Add(this.groupBox3);
        this.panel1.Controls.Add(this.groupBox2);
        this.panel1.Controls.Add(this.aimDot);
        this.panel1.Controls.Add(this.cameraWindow1);
        this.panel1.Controls.Add(this.groupBox1);
        this.panel1.Location = new System.Drawing.Point(0, 27);
        this.panel1.Name = "panel1";
        this.panel1.Size = new System.Drawing.Size(858, 529);
        this.panel1.TabIndex = 2;
        // 
        // groupBox5
        // 
        this.groupBox5.Controls.Add(this.AllOffButton);
        this.groupBox5.ForeColor = System.Drawing.Color.Red;
        this.groupBox5.Location = new System.Drawing.Point(36, 209);
        this.groupBox5.Name = "groupBox5";
        this.groupBox5.Size = new System.Drawing.Size(196, 95);
        this.groupBox5.TabIndex = 6;
        this.groupBox5.TabStop = false;
        this.groupBox5.Text = "All Off";
        // 
        // AllOffButton
        // 
        this.AllOffButton.BackColor = System.Drawing.Color.Red;
        this.AllOffButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.AllOffButton.ForeColor = System.Drawing.Color.White;
        this.AllOffButton.Location = new System.Drawing.Point(58, 38);
        this.AllOffButton.Name = "AllOffButton";
        this.AllOffButton.Size = new System.Drawing.Size(75, 23);
        this.AllOffButton.TabIndex = 3;
        this.AllOffButton.Text = "OFF";
        this.AllOffButton.UseVisualStyleBackColor = false;
        this.AllOffButton.Click += new System.EventHandler(this.AllOffButton_Click);
        // 
        // groupBox4
        // 
        this.groupBox4.Controls.Add(this.GunFireButton);
        this.groupBox4.Location = new System.Drawing.Point(624, 413);
        this.groupBox4.Name = "groupBox4";
        this.groupBox4.Size = new System.Drawing.Size(196, 95);
        this.groupBox4.TabIndex = 5;
        this.groupBox4.TabStop = false;
        this.groupBox4.Text = "Gun Fire";
        // 
        // GunFireButton
        // 
        this.GunFireButton.BackColor = System.Drawing.Color.Red;
        this.GunFireButton.Enabled = false;
        this.GunFireButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.GunFireButton.ForeColor = System.Drawing.Color.White;
        this.GunFireButton.Location = new System.Drawing.Point(58, 38);
        this.GunFireButton.Name = "GunFireButton";
        this.GunFireButton.Size = new System.Drawing.Size(75, 23);
        this.GunFireButton.TabIndex = 3;
        this.GunFireButton.Text = "OFF";
        this.GunFireButton.UseVisualStyleBackColor = false;
        this.GunFireButton.Click += new System.EventHandler(this.GunFireButton_Click);
        // 
        // groupBox3
        // 
        this.groupBox3.Controls.Add(this.GunTrackButton);
        this.groupBox3.Location = new System.Drawing.Point(428, 413);
        this.groupBox3.Name = "groupBox3";
        this.groupBox3.Size = new System.Drawing.Size(196, 95);
        this.groupBox3.TabIndex = 5;
        this.groupBox3.TabStop = false;
        this.groupBox3.Text = "Gun Track";
        // 
        // GunTrackButton
        // 
        this.GunTrackButton.BackColor = System.Drawing.Color.Red;
        this.GunTrackButton.Enabled = false;
        this.GunTrackButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.GunTrackButton.ForeColor = System.Drawing.Color.White;
        this.GunTrackButton.Location = new System.Drawing.Point(58, 38);
        this.GunTrackButton.Name = "GunTrackButton";
        this.GunTrackButton.Size = new System.Drawing.Size(75, 23);
        this.GunTrackButton.TabIndex = 3;
        this.GunTrackButton.Text = "OFF";
        this.GunTrackButton.UseVisualStyleBackColor = false;
        this.GunTrackButton.Click += new System.EventHandler(this.GunTrackButton_Click);
        // 
        // groupBox2
        // 
        this.groupBox2.Controls.Add(this.MotionDetectionButton);
        this.groupBox2.Location = new System.Drawing.Point(232, 413);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(196, 95);
        this.groupBox2.TabIndex = 5;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "Motion Detection";
        // 
        // MotionDetectionButton
        // 
        this.MotionDetectionButton.BackColor = System.Drawing.Color.Red;
        this.MotionDetectionButton.Enabled = false;
        this.MotionDetectionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.MotionDetectionButton.ForeColor = System.Drawing.Color.White;
        this.MotionDetectionButton.Location = new System.Drawing.Point(58, 38);
        this.MotionDetectionButton.Name = "MotionDetectionButton";
        this.MotionDetectionButton.Size = new System.Drawing.Size(75, 23);
        this.MotionDetectionButton.TabIndex = 3;
        this.MotionDetectionButton.Text = "OFF";
        this.MotionDetectionButton.UseVisualStyleBackColor = false;
        this.MotionDetectionButton.Click += new System.EventHandler(this.MotionDetectionButton_Click);
        // 
        // aimDot
        // 
        this.aimDot.BackColor = System.Drawing.Color.Red;
        this.aimDot.Location = new System.Drawing.Point(421, 252);
        this.aimDot.Name = "aimDot";
        this.aimDot.Size = new System.Drawing.Size(5, 5);
        this.aimDot.TabIndex = 2;
        this.aimDot.TabStop = false;
        this.aimDot.Visible = false;
        // 
        // cameraWindow1
        // 
        this.cameraWindow1.AutoSizeControl = true;
        this.cameraWindow1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
        this.cameraWindow1.Camera = null;
        this.cameraWindow1.Location = new System.Drawing.Point(268, 143);
        this.cameraWindow1.Name = "cameraWindow1";
        this.cameraWindow1.Size = new System.Drawing.Size(322, 242);
        this.cameraWindow1.TabIndex = 1;
        this.cameraWindow1.Text = "cameraWindow1";
        // 
        // groupBox1
        // 
        this.groupBox1.Controls.Add(this.CameraFeedButton);
        this.groupBox1.Location = new System.Drawing.Point(36, 413);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(196, 95);
        this.groupBox1.TabIndex = 4;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "Camera Feed";
        // 
        // CameraFeedButton
        // 
        this.CameraFeedButton.BackColor = System.Drawing.Color.Red;
        this.CameraFeedButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.CameraFeedButton.ForeColor = System.Drawing.Color.White;
        this.CameraFeedButton.Location = new System.Drawing.Point(58, 38);
        this.CameraFeedButton.Name = "CameraFeedButton";
        this.CameraFeedButton.Size = new System.Drawing.Size(75, 23);
        this.CameraFeedButton.TabIndex = 3;
        this.CameraFeedButton.Text = "OFF";
        this.CameraFeedButton.UseVisualStyleBackColor = false;
        this.CameraFeedButton.Click += new System.EventHandler(this.CameraFeedButton_Click);
        // 
        // statusBar
        // 
        this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fpsLabel,
            this.objectsCountLabel});
        this.statusBar.Location = new System.Drawing.Point(0, 559);
        this.statusBar.Name = "statusBar";
        this.statusBar.Size = new System.Drawing.Size(858, 22);
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
        this.objectsCountLabel.Size = new System.Drawing.Size(693, 17);
        this.objectsCountLabel.Spring = true;
        this.objectsCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // timer
        // 
        this.timer.Interval = 1000;
        this.timer.Tick += new System.EventHandler(this.timer_Tick);
        // 
        // TrackingTimer
        // 
        this.TrackingTimer.Interval = 150;
        this.TrackingTimer.Tick += new System.EventHandler(this.TrackingTimer_Tick);
        // 
        // openFileDialog1
        // 
        this.openFileDialog1.FileName = "openFileDialog1";
        // 
        // MainForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(858, 581);
        this.Controls.Add(this.statusBar);
        this.Controls.Add(this.menuStrip1);
        this.Controls.Add(this.panel1);
        this.MainMenuStrip = this.menuStrip1;
        this.Name = "MainForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Autonomous Sentry Gun";
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
        this.menuStrip1.ResumeLayout(false);
        this.menuStrip1.PerformLayout();
        this.panel1.ResumeLayout(false);
        this.groupBox5.ResumeLayout(false);
        this.groupBox4.ResumeLayout(false);
        this.groupBox3.ResumeLayout(false);
        this.groupBox2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.aimDot)).EndInit();
        this.groupBox1.ResumeLayout(false);
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
    private System.Windows.Forms.ToolStripMenuItem motionDetectionSettingsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem dataTransmissionToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem servosSettingsToolStripMenuItem;
    private CameraWindow cameraWindow1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.StatusStrip statusBar;
    private System.Windows.Forms.ToolStripStatusLabel fpsLabel;
    private System.Windows.Forms.ToolStripStatusLabel objectsCountLabel;
    private System.Windows.Forms.Timer timer;
    private System.Windows.Forms.Timer TrackingTimer;
    private System.Windows.Forms.PictureBox aimDot;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    private System.Windows.Forms.ToolStripMenuItem soundSettingsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem onOffSoundMenuItem;
    private System.Windows.Forms.ToolStripMenuItem remoteAimToolStripMenuItem;
    private System.Windows.Forms.Button CameraFeedButton;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.Button GunFireButton;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.Button GunTrackButton;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Button MotionDetectionButton;
    private System.Windows.Forms.GroupBox groupBox5;
    private System.Windows.Forms.Button AllOffButton;
    private System.Windows.Forms.ToolStripMenuItem fPSToolStripMenuItem;
  }
}

