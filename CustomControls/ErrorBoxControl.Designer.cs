namespace CustomControls
{
    partial class ErrorBoxControl
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.closeButton = new System.Windows.Forms.Button();
            this.errorTextbox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Location = new System.Drawing.Point(130, 3);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(25, 25);
            this.closeButton.TabIndex = 0;
            this.closeButton.Text = "X";
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // errorTextbox
            // 
            this.errorTextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.errorTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.errorTextbox.ForeColor = System.Drawing.SystemColors.Menu;
            this.errorTextbox.Location = new System.Drawing.Point(3, 5);
            this.errorTextbox.Name = "errorTextbox";
            this.errorTextbox.ReadOnly = true;
            this.errorTextbox.Size = new System.Drawing.Size(120, 78);
            this.errorTextbox.TabIndex = 1;
            this.errorTextbox.Text = "New Error";
            // 
            // ErrorBoxControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Controls.Add(this.errorTextbox);
            this.Controls.Add(this.closeButton);
            this.Name = "ErrorBoxControl";
            this.Size = new System.Drawing.Size(158, 86);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox errorTextbox;
        private System.Windows.Forms.Button closeButton;
    }
}
