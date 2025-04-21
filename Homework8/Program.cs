using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleApp1;

class Program
{
    static Dictionary<long, int> userQuestionIndex = new();
    static List<(string Question, string[] Options, int CorrectIndex, string Explanation)> questions = new()
    {
        ("В каком веке начался Ренессанс?", new[] {"XII", "XIV", "XVII", "XIX"}, 1, "Ренессанс начался в XIV веке во Флоренции."),
        ("Кто написал фреску 'Сотворение Адама'?", new[] {"Рафаэль", "Боттичелли", "Микеланджело", "Да Винчи"}, 2, "Фреска была создана Микеланджело в Сикстинской капелле."),
        ("Картина 'Звёздная ночь' принадлежит...", new[] {"Клоду Моне", "Пабло Пикассо", "Винсенту Ван Гогу", "Эдгару Дега"}, 2, "'Звёздная ночь' — работа Ван Гога, написанная в 1889 году.")
    };

    static async Task Main()
    {
        using var cts = new CancellationTokenSource();
        var botClient = new TelegramBotClient("8001071641:AAEwI7LwMGZxRpQzub6B5OL7cBxELDG6clY", cancellationToken: cts.Token);

        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };

        botClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            errorHandler: HandleErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: cts.Token
        );

        var me = await botClient.GetMe();
        Console.WriteLine($"Бот под именем @{me.Username}, запущен.");
        Console.ReadLine();
        cts.Cancel();
    }

    static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message is { Text: { } messageText } message)
        {
            var chatId = message.Chat.Id;
            if (messageText.Equals("/start", StringComparison.OrdinalIgnoreCase))
            {
                userQuestionIndex[chatId] = 0;
                await SendQuestionAsync(botClient, chatId, cancellationToken);
            }
        }
        else if (update.CallbackQuery is { } callbackQuery)
        {
            var chatId = callbackQuery.Message.Chat.Id;
            int selectedIndex = int.Parse(callbackQuery.Data);
            int questionIndex = userQuestionIndex[chatId];
            var (question, options, correctIndex, explanation) = questions[questionIndex];

            string responseText = selectedIndex == correctIndex
                ? "Правильно " + explanation
                : "Неправильно " + explanation;

            await botClient.SendMessage(chatId, responseText, cancellationToken: cancellationToken);

            userQuestionIndex[chatId]++;
            if (userQuestionIndex[chatId] < questions.Count)
                await SendQuestionAsync(botClient, chatId, cancellationToken);
            else
                await botClient.SendMessage(chatId, "Викторина завершена", cancellationToken: cancellationToken);
        }
    }

    static async Task SendQuestionAsync(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
    {
        int index = userQuestionIndex[chatId];
        var (question, options, _, _) = questions[index];

        var buttons = options.Select((opt, i) => InlineKeyboardButton.WithCallbackData(opt, i.ToString())).ToArray();
        var markup = new InlineKeyboardMarkup(buttons.Chunk(2));

        await botClient.SendMessage(
            chatId: chatId,
            text: question,
            replyMarkup: markup,
            cancellationToken: cancellationToken);
    }

    static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };
        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }
}
