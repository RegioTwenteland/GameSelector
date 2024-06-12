namespace GameSelector.NfcReader
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
            selectedCardListbox = new System.Windows.Forms.ListBox();
            insertCardButton = new System.Windows.Forms.Button();
            removeCardButton = new System.Windows.Forms.Button();
            swipeButton = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // selectedCardListbox
            // 
            selectedCardListbox.FormattingEnabled = true;
            selectedCardListbox.ItemHeight = 15;
            selectedCardListbox.Items.AddRange(new object[] { "kaart1", "kaart2", "kaart3", "kaart4" });
            selectedCardListbox.Location = new System.Drawing.Point(10, 12);
            selectedCardListbox.Margin = new System.Windows.Forms.Padding(2);
            selectedCardListbox.Name = "selectedCardListbox";
            selectedCardListbox.Size = new System.Drawing.Size(202, 169);
            selectedCardListbox.TabIndex = 0;
            // 
            // insertCardButton
            // 
            insertCardButton.Location = new System.Drawing.Point(217, 12);
            insertCardButton.Margin = new System.Windows.Forms.Padding(2);
            insertCardButton.Name = "insertCardButton";
            insertCardButton.Size = new System.Drawing.Size(142, 40);
            insertCardButton.TabIndex = 1;
            insertCardButton.Text = "Invoeren";
            insertCardButton.UseVisualStyleBackColor = true;
            insertCardButton.Click += insertCardButton_Click;
            // 
            // removeCardButton
            // 
            removeCardButton.Location = new System.Drawing.Point(218, 75);
            removeCardButton.Margin = new System.Windows.Forms.Padding(2);
            removeCardButton.Name = "removeCardButton";
            removeCardButton.Size = new System.Drawing.Size(142, 40);
            removeCardButton.TabIndex = 2;
            removeCardButton.Text = "Weghalen";
            removeCardButton.UseVisualStyleBackColor = true;
            removeCardButton.Click += removeCardButton_Click;
            // 
            // swipeButton
            // 
            swipeButton.Location = new System.Drawing.Point(218, 140);
            swipeButton.Margin = new System.Windows.Forms.Padding(2);
            swipeButton.Name = "swipeButton";
            swipeButton.Size = new System.Drawing.Size(142, 40);
            swipeButton.TabIndex = 3;
            swipeButton.Text = "Swipe";
            swipeButton.UseVisualStyleBackColor = true;
            swipeButton.Click += swipeButton_Click;
            // 
            // SimulatorView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(369, 193);
            Controls.Add(swipeButton);
            Controls.Add(removeCardButton);
            Controls.Add(insertCardButton);
            Controls.Add(selectedCardListbox);
            Margin = new System.Windows.Forms.Padding(2);
            Name = "SimulatorView";
            Text = "SimulatorView";
            FormClosed += SimulatorView_FormClosed;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ListBox selectedCardListbox;
        private System.Windows.Forms.Button insertCardButton;
        private System.Windows.Forms.Button removeCardButton;
        private System.Windows.Forms.Button swipeButton;
    }
}