namespace GaugeReader
{
    partial class GaugeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GaugeForm));
            this.OpenButton = new System.Windows.Forms.Button();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.LogTextBox = new System.Windows.Forms.TextBox();
            this.OutputImageFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.ProfileComboBox = new System.Windows.Forms.ComboBox();
            this.DebugCheckBox = new System.Windows.Forms.CheckBox();
            this.TestAllButton = new System.Windows.Forms.Button();
            this.TestFolderButton = new System.Windows.Forms.Button();
            this.FolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.ProfileLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // OpenButton
            // 
            this.OpenButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OpenButton.Location = new System.Drawing.Point(12, 872);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(75, 23);
            this.OpenButton.TabIndex = 8;
            this.OpenButton.Text = "Open";
            this.OpenButton.UseVisualStyleBackColor = true;
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "openFileDialog1";
            // 
            // LogTextBox
            // 
            this.LogTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogTextBox.Location = new System.Drawing.Point(12, 752);
            this.LogTextBox.Multiline = true;
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogTextBox.Size = new System.Drawing.Size(1242, 114);
            this.LogTextBox.TabIndex = 10;
            // 
            // OutputImageFlowLayoutPanel
            // 
            this.OutputImageFlowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputImageFlowLayoutPanel.AutoScroll = true;
            this.OutputImageFlowLayoutPanel.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.OutputImageFlowLayoutPanel.Location = new System.Drawing.Point(12, 12);
            this.OutputImageFlowLayoutPanel.Name = "OutputImageFlowLayoutPanel";
            this.OutputImageFlowLayoutPanel.Size = new System.Drawing.Size(1242, 707);
            this.OutputImageFlowLayoutPanel.TabIndex = 11;
            // 
            // ProfileComboBox
            // 
            this.ProfileComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ProfileComboBox.DisplayMember = "Name";
            this.ProfileComboBox.FormattingEnabled = true;
            this.ProfileComboBox.Location = new System.Drawing.Point(53, 725);
            this.ProfileComboBox.Name = "ProfileComboBox";
            this.ProfileComboBox.Size = new System.Drawing.Size(121, 21);
            this.ProfileComboBox.TabIndex = 12;
            // 
            // DebugCheckBox
            // 
            this.DebugCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DebugCheckBox.AutoSize = true;
            this.DebugCheckBox.Checked = true;
            this.DebugCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DebugCheckBox.Location = new System.Drawing.Point(1196, 727);
            this.DebugCheckBox.Name = "DebugCheckBox";
            this.DebugCheckBox.Size = new System.Drawing.Size(58, 17);
            this.DebugCheckBox.TabIndex = 13;
            this.DebugCheckBox.Text = "Debug";
            this.DebugCheckBox.UseVisualStyleBackColor = true;
            // 
            // TestAllButton
            // 
            this.TestAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TestAllButton.Location = new System.Drawing.Point(93, 872);
            this.TestAllButton.Name = "TestAllButton";
            this.TestAllButton.Size = new System.Drawing.Size(75, 23);
            this.TestAllButton.TabIndex = 14;
            this.TestAllButton.Text = "Test all";
            this.TestAllButton.UseVisualStyleBackColor = true;
            this.TestAllButton.Click += new System.EventHandler(this.TestAllButton_Click);
            // 
            // TestFolderButton
            // 
            this.TestFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TestFolderButton.Location = new System.Drawing.Point(174, 872);
            this.TestFolderButton.Name = "TestFolderButton";
            this.TestFolderButton.Size = new System.Drawing.Size(75, 23);
            this.TestFolderButton.TabIndex = 15;
            this.TestFolderButton.Text = "Test folder";
            this.TestFolderButton.UseVisualStyleBackColor = true;
            this.TestFolderButton.Click += new System.EventHandler(this.TestFolderButton_Click);
            // 
            // ProfileLabel
            // 
            this.ProfileLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ProfileLabel.AutoSize = true;
            this.ProfileLabel.Location = new System.Drawing.Point(12, 729);
            this.ProfileLabel.Name = "ProfileLabel";
            this.ProfileLabel.Size = new System.Drawing.Size(36, 13);
            this.ProfileLabel.TabIndex = 16;
            this.ProfileLabel.Text = "Profile";
            // 
            // GaugeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1266, 906);
            this.Controls.Add(this.ProfileLabel);
            this.Controls.Add(this.TestFolderButton);
            this.Controls.Add(this.TestAllButton);
            this.Controls.Add(this.DebugCheckBox);
            this.Controls.Add(this.ProfileComboBox);
            this.Controls.Add(this.OutputImageFlowLayoutPanel);
            this.Controls.Add(this.LogTextBox);
            this.Controls.Add(this.OpenButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GaugeForm";
            this.Text = "GaugeReader";
            this.Load += new System.EventHandler(this.GaugeForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button OpenButton;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.TextBox LogTextBox;
        private System.Windows.Forms.FlowLayoutPanel OutputImageFlowLayoutPanel;
        private System.Windows.Forms.ComboBox ProfileComboBox;
        private System.Windows.Forms.CheckBox DebugCheckBox;
        private System.Windows.Forms.Button TestAllButton;
        private System.Windows.Forms.Button TestFolderButton;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog;
        private System.Windows.Forms.Label ProfileLabel;
    }
}

