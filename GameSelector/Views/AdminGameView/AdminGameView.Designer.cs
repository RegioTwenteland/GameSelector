using CustomControls;

namespace GameSelector.Views.AdminGameView
{
    partial class AdminGameView
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
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            grid = new GameSelectorDataGridView();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            bulkRemoveButton = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            sumDisplayLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            selectedRowsLabel = new System.Windows.Forms.Label();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 405F));
            tableLayoutPanel1.Controls.Add(grid, 0, 0);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 1);
            tableLayoutPanel1.Controls.Add(panel1, 1, 1);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.0382156F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.96178341F));
            tableLayoutPanel1.Size = new System.Drawing.Size(990, 628);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // grid
            // 
            grid.AllowDrop = true;
            grid.AllowUserToOrderColumns = true;
            grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tableLayoutPanel1.SetColumnSpan(grid, 2);
            grid.Dock = System.Windows.Forms.DockStyle.Fill;
            grid.Location = new System.Drawing.Point(3, 3);
            grid.Name = "grid";
            grid.Size = new System.Drawing.Size(984, 572);
            grid.TabIndex = 0;
            grid.CellValueChanged += grid_CellValueChanged;
            grid.SelectionChanged += grid_SelectionChanged;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(bulkRemoveButton);
            flowLayoutPanel1.Location = new System.Drawing.Point(3, 581);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new System.Drawing.Size(200, 44);
            flowLayoutPanel1.TabIndex = 2;
            // 
            // bulkRemoveButton
            // 
            bulkRemoveButton.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            bulkRemoveButton.Location = new System.Drawing.Point(3, 3);
            bulkRemoveButton.Name = "bulkRemoveButton";
            bulkRemoveButton.Size = new System.Drawing.Size(41, 41);
            bulkRemoveButton.TabIndex = 1;
            bulkRemoveButton.Text = "🗑";
            bulkRemoveButton.UseVisualStyleBackColor = true;
            bulkRemoveButton.Click += bulkRemoveButton_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(selectedRowsLabel);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(sumDisplayLabel);
            panel1.Controls.Add(label1);
            panel1.Location = new System.Drawing.Point(588, 581);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(399, 44);
            panel1.TabIndex = 3;
            // 
            // sumDisplayLabel
            // 
            sumDisplayLabel.AutoSize = true;
            sumDisplayLabel.Location = new System.Drawing.Point(62, 3);
            sumDisplayLabel.Name = "sumDisplayLabel";
            sumDisplayLabel.Size = new System.Drawing.Size(13, 15);
            sumDisplayLabel.TabIndex = 1;
            sumDisplayLabel.Text = "0";
            // 
            // label1
            // 
            label1.Location = new System.Drawing.Point(3, 3);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(53, 15);
            label1.TabIndex = 0;
            label1.Text = "Som:";
            // 
            // label2
            // 
            label2.Location = new System.Drawing.Point(3, 20);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(53, 15);
            label2.TabIndex = 2;
            label2.Text = "Rijen:";
            // 
            // selectedRowsLabel
            // 
            selectedRowsLabel.AutoSize = true;
            selectedRowsLabel.Location = new System.Drawing.Point(62, 20);
            selectedRowsLabel.Name = "selectedRowsLabel";
            selectedRowsLabel.Size = new System.Drawing.Size(13, 15);
            selectedRowsLabel.TabIndex = 3;
            selectedRowsLabel.Text = "0";
            // 
            // AdminGroupView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.ControlLightLight;
            Controls.Add(tableLayoutPanel1);
            Name = "AdminGroupView";
            Size = new System.Drawing.Size(990, 628);
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private GameSelectorDataGridView grid;
        private System.Windows.Forms.Button bulkRemoveButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label sumDisplayLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label selectedRowsLabel;
        private System.Windows.Forms.Label label2;
    }
}
