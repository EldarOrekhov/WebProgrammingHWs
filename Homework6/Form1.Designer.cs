namespace WinFormsApp1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cmbMode;
        private System.Windows.Forms.Button btnRoll;
        private System.Windows.Forms.Button btnPlayer1Roll;
        private System.Windows.Forms.Button btnPlayer2Roll;
        private System.Windows.Forms.Label lblPlayer1;
        private System.Windows.Forms.Label lblPlayer2;
        private System.Windows.Forms.Label lblResult;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            cmbMode = new ComboBox();
            btnRoll = new Button();
            btnPlayer1Roll = new Button();
            btnPlayer2Roll = new Button();
            lblPlayer1 = new Label();
            lblPlayer2 = new Label();
            lblResult = new Label();
            SuspendLayout();
            // 
            // cmbMode
            // 
            cmbMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMode.FormattingEnabled = true;
            cmbMode.Location = new Point(30, 20);
            cmbMode.Name = "cmbMode";
            cmbMode.Size = new Size(250, 28);
            cmbMode.TabIndex = 0;
            cmbMode.SelectedIndexChanged += cmbMode_SelectedIndexChanged;
            // 
            // btnRoll
            // 
            btnRoll.Font = new Font("Microsoft Sans Serif", 9F);
            btnRoll.Location = new Point(300, 20);
            btnRoll.Name = "btnRoll";
            btnRoll.Size = new Size(120, 30);
            btnRoll.TabIndex = 1;
            btnRoll.Text = "Бросить";
            btnRoll.UseVisualStyleBackColor = true;
            btnRoll.Click += btnRoll_Click;
            // 
            // btnPlayer1Roll
            // 
            btnPlayer1Roll.Font = new Font("Microsoft Sans Serif", 9F);
            btnPlayer1Roll.Location = new Point(30, 60);
            btnPlayer1Roll.Name = "btnPlayer1Roll";
            btnPlayer1Roll.Size = new Size(150, 30);
            btnPlayer1Roll.TabIndex = 2;
            btnPlayer1Roll.Text = "Игрок 1";
            btnPlayer1Roll.UseVisualStyleBackColor = true;
            btnPlayer1Roll.Click += btnPlayer1Roll_Click;
            // 
            // btnPlayer2Roll
            // 
            btnPlayer2Roll.Font = new Font("Microsoft Sans Serif", 9F);
            btnPlayer2Roll.Location = new Point(270, 60);
            btnPlayer2Roll.Name = "btnPlayer2Roll";
            btnPlayer2Roll.Size = new Size(150, 30);
            btnPlayer2Roll.TabIndex = 3;
            btnPlayer2Roll.Text = "Игрок 2 ";
            btnPlayer2Roll.UseVisualStyleBackColor = true;
            btnPlayer2Roll.Click += btnPlayer2Roll_Click;
            // 
            // lblPlayer1
            // 
            lblPlayer1.Font = new Font("Microsoft Sans Serif", 36F, FontStyle.Bold);
            lblPlayer1.Location = new Point(30, 110);
            lblPlayer1.Name = "lblPlayer1";
            lblPlayer1.Size = new Size(150, 80);
            lblPlayer1.TabIndex = 4;
            lblPlayer1.Text = "0";
            lblPlayer1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblPlayer2
            // 
            lblPlayer2.Font = new Font("Microsoft Sans Serif", 36F, FontStyle.Bold);
            lblPlayer2.Location = new Point(270, 110);
            lblPlayer2.Name = "lblPlayer2";
            lblPlayer2.Size = new Size(150, 80);
            lblPlayer2.TabIndex = 5;
            lblPlayer2.Text = "0";
            lblPlayer2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblResult
            // 
            lblResult.Font = new Font("Microsoft Sans Serif", 12F);
            lblResult.Location = new Point(30, 210);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(390, 80);
            lblResult.TabIndex = 6;
            lblResult.TextAlign = ContentAlignment.TopCenter;
            // 
            // Form1
            // 
            ClientSize = new Size(454, 311);
            Controls.Add(lblResult);
            Controls.Add(lblPlayer2);
            Controls.Add(lblPlayer1);
            Controls.Add(btnPlayer2Roll);
            Controls.Add(btnPlayer1Roll);
            Controls.Add(btnRoll);
            Controls.Add(cmbMode);
            Name = "Form1";
            Text = "Игра в кости";
            ResumeLayout(false);
        }
    }
}
