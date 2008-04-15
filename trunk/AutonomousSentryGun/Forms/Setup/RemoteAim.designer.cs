namespace AutonomousSentryGun.Forms.Test
{
  partial class RemoteAim
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
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.XTextBox = new System.Windows.Forms.TextBox();
        this.YTextBox = new System.Windows.Forms.TextBox();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.centerButton = new System.Windows.Forms.Button();
        this.PosIncTextBox = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        this.fireBox = new System.Windows.Forms.CheckBox();
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
        this.gridBox = new AutonomousSentryGun.CameraWindow();
        ((System.ComponentModel.ISupportInitialize)(this.redDot)).BeginInit();
        this.groupBox1.SuspendLayout();
        this.groupBox2.SuspendLayout();
        this.groupBox3.SuspendLayout();
        this.SuspendLayout();
        // 
        // redDot
        // 
        this.redDot.Anchor = System.Windows.Forms.AnchorStyles.None;
        this.redDot.BackColor = System.Drawing.Color.Yellow;
        this.redDot.Location = new System.Drawing.Point(169, 241);
        this.redDot.Name = "redDot";
        this.redDot.Size = new System.Drawing.Size(9, 9);
        this.redDot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
        this.redDot.TabIndex = 1;
        this.redDot.TabStop = false;
        this.redDot.MouseMove += new System.Windows.Forms.MouseEventHandler(this.redDot_MouseMove);
        this.redDot.MouseDown += new System.Windows.Forms.MouseEventHandler(this.redDot_MouseDown);
        this.redDot.MouseUp += new System.Windows.Forms.MouseEventHandler(this.redDot_MouseUp);
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(11, 25);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(17, 13);
        this.label2.TabIndex = 4;
        this.label2.Text = "X:";
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(11, 69);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(17, 13);
        this.label3.TabIndex = 5;
        this.label3.Text = "Y:";
        // 
        // XTextBox
        // 
        this.XTextBox.Location = new System.Drawing.Point(34, 22);
        this.XTextBox.Name = "XTextBox";
        this.XTextBox.Size = new System.Drawing.Size(46, 20);
        this.XTextBox.TabIndex = 8;
        this.XTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.XTextBox_KeyDown);
        this.XTextBox.Leave += new System.EventHandler(this.XTextBox_Leave);
        // 
        // YTextBox
        // 
        this.YTextBox.Location = new System.Drawing.Point(34, 66);
        this.YTextBox.Name = "YTextBox";
        this.YTextBox.Size = new System.Drawing.Size(46, 20);
        this.YTextBox.TabIndex = 9;
        this.YTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.YTextBox_KeyDown);
        this.YTextBox.Leave += new System.EventHandler(this.YTextBox_Leave);
        // 
        // groupBox1
        // 
        this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.XTextBox);
        this.groupBox1.Controls.Add(this.YTextBox);
        this.groupBox1.Controls.Add(this.label3);
        this.groupBox1.Location = new System.Drawing.Point(53, 412);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(102, 100);
        this.groupBox1.TabIndex = 12;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "Coordinate";
        // 
        // centerButton
        // 
        this.centerButton.Location = new System.Drawing.Point(164, 423);
        this.centerButton.Name = "centerButton";
        this.centerButton.Size = new System.Drawing.Size(75, 23);
        this.centerButton.TabIndex = 19;
        this.centerButton.Text = "Center";
        this.centerButton.UseVisualStyleBackColor = true;
        this.centerButton.Click += new System.EventHandler(this.centerButton_Click);
        // 
        // PosIncTextBox
        // 
        this.PosIncTextBox.Location = new System.Drawing.Point(264, 492);
        this.PosIncTextBox.Name = "PosIncTextBox";
        this.PosIncTextBox.Size = new System.Drawing.Size(60, 20);
        this.PosIncTextBox.TabIndex = 20;
        this.PosIncTextBox.TextChanged += new System.EventHandler(this.PosIncTextBox_TextChanged);
        this.PosIncTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PosIncTextBox_KeyDown);
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(161, 495);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(97, 13);
        this.label4.TabIndex = 21;
        this.label4.Text = "Position Increment:";
        // 
        // fireBox
        // 
        this.fireBox.AutoSize = true;
        this.fireBox.Location = new System.Drawing.Point(164, 461);
        this.fireBox.Name = "fireBox";
        this.fireBox.Size = new System.Drawing.Size(68, 17);
        this.fireBox.TabIndex = 22;
        this.fireBox.Text = "FIRE!!!!!!";
        this.fireBox.UseVisualStyleBackColor = true;
        this.fireBox.CheckedChanged += new System.EventHandler(this.fireBox_CheckedChanged);
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.BackColor = System.Drawing.Color.White;
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(148, 120);
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
        this.groupBox2.Location = new System.Drawing.Point(31, 12);
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
        // 
        // textBoxYServo
        // 
        this.textBoxYServo.Location = new System.Drawing.Point(34, 66);
        this.textBoxYServo.Name = "textBoxYServo";
        this.textBoxYServo.Size = new System.Drawing.Size(46, 20);
        this.textBoxYServo.TabIndex = 9;
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
        this.groupBox3.Location = new System.Drawing.Point(209, 12);
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
        // 
        // textBoxYCoord
        // 
        this.textBoxYCoord.Location = new System.Drawing.Point(34, 66);
        this.textBoxYCoord.Name = "textBoxYCoord";
        this.textBoxYCoord.Size = new System.Drawing.Size(46, 20);
        this.textBoxYCoord.TabIndex = 9;
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
        // gridBox
        // 
        this.gridBox.AutoSizeControl = true;
        this.gridBox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
        this.gridBox.Camera = null;
        this.gridBox.Location = new System.Drawing.Point(15, 139);
        this.gridBox.Name = "gridBox";
        this.gridBox.Size = new System.Drawing.Size(322, 242);
        this.gridBox.TabIndex = 23;
        this.gridBox.Text = "cameraWindow1";
        // 
        // RemoteAim
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(353, 520);
        this.Controls.Add(this.groupBox3);
        this.Controls.Add(this.groupBox2);
        this.Controls.Add(this.redDot);
        this.Controls.Add(this.gridBox);
        this.Controls.Add(this.fireBox);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.PosIncTextBox);
        this.Controls.Add(this.centerButton);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.groupBox1);
        this.Name = "RemoteAim";
        this.Text = "RemoteAim";
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RemoteAim_FormClosing);
        this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RemoteAim_KeyDown);
        ((System.ComponentModel.ISupportInitialize)(this.redDot)).EndInit();
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        this.groupBox3.ResumeLayout(false);
        this.groupBox3.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox redDot;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox XTextBox;
    private System.Windows.Forms.TextBox YTextBox;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button centerButton;
    private System.Windows.Forms.TextBox PosIncTextBox;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.CheckBox fireBox;
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

  }
}