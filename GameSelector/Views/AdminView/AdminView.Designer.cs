namespace GameSelector.Views
{
    partial class AdminView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminView));
            saveGroupButton = new System.Windows.Forms.Button();
            groupNameTextbox = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            scoutingNameTextbox = new System.Windows.Forms.TextBox();
            tabControl1 = new System.Windows.Forms.TabControl();
            groupsTab = new System.Windows.Forms.TabPage();
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
            flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            addGroupButton = new System.Windows.Forms.Button();
            deleteGroupButton = new System.Windows.Forms.Button();
            label8 = new System.Windows.Forms.Label();
            cardIdTextbox = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            startTimePicker = new System.Windows.Forms.DateTimePicker();
            currentGameText = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
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
            adminTab = new System.Windows.Forms.TabPage();
            label11 = new System.Windows.Forms.Label();
            animationLengthTextbox = new System.Windows.Forms.TextBox();
            testUserViewButton = new System.Windows.Forms.Button();
            closeButton = new System.Windows.Forms.Button();
            label14 = new System.Windows.Forms.Label();
            gameTimeoutTextbox = new System.Windows.Forms.TextBox();
            saveGlobalSettings = new System.Windows.Forms.Button();
            hideButton = new System.Windows.Forms.Button();
            gameStateLabel = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            startStopGameButton = new System.Windows.Forms.Button();
            errorFlowLayout = new System.Windows.Forms.FlowLayoutPanel();
            tabControl1.SuspendLayout();
            groupsTab.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            gamesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)priorityNumber).BeginInit();
            ((System.ComponentModel.ISupportInitialize)maxPlayerAmountNumber).BeginInit();
            ((System.ComponentModel.ISupportInitialize)timeoutNumber).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            adminTab.SuspendLayout();
            SuspendLayout();
            // 
            // saveGroupButton
            // 
            saveGroupButton.Location = new System.Drawing.Point(200, 327);
            saveGroupButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            saveGroupButton.Name = "saveGroupButton";
            saveGroupButton.Size = new System.Drawing.Size(456, 27);
            saveGroupButton.TabIndex = 100;
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
            groupNameTextbox.TabIndex = 1;
            groupNameTextbox.TextChanged += GroupDataChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(200, 67);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(72, 15);
            label1.TabIndex = 100;
            label1.Text = "Groep naam";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(200, 37);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(87, 15);
            label2.TabIndex = 100;
            label2.Text = "Scouting naam";
            // 
            // scoutingNameTextbox
            // 
            scoutingNameTextbox.Location = new System.Drawing.Point(303, 33);
            scoutingNameTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            scoutingNameTextbox.Name = "scoutingNameTextbox";
            scoutingNameTextbox.Size = new System.Drawing.Size(352, 23);
            scoutingNameTextbox.TabIndex = 0;
            scoutingNameTextbox.TextChanged += GroupDataChanged;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(groupsTab);
            tabControl1.Controls.Add(gamesTab);
            tabControl1.Controls.Add(adminTab);
            tabControl1.Dock = System.Windows.Forms.DockStyle.Left;
            tabControl1.Location = new System.Drawing.Point(6, 6);
            tabControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(825, 656);
            tabControl1.TabIndex = 100;
            // 
            // groupsTab
            // 
            groupsTab.Controls.Add(endGameForGroup);
            groupsTab.Controls.Add(groupRemarksText);
            groupsTab.Controls.Add(label16);
            groupsTab.Controls.Add(showPlayedGamesButton);
            groupsTab.Controls.Add(isAdminCheckbox);
            groupsTab.Controls.Add(label13);
            groupsTab.Controls.Add(removeCardFromGroupButton);
            groupsTab.Controls.Add(addCardToGroupButton);
            groupsTab.Controls.Add(lastScannedCardTextbox);
            groupsTab.Controls.Add(label12);
            groupsTab.Controls.Add(groupsListBox);
            groupsTab.Controls.Add(flowLayoutPanel2);
            groupsTab.Controls.Add(label8);
            groupsTab.Controls.Add(cardIdTextbox);
            groupsTab.Controls.Add(label1);
            groupsTab.Controls.Add(label3);
            groupsTab.Controls.Add(startTimePicker);
            groupsTab.Controls.Add(scoutingNameTextbox);
            groupsTab.Controls.Add(saveGroupButton);
            groupsTab.Controls.Add(groupNameTextbox);
            groupsTab.Controls.Add(label2);
            groupsTab.Controls.Add(currentGameText);
            groupsTab.Controls.Add(label4);
            groupsTab.Location = new System.Drawing.Point(4, 24);
            groupsTab.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupsTab.Name = "groupsTab";
            groupsTab.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupsTab.Size = new System.Drawing.Size(817, 628);
            groupsTab.TabIndex = 100;
            groupsTab.Text = "Groepen";
            groupsTab.UseVisualStyleBackColor = true;
            // 
            // endGameForGroup
            // 
            endGameForGroup.Location = new System.Drawing.Point(620, 91);
            endGameForGroup.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            endGameForGroup.Name = "endGameForGroup";
            endGameForGroup.Size = new System.Drawing.Size(36, 27);
            endGameForGroup.TabIndex = 106;
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
            groupRemarksText.TabIndex = 105;
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
            label16.TabIndex = 104;
            label16.Text = "Opmerkingen";
            // 
            // showPlayedGamesButton
            // 
            showPlayedGamesButton.Location = new System.Drawing.Point(203, 587);
            showPlayedGamesButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            showPlayedGamesButton.Name = "showPlayedGamesButton";
            showPlayedGamesButton.Size = new System.Drawing.Size(172, 27);
            showPlayedGamesButton.TabIndex = 103;
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
            isAdminCheckbox.TabIndex = 102;
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
            label13.TabIndex = 101;
            label13.Text = "Admin";
            // 
            // removeCardFromGroupButton
            // 
            removeCardFromGroupButton.Location = new System.Drawing.Point(620, 1);
            removeCardFromGroupButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            removeCardFromGroupButton.Name = "removeCardFromGroupButton";
            removeCardFromGroupButton.Size = new System.Drawing.Size(36, 27);
            removeCardFromGroupButton.TabIndex = 100;
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
            addCardToGroupButton.TabIndex = 100;
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
            lastScannedCardTextbox.TabIndex = 2;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new System.Drawing.Point(205, 567);
            label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(120, 15);
            label12.TabIndex = 100;
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
            groupsListBox.TabIndex = 100;
            groupsListBox.SelectedIndexChanged += groupsListBox_SelectedIndexChanged;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(addGroupButton);
            flowLayoutPanel2.Controls.Add(deleteGroupButton);
            flowLayoutPanel2.Location = new System.Drawing.Point(7, 584);
            flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new System.Drawing.Size(186, 36);
            flowLayoutPanel2.TabIndex = 100;
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
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(200, 7);
            label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(48, 15);
            label8.TabIndex = 100;
            label8.Text = "Kaart ID";
            // 
            // cardIdTextbox
            // 
            cardIdTextbox.Enabled = false;
            cardIdTextbox.Location = new System.Drawing.Point(303, 3);
            cardIdTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cardIdTextbox.Name = "cardIdTextbox";
            cardIdTextbox.Size = new System.Drawing.Size(305, 23);
            cardIdTextbox.TabIndex = 100;
            cardIdTextbox.TextChanged += GroupDataChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(200, 97);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(67, 15);
            label3.TabIndex = 100;
            label3.Text = "Huidig spel";
            // 
            // startTimePicker
            // 
            startTimePicker.Location = new System.Drawing.Point(303, 125);
            startTimePicker.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            startTimePicker.Name = "startTimePicker";
            startTimePicker.Size = new System.Drawing.Size(352, 23);
            startTimePicker.TabIndex = 100;
            // 
            // currentGameText
            // 
            currentGameText.Enabled = false;
            currentGameText.Location = new System.Drawing.Point(303, 93);
            currentGameText.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            currentGameText.Name = "currentGameText";
            currentGameText.Size = new System.Drawing.Size(305, 23);
            currentGameText.TabIndex = 100;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(200, 132);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(54, 15);
            label4.TabIndex = 100;
            label4.Text = "Begintijd";
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
            // adminTab
            // 
            adminTab.Controls.Add(label11);
            adminTab.Controls.Add(animationLengthTextbox);
            adminTab.Controls.Add(testUserViewButton);
            adminTab.Controls.Add(closeButton);
            adminTab.Controls.Add(label14);
            adminTab.Controls.Add(gameTimeoutTextbox);
            adminTab.Controls.Add(saveGlobalSettings);
            adminTab.Controls.Add(hideButton);
            adminTab.Controls.Add(gameStateLabel);
            adminTab.Controls.Add(label9);
            adminTab.Controls.Add(startStopGameButton);
            adminTab.Location = new System.Drawing.Point(4, 24);
            adminTab.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            adminTab.Name = "adminTab";
            adminTab.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            adminTab.Size = new System.Drawing.Size(817, 628);
            adminTab.TabIndex = 100;
            adminTab.Text = "Admin";
            adminTab.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(19, 219);
            label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(118, 15);
            label11.TabIndex = 108;
            label11.Text = "Animatie lengte (ms)";
            // 
            // animationLengthTextbox
            // 
            animationLengthTextbox.Location = new System.Drawing.Point(144, 216);
            animationLengthTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            animationLengthTextbox.Name = "animationLengthTextbox";
            animationLengthTextbox.Size = new System.Drawing.Size(107, 23);
            animationLengthTextbox.TabIndex = 107;
            // 
            // testUserViewButton
            // 
            testUserViewButton.Location = new System.Drawing.Point(10, 593);
            testUserViewButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            testUserViewButton.Name = "testUserViewButton";
            testUserViewButton.Size = new System.Drawing.Size(130, 27);
            testUserViewButton.TabIndex = 106;
            testUserViewButton.Text = "Test user view";
            testUserViewButton.UseVisualStyleBackColor = true;
            testUserViewButton.Visible = false;
            testUserViewButton.Click += testUserViewButton_Click;
            // 
            // closeButton
            // 
            closeButton.Location = new System.Drawing.Point(7, 74);
            closeButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            closeButton.Name = "closeButton";
            closeButton.Size = new System.Drawing.Size(130, 27);
            closeButton.TabIndex = 105;
            closeButton.Text = "Programma sluiten";
            closeButton.UseVisualStyleBackColor = true;
            closeButton.Click += closeButton_Click;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new System.Drawing.Point(7, 189);
            label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(130, 15);
            label14.TabIndex = 104;
            label14.Text = "Spel timeout (minuten)";
            // 
            // gameTimeoutTextbox
            // 
            gameTimeoutTextbox.Location = new System.Drawing.Point(144, 186);
            gameTimeoutTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gameTimeoutTextbox.Name = "gameTimeoutTextbox";
            gameTimeoutTextbox.Size = new System.Drawing.Size(107, 23);
            gameTimeoutTextbox.TabIndex = 103;
            gameTimeoutTextbox.TextChanged += ForceTextboxToInt;
            // 
            // saveGlobalSettings
            // 
            saveGlobalSettings.Location = new System.Drawing.Point(7, 246);
            saveGlobalSettings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            saveGlobalSettings.Name = "saveGlobalSettings";
            saveGlobalSettings.Size = new System.Drawing.Size(244, 27);
            saveGlobalSettings.TabIndex = 102;
            saveGlobalSettings.Text = "Opslaan";
            saveGlobalSettings.UseVisualStyleBackColor = true;
            saveGlobalSettings.Click += saveGlobalSettings_Click;
            // 
            // hideButton
            // 
            hideButton.Location = new System.Drawing.Point(7, 40);
            hideButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            hideButton.Name = "hideButton";
            hideButton.Size = new System.Drawing.Size(130, 27);
            hideButton.TabIndex = 101;
            hideButton.Text = "Vergrendelen";
            hideButton.UseVisualStyleBackColor = true;
            hideButton.Click += hideButton_Click;
            // 
            // gameStateLabel
            // 
            gameStateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            gameStateLabel.Location = new System.Drawing.Point(258, 10);
            gameStateLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            gameStateLabel.Name = "gameStateLabel";
            gameStateLabel.Size = new System.Drawing.Size(138, 18);
            gameStateLabel.TabIndex = 100;
            gameStateLabel.Text = "GEPAUZEERD";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(144, 13);
            label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(104, 15);
            label9.TabIndex = 100;
            label9.Text = "Spel is momenteel";
            // 
            // startStopGameButton
            // 
            startStopGameButton.Location = new System.Drawing.Point(7, 7);
            startStopGameButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            startStopGameButton.Name = "startStopGameButton";
            startStopGameButton.Size = new System.Drawing.Size(130, 27);
            startStopGameButton.TabIndex = 100;
            startStopGameButton.Text = "Start spel";
            startStopGameButton.UseVisualStyleBackColor = true;
            startStopGameButton.Click += startStopGameButton_Click;
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
            // AdminView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1068, 668);
            ControlBox = false;
            Controls.Add(errorFlowLayout);
            Controls.Add(tabControl1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "AdminView";
            Padding = new System.Windows.Forms.Padding(6);
            Text = "Admin";
            Load += Form1_Load;
            tabControl1.ResumeLayout(false);
            groupsTab.ResumeLayout(false);
            groupsTab.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            gamesTab.ResumeLayout(false);
            gamesTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)priorityNumber).EndInit();
            ((System.ComponentModel.ISupportInitialize)maxPlayerAmountNumber).EndInit();
            ((System.ComponentModel.ISupportInitialize)timeoutNumber).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            adminTab.ResumeLayout(false);
            adminTab.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Button saveGroupButton;
        private System.Windows.Forms.TextBox groupNameTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox scoutingNameTextbox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage groupsTab;
        private System.Windows.Forms.TabPage gamesTab;
        private System.Windows.Forms.ListBox gamesListBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox gameDescriptionTextbox;
        private System.Windows.Forms.RichTextBox gameExplanationTextbox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox cardIdTextbox;
        private System.Windows.Forms.FlowLayoutPanel errorFlowLayout;
        private System.Windows.Forms.TabPage adminTab;
        private System.Windows.Forms.Label gameStateLabel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button startStopGameButton;
        private System.Windows.Forms.TextBox gameCodeTextbox;
        private System.Windows.Forms.Button deleteGameButton;
        private System.Windows.Forms.Button addGameButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button saveGameButton;
        private System.Windows.Forms.ListBox groupsListBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button addGroupButton;
        private System.Windows.Forms.Button deleteGroupButton;
        private System.Windows.Forms.Button addCardToGroupButton;
        private System.Windows.Forms.TextBox lastScannedCardTextbox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button removeCardFromGroupButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker startTimePicker;
        private System.Windows.Forms.TextBox currentGameText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox isAdminCheckbox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button hideButton;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox gameTimeoutTextbox;
        private System.Windows.Forms.Button saveGlobalSettings;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button showPlayedGamesButton;
        private System.Windows.Forms.RichTextBox groupRemarksText;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.RichTextBox gameRemarksText;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox gameActiveCheckbox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button testUserViewButton;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.NumericUpDown timeoutNumber;
        private System.Windows.Forms.Button endGameForGroup;
        private System.Windows.Forms.NumericUpDown maxPlayerAmountNumber;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox animationLengthTextbox;
        private System.Windows.Forms.NumericUpDown priorityNumber;
    }
}

