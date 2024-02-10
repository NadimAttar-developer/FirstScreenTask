using System.Text.Json;

namespace GameConsole;
public abstract class GameProcess
{
    protected int Score { get; set; }
    protected string GameStatus { get; set; }

    public GameProcess(int score, string gameStatus)
    {
        Score = score;
        GameStatus = gameStatus;
    }

    public async Task<List<GameConsoleInputs>> LoadScores(string filePath)
    {
        string json = await File.ReadAllTextAsync(filePath);
        if (string.IsNullOrEmpty(json))
            return new List<GameConsoleInputs>();

        return JsonSerializer.Deserialize<List<GameConsoleInputs>>(json) ?? new List<GameConsoleInputs>();
    }
    public async Task SaveScores(string filePath, List<GameConsoleInputs> scores)
    {
        string json = JsonSerializer.Serialize(scores);
        await File.WriteAllTextAsync(filePath, json);
    }
    public string GetFilePath()
    {
        string fileName = "scores.json";
        return Path.Combine(GetOrCreateSharedDirectory(), fileName);
    }
    public string GetOrCreateSharedDirectory()
    {
        string sharedDirectory = @"C:\SharedDirectory"; // Replace with your desired directory path

        // Create the directory if it doesn't exist
        if (!Directory.Exists(sharedDirectory))
        {
            Directory.CreateDirectory(sharedDirectory);
        }

        return sharedDirectory;
    }
}
