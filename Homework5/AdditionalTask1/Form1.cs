using System.Net;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string url = "https://www.gutenberg.org/cache/epub/1524/pg1524.txt";

            using (WebClient client = new WebClient())
            {
                try
                {
                    string hamletText = client.DownloadString(url);
                    txtHamlet.Text = hamletText;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке текста: " + ex.Message);
                }
            }
        }
    }
}