using MainBoard;
using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

class MainBoardApp
{
    private ConcurrentDictionary<string, GameConsoleInputs> oldGameConsoleInfos;

    private ConcurrentDictionary<string, GameConsoleInputs> newGameConsoleInfos;

    public MainBoardApp()
    {
        oldGameConsoleInfos = CreateInstanceFromConsoleInfo();
        newGameConsoleInfos = CreateInstanceFromConsoleInfo();
    }

    public async Task StartAsync()
    {
        try
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 8080);
            listener.Start();

            Console.WriteLine("Main Board is listening for connections...");

            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                _ = Task.Run(() => HandleClientAsync(client));
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Something went wrong, Try again later");
        }
    }

    private async Task HandleClientAsync(TcpClient client)
    {
        try
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];

            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string consoleType = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            if (consoleType.Contains("New"))
            {
                await HandleNewConsoleAsync(stream);
            }
            else
            {
                await HandleOldConsoleAsync();
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Something went wrong, Try again later");
        }
    }

    private async Task HandleNewConsoleAsync(NetworkStream stream)
    {
        try
        {
            byte[] buffer = new byte[1024];

            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string jsonConsoleData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            var gameConsoleInputs = JsonSerializer.Deserialize<GameConsoleInputs>(jsonConsoleData);

            if (gameConsoleInputs is not null)
            {
                newGameConsoleInfos.AddOrUpdate(gameConsoleInputs.ConsoleCode,
                    gameConsoleInputs,
                    (key, existingInfo) => gameConsoleInputs);

                while (true)
                {
                    bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    string jsonScoreData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    var scoreData = JsonSerializer.Deserialize<ScoreData>(jsonScoreData);
                    if (scoreData != null)
                    {
                        gameConsoleInputs.Score = scoreData.Score;
                        gameConsoleInputs.Status = scoreData.Status;
                    }

                    PrintInformation(gameConsoleInputs);
                }
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Something went wrong, Try again later");
        }
    }

    private async Task HandleOldConsoleAsync()
    {
        string filePath = JsonFile.GetFilePath();

        while (true)
        {
            var data = await LoadScores(filePath);
            // Periodically check for updates from old consoles
            await Task.Delay(5000);

            foreach (var gameConsoleInput in data)
            {
                PrintInformation(gameConsoleInput);
            }
        }
    }

    #region Main Method
    public static async Task Main()
    {
        try
        {
            JsonFile.RemoveJsonFile();
            MainBoardApp mainBoard = new MainBoardApp();
            await mainBoard.StartAsync();
        }
        catch (Exception)
        {
            Console.WriteLine("Something went wrong, Try again later");
        }
    }
    #endregion

    #region Helper Methods

    static async Task<List<GameConsoleInputs>> LoadScores(string filePath)
    {
        string json = await File.ReadAllTextAsync(filePath);
        if (string.IsNullOrEmpty(json))
            return new List<GameConsoleInputs>();

        return JsonSerializer.Deserialize<List<GameConsoleInputs>>(json) ?? new List<GameConsoleInputs>();
    }

    private void PrintInformation(GameConsoleInputs gameConsoleInputs)
    {
        Console.WriteLine($"Updated information for {gameConsoleInputs.GamerName}: " +
                    $"Name={gameConsoleInputs.GamerName}," +
                    $" Number={gameConsoleInputs.ConsoleNumber}, " +
                    $"Code={gameConsoleInputs.ConsoleCode}, " +
                    $"Score={gameConsoleInputs.Score}, " +
                    $"Status={GetStatusForCurrentProcess(gameConsoleInputs.Status)}");
    }

    private string GetStatusForCurrentProcess(string status)
    {
        return status == "start" ? "Running" : "Stopped";
    }
    private ConcurrentDictionary<string, GameConsoleInputs> CreateInstanceFromConsoleInfo()
    {
        return new ConcurrentDictionary<string, GameConsoleInputs>();
    }

    #endregion



}