﻿namespace AutonomousSentryGun.Forms.Test
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
      this.upButton = new System.Windows.Forms.Button();
      this.downButton = new System.Windows.Forms.Button();
      this.leftButton = new System.Windows.Forms.Button();
      this.rightButton = new System.Windows.Forms.Button();
      this.centerButton = new System.Windows.Forms.Button();
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
      this.XTextBox.Leave += new System.EventHandler(this.XTextBox_Leave);
      this.XTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.XTextBox_KeyPress);
      // 
      // YTextBox
      // 
      this.YTextBox.Location = new System.Drawing.Point(111, 62);
      this.YTextBox.Name = "YTextBox";
      this.YTextBox.Size = new System.Drawing.Size(46, 20);
      this.YTextBox.TabIndex = 9;
      this.YTextBox.Leave += new System.EventHandler(this.YTextBox_Leave);
      this.YTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.YTextBox_KeyPress);
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
      // upButton
      // 
      this.upButton.Location = new System.Drawing.Point(331, 288);
      this.upButton.Name = "upButton";
      this.upButton.Size = new System.Drawing.Size(75, 23);
      this.upButton.TabIndex = 15;
      this.upButton.Text = "Up";
      this.upButton.UseVisualStyleBackColor = true;
      this.upButton.Click += new System.EventHandler(this.upButton_Click);
      // 
      // downButton
      // 
      this.downButton.Location = new System.Drawing.Point(331, 427);
      this.downButton.Name = "downButton";
      this.downButton.Size = new System.Drawing.Size(75, 23);
      this.downButton.TabIndex = 16;
      this.downButton.Text = "Down";
      this.downButton.UseVisualStyleBackColor = true;
      this.downButton.Click += new System.EventHandler(this.downButton_Click);
      // 
      // leftButton
      // 
      this.leftButton.Location = new System.Drawing.Point(236, 353);
      this.leftButton.Name = "leftButton";
      this.leftButton.Size = new System.Drawing.Size(75, 23);
      this.leftButton.TabIndex = 17;
      this.leftButton.Text = "Left";
      this.leftButton.UseVisualStyleBackColor = true;
      this.leftButton.Click += new System.EventHandler(this.leftButton_Click);
      // 
      // rightButton
      // 
      this.rightButton.Location = new System.Drawing.Point(435, 353);
      this.rightButton.Name = "rightButton";
      this.rightButton.Size = new System.Drawing.Size(75, 23);
      this.rightButton.TabIndex = 18;
      this.rightButton.Text = "Right";
      this.rightButton.UseVisualStyleBackColor = true;
      this.rightButton.Click += new System.EventHandler(this.rightButton_Click);
      // 
      // centerButton
      // 
      this.centerButton.Location = new System.Drawing.Point(331, 353);
      this.centerButton.Name = "centerButton";
      this.centerButton.Size = new System.Drawing.Size(75, 23);
      this.centerButton.TabIndex = 19;
      this.centerButton.Text = "Center";
      this.centerButton.UseVisualStyleBackColor = true;
      this.centerButton.Click += new System.EventHandler(this.centerButton_Click);
      // 
      // TransmitPosition
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(522, 520);
      this.Controls.Add(this.centerButton);
      this.Controls.Add(this.rightButton);
      this.Controls.Add(this.leftButton);
      this.Controls.Add(this.downButton);
      this.Controls.Add(this.upButton);
      this.Controls.Add(this.redDot);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.gridBox);
      this.Controls.Add(this.groupBox1);
      this.Name = "TransmitPosition";
      this.Text = "TransmitPosition";
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
    private System.Windows.Forms.Button upButton;
    private System.Windows.Forms.Button downButton;
    private System.Windows.Forms.Button leftButton;
    private System.Windows.Forms.Button rightButton;
    private System.Windows.Forms.Button centerButton;

  }
}