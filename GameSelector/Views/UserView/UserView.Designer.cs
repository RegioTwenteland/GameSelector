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
            this.SuspendLayout();
            // 
            // gameAnnouncerLabel
            // 
            this.gameAnnouncerLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gameAnnouncerLabel.AutoSize = true;
            this.gameAnnouncerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameAnnouncerLabel.Location = new System.Drawing.Point(198, 215);
            this.gameAnnouncerLabel.Name = "gameAnnouncerLabel";
            this.gameAnnouncerLabel.Size = new System.Drawing.Size(449, 39);
            this.gameAnnouncerLabel.TabIndex = 0;
            this.gameAnnouncerLabel.Text = "Houd je kaart tegen de lezer";
            // 
            // UserView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
    }
}