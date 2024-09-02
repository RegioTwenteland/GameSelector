namespace GameSelector.Views.StartupView
{
    partial class StartupView
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
            newFileButton = new System.Windows.Forms.Button();
            openFileButton = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // newFileButton
            // 
            newFileButton.Location = new System.Drawing.Point(12, 12);
            newFileButton.Name = "newFileButton";
            newFileButton.Size = new System.Drawing.Size(158, 47);
            newFileButton.TabIndex = 0;
            newFileButton.Text = "Nieuw bestand";
            newFileButton.UseVisualStyleBackColor = true;
            newFileButton.Click += newFileButton_Click;
            // 
            // openFileButton
            // 
            openFileButton.Location = new System.Drawing.Point(176, 12);
            openFileButton.Name = "openFileButton";
            openFileButton.Size = new System.Drawing.Size(158, 47);
            openFileButton.TabIndex = 1;
            openFileButton.Text = "Bestand openen";
            openFileButton.UseVisualStyleBackColor = true;
            openFileButton.Click += openFileButton_Click;
            // 
            // StartupView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(348, 73);
            Controls.Add(openFileButton);
            Controls.Add(newFileButton);
            Name = "StartupView";
            Text = "Admin";
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button newFileButton;
        private System.Windows.Forms.Button openFileButton;
    }
}