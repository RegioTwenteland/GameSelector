namespace GameSelector
{
    partial class TestView
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
            this.btnSchrijf = new System.Windows.Forms.Button();
            this.groupIdText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupNameText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.currentGameText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.startTimePicker = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // btnSchrijf
            // 
            this.btnSchrijf.Location = new System.Drawing.Point(15, 125);
            this.btnSchrijf.Name = "btnSchrijf";
            this.btnSchrijf.Size = new System.Drawing.Size(286, 23);
            this.btnSchrijf.TabIndex = 1;
            this.btnSchrijf.Text = "Schrijf";
            this.btnSchrijf.UseVisualStyleBackColor = true;
            this.btnSchrijf.Click += new System.EventHandler(this.btnSchrijf_Click);
            // 
            // groupIdText
            // 
            this.groupIdText.Location = new System.Drawing.Point(101, 6);
            this.groupIdText.Name = "groupIdText";
            this.groupIdText.Size = new System.Drawing.Size(200, 20);
            this.groupIdText.TabIndex = 2;
            this.groupIdText.TextChanged += new System.EventHandler(this.ForceTextboxToInt);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Groep ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Groep naam";
            // 
            // groupNameText
            // 
            this.groupNameText.Location = new System.Drawing.Point(101, 32);
            this.groupNameText.Name = "groupNameText";
            this.groupNameText.Size = new System.Drawing.Size(200, 20);
            this.groupNameText.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Huidig spel";
            // 
            // currentGameText
            // 
            this.currentGameText.Location = new System.Drawing.Point(101, 58);
            this.currentGameText.Name = "currentGameText";
            this.currentGameText.Size = new System.Drawing.Size(200, 20);
            this.currentGameText.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Begintijd";
            // 
            // startTimePicker
            // 
            this.startTimePicker.Location = new System.Drawing.Point(101, 85);
            this.startTimePicker.Name = "startTimePicker";
            this.startTimePicker.Size = new System.Drawing.Size(200, 20);
            this.startTimePicker.TabIndex = 10;
            // 
            // TestView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 169);
            this.Controls.Add(this.startTimePicker);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.currentGameText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupNameText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupIdText);
            this.Controls.Add(this.btnSchrijf);
            this.Name = "TestView";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSchrijf;
        private System.Windows.Forms.TextBox groupIdText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox groupNameText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox currentGameText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker startTimePicker;
    }
}

