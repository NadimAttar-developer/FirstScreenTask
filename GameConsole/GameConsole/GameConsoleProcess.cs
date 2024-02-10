
using System.Net.Sockets;

namespace GameConsole;
public static class GameConsoleProcess
{
    public static int Score { get; set; }
    public static string GameStatus { get; set; }

    public static async Task RunTheProcessOfTheGame(
        NetworkStream stream, string consoleType)
    {
        try
        {
            GameStatus = CheckGameStatusKeyword();

            while (true)
            {
                // Check the game status
                if (GameStatus.Equals("start"))
                {
                    string data = ReadValue();

                    if (!data.ToLower().Equals("x"))
                    {
                        if (consoleType.ToLower().Equals("new"))
                        {
                            var gameRunProcess = new GameRunProcess(++Score, GameStatus);
                            await gameRunProcess.SendRequestForRunningProcess(stream, data);
                        }
                        else
                        {
                            var gameRunProcessForOldConsole = new GameRunProcessForOldConsole(++Score, GameStatus);
                            await gameRunProcessForOldConsole.SaveProcessInJsonFormatWhenRunningProcess(data);
                        }
                    }
                    else
                    {
                        GameStatus = "stop";
                        if (consoleType.ToLower().Equals("new"))
                        {
                            var gameStopProcess = new GameStopProcess(Score, GameStatus);
                            await gameStopProcess.SendRequestToStopProcess(stream);
                        }
                        else
                        {
                            var gameStopProcessForOldConsole = new GameStopProcessForOldConsole(Score, GameStatus);
                            await gameStopProcessForOldConsole.SaveProcessInJsonFormatWhenStopProcess();
                        }

                        Score = 0;
                    }
                }
                else
                {
                    GameStatus = CheckGameStatusKeyword();
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static string CheckGameStatusKeyword()
    {
        string gameStatus = ReadGameStatus();

        if (gameStatus != "start" && gameStatus != "stop")
            return "stop";

        return gameStatus;
    }

    private static string ReadValue()
    {
        Console.Write("Press Space to increase your score Or Press X to stop the process: ");
        return Console.ReadLine();
    }

    private static string ReadGameStatus()
    {
        Console.Write("To start the game enter start: ");
        return Console.ReadLine();
    }
}
