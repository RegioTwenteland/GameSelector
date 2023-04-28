namespace NfcReader
{
    partial class SimulatorView
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
            this.selectedCardListbox = new System.Windows.Forms.ListBox();
            this.insertCardButton = new System.Windows.Forms.Button();
            this.removeCardButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // selectedCardListbox
            // 
            this.selectedCardListbox.FormattingEnabled = true;
            this.selectedCardListbox.ItemHeight = 16;
            this.selectedCardListbox.Items.AddRange(new object[] {
            "kaart1",
            "kaart2",
            "kaart3",
            "kaart4"});
            this.selectedCardListbox.Location = new System.Drawing.Point(12, 12);
            this.selectedCardListbox.Name = "selectedCardListbox";
            this.selectedCardListbox.Size = new System.Drawing.Size(230, 180);
            this.selectedCardListbox.TabIndex = 0;
            // 
            // insertCardButton
            // 
            this.insertCardButton.Location = new System.Drawing.Point(248, 12);
            this.insertCardButton.Name = "insertCardButton";
            this.insertCardButton.Size = new System.Drawing.Size(162, 43);
            this.insertCardButton.TabIndex = 1;
            this.insertCardButton.Text = "Invoeren";
            this.insertCardButton.UseVisualStyleBackColor = true;
            this.insertCardButton.Click += new System.EventHandler(this.insertCardButton_Click);
            // 
            // removeCardButton
            // 
            this.removeCardButton.Location = new System.Drawing.Point(248, 149);
            this.removeCardButton.Name = "removeCardButton";
            this.removeCardButton.Size = new System.Drawing.Size(162, 43);
            this.removeCardButton.TabIndex = 2;
            this.removeCardButton.Text = "Weghalen";
            this.removeCardButton.UseVisualStyleBackColor = true;
            this.removeCardButton.Click += new System.EventHandler(this.removeCardButton_Click);
            // 
            // SimulatorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 205);
            this.Controls.Add(this.removeCardButton);
            this.Controls.Add(this.insertCardButton);
            this.Controls.Add(this.selectedCardListbox);
            this.Name = "SimulatorView";
            this.Text = "SimulatorView";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox selectedCardListbox;
        private System.Windows.Forms.Button insertCardButton;
        private System.Windows.Forms.Button removeCardButton;
    }
}