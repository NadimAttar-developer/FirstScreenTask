using GameConsole;
using System.Net.Sockets;
using System.Text.Json;
using System.Text;

class NewGameConsoleApp
{
    public static int Score { get; set; }
    public static string GameStatus { get; set; }
    public static async Task Main()
    {
        GameConsoleInfo.EnterInforamtionFromConsole();

        TcpClient client = new TcpClient("127.0.0.1", 8080);
        NetworkStream stream = client.GetStream();

        // Send the type of console
        byte[] consoleType = Encoding.UTF8.GetBytes(GameConsoleInfo.ConsoleType);
        await stream.WriteAsync(consoleType, 0, consoleType.Length);

        // Send console data in JSON format
        var consoleData = new GameConsoleInputs
        {
            GamerName = GameConsoleInfo.GamerName,
            ConsoleNumber = GameConsoleInfo.ConsoleNumber,
            ConsoleCode = GameConsoleInfo.ConsoleCode,
            Status = "",
            Score = 0
        };

        string jsonData = JsonSerializer.Serialize(consoleData);

        byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonData);
        await stream.WriteAsync(jsonBytes, 0, jsonBytes.Length);

        await GameConsoleProcess.RunTheProcessOfTheGame(stream, GameConsoleInfo.ConsoleType);
    }
}