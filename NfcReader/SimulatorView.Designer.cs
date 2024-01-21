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
            this.swipeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // selectedCardListbox
            // 
            this.selectedCardListbox.FormattingEnabled = true;
            this.selectedCardListbox.Items.AddRange(new object[] {
            "kaart1",
            "kaart2",
            "kaart3",
            "kaart4"});
            this.selectedCardListbox.Location = new System.Drawing.Point(9, 10);
            this.selectedCardListbox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.selectedCardListbox.Name = "selectedCardListbox";
            this.selectedCardListbox.Size = new System.Drawing.Size(174, 147);
            this.selectedCardListbox.TabIndex = 0;
            // 
            // insertCardButton
            // 
            this.insertCardButton.Location = new System.Drawing.Point(186, 10);
            this.insertCardButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.insertCardButton.Name = "insertCardButton";
            this.insertCardButton.Size = new System.Drawing.Size(122, 35);
            this.insertCardButton.TabIndex = 1;
            this.insertCardButton.Text = "Invoeren";
            this.insertCardButton.UseVisualStyleBackColor = true;
            this.insertCardButton.Click += new System.EventHandler(this.insertCardButton_Click);
            // 
            // removeCardButton
            // 
            this.removeCardButton.Location = new System.Drawing.Point(187, 65);
            this.removeCardButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.removeCardButton.Name = "removeCardButton";
            this.removeCardButton.Size = new System.Drawing.Size(122, 35);
            this.removeCardButton.TabIndex = 2;
            this.removeCardButton.Text = "Weghalen";
            this.removeCardButton.UseVisualStyleBackColor = true;
            this.removeCardButton.Click += new System.EventHandler(this.removeCardButton_Click);
            // 
            // swipeButton
            // 
            this.swipeButton.Location = new System.Drawing.Point(187, 121);
            this.swipeButton.Margin = new System.Windows.Forms.Padding(2);
            this.swipeButton.Name = "swipeButton";
            this.swipeButton.Size = new System.Drawing.Size(122, 35);
            this.swipeButton.TabIndex = 3;
            this.swipeButton.Text = "Swipe";
            this.swipeButton.UseVisualStyleBackColor = true;
            this.swipeButton.Click += new System.EventHandler(this.swipeButton_Click);
            // 
            // SimulatorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 167);
            this.Controls.Add(this.swipeButton);
            this.Controls.Add(this.removeCardButton);
            this.Controls.Add(this.insertCardButton);
            this.Controls.Add(this.selectedCardListbox);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "SimulatorView";
            this.Text = "SimulatorView";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox selectedCardListbox;
        private System.Windows.Forms.Button insertCardButton;
        private System.Windows.Forms.Button removeCardButton;
        private System.Windows.Forms.Button swipeButton;
    }
}