
namespace MainBoard;
public static class JsonFile
{
    public static string GetSharedDirectory()
    {
        try
        {
            // Specify the shared directory path
            var sharedDirectory = @"C:\SharedDirectory";

            if (!Directory.Exists(sharedDirectory))
            {
                Directory.CreateDirectory(sharedDirectory);
            }

            return sharedDirectory;
        }
        catch (Exception)
        {
            throw;
        }

    }

    public static string GetFilePath()
    {
        try
        {
            string fileName = "scores.json";
            var filePath = Path.Combine(GetSharedDirectory(), fileName);

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "");
            }

            return filePath;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static void RemoveJsonFile()
    {
        try
        {
            string fileName = "scores.json";
            var filePath = Path.Combine(GetSharedDirectory(), fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

}
