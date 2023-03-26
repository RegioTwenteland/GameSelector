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
            this.btnLees = new System.Windows.Forms.Button();
            this.btnSchrijf = new System.Windows.Forms.Button();
            this.dataView = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnLees
            // 
            this.btnLees.Location = new System.Drawing.Point(480, 118);
            this.btnLees.Name = "btnLees";
            this.btnLees.Size = new System.Drawing.Size(75, 23);
            this.btnLees.TabIndex = 0;
            this.btnLees.Text = "Lees data";
            this.btnLees.UseVisualStyleBackColor = true;
            this.btnLees.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSchrijf
            // 
            this.btnSchrijf.Location = new System.Drawing.Point(480, 85);
            this.btnSchrijf.Name = "btnSchrijf";
            this.btnSchrijf.Size = new System.Drawing.Size(75, 23);
            this.btnSchrijf.TabIndex = 1;
            this.btnSchrijf.Text = "Schrijf";
            this.btnSchrijf.UseVisualStyleBackColor = true;
            this.btnSchrijf.Click += new System.EventHandler(this.btnSchrijf_Click);
            // 
            // dataView
            // 
            this.dataView.Enabled = false;
            this.dataView.Location = new System.Drawing.Point(49, 56);
            this.dataView.Name = "dataView";
            this.dataView.Size = new System.Drawing.Size(284, 223);
            this.dataView.TabIndex = 2;
            this.dataView.Text = "";
            // 
            // TestView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataView);
            this.Controls.Add(this.btnSchrijf);
            this.Controls.Add(this.btnLees);
            this.Name = "TestView";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLees;
        private System.Windows.Forms.Button btnSchrijf;
        private System.Windows.Forms.RichTextBox dataView;
    }
}

