namespace GameSelector.Views
{
    partial class UserView
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
            this.gameAnnouncerLabel = new System.Windows.Forms.Label();
            this.gameCodeLabel = new System.Windows.Forms.Label();
            this.gameDescriptionLabel = new System.Windows.Forms.Label();
            this.gameExplanationLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // gameAnnouncerLabel
            // 
            this.gameAnnouncerLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gameAnnouncerLabel.AutoSize = true;
            this.gameAnnouncerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameAnnouncerLabel.Location = new System.Drawing.Point(12, 9);
            this.gameAnnouncerLabel.Name = "gameAnnouncerLabel";
            this.gameAnnouncerLabel.Size = new System.Drawing.Size(102, 39);
            this.gameAnnouncerLabel.TabIndex = 0;
            this.gameAnnouncerLabel.Text = "(Kop)";
            // 
            // gameCodeLabel
            // 
            this.gameCodeLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gameCodeLabel.AutoSize = true;
            this.gameCodeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameCodeLabel.Location = new System.Drawing.Point(12, 48);
            this.gameCodeLabel.Name = "gameCodeLabel";
            this.gameCodeLabel.Size = new System.Drawing.Size(123, 39);
            this.gameCodeLabel.TabIndex = 1;
            this.gameCodeLabel.Text = "(Code)";
            // 
            // gameDescriptionLabel
            // 
            this.gameDescriptionLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gameDescriptionLabel.AutoSize = true;
            this.gameDescriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameDescriptionLabel.Location = new System.Drawing.Point(12, 87);
            this.gameDescriptionLabel.Name = "gameDescriptionLabel";
            this.gameDescriptionLabel.Size = new System.Drawing.Size(245, 39);
            this.gameDescriptionLabel.TabIndex = 2;
            this.gameDescriptionLabel.Text = "(Omschrijving)";
            // 
            // gameExplanationLabel
            // 
            this.gameExplanationLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gameExplanationLabel.AutoSize = true;
            this.gameExplanationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameExplanationLabel.Location = new System.Drawing.Point(12, 126);
            this.gameExplanationLabel.Name = "gameExplanationLabel";
            this.gameExplanationLabel.Size = new System.Drawing.Size(130, 39);
            this.gameExplanationLabel.TabIndex = 3;
            this.gameExplanationLabel.Text = "(Uitleg)";
            // 
            // UserView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gameExplanationLabel);
            this.Controls.Add(this.gameDescriptionLabel);
            this.Controls.Add(this.gameCodeLabel);
            this.Controls.Add(this.gameAnnouncerLabel);
            this.Name = "UserView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Welk spel speel jij?";
            this.Load += new System.EventHandler(this.UserView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label gameAnnouncerLabel;
        private System.Windows.Forms.Label gameCodeLabel;
        private System.Windows.Forms.Label gameDescriptionLabel;
        private System.Windows.Forms.Label gameExplanationLabel;
    }
}