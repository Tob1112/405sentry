namespace AutonomousSentryGun
{
    partial class MotionSettings
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
            this.ALBar = new System.Windows.Forms.TrackBar();
            this.WBar = new System.Windows.Forms.TrackBar();
            this.HBar = new System.Windows.Forms.TrackBar();
            this.ALBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.Slabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Title2 = new System.Windows.Forms.Label();
            this.Wlabel = new System.Windows.Forms.Label();
            this.Hlabel = new System.Windows.Forms.Label();
            this.WBox = new System.Windows.Forms.TextBox();
            this.areaVisual = new System.Windows.Forms.PictureBox();
            this.HBox = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.WBoxMax = new System.Windows.Forms.TextBox();
            this.areaVisualMax = new System.Windows.Forms.PictureBox();
            this.HBoxMax = new System.Windows.Forms.TextBox();
            this.WBarMax = new System.Windows.Forms.TrackBar();
            this.HBarMax = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.ALBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HBar)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.areaVisual)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.areaVisualMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WBarMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HBarMax)).BeginInit();
            this.SuspendLayout();
            // 
            // ALBar
            // 
            this.ALBar.Location = new System.Drawing.Point(48, 55);
            this.ALBar.Maximum = 100;
            this.ALBar.Name = "ALBar";
            this.ALBar.Size = new System.Drawing.Size(104, 42);
            this.ALBar.TabIndex = 0;
            this.ALBar.TickFrequency = 5;
            this.ALBar.Value = 25;
            this.ALBar.Scroll += new System.EventHandler(this.ALBar_Scroll);
            // 
            // WBar
            // 
            this.WBar.Location = new System.Drawing.Point(162, 210);
            this.WBar.Maximum = 150;
            this.WBar.Minimum = 10;
            this.WBar.Name = "WBar";
            this.WBar.Size = new System.Drawing.Size(104, 42);
            this.WBar.TabIndex = 1;
            this.WBar.TickFrequency = 5;
            this.WBar.Value = 10;
            this.WBar.Scroll += new System.EventHandler(this.WBar_Scroll);
            // 
            // HBar
            // 
            this.HBar.Location = new System.Drawing.Point(30, 210);
            this.HBar.Maximum = 150;
            this.HBar.Minimum = 10;
            this.HBar.Name = "HBar";
            this.HBar.Size = new System.Drawing.Size(104, 42);
            this.HBar.TabIndex = 2;
            this.HBar.TickFrequency = 5;
            this.HBar.Value = 10;
            this.HBar.Scroll += new System.EventHandler(this.HBar_Scroll);
            // 
            // ALBox
            // 
            this.ALBox.Location = new System.Drawing.Point(119, 29);
            this.ALBox.Name = "ALBox";
            this.ALBox.Size = new System.Drawing.Size(62, 20);
            this.ALBox.TabIndex = 3;
            this.ALBox.Text = ".25";
            this.ALBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Slabel);
            this.panel1.Controls.Add(this.ALBox);
            this.panel1.Controls.Add(this.ALBar);
            this.panel1.Location = new System.Drawing.Point(52, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(210, 107);
            this.panel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Motion Sensitivity";
            // 
            // Slabel
            // 
            this.Slabel.AutoSize = true;
            this.Slabel.Location = new System.Drawing.Point(18, 32);
            this.Slabel.Name = "Slabel";
            this.Slabel.Size = new System.Drawing.Size(95, 13);
            this.Slabel.TabIndex = 4;
            this.Slabel.Text = "AlarmLevel (0 to 1)";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Title2);
            this.panel2.Controls.Add(this.Wlabel);
            this.panel2.Controls.Add(this.Hlabel);
            this.panel2.Controls.Add(this.WBox);
            this.panel2.Controls.Add(this.areaVisual);
            this.panel2.Controls.Add(this.HBox);
            this.panel2.Controls.Add(this.WBar);
            this.panel2.Controls.Add(this.HBar);
            this.panel2.Location = new System.Drawing.Point(9, 147);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(297, 255);
            this.panel2.TabIndex = 5;
            // 
            // Title2
            // 
            this.Title2.AutoSize = true;
            this.Title2.Location = new System.Drawing.Point(98, 9);
            this.Title2.Name = "Title2";
            this.Title2.Size = new System.Drawing.Size(108, 13);
            this.Title2.TabIndex = 10;
            this.Title2.Text = "Minimum Motion Area";
            // 
            // Wlabel
            // 
            this.Wlabel.AutoSize = true;
            this.Wlabel.Location = new System.Drawing.Point(180, 187);
            this.Wlabel.Name = "Wlabel";
            this.Wlabel.Size = new System.Drawing.Size(35, 13);
            this.Wlabel.TabIndex = 9;
            this.Wlabel.Text = "Width";
            // 
            // Hlabel
            // 
            this.Hlabel.AutoSize = true;
            this.Hlabel.Location = new System.Drawing.Point(40, 187);
            this.Hlabel.Name = "Hlabel";
            this.Hlabel.Size = new System.Drawing.Size(38, 13);
            this.Hlabel.TabIndex = 8;
            this.Hlabel.Text = "Height";
            // 
            // WBox
            // 
            this.WBox.Location = new System.Drawing.Point(221, 184);
            this.WBox.Name = "WBox";
            this.WBox.Size = new System.Drawing.Size(45, 20);
            this.WBox.TabIndex = 7;
            this.WBox.Text = "80";
            this.WBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // areaVisual
            // 
            this.areaVisual.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.areaVisual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.areaVisual.Location = new System.Drawing.Point(81, 27);
            this.areaVisual.Name = "areaVisual";
            this.areaVisual.Size = new System.Drawing.Size(80, 80);
            this.areaVisual.TabIndex = 6;
            this.areaVisual.TabStop = false;
            // 
            // HBox
            // 
            this.HBox.Location = new System.Drawing.Point(84, 184);
            this.HBox.Name = "HBox";
            this.HBox.Size = new System.Drawing.Size(45, 20);
            this.HBox.TabIndex = 4;
            this.HBox.Text = "80";
            this.HBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.WBoxMax);
            this.panel3.Controls.Add(this.areaVisualMax);
            this.panel3.Controls.Add(this.HBoxMax);
            this.panel3.Controls.Add(this.WBarMax);
            this.panel3.Controls.Add(this.HBarMax);
            this.panel3.Location = new System.Drawing.Point(312, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(544, 394);
            this.panel3.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(226, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Maximum Motion Area";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(302, 326);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Width";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(162, 326);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Height";
            // 
            // WBoxMax
            // 
            this.WBoxMax.Location = new System.Drawing.Point(343, 323);
            this.WBoxMax.Name = "WBoxMax";
            this.WBoxMax.Size = new System.Drawing.Size(45, 20);
            this.WBoxMax.TabIndex = 7;
            this.WBoxMax.Text = "354";
            this.WBoxMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // areaVisualMax
            // 
            this.areaVisualMax.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.areaVisualMax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.areaVisualMax.Location = new System.Drawing.Point(106, 20);
            this.areaVisualMax.Name = "areaVisualMax";
            this.areaVisualMax.Size = new System.Drawing.Size(354, 290);
            this.areaVisualMax.TabIndex = 6;
            this.areaVisualMax.TabStop = false;
            // 
            // HBoxMax
            // 
            this.HBoxMax.Location = new System.Drawing.Point(206, 323);
            this.HBoxMax.Name = "HBoxMax";
            this.HBoxMax.Size = new System.Drawing.Size(45, 20);
            this.HBoxMax.TabIndex = 4;
            this.HBoxMax.Text = "290";
            this.HBoxMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // WBarMax
            // 
            this.WBarMax.Location = new System.Drawing.Point(284, 349);
            this.WBarMax.Maximum = 354;
            this.WBarMax.Minimum = 50;
            this.WBarMax.Name = "WBarMax";
            this.WBarMax.Size = new System.Drawing.Size(104, 42);
            this.WBarMax.TabIndex = 1;
            this.WBarMax.TickFrequency = 10;
            this.WBarMax.Value = 354;
            this.WBarMax.Scroll += new System.EventHandler(this.WBarMax_Scroll);
            // 
            // HBarMax
            // 
            this.HBarMax.Location = new System.Drawing.Point(152, 349);
            this.HBarMax.Maximum = 290;
            this.HBarMax.Minimum = 50;
            this.HBarMax.Name = "HBarMax";
            this.HBarMax.Size = new System.Drawing.Size(104, 42);
            this.HBarMax.TabIndex = 2;
            this.HBarMax.TickFrequency = 10;
            this.HBarMax.Value = 290;
            this.HBarMax.Scroll += new System.EventHandler(this.HBarMax_Scroll);
            // 
            // MotionSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 414);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "MotionSettings";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ALBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HBar)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.areaVisual)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.areaVisualMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WBarMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HBarMax)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar ALBar;
        private System.Windows.Forms.TrackBar WBar;
        private System.Windows.Forms.TrackBar HBar;
        private System.Windows.Forms.TextBox ALBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox HBox;
        private System.Windows.Forms.PictureBox areaVisual;
        private System.Windows.Forms.TextBox WBox;
        private System.Windows.Forms.Label Wlabel;
        private System.Windows.Forms.Label Hlabel;
        private System.Windows.Forms.Label Slabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Title2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox WBoxMax;
        private System.Windows.Forms.PictureBox areaVisualMax;
        private System.Windows.Forms.TextBox HBoxMax;
        private System.Windows.Forms.TrackBar WBarMax;
        private System.Windows.Forms.TrackBar HBarMax;
    }
}