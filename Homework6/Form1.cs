using System.Text;
using System.Text.Json;

namespace WinFormsApp1

{
    public partial class Form1 : Form
    {
        private string apiKey = "b7c5ec4a-84dc-443e-8fbf-c88d093cf7be";
        private readonly HttpClient httpClient = new HttpClient();
        private Random localRandom = new Random();

        private int? player1Value = null;
        private int? player2Value = null;

        public Form1()
        {
            InitializeComponent();

            cmbMode.Items.Add("Человек против Человека");
            cmbMode.Items.Add("Человек против Компьютера");
            cmbMode.SelectedIndex = 0;
        }

        private async void btnRoll_Click(object sender, EventArgs e)
        {
            btnRoll.Enabled = false;
            lblResult.Text = "Бросаем кости";

            for (int i = 0; i < 10; i++)
            {
                lblPlayer1.Text = localRandom.Next(1, 7).ToString();
                lblPlayer2.Text = localRandom.Next(1, 7).ToString();
                await Task.Delay(100);
            }

            int playerRoll = await GetDiceRollAsync();
            int computerRoll = await GetDiceRollAsync();

            lblPlayer1.Text = playerRoll.ToString();
            lblPlayer2.Text = computerRoll.ToString();

            string result = $"Вы: {playerRoll}, Компьютер: {computerRoll}";
            if (playerRoll > computerRoll)
                result += "\nВы победили";
            else if (playerRoll < computerRoll)
                result += "\nКомпьютер победил";
            else
                result += "\nНичья";

            lblResult.Text = result;
            btnRoll.Enabled = true;
        }

        private async void btnPlayer1Roll_Click(object sender, EventArgs e)
        {
            btnPlayer1Roll.Enabled = false;
            lblResult.Text = "Игрок 1 бросает...";
            for (int i = 0; i < 10; i++)
            {
                lblPlayer1.Text = localRandom.Next(1, 7).ToString();
                await Task.Delay(100);
            }

            player1Value = await GetDiceRollAsync();
            lblPlayer1.Text = player1Value.ToString();
            lblResult.Text = "Ход игрока 2";
            btnPlayer2Roll.Enabled = true;
        }

        private async void btnPlayer2Roll_Click(object sender, EventArgs e)
        {
            btnPlayer2Roll.Enabled = false;
            lblResult.Text = "Игрок 2 бросает...";
            for (int i = 0; i < 10; i++)
            {
                lblPlayer2.Text = localRandom.Next(1, 7).ToString();
                await Task.Delay(100);
            }

            player2Value = await GetDiceRollAsync();
            lblPlayer2.Text = player2Value.ToString();

            string result = $"Игрок 1: {player1Value}, Игрок 2: {player2Value}";
            if (player1Value > player2Value)
                result += "\nПобедил Игрок 1";
            else if (player1Value < player2Value)
                result += "\nПобедил Игрок 2";
            else
                result += "\nНичья";

            lblResult.Text = result;
            btnPlayer1Roll.Enabled = true;
            player1Value = null;
            player2Value = null;
        }

        private async Task<int> GetDiceRollAsync()
        {
            var requestData = new
            {
                jsonrpc = "2.0",
                method = "generateIntegers",
                @params = new
                {
                    apiKey = apiKey,
                    n = 1,
                    min = 1,
                    max = 6
                },
                id = 0
            };

            var content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://api.random.org/json-rpc/4/invoke", content);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            return doc.RootElement.GetProperty("result").GetProperty("random").GetProperty("data")[0].GetInt32();
        }

        private void cmbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isPvP = cmbMode.SelectedIndex == 0;

            btnRoll.Visible = !isPvP;
            btnPlayer1Roll.Visible = isPvP;
            btnPlayer2Roll.Visible = isPvP;
            btnPlayer1Roll.Enabled = isPvP;
            btnPlayer2Roll.Enabled = false;

            lblPlayer1.Text = "0";
            lblPlayer2.Text = "0";
            lblResult.Text = "";
        }
    }
}
