
namespace GameConsole;
public class GameStopProcessForOldConsole : GameProcess
{
    public GameStopProcessForOldConsole(int score, string gameStatus) : base(score, gameStatus)
    { }

    public async Task SaveProcessInJsonFormatWhenStopProcess()
    {
        try
        {
            string filePath = GetFilePath();

            var scores = await LoadScores(filePath);

            // Check if the player already exists in the scores
            var existingScore = scores
                .FirstOrDefault(s => s.GamerName == GameConsoleInfo.GamerName &&
                s.ConsoleCode == GameConsoleInfo.ConsoleCode &&
                s.ConsoleNumber == GameConsoleInfo.ConsoleNumber);

            if (existingScore != null)
            {
                // Update the existing score
                existingScore.Score = Score;
                existingScore.Status = GameStatus;
            }
            else
            {
                // Add a new score
                scores.Add(new GameConsoleInputs
                {
                    GamerName = GameConsoleInfo.GamerName,
                    ConsoleCode = GameConsoleInfo.ConsoleCode,
                    ConsoleNumber = GameConsoleInfo.ConsoleNumber,
                    Status = GameStatus,
                    Score = Score
                });
            }

            await SaveScores(filePath, scores);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
