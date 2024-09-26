namespace Learning_FileHandling.FileProgram;

public static class PlayingWithFilesProgram
{
    public static void PlayingWithFilesProgramMain()
    {
        PlayingWithFiles playingWithFiles = new();
        playingWithFiles.FileMethod();
    }
}

public class PlayingWithFiles
{
    public void FileMethod()
    {
        int count = 0;
        try
        {
            WriteHeadingToScreen();
            Console.WriteLine("Please enter a name for your life");
            string fileName = Console.ReadLine()!;
            Console.WriteLine();
            string[] line = new string[3];
            TextFileFunctions text = new TextFileFunctions(fileName);
            do
            {
                Console.WriteLine($"Please add {(count > 0 ? "another line" : "a line")} to file '{fileName}'. ");
                line[count] = Console.ReadLine()!;
                count++;
            } while (count < 3);

            text.WriteTextToFile(line);

            Console.Clear();

            WriteHeadingToScreen();

            Console.WriteLine(text.ReadFile());
        }
        catch (IOException ex)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
    }

    private void WriteHeadingToScreen()
    {
        Console.WriteLine("Basic .Net Cross Platform File Handling");
        Console.WriteLine(new string('-',40));
        Console.WriteLine();
    }
}
