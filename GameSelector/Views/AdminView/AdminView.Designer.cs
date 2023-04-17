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
            this.saveGroupButton = new System.Windows.Forms.Button();
            this.groupNameTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.scoutingNameTextbox = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.groupsTab = new System.Windows.Forms.TabPage();
            this.removeCardFromGroupButton = new System.Windows.Forms.Button();
            this.addCardToGroupButton = new System.Windows.Forms.Button();
            this.lastScannedCardTextbox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupsListBox = new System.Windows.Forms.ListBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.addGroupButton = new System.Windows.Forms.Button();
            this.deleteGroupButton = new System.Windows.Forms.Button();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.label8 = new System.Windows.Forms.Label();
            this.cardIdTextbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.startTimePicker = new System.Windows.Forms.DateTimePicker();
            this.currentGameText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gamesTab = new System.Windows.Forms.TabPage();
            this.currentOccupantTextbox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.saveGameButton = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.addGameButton = new System.Windows.Forms.Button();
            this.incPrioButton = new System.Windows.Forms.Button();
            this.decPrioButton = new System.Windows.Forms.Button();
            this.deleteGameButton = new System.Windows.Forms.Button();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.gamesListBox = new System.Windows.Forms.ListBox();
            this.gameCodeTextbox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.gameColorComboBox = new System.Windows.Forms.ComboBox();
            this.gameDescriptionTextbox = new System.Windows.Forms.TextBox();
            this.gameExplanationTextbox = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.adminTab = new System.Windows.Forms.TabPage();
            this.gameStateLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.startStopGameButton = new System.Windows.Forms.Button();
            this.errorFlowLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.tabControl1.SuspendLayout();
            this.groupsTab.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.gamesTab.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.adminTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveGroupButton
            // 
            this.saveGroupButton.Location = new System.Drawing.Point(171, 134);
            this.saveGroupButton.Name = "saveGroupButton";
            this.saveGroupButton.Size = new System.Drawing.Size(289, 23);
            this.saveGroupButton.TabIndex = 1;
            this.saveGroupButton.Text = "Opslaan";
            this.saveGroupButton.UseVisualStyleBackColor = true;
            this.saveGroupButton.Click += new System.EventHandler(this.saveGroupButton_Click);
            // 
            // groupNameTextbox
            // 
            this.groupNameTextbox.Location = new System.Drawing.Point(260, 55);
            this.groupNameTextbox.Name = "groupNameTextbox";
            this.groupNameTextbox.Size = new System.Drawing.Size(200, 20);
            this.groupNameTextbox.TabIndex = 2;
            this.groupNameTextbox.TextChanged += new System.EventHandler(this.GroupDataChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(171, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Groep naam";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(171, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Scouting naam";
            // 
            // scoutingNameTextbox
            // 
            this.scoutingNameTextbox.Location = new System.Drawing.Point(260, 29);
            this.scoutingNameTextbox.Name = "scoutingNameTextbox";
            this.scoutingNameTextbox.Size = new System.Drawing.Size(200, 20);
            this.scoutingNameTextbox.TabIndex = 4;
            this.scoutingNameTextbox.TextChanged += new System.EventHandler(this.GroupDataChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.groupsTab);
            this.tabControl1.Controls.Add(this.gamesTab);
            this.tabControl1.Controls.Add(this.adminTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabControl1.Location = new System.Drawing.Point(5, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(707, 569);
            this.tabControl1.TabIndex = 11;
            // 
            // groupsTab
            // 
            this.groupsTab.Controls.Add(this.removeCardFromGroupButton);
            this.groupsTab.Controls.Add(this.addCardToGroupButton);
            this.groupsTab.Controls.Add(this.lastScannedCardTextbox);
            this.groupsTab.Controls.Add(this.label12);
            this.groupsTab.Controls.Add(this.groupsListBox);
            this.groupsTab.Controls.Add(this.flowLayoutPanel2);
            this.groupsTab.Controls.Add(this.label8);
            this.groupsTab.Controls.Add(this.cardIdTextbox);
            this.groupsTab.Controls.Add(this.label1);
            this.groupsTab.Controls.Add(this.label3);
            this.groupsTab.Controls.Add(this.startTimePicker);
            this.groupsTab.Controls.Add(this.scoutingNameTextbox);
            this.groupsTab.Controls.Add(this.saveGroupButton);
            this.groupsTab.Controls.Add(this.groupNameTextbox);
            this.groupsTab.Controls.Add(this.label2);
            this.groupsTab.Controls.Add(this.currentGameText);
            this.groupsTab.Controls.Add(this.label4);
            this.groupsTab.Location = new System.Drawing.Point(4, 22);
            this.groupsTab.Name = "groupsTab";
            this.groupsTab.Padding = new System.Windows.Forms.Padding(3);
            this.groupsTab.Size = new System.Drawing.Size(699, 543);
            this.groupsTab.TabIndex = 0;
            this.groupsTab.Text = "Groepen";
            this.groupsTab.UseVisualStyleBackColor = true;
            // 
            // removeCardFromGroupButton
            // 
            this.removeCardFromGroupButton.Location = new System.Drawing.Point(429, 1);
            this.removeCardFromGroupButton.Name = "removeCardFromGroupButton";
            this.removeCardFromGroupButton.Size = new System.Drawing.Size(31, 23);
            this.removeCardFromGroupButton.TabIndex = 18;
            this.removeCardFromGroupButton.Text = "✖";
            this.removeCardFromGroupButton.UseVisualStyleBackColor = true;
            this.removeCardFromGroupButton.Click += new System.EventHandler(this.removeCardFromGroupButton_Click);
            // 
            // addCardToGroupButton
            // 
            this.addCardToGroupButton.Location = new System.Drawing.Point(377, 247);
            this.addCardToGroupButton.Name = "addCardToGroupButton";
            this.addCardToGroupButton.Size = new System.Drawing.Size(145, 23);
            this.addCardToGroupButton.TabIndex = 17;
            this.addCardToGroupButton.Text = "Koppel aan huidige groep";
            this.addCardToGroupButton.UseVisualStyleBackColor = true;
            this.addCardToGroupButton.Click += new System.EventHandler(this.addCardToGroupButton_Click);
            // 
            // lastScannedCardTextbox
            // 
            this.lastScannedCardTextbox.Enabled = false;
            this.lastScannedCardTextbox.Location = new System.Drawing.Point(290, 249);
            this.lastScannedCardTextbox.Name = "lastScannedCardTextbox";
            this.lastScannedCardTextbox.Size = new System.Drawing.Size(81, 20);
            this.lastScannedCardTextbox.TabIndex = 16;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(171, 252);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(113, 13);
            this.label12.TabIndex = 15;
            this.label12.Text = "Laatst gescande kaart";
            // 
            // groupsListBox
            // 
            this.groupsListBox.FormattingEnabled = true;
            this.groupsListBox.Location = new System.Drawing.Point(6, 6);
            this.groupsListBox.Name = "groupsListBox";
            this.groupsListBox.Size = new System.Drawing.Size(159, 498);
            this.groupsListBox.TabIndex = 14;
            this.groupsListBox.SelectedIndexChanged += new System.EventHandler(this.groupsListBox_SelectedIndexChanged);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.addGroupButton);
            this.flowLayoutPanel2.Controls.Add(this.deleteGroupButton);
            this.flowLayoutPanel2.Controls.Add(this.materialLabel2);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(6, 506);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(159, 31);
            this.flowLayoutPanel2.TabIndex = 13;
            // 
            // addGroupButton
            // 
            this.addGroupButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addGroupButton.Location = new System.Drawing.Point(3, 3);
            this.addGroupButton.Name = "addGroupButton";
            this.addGroupButton.Size = new System.Drawing.Size(73, 23);
            this.addGroupButton.TabIndex = 10;
            this.addGroupButton.Text = "➕";
            this.addGroupButton.UseVisualStyleBackColor = true;
            this.addGroupButton.Click += new System.EventHandler(this.addGroupButton_Click);
            // 
            // deleteGroupButton
            // 
            this.deleteGroupButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteGroupButton.Location = new System.Drawing.Point(82, 3);
            this.deleteGroupButton.Name = "deleteGroupButton";
            this.deleteGroupButton.Size = new System.Drawing.Size(73, 23);
            this.deleteGroupButton.TabIndex = 8;
            this.deleteGroupButton.Text = "✖";
            this.deleteGroupButton.UseVisualStyleBackColor = true;
            this.deleteGroupButton.Click += new System.EventHandler(this.deleteGroupButton_Click);
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(3, 29);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(108, 19);
            this.materialLabel2.TabIndex = 11;
            this.materialLabel2.Text = "materialLabel2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(171, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Kaart ID";
            // 
            // cardIdTextbox
            // 
            this.cardIdTextbox.Enabled = false;
            this.cardIdTextbox.Location = new System.Drawing.Point(260, 3);
            this.cardIdTextbox.Name = "cardIdTextbox";
            this.cardIdTextbox.Size = new System.Drawing.Size(163, 20);
            this.cardIdTextbox.TabIndex = 11;
            this.cardIdTextbox.TextChanged += new System.EventHandler(this.GroupDataChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(171, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Huidig spel";
            // 
            // startTimePicker
            // 
            this.startTimePicker.Location = new System.Drawing.Point(260, 108);
            this.startTimePicker.Name = "startTimePicker";
            this.startTimePicker.Size = new System.Drawing.Size(200, 20);
            this.startTimePicker.TabIndex = 10;
            // 
            // currentGameText
            // 
            this.currentGameText.Enabled = false;
            this.currentGameText.Location = new System.Drawing.Point(260, 81);
            this.currentGameText.Name = "currentGameText";
            this.currentGameText.Size = new System.Drawing.Size(200, 20);
            this.currentGameText.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(171, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Begintijd";
            // 
            // gamesTab
            // 
            this.gamesTab.Controls.Add(this.currentOccupantTextbox);
            this.gamesTab.Controls.Add(this.label11);
            this.gamesTab.Controls.Add(this.saveGameButton);
            this.gamesTab.Controls.Add(this.label10);
            this.gamesTab.Controls.Add(this.flowLayoutPanel1);
            this.gamesTab.Controls.Add(this.gamesListBox);
            this.gamesTab.Controls.Add(this.gameCodeTextbox);
            this.gamesTab.Controls.Add(this.label5);
            this.gamesTab.Controls.Add(this.gameColorComboBox);
            this.gamesTab.Controls.Add(this.gameDescriptionTextbox);
            this.gamesTab.Controls.Add(this.gameExplanationTextbox);
            this.gamesTab.Controls.Add(this.label6);
            this.gamesTab.Controls.Add(this.label7);
            this.gamesTab.Location = new System.Drawing.Point(4, 22);
            this.gamesTab.Name = "gamesTab";
            this.gamesTab.Padding = new System.Windows.Forms.Padding(3);
            this.gamesTab.Size = new System.Drawing.Size(699, 543);
            this.gamesTab.TabIndex = 1;
            this.gamesTab.Text = "Spellen";
            this.gamesTab.UseVisualStyleBackColor = true;
            // 
            // currentOccupantTextbox
            // 
            this.currentOccupantTextbox.Enabled = false;
            this.currentOccupantTextbox.Location = new System.Drawing.Point(270, 168);
            this.currentOccupantTextbox.Name = "currentOccupantTextbox";
            this.currentOccupantTextbox.Size = new System.Drawing.Size(260, 20);
            this.currentOccupantTextbox.TabIndex = 15;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(171, 171);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 17);
            this.label11.TabIndex = 14;
            this.label11.Text = "Huidige bezetting";
            // 
            // saveGameButton
            // 
            this.saveGameButton.Location = new System.Drawing.Point(174, 194);
            this.saveGameButton.Name = "saveGameButton";
            this.saveGameButton.Size = new System.Drawing.Size(356, 23);
            this.saveGameButton.TabIndex = 13;
            this.saveGameButton.Text = "Opslaan";
            this.saveGameButton.UseVisualStyleBackColor = true;
            this.saveGameButton.Click += new System.EventHandler(this.saveGameButton_Click);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(171, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(93, 17);
            this.label10.TabIndex = 12;
            this.label10.Text = "Code";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.addGameButton);
            this.flowLayoutPanel1.Controls.Add(this.incPrioButton);
            this.flowLayoutPanel1.Controls.Add(this.decPrioButton);
            this.flowLayoutPanel1.Controls.Add(this.deleteGameButton);
            this.flowLayoutPanel1.Controls.Add(this.materialLabel1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(6, 506);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(159, 31);
            this.flowLayoutPanel1.TabIndex = 11;
            // 
            // addGameButton
            // 
            this.addGameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addGameButton.Location = new System.Drawing.Point(3, 3);
            this.addGameButton.Name = "addGameButton";
            this.addGameButton.Size = new System.Drawing.Size(33, 23);
            this.addGameButton.TabIndex = 10;
            this.addGameButton.Text = "➕";
            this.addGameButton.UseVisualStyleBackColor = true;
            this.addGameButton.Click += new System.EventHandler(this.addGameButton_Click);
            // 
            // incPrioButton
            // 
            this.incPrioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.incPrioButton.Location = new System.Drawing.Point(42, 3);
            this.incPrioButton.Name = "incPrioButton";
            this.incPrioButton.Size = new System.Drawing.Size(33, 23);
            this.incPrioButton.TabIndex = 7;
            this.incPrioButton.Text = "↑";
            this.incPrioButton.UseVisualStyleBackColor = true;
            this.incPrioButton.Click += new System.EventHandler(this.incPrioButton_Click);
            // 
            // decPrioButton
            // 
            this.decPrioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.decPrioButton.Location = new System.Drawing.Point(81, 3);
            this.decPrioButton.Name = "decPrioButton";
            this.decPrioButton.Size = new System.Drawing.Size(33, 23);
            this.decPrioButton.TabIndex = 9;
            this.decPrioButton.Text = "↓";
            this.decPrioButton.UseVisualStyleBackColor = true;
            this.decPrioButton.Click += new System.EventHandler(this.decPrioButton_Click);
            // 
            // deleteGameButton
            // 
            this.deleteGameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteGameButton.Location = new System.Drawing.Point(120, 3);
            this.deleteGameButton.Name = "deleteGameButton";
            this.deleteGameButton.Size = new System.Drawing.Size(33, 23);
            this.deleteGameButton.TabIndex = 8;
            this.deleteGameButton.Text = "✖";
            this.deleteGameButton.UseVisualStyleBackColor = true;
            this.deleteGameButton.Click += new System.EventHandler(this.deleteGameButton_Click);
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(3, 29);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(108, 19);
            this.materialLabel1.TabIndex = 11;
            this.materialLabel1.Text = "materialLabel1";
            // 
            // gamesListBox
            // 
            this.gamesListBox.FormattingEnabled = true;
            this.gamesListBox.Location = new System.Drawing.Point(6, 6);
            this.gamesListBox.Name = "gamesListBox";
            this.gamesListBox.Size = new System.Drawing.Size(159, 498);
            this.gamesListBox.TabIndex = 1;
            this.gamesListBox.SelectedIndexChanged += new System.EventHandler(this.gamesListBox_SelectedIndexChanged);
            // 
            // gameCodeTextbox
            // 
            this.gameCodeTextbox.Location = new System.Drawing.Point(270, 3);
            this.gameCodeTextbox.Name = "gameCodeTextbox";
            this.gameCodeTextbox.Size = new System.Drawing.Size(260, 20);
            this.gameCodeTextbox.TabIndex = 9;
            this.gameCodeTextbox.TextChanged += new System.EventHandler(this.GameDataChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(171, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "Omschrijving";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gameColorComboBox
            // 
            this.gameColorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gameColorComboBox.FormattingEnabled = true;
            this.gameColorComboBox.Items.AddRange(new object[] {
            "Blauw",
            "Rood",
            "Groen"});
            this.gameColorComboBox.Location = new System.Drawing.Point(270, 141);
            this.gameColorComboBox.Name = "gameColorComboBox";
            this.gameColorComboBox.Size = new System.Drawing.Size(260, 21);
            this.gameColorComboBox.TabIndex = 7;
            this.gameColorComboBox.TextChanged += new System.EventHandler(this.GameDataChanged);
            // 
            // gameDescriptionTextbox
            // 
            this.gameDescriptionTextbox.Location = new System.Drawing.Point(270, 29);
            this.gameDescriptionTextbox.Name = "gameDescriptionTextbox";
            this.gameDescriptionTextbox.Size = new System.Drawing.Size(260, 20);
            this.gameDescriptionTextbox.TabIndex = 6;
            this.gameDescriptionTextbox.TextChanged += new System.EventHandler(this.GameDataChanged);
            // 
            // gameExplanationTextbox
            // 
            this.gameExplanationTextbox.Location = new System.Drawing.Point(270, 55);
            this.gameExplanationTextbox.Name = "gameExplanationTextbox";
            this.gameExplanationTextbox.Size = new System.Drawing.Size(260, 80);
            this.gameExplanationTextbox.TabIndex = 7;
            this.gameExplanationTextbox.Text = "";
            this.gameExplanationTextbox.TextChanged += new System.EventHandler(this.GameDataChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(171, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 17);
            this.label6.TabIndex = 3;
            this.label6.Text = "Uitleg";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(171, 141);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 17);
            this.label7.TabIndex = 4;
            this.label7.Text = "Kleur";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // adminTab
            // 
            this.adminTab.Controls.Add(this.gameStateLabel);
            this.adminTab.Controls.Add(this.label9);
            this.adminTab.Controls.Add(this.startStopGameButton);
            this.adminTab.Location = new System.Drawing.Point(4, 22);
            this.adminTab.Name = "adminTab";
            this.adminTab.Padding = new System.Windows.Forms.Padding(3);
            this.adminTab.Size = new System.Drawing.Size(699, 543);
            this.adminTab.TabIndex = 2;
            this.adminTab.Text = "Admin";
            this.adminTab.UseVisualStyleBackColor = true;
            // 
            // gameStateLabel
            // 
            this.gameStateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameStateLabel.Location = new System.Drawing.Point(185, 9);
            this.gameStateLabel.Name = "gameStateLabel";
            this.gameStateLabel.Size = new System.Drawing.Size(118, 16);
            this.gameStateLabel.TabIndex = 2;
            this.gameStateLabel.Text = "GEPAUZEERD";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(87, 11);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Spel is momenteel";
            // 
            // startStopGameButton
            // 
            this.startStopGameButton.Location = new System.Drawing.Point(6, 6);
            this.startStopGameButton.Name = "startStopGameButton";
            this.startStopGameButton.Size = new System.Drawing.Size(75, 23);
            this.startStopGameButton.TabIndex = 0;
            this.startStopGameButton.Text = "Start spel";
            this.startStopGameButton.UseVisualStyleBackColor = true;
            this.startStopGameButton.Click += new System.EventHandler(this.startStopGameButton_Click);
            // 
            // errorFlowLayout
            // 
            this.errorFlowLayout.AutoScroll = true;
            this.errorFlowLayout.Dock = System.Windows.Forms.DockStyle.Right;
            this.errorFlowLayout.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.errorFlowLayout.Location = new System.Drawing.Point(710, 5);
            this.errorFlowLayout.Name = "errorFlowLayout";
            this.errorFlowLayout.Padding = new System.Windows.Forms.Padding(5);
            this.errorFlowLayout.Size = new System.Drawing.Size(200, 569);
            this.errorFlowLayout.TabIndex = 13;
            // 
            // AdminView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 579);
            this.Controls.Add(this.errorFlowLayout);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AdminView";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "Admin";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.groupsTab.ResumeLayout(false);
            this.groupsTab.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.gamesTab.ResumeLayout(false);
            this.gamesTab.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.adminTab.ResumeLayout(false);
            this.adminTab.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox gameColorComboBox;
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
        private System.Windows.Forms.Button decPrioButton;
        private System.Windows.Forms.Button incPrioButton;
        private System.Windows.Forms.Button addGameButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label10;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private System.Windows.Forms.Button saveGameButton;
        private System.Windows.Forms.TextBox currentOccupantTextbox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListBox groupsListBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button addGroupButton;
        private System.Windows.Forms.Button deleteGroupButton;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private System.Windows.Forms.Button addCardToGroupButton;
        private System.Windows.Forms.TextBox lastScannedCardTextbox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button removeCardFromGroupButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker startTimePicker;
        private System.Windows.Forms.TextBox currentGameText;
        private System.Windows.Forms.Label label4;
    }
}

