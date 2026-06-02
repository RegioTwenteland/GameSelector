namespace GameSelector.Views.AdminSettingsView
{
    partial class AdminSettingsView
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
            label11 = new System.Windows.Forms.Label();
            animationLengthTextbox = new System.Windows.Forms.TextBox();
            testUserViewButton = new System.Windows.Forms.Button();
            label14 = new System.Windows.Forms.Label();
            gameTimeoutTextbox = new System.Windows.Forms.TextBox();
            saveGlobalSettings = new System.Windows.Forms.Button();
            hideButton = new System.Windows.Forms.Button();
            gameStateLabel = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            startStopGameButton = new System.Windows.Forms.Button();
            importGamesButton = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(16, 113);
            label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(118, 15);
            label11.TabIndex = 119;
            label11.Text = "Animatie lengte (ms)";
            // 
            // animationLengthTextbox
            // 
            animationLengthTextbox.Location = new System.Drawing.Point(141, 110);
            animationLengthTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            animationLengthTextbox.Name = "animationLengthTextbox";
            animationLengthTextbox.Size = new System.Drawing.Size(107, 23);
            animationLengthTextbox.TabIndex = 118;
            // 
            // testUserViewButton
            // 
            testUserViewButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            testUserViewButton.Location = new System.Drawing.Point(4, 594);
            testUserViewButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            testUserViewButton.Name = "testUserViewButton";
            testUserViewButton.Size = new System.Drawing.Size(130, 27);
            testUserViewButton.TabIndex = 117;
            testUserViewButton.Text = "Test user view";
            testUserViewButton.UseVisualStyleBackColor = true;
            testUserViewButton.Visible = false;
            testUserViewButton.Click += testUserViewButton_Click;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new System.Drawing.Point(4, 83);
            label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(130, 15);
            label14.TabIndex = 115;
            label14.Text = "Spel timeout (minuten)";
            // 
            // gameTimeoutTextbox
            // 
            gameTimeoutTextbox.Location = new System.Drawing.Point(141, 80);
            gameTimeoutTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gameTimeoutTextbox.Name = "gameTimeoutTextbox";
            gameTimeoutTextbox.Size = new System.Drawing.Size(107, 23);
            gameTimeoutTextbox.TabIndex = 114;
            // 
            // saveGlobalSettings
            // 
            saveGlobalSettings.Location = new System.Drawing.Point(4, 140);
            saveGlobalSettings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            saveGlobalSettings.Name = "saveGlobalSettings";
            saveGlobalSettings.Size = new System.Drawing.Size(244, 27);
            saveGlobalSettings.TabIndex = 113;
            saveGlobalSettings.Text = "Opslaan";
            saveGlobalSettings.UseVisualStyleBackColor = true;
            saveGlobalSettings.Click += saveGlobalSettings_Click;
            // 
            // hideButton
            // 
            hideButton.Location = new System.Drawing.Point(4, 36);
            hideButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            hideButton.Name = "hideButton";
            hideButton.Size = new System.Drawing.Size(130, 27);
            hideButton.TabIndex = 112;
            hideButton.Text = "Vergrendelen";
            hideButton.UseVisualStyleBackColor = true;
            hideButton.Click += hideButton_Click;
            // 
            // gameStateLabel
            // 
            gameStateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            gameStateLabel.Location = new System.Drawing.Point(255, 6);
            gameStateLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            gameStateLabel.Name = "gameStateLabel";
            gameStateLabel.Size = new System.Drawing.Size(138, 18);
            gameStateLabel.TabIndex = 109;
            gameStateLabel.Text = "GEPAUZEERD";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(141, 9);
            label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(104, 15);
            label9.TabIndex = 110;
            label9.Text = "Spel is momenteel";
            // 
            // startStopGameButton
            // 
            startStopGameButton.Location = new System.Drawing.Point(4, 3);
            startStopGameButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            startStopGameButton.Name = "startStopGameButton";
            startStopGameButton.Size = new System.Drawing.Size(130, 27);
            startStopGameButton.TabIndex = 111;
            startStopGameButton.Text = "Start spel";
            startStopGameButton.UseVisualStyleBackColor = true;
            startStopGameButton.Click += startStopGameButton_Click;
            // 
            // importGamesButton
            // 
            importGamesButton.Location = new System.Drawing.Point(4, 195);
            importGamesButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            importGamesButton.Name = "importGamesButton";
            importGamesButton.Size = new System.Drawing.Size(241, 27);
            importGamesButton.TabIndex = 120;
            importGamesButton.Text = "Importeer spellen";
            importGamesButton.UseVisualStyleBackColor = true;
            importGamesButton.Click += importGamesButton_Click;
            // 
            // AdminSettingsView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.ControlLightLight;
            Controls.Add(importGamesButton);
            Controls.Add(label11);
            Controls.Add(animationLengthTextbox);
            Controls.Add(testUserViewButton);
            Controls.Add(label14);
            Controls.Add(gameTimeoutTextbox);
            Controls.Add(saveGlobalSettings);
            Controls.Add(hideButton);
            Controls.Add(gameStateLabel);
            Controls.Add(label9);
            Controls.Add(startStopGameButton);
            Name = "AdminSettingsView";
            Size = new System.Drawing.Size(916, 624);
            Load += AdminSettingsView_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox animationLengthTextbox;
        private System.Windows.Forms.Button testUserViewButton;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox gameTimeoutTextbox;
        private System.Windows.Forms.Button saveGlobalSettings;
        private System.Windows.Forms.Button hideButton;
        private System.Windows.Forms.Label gameStateLabel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button startStopGameButton;
        private System.Windows.Forms.Button importGamesButton;
    }
}
