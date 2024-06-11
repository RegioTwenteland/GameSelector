namespace GameSelector.Views.AdminScaffoldView
{
    partial class AdminScaffoldView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminScaffoldView));
            tabControl = new System.Windows.Forms.TabControl();
            gamesTab = new System.Windows.Forms.TabPage();
            priorityNumber = new System.Windows.Forms.NumericUpDown();
            maxPlayerAmountNumber = new System.Windows.Forms.NumericUpDown();
            label21 = new System.Windows.Forms.Label();
            timeoutNumber = new System.Windows.Forms.NumericUpDown();
            label20 = new System.Windows.Forms.Label();
            label19 = new System.Windows.Forms.Label();
            gameActiveCheckbox = new System.Windows.Forms.CheckBox();
            label18 = new System.Windows.Forms.Label();
            gameRemarksText = new System.Windows.Forms.RichTextBox();
            label17 = new System.Windows.Forms.Label();
            label15 = new System.Windows.Forms.Label();
            saveGameButton = new System.Windows.Forms.Button();
            label10 = new System.Windows.Forms.Label();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            addGameButton = new System.Windows.Forms.Button();
            deleteGameButton = new System.Windows.Forms.Button();
            gamesListBox = new System.Windows.Forms.ListBox();
            gameCodeTextbox = new System.Windows.Forms.TextBox();
            label5 = new System.Windows.Forms.Label();
            gameDescriptionTextbox = new System.Windows.Forms.TextBox();
            gameExplanationTextbox = new System.Windows.Forms.RichTextBox();
            label6 = new System.Windows.Forms.Label();
            errorFlowLayout = new System.Windows.Forms.FlowLayoutPanel();
            tabControl.SuspendLayout();
            gamesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)priorityNumber).BeginInit();
            ((System.ComponentModel.ISupportInitialize)maxPlayerAmountNumber).BeginInit();
            ((System.ComponentModel.ISupportInitialize)timeoutNumber).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(gamesTab);
            tabControl.Dock = System.Windows.Forms.DockStyle.Left;
            tabControl.Location = new System.Drawing.Point(6, 6);
            tabControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new System.Drawing.Size(825, 656);
            tabControl.TabIndex = 100;
            // 
            // gamesTab
            // 
            gamesTab.Controls.Add(priorityNumber);
            gamesTab.Controls.Add(maxPlayerAmountNumber);
            gamesTab.Controls.Add(label21);
            gamesTab.Controls.Add(timeoutNumber);
            gamesTab.Controls.Add(label20);
            gamesTab.Controls.Add(label19);
            gamesTab.Controls.Add(gameActiveCheckbox);
            gamesTab.Controls.Add(label18);
            gamesTab.Controls.Add(gameRemarksText);
            gamesTab.Controls.Add(label17);
            gamesTab.Controls.Add(label15);
            gamesTab.Controls.Add(saveGameButton);
            gamesTab.Controls.Add(label10);
            gamesTab.Controls.Add(flowLayoutPanel1);
            gamesTab.Controls.Add(gamesListBox);
            gamesTab.Controls.Add(gameCodeTextbox);
            gamesTab.Controls.Add(label5);
            gamesTab.Controls.Add(gameDescriptionTextbox);
            gamesTab.Controls.Add(gameExplanationTextbox);
            gamesTab.Controls.Add(label6);
            gamesTab.Location = new System.Drawing.Point(4, 24);
            gamesTab.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gamesTab.Name = "gamesTab";
            gamesTab.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gamesTab.Size = new System.Drawing.Size(817, 628);
            gamesTab.TabIndex = 100;
            gamesTab.Text = "Spellen";
            gamesTab.UseVisualStyleBackColor = true;
            // 
            // priorityNumber
            // 
            priorityNumber.Location = new System.Drawing.Point(315, 186);
            priorityNumber.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            priorityNumber.Name = "priorityNumber";
            priorityNumber.Size = new System.Drawing.Size(303, 23);
            priorityNumber.TabIndex = 118;
            priorityNumber.ValueChanged += GameDataChanged;
            // 
            // maxPlayerAmountNumber
            // 
            maxPlayerAmountNumber.Location = new System.Drawing.Point(315, 392);
            maxPlayerAmountNumber.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            maxPlayerAmountNumber.Name = "maxPlayerAmountNumber";
            maxPlayerAmountNumber.Size = new System.Drawing.Size(303, 23);
            maxPlayerAmountNumber.TabIndex = 117;
            maxPlayerAmountNumber.ValueChanged += GameDataChanged;
            // 
            // label21
            // 
            label21.Location = new System.Drawing.Point(200, 392);
            label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label21.Name = "label21";
            label21.Size = new System.Drawing.Size(108, 20);
            label21.TabIndex = 115;
            label21.Text = "Max spelers";
            label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timeoutNumber
            // 
            timeoutNumber.Location = new System.Drawing.Point(315, 362);
            timeoutNumber.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            timeoutNumber.Name = "timeoutNumber";
            timeoutNumber.Size = new System.Drawing.Size(231, 23);
            timeoutNumber.TabIndex = 114;
            timeoutNumber.ValueChanged += GameDataChanged;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new System.Drawing.Point(553, 365);
            label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label20.Name = "label20";
            label20.Size = new System.Drawing.Size(52, 15);
            label20.TabIndex = 113;
            label20.Text = "Minuten";
            // 
            // label19
            // 
            label19.Location = new System.Drawing.Point(200, 362);
            label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label19.Name = "label19";
            label19.Size = new System.Drawing.Size(108, 20);
            label19.TabIndex = 112;
            label19.Text = "Timeout";
            label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gameActiveCheckbox
            // 
            gameActiveCheckbox.AutoSize = true;
            gameActiveCheckbox.Location = new System.Drawing.Point(315, 163);
            gameActiveCheckbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gameActiveCheckbox.Name = "gameActiveCheckbox";
            gameActiveCheckbox.Size = new System.Drawing.Size(15, 14);
            gameActiveCheckbox.TabIndex = 109;
            gameActiveCheckbox.UseVisualStyleBackColor = true;
            gameActiveCheckbox.CheckedChanged += GameDataChanged;
            // 
            // label18
            // 
            label18.Location = new System.Drawing.Point(200, 164);
            label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label18.Name = "label18";
            label18.Size = new System.Drawing.Size(51, 15);
            label18.TabIndex = 108;
            label18.Text = "Actief";
            // 
            // gameRemarksText
            // 
            gameRemarksText.Location = new System.Drawing.Point(315, 216);
            gameRemarksText.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gameRemarksText.Name = "gameRemarksText";
            gameRemarksText.Size = new System.Drawing.Size(303, 138);
            gameRemarksText.TabIndex = 107;
            gameRemarksText.Text = "";
            gameRemarksText.TextChanged += GameDataChanged;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new System.Drawing.Point(200, 275);
            label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label17.Name = "label17";
            label17.Size = new System.Drawing.Size(80, 15);
            label17.TabIndex = 106;
            label17.Text = "Opmerkingen";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new System.Drawing.Point(200, 188);
            label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(52, 15);
            label15.TabIndex = 101;
            label15.Text = "Prioriteit";
            // 
            // saveGameButton
            // 
            saveGameButton.Location = new System.Drawing.Point(203, 444);
            saveGameButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            saveGameButton.Name = "saveGameButton";
            saveGameButton.Size = new System.Drawing.Size(415, 27);
            saveGameButton.TabIndex = 100;
            saveGameButton.Text = "Opslaan";
            saveGameButton.UseVisualStyleBackColor = true;
            saveGameButton.Click += saveGameButton_Click;
            // 
            // label10
            // 
            label10.Location = new System.Drawing.Point(200, 5);
            label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(108, 20);
            label10.TabIndex = 100;
            label10.Text = "Code";
            label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(addGameButton);
            flowLayoutPanel1.Controls.Add(deleteGameButton);
            flowLayoutPanel1.Location = new System.Drawing.Point(7, 584);
            flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new System.Drawing.Size(186, 36);
            flowLayoutPanel1.TabIndex = 100;
            // 
            // addGameButton
            // 
            addGameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            addGameButton.Location = new System.Drawing.Point(4, 3);
            addGameButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            addGameButton.Name = "addGameButton";
            addGameButton.Size = new System.Drawing.Size(85, 27);
            addGameButton.TabIndex = 100;
            addGameButton.Text = "➕";
            addGameButton.UseVisualStyleBackColor = true;
            addGameButton.Click += addGameButton_Click;
            // 
            // deleteGameButton
            // 
            deleteGameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            deleteGameButton.Location = new System.Drawing.Point(97, 3);
            deleteGameButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            deleteGameButton.Name = "deleteGameButton";
            deleteGameButton.Size = new System.Drawing.Size(85, 27);
            deleteGameButton.TabIndex = 100;
            deleteGameButton.Text = "✖";
            deleteGameButton.UseVisualStyleBackColor = true;
            deleteGameButton.Click += deleteGameButton_Click;
            // 
            // gamesListBox
            // 
            gamesListBox.FormattingEnabled = true;
            gamesListBox.ItemHeight = 15;
            gamesListBox.Location = new System.Drawing.Point(7, 7);
            gamesListBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gamesListBox.Name = "gamesListBox";
            gamesListBox.Size = new System.Drawing.Size(185, 574);
            gamesListBox.TabIndex = 100;
            gamesListBox.SelectedIndexChanged += gamesListBox_SelectedIndexChanged;
            // 
            // gameCodeTextbox
            // 
            gameCodeTextbox.Location = new System.Drawing.Point(315, 3);
            gameCodeTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gameCodeTextbox.Name = "gameCodeTextbox";
            gameCodeTextbox.Size = new System.Drawing.Size(303, 23);
            gameCodeTextbox.TabIndex = 3;
            gameCodeTextbox.TextChanged += GameDataChanged;
            // 
            // label5
            // 
            label5.Location = new System.Drawing.Point(200, 35);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(108, 20);
            label5.TabIndex = 100;
            label5.Text = "Omschrijving";
            label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gameDescriptionTextbox
            // 
            gameDescriptionTextbox.Location = new System.Drawing.Point(315, 33);
            gameDescriptionTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gameDescriptionTextbox.Name = "gameDescriptionTextbox";
            gameDescriptionTextbox.Size = new System.Drawing.Size(303, 23);
            gameDescriptionTextbox.TabIndex = 4;
            gameDescriptionTextbox.TextChanged += GameDataChanged;
            // 
            // gameExplanationTextbox
            // 
            gameExplanationTextbox.Location = new System.Drawing.Point(315, 63);
            gameExplanationTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gameExplanationTextbox.Name = "gameExplanationTextbox";
            gameExplanationTextbox.Size = new System.Drawing.Size(303, 92);
            gameExplanationTextbox.TabIndex = 5;
            gameExplanationTextbox.Text = "";
            gameExplanationTextbox.TextChanged += GameDataChanged;
            // 
            // label6
            // 
            label6.Location = new System.Drawing.Point(200, 100);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(108, 20);
            label6.TabIndex = 100;
            label6.Text = "Uitleg";
            label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // errorFlowLayout
            // 
            errorFlowLayout.AutoScroll = true;
            errorFlowLayout.Dock = System.Windows.Forms.DockStyle.Right;
            errorFlowLayout.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            errorFlowLayout.Location = new System.Drawing.Point(829, 6);
            errorFlowLayout.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            errorFlowLayout.Name = "errorFlowLayout";
            errorFlowLayout.Padding = new System.Windows.Forms.Padding(6);
            errorFlowLayout.Size = new System.Drawing.Size(233, 656);
            errorFlowLayout.TabIndex = 100;
            // 
            // AdminScaffoldView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1068, 668);
            Controls.Add(errorFlowLayout);
            Controls.Add(tabControl);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "AdminScaffoldView";
            Padding = new System.Windows.Forms.Padding(6);
            Text = "Admin";
            Load += Form1_Load;
            tabControl.ResumeLayout(false);
            gamesTab.ResumeLayout(false);
            gamesTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)priorityNumber).EndInit();
            ((System.ComponentModel.ISupportInitialize)maxPlayerAmountNumber).EndInit();
            ((System.ComponentModel.ISupportInitialize)timeoutNumber).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage gamesTab;
        private System.Windows.Forms.ListBox gamesListBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox gameDescriptionTextbox;
        private System.Windows.Forms.RichTextBox gameExplanationTextbox;
        private System.Windows.Forms.FlowLayoutPanel errorFlowLayout;
        private System.Windows.Forms.TextBox gameCodeTextbox;
        private System.Windows.Forms.Button deleteGameButton;
        private System.Windows.Forms.Button addGameButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button saveGameButton;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.RichTextBox gameRemarksText;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox gameActiveCheckbox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.NumericUpDown timeoutNumber;
        private System.Windows.Forms.NumericUpDown maxPlayerAmountNumber;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.NumericUpDown priorityNumber;
    }
}

