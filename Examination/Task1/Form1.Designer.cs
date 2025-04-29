namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.TextBox txtSavePath;
        private System.Windows.Forms.TextBox txtTags;
        private System.Windows.Forms.TextBox txtThreads;
        private System.Windows.Forms.Button btnStartDownload;
        private System.Windows.Forms.Button btnCancelDownload;
        private System.Windows.Forms.Button btnPauseDownload;
        private System.Windows.Forms.Button btnResumeDownload;
        private System.Windows.Forms.Button btnDeleteDownload;
        private System.Windows.Forms.Button btnSearchByTag;
        private System.Windows.Forms.DataGridView dgvDownloads;

        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.Label lblSavePath;
        private System.Windows.Forms.Label lblThreads;
        private System.Windows.Forms.Label lblTags;

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

        private void InitializeComponent()
        {
            txtUrl = new TextBox();
            txtSavePath = new TextBox();
            txtTags = new TextBox();
            txtThreads = new TextBox();
            btnStartDownload = new Button();
            btnCancelDownload = new Button();
            btnPauseDownload = new Button();
            btnResumeDownload = new Button();
            btnDeleteDownload = new Button();
            btnSearchByTag = new Button();
            dgvDownloads = new DataGridView();
            lblUrl = new Label();
            lblSavePath = new Label();
            lblThreads = new Label();
            lblTags = new Label();
            Url = new DataGridViewTextBoxColumn();
            SavePath = new DataGridViewTextBoxColumn();
            Status = new DataGridViewTextBoxColumn();
            Tags = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvDownloads).BeginInit();
            SuspendLayout();
            // 
            // txtUrl
            // 
            txtUrl.Location = new Point(110, 20);
            txtUrl.Name = "txtUrl";
            txtUrl.Size = new Size(654, 27);
            txtUrl.TabIndex = 0;
            // 
            // txtSavePath
            // 
            txtSavePath.Location = new Point(110, 60);
            txtSavePath.Name = "txtSavePath";
            txtSavePath.Size = new Size(654, 27);
            txtSavePath.TabIndex = 1;
            // 
            // txtTags
            // 
            txtTags.Location = new Point(110, 100);
            txtTags.Name = "txtTags";
            txtTags.Size = new Size(515, 27);
            txtTags.TabIndex = 2;
            // 
            // txtThreads
            // 
            txtThreads.Location = new Point(110, 140);
            txtThreads.Name = "txtThreads";
            txtThreads.Size = new Size(100, 27);
            txtThreads.TabIndex = 3;
            // 
            // btnStartDownload
            // 
            btnStartDownload.Location = new Point(10, 182);
            btnStartDownload.Name = "btnStartDownload";
            btnStartDownload.Size = new Size(200, 30);
            btnStartDownload.TabIndex = 4;
            btnStartDownload.Text = "Начать загрузку";
            btnStartDownload.Click += btnStartDownload_Click;
            // 
            // btnCancelDownload
            // 
            btnCancelDownload.Location = new Point(10, 242);
            btnCancelDownload.Name = "btnCancelDownload";
            btnCancelDownload.Size = new Size(184, 30);
            btnCancelDownload.TabIndex = 5;
            btnCancelDownload.Text = "Отменить загрузку";
            btnCancelDownload.Click += btnCancelDownload_Click;
            // 
            // btnPauseDownload
            // 
            btnPauseDownload.Location = new Point(580, 242);
            btnPauseDownload.Name = "btnPauseDownload";
            btnPauseDownload.Size = new Size(184, 30);
            btnPauseDownload.TabIndex = 6;
            btnPauseDownload.Text = "Пауза";
            btnPauseDownload.Click += btnPauseDownload_Click;
            // 
            // btnResumeDownload
            // 
            btnResumeDownload.Location = new Point(390, 242);
            btnResumeDownload.Name = "btnResumeDownload";
            btnResumeDownload.Size = new Size(184, 30);
            btnResumeDownload.TabIndex = 7;
            btnResumeDownload.Text = "Возобновить";
            btnResumeDownload.Click += btnResumeDownload_Click;
            // 
            // btnDeleteDownload
            // 
            btnDeleteDownload.Location = new Point(200, 242);
            btnDeleteDownload.Name = "btnDeleteDownload";
            btnDeleteDownload.Size = new Size(184, 30);
            btnDeleteDownload.TabIndex = 8;
            btnDeleteDownload.Text = "Удалить";
            btnDeleteDownload.Click += btnDeleteDownload_Click;
            // 
            // btnSearchByTag
            // 
            btnSearchByTag.BackColor = SystemColors.Control;
            btnSearchByTag.Location = new Point(631, 100);
            btnSearchByTag.Name = "btnSearchByTag";
            btnSearchByTag.Size = new Size(133, 27);
            btnSearchByTag.TabIndex = 9;
            btnSearchByTag.Text = "Поиск по тегу";
            btnSearchByTag.UseVisualStyleBackColor = false;
            btnSearchByTag.Click += btnSearchByTag_Click;
            // 
            // dgvDownloads
            // 
            dgvDownloads.BackgroundColor = SystemColors.Control;
            dgvDownloads.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDownloads.Columns.AddRange(new DataGridViewColumn[] { Url, SavePath, Status, Tags });
            dgvDownloads.Location = new Point(10, 278);
            dgvDownloads.Name = "dgvDownloads";
            dgvDownloads.RowHeadersWidth = 51;
            dgvDownloads.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDownloads.Size = new Size(760, 220);
            dgvDownloads.TabIndex = 10;
            // 
            // lblUrl
            // 
            lblUrl.Location = new Point(10, 20);
            lblUrl.Name = "lblUrl";
            lblUrl.Size = new Size(100, 23);
            lblUrl.TabIndex = 11;
            lblUrl.Text = "URL:";
            // 
            // lblSavePath
            // 
            lblSavePath.Location = new Point(10, 60);
            lblSavePath.Name = "lblSavePath";
            lblSavePath.Size = new Size(100, 23);
            lblSavePath.TabIndex = 12;
            lblSavePath.Text = "Сохранить в:";
            // 
            // lblThreads
            // 
            lblThreads.Location = new Point(10, 140);
            lblThreads.Name = "lblThreads";
            lblThreads.Size = new Size(100, 23);
            lblThreads.TabIndex = 13;
            lblThreads.Text = "Потоки:";
            // 
            // lblTags
            // 
            lblTags.Location = new Point(10, 100);
            lblTags.Name = "lblTags";
            lblTags.Size = new Size(100, 23);
            lblTags.TabIndex = 14;
            lblTags.Text = "Теги:";
            // 
            // Url
            // 
            Url.HeaderText = "Url";
            Url.MinimumWidth = 6;
            Url.Name = "Url";
            Url.Width = 125;
            // 
            // SavePath
            // 
            SavePath.HeaderText = "Путь";
            SavePath.MinimumWidth = 6;
            SavePath.Name = "SavePath";
            SavePath.Width = 125;
            // 
            // Status
            // 
            Status.HeaderText = "Статус";
            Status.MinimumWidth = 6;
            Status.Name = "Status";
            Status.Width = 125;
            // 
            // Tags
            // 
            Tags.HeaderText = "Теги";
            Tags.MinimumWidth = 6;
            Tags.Name = "Tags";
            Tags.Width = 125;
            // 
            // Form1
            // 
            ClientSize = new Size(784, 561);
            Controls.Add(txtUrl);
            Controls.Add(txtSavePath);
            Controls.Add(txtTags);
            Controls.Add(txtThreads);
            Controls.Add(btnStartDownload);
            Controls.Add(btnCancelDownload);
            Controls.Add(btnPauseDownload);
            Controls.Add(btnResumeDownload);
            Controls.Add(btnDeleteDownload);
            Controls.Add(btnSearchByTag);
            Controls.Add(dgvDownloads);
            Controls.Add(lblUrl);
            Controls.Add(lblSavePath);
            Controls.Add(lblThreads);
            Controls.Add(lblTags);
            Name = "Form1";
            Text = "Загрузчик файлов";
            ((System.ComponentModel.ISupportInitialize)dgvDownloads).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridViewTextBoxColumn Url;
        private DataGridViewTextBoxColumn SavePath;
        private DataGridViewTextBoxColumn Status;
        private DataGridViewTextBoxColumn Tags;
    }
}