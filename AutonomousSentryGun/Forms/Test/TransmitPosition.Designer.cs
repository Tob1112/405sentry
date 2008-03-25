namespace AutonomousSentryGun.Forms.Test
{
  partial class TransmitPosition
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransmitPosition));
        this.redDot = new System.Windows.Forms.PictureBox();
        this.gridBox = new System.Windows.Forms.PictureBox();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.XTextBox = new System.Windows.Forms.TextBox();
        this.YTextBox = new System.Windows.Forms.TextBox();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.label1 = new System.Windows.Forms.Label();
        this.centerButton = new System.Windows.Forms.Button();
        this.PosIncTextBox = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        this.fireBox = new System.Windows.Forms.CheckBox();
        ((System.ComponentModel.ISupportInitialize)(this.redDot)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.gridBox)).BeginInit();
        this.groupBox1.SuspendLayout();
        this.SuspendLayout();
        // 
        // redDot
        // 
        this.redDot.Anchor = System.Windows.Forms.AnchorStyles.None;
        this.redDot.Image = ((System.Drawing.Image)(resources.GetObject("redDot.Image")));
        this.redDot.Location = new System.Drawing.Point(139, 10);
        this.redDot.Name = "redDot";
        this.redDot.Size = new System.Drawing.Size(5, 5);
        this.redDot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
        this.redDot.TabIndex = 1;
        this.redDot.TabStop = false;
        // 
        // gridBox
        // 
        this.gridBox.Image = ((System.Drawing.Image)(resources.GetObject("gridBox.Image")));
        this.gridBox.Location = new System.Drawing.Point(14, 12);
        this.gridBox.Name = "gridBox";
        this.gridBox.Size = new System.Drawing.Size(256, 256);
        this.gridBox.TabIndex = 2;
        this.gridBox.TabStop = false;
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(88, 21);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(17, 13);
        this.label2.TabIndex = 4;
        this.label2.Text = "X:";
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(88, 65);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(17, 13);
        this.label3.TabIndex = 5;
        this.label3.Text = "Y:";
        // 
        // XTextBox
        // 
        this.XTextBox.Location = new System.Drawing.Point(111, 18);
        this.XTextBox.Name = "XTextBox";
        this.XTextBox.Size = new System.Drawing.Size(46, 20);
        this.XTextBox.TabIndex = 8;
        this.XTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.XTextBox_KeyDown);
        this.XTextBox.Leave += new System.EventHandler(this.XTextBox_Leave);
        // 
        // YTextBox
        // 
        this.YTextBox.Location = new System.Drawing.Point(111, 62);
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
        this.groupBox1.Location = new System.Drawing.Point(21, 276);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(200, 100);
        this.groupBox1.TabIndex = 12;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "Coordinate";
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.BackColor = System.Drawing.Color.White;
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(15, 249);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(21, 9);
        this.label1.TabIndex = 14;
        this.label1.Text = "(0,0)\r\n";
        // 
        // centerButton
        // 
        this.centerButton.Location = new System.Drawing.Point(77, 405);
        this.centerButton.Name = "centerButton";
        this.centerButton.Size = new System.Drawing.Size(75, 23);
        this.centerButton.TabIndex = 19;
        this.centerButton.Text = "Center";
        this.centerButton.UseVisualStyleBackColor = true;
        this.centerButton.Click += new System.EventHandler(this.centerButton_Click);
        // 
        // PosIncTextBox
        // 
        this.PosIncTextBox.Location = new System.Drawing.Point(132, 449);
        this.PosIncTextBox.Name = "PosIncTextBox";
        this.PosIncTextBox.Size = new System.Drawing.Size(60, 20);
        this.PosIncTextBox.TabIndex = 20;
        this.PosIncTextBox.TextChanged += new System.EventHandler(this.PosIncTextBox_TextChanged);
        this.PosIncTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PosIncTextBox_KeyDown);
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(29, 452);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(97, 13);
        this.label4.TabIndex = 21;
        this.label4.Text = "Position Increment:";
        // 
        // fireBox
        // 
        this.fireBox.AutoSize = true;
        this.fireBox.Location = new System.Drawing.Point(375, 358);
        this.fireBox.Name = "fireBox";
        this.fireBox.Size = new System.Drawing.Size(68, 17);
        this.fireBox.TabIndex = 22;
        this.fireBox.Text = "FIRE!!!!!!";
        this.fireBox.UseVisualStyleBackColor = true;
        this.fireBox.CheckedChanged += new System.EventHandler(this.fireBox_CheckedChanged);
        // 
        // TransmitPosition
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(522, 520);
        this.Controls.Add(this.fireBox);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.PosIncTextBox);
        this.Controls.Add(this.centerButton);
        this.Controls.Add(this.redDot);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.gridBox);
        this.Controls.Add(this.groupBox1);
        this.Name = "TransmitPosition";
        this.Text = "TransmitPosition";
        this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TransmitPosition_KeyDown);
        ((System.ComponentModel.ISupportInitialize)(this.redDot)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.gridBox)).EndInit();
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox redDot;
    private System.Windows.Forms.PictureBox gridBox;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox XTextBox;
    private System.Windows.Forms.TextBox YTextBox;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button centerButton;
    private System.Windows.Forms.TextBox PosIncTextBox;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.CheckBox fireBox;

  }
}