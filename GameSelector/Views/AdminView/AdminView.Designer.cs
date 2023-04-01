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
            this.writeCardButton = new System.Windows.Forms.Button();
            this.groupIdText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupNameText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.currentGameText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.startTimePicker = new System.Windows.Forms.DateTimePicker();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.gamesListBox = new System.Windows.Forms.ListBox();
            this.addGameButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gameColorComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.deleteGameButton = new System.Windows.Forms.Button();
            this.gameDescriptionTextbox = new System.Windows.Forms.TextBox();
            this.gameExplanationTextbox = new System.Windows.Forms.RichTextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.cardIdText = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // writeCardButton
            // 
            this.writeCardButton.Location = new System.Drawing.Point(8, 151);
            this.writeCardButton.Name = "writeCardButton";
            this.writeCardButton.Size = new System.Drawing.Size(286, 23);
            this.writeCardButton.TabIndex = 1;
            this.writeCardButton.Text = "Schrijf";
            this.writeCardButton.UseVisualStyleBackColor = true;
            this.writeCardButton.Click += new System.EventHandler(this.writeCardButton_Click);
            // 
            // groupIdText
            // 
            this.groupIdText.Location = new System.Drawing.Point(94, 32);
            this.groupIdText.Name = "groupIdText";
            this.groupIdText.Size = new System.Drawing.Size(200, 20);
            this.groupIdText.TabIndex = 2;
            this.groupIdText.TextChanged += new System.EventHandler(this.ForceTextboxToInt);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Groep ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Groep naam";
            // 
            // groupNameText
            // 
            this.groupNameText.Location = new System.Drawing.Point(94, 58);
            this.groupNameText.Name = "groupNameText";
            this.groupNameText.Size = new System.Drawing.Size(200, 20);
            this.groupNameText.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Huidig spel";
            // 
            // currentGameText
            // 
            this.currentGameText.Enabled = false;
            this.currentGameText.Location = new System.Drawing.Point(94, 84);
            this.currentGameText.Name = "currentGameText";
            this.currentGameText.Size = new System.Drawing.Size(200, 20);
            this.currentGameText.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Begintijd";
            // 
            // startTimePicker
            // 
            this.startTimePicker.Location = new System.Drawing.Point(94, 111);
            this.startTimePicker.Name = "startTimePicker";
            this.startTimePicker.Size = new System.Drawing.Size(200, 20);
            this.startTimePicker.TabIndex = 10;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(908, 589);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel2);
            this.tabPage2.Controls.Add(this.tableLayoutPanel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(900, 563);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Spellen";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.gamesListBox, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.addGameButton, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(165, 557);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // gamesListBox
            // 
            this.gamesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gamesListBox.FormattingEnabled = true;
            this.gamesListBox.Location = new System.Drawing.Point(3, 3);
            this.gamesListBox.Name = "gamesListBox";
            this.gamesListBox.Size = new System.Drawing.Size(159, 522);
            this.gamesListBox.TabIndex = 1;
            this.gamesListBox.SelectedIndexChanged += new System.EventHandler(this.gamesListBox_SelectedIndexChanged);
            // 
            // addGameButton
            // 
            this.addGameButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addGameButton.Enabled = false;
            this.addGameButton.Location = new System.Drawing.Point(3, 531);
            this.addGameButton.Name = "addGameButton";
            this.addGameButton.Size = new System.Drawing.Size(159, 23);
            this.addGameButton.TabIndex = 2;
            this.addGameButton.Text = "Spel toevoegen";
            this.addGameButton.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.56863F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.43137F));
            this.tableLayoutPanel1.Controls.Add(this.gameColorComboBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.deleteGameButton, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.gameDescriptionTextbox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.gameExplanationTextbox, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(176, 6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(408, 281);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // gameColorComboBox
            // 
            this.gameColorComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.gameColorComboBox.FormattingEnabled = true;
            this.gameColorComboBox.Items.AddRange(new object[] {
            "Blauw",
            "Rood",
            "Groen"});
            this.gameColorComboBox.Location = new System.Drawing.Point(193, 164);
            this.gameColorComboBox.Name = "gameColorComboBox";
            this.gameColorComboBox.Size = new System.Drawing.Size(212, 21);
            this.gameColorComboBox.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(61, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Omschrijving";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(79, 168);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Kleur";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(78, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Uitleg";
            // 
            // deleteGameButton
            // 
            this.deleteGameButton.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.SetColumnSpan(this.deleteGameButton, 2);
            this.deleteGameButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deleteGameButton.Enabled = false;
            this.deleteGameButton.Location = new System.Drawing.Point(3, 213);
            this.deleteGameButton.Name = "deleteGameButton";
            this.deleteGameButton.Size = new System.Drawing.Size(402, 65);
            this.deleteGameButton.TabIndex = 5;
            this.deleteGameButton.Text = "Spel verwijderen";
            this.deleteGameButton.UseVisualStyleBackColor = false;
            // 
            // gameDescriptionTextbox
            // 
            this.gameDescriptionTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.gameDescriptionTextbox.Location = new System.Drawing.Point(193, 25);
            this.gameDescriptionTextbox.Name = "gameDescriptionTextbox";
            this.gameDescriptionTextbox.Size = new System.Drawing.Size(212, 20);
            this.gameDescriptionTextbox.TabIndex = 6;
            // 
            // gameExplanationTextbox
            // 
            this.gameExplanationTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gameExplanationTextbox.Location = new System.Drawing.Point(193, 73);
            this.gameExplanationTextbox.Name = "gameExplanationTextbox";
            this.gameExplanationTextbox.Size = new System.Drawing.Size(212, 64);
            this.gameExplanationTextbox.TabIndex = 7;
            this.gameExplanationTextbox.Text = "";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.cardIdText);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.startTimePicker);
            this.tabPage1.Controls.Add(this.groupNameText);
            this.tabPage1.Controls.Add(this.writeCardButton);
            this.tabPage1.Controls.Add(this.groupIdText);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.currentGameText);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(900, 563);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "NFC-kaart";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Kaart ID";
            // 
            // cardIdText
            // 
            this.cardIdText.Enabled = false;
            this.cardIdText.Location = new System.Drawing.Point(94, 6);
            this.cardIdText.Name = "cardIdText";
            this.cardIdText.Size = new System.Drawing.Size(200, 20);
            this.cardIdText.TabIndex = 11;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(900, 563);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Groepen";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // AdminView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 589);
            this.Controls.Add(this.tabControl1);
            this.Name = "AdminView";
            this.Text = "Admin";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button writeCardButton;
        private System.Windows.Forms.TextBox groupIdText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox groupNameText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox currentGameText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker startTimePicker;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox gamesListBox;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button addGameButton;
        private System.Windows.Forms.ComboBox gameColorComboBox;
        private System.Windows.Forms.Button deleteGameButton;
        private System.Windows.Forms.TextBox gameDescriptionTextbox;
        private System.Windows.Forms.RichTextBox gameExplanationTextbox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox cardIdText;
    }
}

