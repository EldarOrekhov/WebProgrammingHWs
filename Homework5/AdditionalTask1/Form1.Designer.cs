namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnLoad = new Button();
            txtHamlet = new TextBox();
            SuspendLayout();
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(189, 12);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(137, 38);
            btnLoad.TabIndex = 0;
            btnLoad.Text = "Load text";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // txtHamlet
            // 
            txtHamlet.Location = new Point(12, 56);
            txtHamlet.Multiline = true;
            txtHamlet.Name = "txtHamlet";
            txtHamlet.ScrollBars = ScrollBars.Vertical;
            txtHamlet.Size = new Size(484, 465);
            txtHamlet.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(508, 533);
            Controls.Add(txtHamlet);
            Controls.Add(btnLoad);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button StartServerButton;
        private Button StopServerButton;
        private Button SendMessageButton;
        private Label label1;
        private TextBox ServerTextBox;
        private TextBox ClientTextBox;
        private Button btnLoad;
        private TextBox txtHamlet;
    }
}
