namespace AutonomousSentryGun.Forms.Test
{
  partial class FPSAim
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
        this.redDot = new System.Windows.Forms.PictureBox();
        this.centerButton = new System.Windows.Forms.Button();
        this.label1 = new System.Windows.Forms.Label();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.label5 = new System.Windows.Forms.Label();
        this.textBoxXServo = new System.Windows.Forms.TextBox();
        this.textBoxYServo = new System.Windows.Forms.TextBox();
        this.label6 = new System.Windows.Forms.Label();
        this.CoordinateTimer = new System.Windows.Forms.Timer(this.components);
        this.groupBox3 = new System.Windows.Forms.GroupBox();
        this.label7 = new System.Windows.Forms.Label();
        this.textBoxXCoord = new System.Windows.Forms.TextBox();
        this.textBoxYCoord = new System.Windows.Forms.TextBox();
        this.label8 = new System.Windows.Forms.Label();
        this.sensitivitytrackBar = new System.Windows.Forms.TrackBar();
        this.panel1 = new System.Windows.Forms.Panel();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.gridBox = new AutonomousSentryGun.CameraWindow();
        this.DTbutton1 = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)(this.redDot)).BeginInit();
        this.groupBox2.SuspendLayout();
        this.groupBox3.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.sensitivitytrackBar)).BeginInit();
        this.panel1.SuspendLayout();
        this.SuspendLayout();
        // 
        // redDot
        // 
        this.redDot.Anchor = System.Windows.Forms.AnchorStyles.None;
        this.redDot.BackColor = System.Drawing.Color.Yellow;
        this.redDot.Location = new System.Drawing.Point(445, 303);
        this.redDot.Name = "redDot";
        this.redDot.Size = new System.Drawing.Size(5, 5);
        this.redDot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
        this.redDot.TabIndex = 1;
        this.redDot.TabStop = false;
        // 
        // centerButton
        // 
        this.centerButton.Location = new System.Drawing.Point(419, 455);
        this.centerButton.Name = "centerButton";
        this.centerButton.Size = new System.Drawing.Size(75, 23);
        this.centerButton.TabIndex = 19;
        this.centerButton.Text = "Center";
        this.centerButton.UseVisualStyleBackColor = true;
        this.centerButton.Click += new System.EventHandler(this.centerButton_Click);
        this.centerButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.centerButton_KeyDown);
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.BackColor = System.Drawing.Color.White;
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(423, 162);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(21, 9);
        this.label1.TabIndex = 14;
        this.label1.Text = "(0,0)\r\n";
        // 
        // groupBox2
        // 
        this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
        this.groupBox2.Controls.Add(this.label5);
        this.groupBox2.Controls.Add(this.textBoxXServo);
        this.groupBox2.Controls.Add(this.textBoxYServo);
        this.groupBox2.Controls.Add(this.label6);
        this.groupBox2.Location = new System.Drawing.Point(307, 41);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(102, 100);
        this.groupBox2.TabIndex = 24;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "Servos Range";
        // 
        // label5
        // 
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(11, 25);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(17, 13);
        this.label5.TabIndex = 4;
        this.label5.Text = "X:";
        // 
        // textBoxXServo
        // 
        this.textBoxXServo.Location = new System.Drawing.Point(34, 22);
        this.textBoxXServo.Name = "textBoxXServo";
        this.textBoxXServo.Size = new System.Drawing.Size(46, 20);
        this.textBoxXServo.TabIndex = 8;
        this.textBoxXServo.MouseLeave += new System.EventHandler(this.textBoxXServo_MouseLeave);
        this.textBoxXServo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxXServo_KeyDown);
        // 
        // textBoxYServo
        // 
        this.textBoxYServo.Location = new System.Drawing.Point(34, 66);
        this.textBoxYServo.Name = "textBoxYServo";
        this.textBoxYServo.Size = new System.Drawing.Size(46, 20);
        this.textBoxYServo.TabIndex = 9;
        this.textBoxYServo.MouseLeave += new System.EventHandler(this.textBoxYServo_MouseLeave);
        this.textBoxYServo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxYServo_KeyDown);
        // 
        // label6
        // 
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(11, 69);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(17, 13);
        this.label6.TabIndex = 5;
        this.label6.Text = "Y:";
        // 
        // CoordinateTimer
        // 
        this.CoordinateTimer.Interval = 150;
        this.CoordinateTimer.Tick += new System.EventHandler(this.CoordinateTimer_Tick);
        // 
        // groupBox3
        // 
        this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.None;
        this.groupBox3.Controls.Add(this.label7);
        this.groupBox3.Controls.Add(this.textBoxXCoord);
        this.groupBox3.Controls.Add(this.textBoxYCoord);
        this.groupBox3.Controls.Add(this.label8);
        this.groupBox3.Location = new System.Drawing.Point(485, 41);
        this.groupBox3.Name = "groupBox3";
        this.groupBox3.Size = new System.Drawing.Size(115, 100);
        this.groupBox3.TabIndex = 25;
        this.groupBox3.TabStop = false;
        this.groupBox3.Text = "Servo Coordinates";
        // 
        // label7
        // 
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(11, 25);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(17, 13);
        this.label7.TabIndex = 4;
        this.label7.Text = "X:";
        // 
        // textBoxXCoord
        // 
        this.textBoxXCoord.Location = new System.Drawing.Point(34, 22);
        this.textBoxXCoord.Name = "textBoxXCoord";
        this.textBoxXCoord.Size = new System.Drawing.Size(46, 20);
        this.textBoxXCoord.TabIndex = 8;
        this.textBoxXCoord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxXCoord_KeyDown);
        // 
        // textBoxYCoord
        // 
        this.textBoxYCoord.Location = new System.Drawing.Point(34, 66);
        this.textBoxYCoord.Name = "textBoxYCoord";
        this.textBoxYCoord.Size = new System.Drawing.Size(46, 20);
        this.textBoxYCoord.TabIndex = 9;
        this.textBoxYCoord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxYCoord_KeyDown);
        // 
        // label8
        // 
        this.label8.AutoSize = true;
        this.label8.Location = new System.Drawing.Point(11, 69);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(17, 13);
        this.label8.TabIndex = 5;
        this.label8.Text = "Y:";
        // 
        // sensitivitytrackBar
        // 
        this.sensitivitytrackBar.Location = new System.Drawing.Point(49, 45);
        this.sensitivitytrackBar.Minimum = 1;
        this.sensitivitytrackBar.Name = "sensitivitytrackBar";
        this.sensitivitytrackBar.Size = new System.Drawing.Size(104, 42);
        this.sensitivitytrackBar.TabIndex = 26;
        this.sensitivitytrackBar.Tag = "";
        this.sensitivitytrackBar.Value = 5;
        this.sensitivitytrackBar.Scroll += new System.EventHandler(this.sensitivitytrackBar_Scroll);
        this.sensitivitytrackBar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sensitivitytrackBar_KeyDown);
        // 
        // panel1
        // 
        this.panel1.Controls.Add(this.label2);
        this.panel1.Controls.Add(this.sensitivitytrackBar);
        this.panel1.Location = new System.Drawing.Point(357, 542);
        this.panel1.Name = "panel1";
        this.panel1.Size = new System.Drawing.Size(200, 100);
        this.panel1.TabIndex = 27;
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(70, 10);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(54, 13);
        this.label2.TabIndex = 27;
        this.label2.Text = "Sensitivity";
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(354, 9);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(204, 13);
        this.label3.TabIndex = 28;
        this.label3.Text = "Press \'Escape\' to toggle aiming on and off";
        // 
        // gridBox
        // 
        this.gridBox.AutoSizeControl = true;
        this.gridBox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
        this.gridBox.Camera = null;
        this.gridBox.Location = new System.Drawing.Point(291, 201);
        this.gridBox.Name = "gridBox";
        this.gridBox.Size = new System.Drawing.Size(322, 242);
        this.gridBox.TabIndex = 23;
        this.gridBox.Text = "cameraWindow1";
        this.gridBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridBox_MouseDown);
        this.gridBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridBox_MouseUp);
        this.gridBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridBox_KeyDown);
        // 
        // DTbutton1
        // 
        this.DTbutton1.Location = new System.Drawing.Point(411, 492);
        this.DTbutton1.Name = "DTbutton1";
        this.DTbutton1.Size = new System.Drawing.Size(94, 38);
        this.DTbutton1.TabIndex = 29;
        this.DTbutton1.Text = "Data Transmission";
        this.DTbutton1.UseVisualStyleBackColor = true;
        this.DTbutton1.Click += new System.EventHandler(this.DTbutton1_Click);
        this.DTbutton1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTbutton1_KeyDown);
        // 
        // FPSAim
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(905, 644);
        this.Controls.Add(this.DTbutton1);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.panel1);
        this.Controls.Add(this.groupBox3);
        this.Controls.Add(this.groupBox2);
        this.Controls.Add(this.redDot);
        this.Controls.Add(this.gridBox);
        this.Controls.Add(this.centerButton);
        this.Controls.Add(this.label1);
        this.Name = "FPSAim";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "FPSAim";
        this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FPSAim_MouseUp);
        this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FPSAim_MouseDown);
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FPSAim_FormClosing);
        this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FPSAim_KeyDown);
        ((System.ComponentModel.ISupportInitialize)(this.redDot)).EndInit();
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        this.groupBox3.ResumeLayout(false);
        this.groupBox3.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.sensitivitytrackBar)).EndInit();
        this.panel1.ResumeLayout(false);
        this.panel1.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox redDot;
    private System.Windows.Forms.Button centerButton;
    private System.Windows.Forms.Label label1;
    private CameraWindow gridBox;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox textBoxXServo;
    private System.Windows.Forms.TextBox textBoxYServo;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Timer CoordinateTimer;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox textBoxXCoord;
    private System.Windows.Forms.TextBox textBoxYCoord;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TrackBar sensitivitytrackBar;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button DTbutton1;

  }
}