namespace GameSelector.Views.AdminSettingsView
{
    partial class SelectEventView
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
            eventsCombobox = new System.Windows.Forms.ComboBox();
            confirmButton = new System.Windows.Forms.Button();
            cancelButton = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // eventsCombobox
            // 
            eventsCombobox.FormattingEnabled = true;
            eventsCombobox.Location = new System.Drawing.Point(12, 12);
            eventsCombobox.Name = "eventsCombobox";
            eventsCombobox.Size = new System.Drawing.Size(302, 23);
            eventsCombobox.TabIndex = 0;
            // 
            // confirmButton
            // 
            confirmButton.Location = new System.Drawing.Point(239, 53);
            confirmButton.Name = "confirmButton";
            confirmButton.Size = new System.Drawing.Size(75, 23);
            confirmButton.TabIndex = 1;
            confirmButton.Text = "OK";
            confirmButton.UseVisualStyleBackColor = true;
            confirmButton.Click += confirmButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new System.Drawing.Point(12, 53);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.TabIndex = 2;
            cancelButton.Text = "Annuleren";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // SelectEventView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(326, 88);
            ControlBox = false;
            Controls.Add(cancelButton);
            Controls.Add(confirmButton);
            Controls.Add(eventsCombobox);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Name = "SelectEventView";
            ShowIcon = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Selecteer activiteit";
            TopMost = true;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ComboBox eventsCombobox;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Button cancelButton;
    }
}