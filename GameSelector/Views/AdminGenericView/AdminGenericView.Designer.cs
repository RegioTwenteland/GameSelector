namespace GameSelector.Views.AdminGenericView
{
    partial class AdminGenericView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminGenericView));
            tabControl = new System.Windows.Forms.TabControl();
            errorFlowLayout = new System.Windows.Forms.FlowLayoutPanel();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tabControl.Location = new System.Drawing.Point(6, 6);
            tabControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new System.Drawing.Size(845, 656);
            tabControl.TabIndex = 100;
            // 
            // errorFlowLayout
            // 
            errorFlowLayout.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            errorFlowLayout.AutoScroll = true;
            errorFlowLayout.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            errorFlowLayout.Location = new System.Drawing.Point(859, 9);
            errorFlowLayout.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            errorFlowLayout.Name = "errorFlowLayout";
            errorFlowLayout.Padding = new System.Windows.Forms.Padding(6);
            errorFlowLayout.Size = new System.Drawing.Size(203, 653);
            errorFlowLayout.TabIndex = 100;
            // 
            // AdminGenericView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1068, 668);
            Controls.Add(errorFlowLayout);
            Controls.Add(tabControl);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "AdminGenericView";
            Padding = new System.Windows.Forms.Padding(6);
            Text = "Admin";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.FlowLayoutPanel errorFlowLayout;
    }
}

