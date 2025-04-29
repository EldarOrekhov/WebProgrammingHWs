using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private DownloadManager _downloadManager = new DownloadManager();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStartDownload_Click(object sender, EventArgs e)
        {
            string url = txtUrl.Text;
            string savePath = txtSavePath.Text;
            string tags = txtTags.Text;

            _downloadManager.StartDownload(url, savePath, tags);
            UpdateDownloadGrid();
        }

        private void btnCancelDownload_Click(object sender, EventArgs e)
        {
            if (dgvDownloads.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvDownloads.SelectedRows)
                {
                    string url = row.Cells["Url"].Value.ToString();
                    _downloadManager.CancelDownload(url);
                    row.Cells["Status"].Value = "Отменено";
                }
            }
            else
            {
                MessageBox.Show("Выберите загрузку для отмены");
            }
        }

        private void btnPauseDownload_Click(object sender, EventArgs e)
        {
            if (dgvDownloads.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvDownloads.SelectedRows)
                {
                    string url = row.Cells["Url"].Value.ToString();
                    _downloadManager.PauseDownload(url);
                    row.Cells["Status"].Value = "Приостановлено"; 
                }
            }
        }

        private void btnResumeDownload_Click(object sender, EventArgs e)
        {
            if (dgvDownloads.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvDownloads.SelectedRows)
                {
                    string url = row.Cells["Url"].Value.ToString();
                    _downloadManager.ResumeDownload(url);
                    row.Cells["Status"].Value = "Загрузка";
                }
            }
        }

        private void btnDeleteDownload_Click(object sender, EventArgs e)
        {
            if (dgvDownloads.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvDownloads.SelectedRows)
                {
                    string url = row.Cells["Url"].Value.ToString();
                    _downloadManager.DeleteDownload(url);
                    dgvDownloads.Rows.Remove(row);
                }
            }
        }

        private void btnSearchByTag_Click(object sender, EventArgs e)
        {
            string tag = txtTags.Text;
            var filteredDownloads = _downloadManager.SearchByTag(tag);
            UpdateDownloadGrid(filteredDownloads);
        }

        private void UpdateDownloadGrid(List<DownloadTask> downloads = null)
        {
            dgvDownloads.Rows.Clear();
            var tasks = downloads ?? _downloadManager.GetActiveDownloads();
            foreach (var task in tasks)
            {
                dgvDownloads.Rows.Add(task.Url, task.SavePath, task.Status, task.Tags);
            }
        }
        public class DownloadManager
        {
            private List<DownloadTask> _downloadTasks = new List<DownloadTask>();
            private HttpClient _httpClient = new HttpClient();

            public void StartDownload(string url, string savePath, string tags)
            {
                var downloadTask = new DownloadTask(url, savePath, tags);
                _downloadTasks.Add(downloadTask);
                Task.Run(() => downloadTask.StartDownload(_httpClient));
            }

            public void CancelDownload(string url)
            {
                var task = _downloadTasks.Find(t => t.Url == url);
                if (task != null)
                {
                    task.Cancel();
                }
            }

            public void PauseDownload(string url)
            {
                var task = _downloadTasks.Find(t => t.Url == url);
                if (task != null)
                {
                    task.Pause();
                }
            }

            public void ResumeDownload(string url)
            {
                var task = _downloadTasks.Find(t => t.Url == url);
                if (task != null)
                {
                    task.Resume();
                }
            }

            public void DeleteDownload(string url)
            {
                var task = _downloadTasks.Find(t => t.Url == url);
                if (task != null)
                {
                    _downloadTasks.Remove(task);
                }
            }

            public List<DownloadTask> GetActiveDownloads()
            {
                return _downloadTasks;
            }

            public List<DownloadTask> SearchByTag(string tag)
            {
                return _downloadTasks.Where(t => t.Tags.Contains(tag)).ToList();
            }
        }

        public class DownloadTask
        {
            public string Url { get; }
            public string SavePath { get; }
            private CancellationTokenSource _cts;
            public string Status { get; private set; }
            public string Tags { get; }
            private bool _isPaused;

            public DownloadTask(string url, string savePath, string tags)
            {
                Url = url;
                SavePath = savePath;
                Tags = tags;
                _cts = new CancellationTokenSource();
                Status = "Ожидает";
                _isPaused = false;
            }

            public async Task StartDownload(HttpClient httpClient)
            {
                Status = "Загрузка";
                try
                {
                    var response = await httpClient.GetAsync(Url, _cts.Token);
                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsByteArrayAsync();
                    await File.WriteAllBytesAsync(SavePath, content, _cts.Token);

                    Status = "Завершено";
                }
                catch (OperationCanceledException)
                {
                    Status = _isPaused ? "Приостановлено" : "Отменено";
                }
                catch (Exception)
                {
                    Status = "Ошибка";
                }
            }

            public void Cancel()
            {
                _cts.Cancel();
            }

            // Пауза
            public void Pause()
            {
                if (!_isPaused)
                {
                    _cts.Cancel();
                    _isPaused = true;
                    Status = "Приостановлено";
                }
            }

            public void Resume()
            {
                if (_isPaused)
                {
                    _cts = new CancellationTokenSource();
                    Status = "Загрузка";
                }
            }
        }
    }
}
