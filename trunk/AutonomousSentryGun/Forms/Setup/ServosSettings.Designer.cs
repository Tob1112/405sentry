namespace AutonomousSentryGun.Forms.Setup
{
  partial class ServosSettings
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
      this.UpdateSettingsButton = new System.Windows.Forms.Button();
      this.XRangeTextBox = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.label2 = new System.Windows.Forms.Label();
      this.YRangeTextBox = new System.Windows.Forms.TextBox();
      this.XCenterTextBox = new System.Windows.Forms.TextBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.label3 = new System.Windows.Forms.Label();
      this.YCenterTextBox = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // UpdateSettingsButton
      // 
      this.UpdateSettingsButton.Location = new System.Drawing.Point(194, 229);
      this.UpdateSettingsButton.Name = "UpdateSettingsButton";
      this.UpdateSettingsButton.Size = new System.Drawing.Size(109, 23);
      this.UpdateSettingsButton.TabIndex = 0;
      this.UpdateSettingsButton.Text = "Update Settings";
      this.UpdateSettingsButton.UseVisualStyleBackColor = true;
      this.UpdateSettingsButton.Click += new System.EventHandler(this.UpdateSettingsButton_Click);
      // 
      // XRangeTextBox
      // 
      this.XRangeTextBox.Location = new System.Drawing.Point(94, 97);
      this.XRangeTextBox.Name = "XRangeTextBox";
      this.XRangeTextBox.Size = new System.Drawing.Size(100, 20);
      this.XRangeTextBox.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(40, 37);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(17, 13);
      this.label1.TabIndex = 3;
      this.label1.Text = "X:";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.YRangeTextBox);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Location = new System.Drawing.Point(31, 63);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(200, 129);
      this.groupBox1.TabIndex = 4;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Range";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(40, 84);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(17, 13);
      this.label2.TabIndex = 6;
      this.label2.Text = "Y:";
      // 
      // YRangeTextBox
      // 
      this.YRangeTextBox.Location = new System.Drawing.Point(63, 81);
      this.YRangeTextBox.Name = "YRangeTextBox";
      this.YRangeTextBox.Size = new System.Drawing.Size(100, 20);
      this.YRangeTextBox.TabIndex = 5;
      // 
      // XCenterTextBox
      // 
      this.XCenterTextBox.Location = new System.Drawing.Point(323, 97);
      this.XCenterTextBox.Name = "XCenterTextBox";
      this.XCenterTextBox.Size = new System.Drawing.Size(100, 20);
      this.XCenterTextBox.TabIndex = 5;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.label3);
      this.groupBox2.Controls.Add(this.YCenterTextBox);
      this.groupBox2.Controls.Add(this.label4);
      this.groupBox2.Location = new System.Drawing.Point(260, 63);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(200, 129);
      this.groupBox2.TabIndex = 6;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Center";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(40, 84);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(17, 13);
      this.label3.TabIndex = 6;
      this.label3.Text = "Y:";
      // 
      // YCenterTextBox
      // 
      this.YCenterTextBox.Location = new System.Drawing.Point(63, 81);
      this.YCenterTextBox.Name = "YCenterTextBox";
      this.YCenterTextBox.Size = new System.Drawing.Size(100, 20);
      this.YCenterTextBox.TabIndex = 5;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(40, 37);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(17, 13);
      this.label4.TabIndex = 3;
      this.label4.Text = "X:";
      // 
      // ServosSettings
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(486, 301);
      this.Controls.Add(this.XCenterTextBox);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.XRangeTextBox);
      this.Controls.Add(this.UpdateSettingsButton);
      this.Controls.Add(this.groupBox1);
      this.Name = "ServosSettings";
      this.Text = "ServosSettings";
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button UpdateSettingsButton;
    private System.Windows.Forms.TextBox XRangeTextBox;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox YRangeTextBox;
    private System.Windows.Forms.TextBox XCenterTextBox;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox YCenterTextBox;
    private System.Windows.Forms.Label label4;
  }
}