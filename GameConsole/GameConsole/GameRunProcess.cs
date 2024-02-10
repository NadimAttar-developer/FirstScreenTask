
using System.Net.Sockets;
using System.Text.Json;
using System.Text;

namespace GameConsole;
public class GameRunProcess : GameProcess
{
    public GameRunProcess(int score, string gameStatus) : base(score, gameStatus)
    { }

    public async Task SendRequestForRunningProcess(
      NetworkStream stream, string data)
    {
        try
        {
            if (data.ToLower().Equals(" "))
            {
                var scoreData = new { Score = Score, Status = GameStatus };
                string scoreJson = JsonSerializer.Serialize(scoreData);
                byte[] scoreBytes = Encoding.UTF8.GetBytes(scoreJson);
                await stream.WriteAsync(scoreBytes, 0, scoreBytes.Length);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}
