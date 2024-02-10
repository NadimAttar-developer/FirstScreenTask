
namespace GameConsole;
public static class GameConsoleInfo
{
    public static string GamerName { get; set; }
    public static string ConsoleNumber { get; set; }
    public static string ConsoleCode { get; set; }
    public static string ConsoleType { get; set; }

    public static void EnterInforamtionFromConsole()
    {
        try
        {
            GamerName = ReadConsoleName();

            ConsoleNumber = ReadConsoleNumber();

            ConsoleCode = ReadConsoleCode();

            ConsoleType = ReadConsoleType();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static string ReadConsoleName()
    {
        Console.Write("Enter console name: ");
        string gamerName = Console.ReadLine();

        if (gamerName == null)
            return string.Empty;

        return gamerName;
    }
    private static string ReadConsoleNumber()
    {
        Console.Write("Enter console number: ");
        return Console.ReadLine();
    }
    private static string ReadConsoleCode()
    {
        Console.Write("Enter console code: ");
        return Console.ReadLine();
    }
    private static string ReadConsoleType()
    {
        Console.Write("Enter console type (note enter n for new console OR o for old console)");
        string consoleType = Console.ReadLine();
        if (consoleType != null && consoleType.ToLower() == "n")
        {
            return "New";
        }

        if (consoleType != null && consoleType.ToLower() == "o")
        {
            return "Old";
        }

        return "New"; // The default type is New Console
    }

}
