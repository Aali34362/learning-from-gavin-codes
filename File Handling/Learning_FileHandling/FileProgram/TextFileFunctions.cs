namespace Learning_FileHandling.FileProgram;

public class TextFileFunctions
{
    private readonly string _rootPath = AppDomain.CurrentDomain.BaseDirectory;

    private string _fileName = "TextFile.txt";

    public TextFileFunctions(string fileName)
    {
        _fileName = fileName;
    }

    public void WriteTextToFile(string[] lines)
    {
        using (StreamWriter outputFile = new StreamWriter(Path.Combine(_rootPath, _fileName), true))
        {
            foreach (string line in lines) 
            {
                outputFile.WriteLine(line);
            }
        };
    }

    ////public string ReadFile()
    ////{
    ////    using (StreamReader outputFile = new StreamReader(Path.Combine(_rootPath, _fileName)))
    ////    {
    ////        return outputFile.ReadToEnd();
    ////    };
    ////}
    
    public string ReadFile() => new StreamReader(Path.Combine(_rootPath, _fileName)).ReadToEnd()!;
}
