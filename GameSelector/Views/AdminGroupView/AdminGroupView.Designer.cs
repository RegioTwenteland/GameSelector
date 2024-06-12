namespace GameSelector.Views.AdminGroupView
{
    partial class AdminGroupView
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
            endGameForGroup = new System.Windows.Forms.Button();
            groupRemarksText = new System.Windows.Forms.RichTextBox();
            label16 = new System.Windows.Forms.Label();
            showPlayedGamesButton = new System.Windows.Forms.Button();
            isAdminCheckbox = new System.Windows.Forms.CheckBox();
            label13 = new System.Windows.Forms.Label();
            removeCardFromGroupButton = new System.Windows.Forms.Button();
            addCardToGroupButton = new System.Windows.Forms.Button();
            lastScannedCardTextbox = new System.Windows.Forms.TextBox();
            label12 = new System.Windows.Forms.Label();
            groupsListBox = new System.Windows.Forms.ListBox();
            label8 = new System.Windows.Forms.Label();
            flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            addGroupButton = new System.Windows.Forms.Button();
            deleteGroupButton = new System.Windows.Forms.Button();
            cardIdTextbox = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            startTimePicker = new System.Windows.Forms.DateTimePicker();
            scoutingNameTextbox = new System.Windows.Forms.TextBox();
            saveGroupButton = new System.Windows.Forms.Button();
            groupNameTextbox = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            currentGameText = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            flowLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // endGameForGroup
            // 
            endGameForGroup.Location = new System.Drawing.Point(620, 91);
            endGameForGroup.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            endGameForGroup.Name = "endGameForGroup";
            endGameForGroup.Size = new System.Drawing.Size(36, 27);
            endGameForGroup.TabIndex = 129;
            endGameForGroup.Text = "✖";
            endGameForGroup.UseVisualStyleBackColor = true;
            endGameForGroup.Click += endGameForGroup_Click;
            // 
            // groupRemarksText
            // 
            groupRemarksText.Location = new System.Drawing.Point(303, 155);
            groupRemarksText.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupRemarksText.Name = "groupRemarksText";
            groupRemarksText.Size = new System.Drawing.Size(352, 138);
            groupRemarksText.TabIndex = 128;
            groupRemarksText.Text = "";
            groupRemarksText.TextChanged += GroupDataChanged;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new System.Drawing.Point(200, 215);
            label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label16.Name = "label16";
            label16.Size = new System.Drawing.Size(80, 15);
            label16.TabIndex = 127;
            label16.Text = "Opmerkingen";
            // 
            // showPlayedGamesButton
            // 
            showPlayedGamesButton.Location = new System.Drawing.Point(203, 587);
            showPlayedGamesButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            showPlayedGamesButton.Name = "showPlayedGamesButton";
            showPlayedGamesButton.Size = new System.Drawing.Size(172, 27);
            showPlayedGamesButton.TabIndex = 126;
            showPlayedGamesButton.Text = "Gespeelde spellen";
            showPlayedGamesButton.UseVisualStyleBackColor = true;
            showPlayedGamesButton.Click += showPlayedGamesButton_Click;
            // 
            // isAdminCheckbox
            // 
            isAdminCheckbox.AutoSize = true;
            isAdminCheckbox.Location = new System.Drawing.Point(303, 300);
            isAdminCheckbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            isAdminCheckbox.Name = "isAdminCheckbox";
            isAdminCheckbox.Size = new System.Drawing.Size(29, 19);
            isAdminCheckbox.TabIndex = 125;
            isAdminCheckbox.Text = " ";
            isAdminCheckbox.UseVisualStyleBackColor = true;
            isAdminCheckbox.CheckedChanged += GroupDataChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new System.Drawing.Point(200, 305);
            label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(43, 15);
            label13.TabIndex = 124;
            label13.Text = "Admin";
            // 
            // removeCardFromGroupButton
            // 
            removeCardFromGroupButton.Location = new System.Drawing.Point(620, 1);
            removeCardFromGroupButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            removeCardFromGroupButton.Name = "removeCardFromGroupButton";
            removeCardFromGroupButton.Size = new System.Drawing.Size(36, 27);
            removeCardFromGroupButton.TabIndex = 122;
            removeCardFromGroupButton.Text = "✖";
            removeCardFromGroupButton.UseVisualStyleBackColor = true;
            removeCardFromGroupButton.Click += removeCardFromGroupButton_Click;
            // 
            // addCardToGroupButton
            // 
            addCardToGroupButton.Location = new System.Drawing.Point(446, 561);
            addCardToGroupButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            addCardToGroupButton.Name = "addCardToGroupButton";
            addCardToGroupButton.Size = new System.Drawing.Size(169, 27);
            addCardToGroupButton.TabIndex = 121;
            addCardToGroupButton.Text = "Koppel aan huidige groep";
            addCardToGroupButton.UseVisualStyleBackColor = true;
            addCardToGroupButton.Click += addCardToGroupButton_Click;
            // 
            // lastScannedCardTextbox
            // 
            lastScannedCardTextbox.Enabled = false;
            lastScannedCardTextbox.Location = new System.Drawing.Point(344, 563);
            lastScannedCardTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            lastScannedCardTextbox.Name = "lastScannedCardTextbox";
            lastScannedCardTextbox.Size = new System.Drawing.Size(94, 23);
            lastScannedCardTextbox.TabIndex = 109;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new System.Drawing.Point(205, 567);
            label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(120, 15);
            label12.TabIndex = 120;
            label12.Text = "Laatst gescande kaart";
            // 
            // groupsListBox
            // 
            groupsListBox.FormattingEnabled = true;
            groupsListBox.ItemHeight = 15;
            groupsListBox.Location = new System.Drawing.Point(7, 7);
            groupsListBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupsListBox.Name = "groupsListBox";
            groupsListBox.Size = new System.Drawing.Size(185, 574);
            groupsListBox.TabIndex = 119;
            groupsListBox.SelectedIndexChanged += groupsListBox_SelectedIndexChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(200, 7);
            label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(48, 15);
            label8.TabIndex = 110;
            label8.Text = "Kaart ID";
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(addGroupButton);
            flowLayoutPanel2.Controls.Add(deleteGroupButton);
            flowLayoutPanel2.Location = new System.Drawing.Point(7, 584);
            flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new System.Drawing.Size(186, 36);
            flowLayoutPanel2.TabIndex = 117;
            // 
            // addGroupButton
            // 
            addGroupButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            addGroupButton.Location = new System.Drawing.Point(4, 3);
            addGroupButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            addGroupButton.Name = "addGroupButton";
            addGroupButton.Size = new System.Drawing.Size(85, 27);
            addGroupButton.TabIndex = 100;
            addGroupButton.Text = "➕";
            addGroupButton.UseVisualStyleBackColor = true;
            addGroupButton.Click += addGroupButton_Click;
            // 
            // deleteGroupButton
            // 
            deleteGroupButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            deleteGroupButton.Location = new System.Drawing.Point(97, 3);
            deleteGroupButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            deleteGroupButton.Name = "deleteGroupButton";
            deleteGroupButton.Size = new System.Drawing.Size(85, 27);
            deleteGroupButton.TabIndex = 100;
            deleteGroupButton.Text = "✖";
            deleteGroupButton.UseVisualStyleBackColor = true;
            deleteGroupButton.Click += deleteGroupButton_Click;
            // 
            // cardIdTextbox
            // 
            cardIdTextbox.Enabled = false;
            cardIdTextbox.Location = new System.Drawing.Point(303, 3);
            cardIdTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cardIdTextbox.Name = "cardIdTextbox";
            cardIdTextbox.Size = new System.Drawing.Size(305, 23);
            cardIdTextbox.TabIndex = 116;
            cardIdTextbox.TextChanged += GroupDataChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(200, 67);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(72, 15);
            label1.TabIndex = 115;
            label1.Text = "Groep naam";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(200, 97);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(67, 15);
            label3.TabIndex = 114;
            label3.Text = "Huidig spel";
            // 
            // startTimePicker
            // 
            startTimePicker.Location = new System.Drawing.Point(303, 125);
            startTimePicker.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            startTimePicker.Name = "startTimePicker";
            startTimePicker.Size = new System.Drawing.Size(352, 23);
            startTimePicker.TabIndex = 113;
            // 
            // scoutingNameTextbox
            // 
            scoutingNameTextbox.Location = new System.Drawing.Point(303, 33);
            scoutingNameTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            scoutingNameTextbox.Name = "scoutingNameTextbox";
            scoutingNameTextbox.Size = new System.Drawing.Size(352, 23);
            scoutingNameTextbox.TabIndex = 107;
            scoutingNameTextbox.TextChanged += GroupDataChanged;
            // 
            // saveGroupButton
            // 
            saveGroupButton.Location = new System.Drawing.Point(200, 327);
            saveGroupButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            saveGroupButton.Name = "saveGroupButton";
            saveGroupButton.Size = new System.Drawing.Size(456, 27);
            saveGroupButton.TabIndex = 112;
            saveGroupButton.Text = "Opslaan";
            saveGroupButton.UseVisualStyleBackColor = true;
            saveGroupButton.Click += saveGroupButton_Click;
            // 
            // groupNameTextbox
            // 
            groupNameTextbox.Location = new System.Drawing.Point(303, 63);
            groupNameTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupNameTextbox.Name = "groupNameTextbox";
            groupNameTextbox.Size = new System.Drawing.Size(352, 23);
            groupNameTextbox.TabIndex = 108;
            groupNameTextbox.TextChanged += GroupDataChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(200, 37);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(87, 15);
            label2.TabIndex = 111;
            label2.Text = "Scouting naam";
            // 
            // currentGameText
            // 
            currentGameText.Enabled = false;
            currentGameText.Location = new System.Drawing.Point(303, 93);
            currentGameText.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            currentGameText.Name = "currentGameText";
            currentGameText.Size = new System.Drawing.Size(305, 23);
            currentGameText.TabIndex = 123;
            currentGameText.TextChanged += GroupDataChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(200, 132);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(54, 15);
            label4.TabIndex = 118;
            label4.Text = "Begintijd";
            // 
            // AdminGroupView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.ControlLightLight;
            Controls.Add(endGameForGroup);
            Controls.Add(groupRemarksText);
            Controls.Add(label16);
            Controls.Add(showPlayedGamesButton);
            Controls.Add(isAdminCheckbox);
            Controls.Add(label13);
            Controls.Add(removeCardFromGroupButton);
            Controls.Add(addCardToGroupButton);
            Controls.Add(lastScannedCardTextbox);
            Controls.Add(label12);
            Controls.Add(groupsListBox);
            Controls.Add(label8);
            Controls.Add(flowLayoutPanel2);
            Controls.Add(cardIdTextbox);
            Controls.Add(label1);
            Controls.Add(label3);
            Controls.Add(startTimePicker);
            Controls.Add(scoutingNameTextbox);
            Controls.Add(saveGroupButton);
            Controls.Add(groupNameTextbox);
            Controls.Add(label2);
            Controls.Add(currentGameText);
            Controls.Add(label4);
            Name = "AdminGroupView";
            Size = new System.Drawing.Size(664, 628);
            flowLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button endGameForGroup;
        private System.Windows.Forms.RichTextBox groupRemarksText;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button showPlayedGamesButton;
        private System.Windows.Forms.CheckBox isAdminCheckbox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button removeCardFromGroupButton;
        private System.Windows.Forms.Button addCardToGroupButton;
        private System.Windows.Forms.TextBox lastScannedCardTextbox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ListBox groupsListBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button addGroupButton;
        private System.Windows.Forms.Button deleteGroupButton;
        private System.Windows.Forms.TextBox cardIdTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker startTimePicker;
        private System.Windows.Forms.TextBox scoutingNameTextbox;
        private System.Windows.Forms.Button saveGroupButton;
        private System.Windows.Forms.TextBox groupNameTextbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox currentGameText;
        private System.Windows.Forms.Label label4;
    }
}
