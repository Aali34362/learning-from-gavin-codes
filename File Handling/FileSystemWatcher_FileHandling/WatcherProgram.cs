namespace FileSystemWatcher_FileHandling;

public static class WatcherProgram
{
    private static readonly string _watcherInputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WatchInput");
    private static readonly string _watcherOutputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HtmlOutput");

    public static void WatcherProgramMain()
    {
        InitializeApplication();

        RunFolderWatcher(_watcherInputPath);
    }

    #region Directory Check
    private static void InitializeApplication()
    {
        EnsureDirectoryExists(_watcherInputPath);
        EnsureDirectoryExists(_watcherOutputPath);
    }

    private static void EnsureDirectoryExists(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
    #endregion

    #region Watcher
    private static void RunFolderWatcher(string directoryPath)
    {

        using (FileSystemWatcher watcher = new FileSystemWatcher())
        {
            watcher.Path = directoryPath;

            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            watcher.Filter = "*.txt";

            watcher.Created += OnChanged;

            watcher.EnableRaisingEvents = true;

            Console.WriteLine("Press 'q' to quit the application");

            while (Console.Read() != 'q') ;
        }

    }
    #endregion

    #region Update
    private static void OnChanged(object sender, FileSystemEventArgs e)
    {
        Directory.Move(e.FullPath, Path.Combine(_watcherOutputPath, e.Name!));

        UpdateHtmlOutputFiles();
    }

    private static void UpdateHtmlOutputFiles()
    {
        UpdateHtmlFileWithTextContent("Accordion.html", "Accordion", PageLayout.Accordion);
        UpdateHtmlFileWithTextContent("Paragraph.html", "Paragraph", PageLayout.Paragraph);
    }

    private static void UpdateHtmlFileWithTextContent(string htmlFileName, string pageHeading, PageLayout pageLayout)
    {
        string content = String.Empty;

        DirectoryInfo directoryInfo = new DirectoryInfo(_watcherOutputPath);
        FileInfo[] files = directoryInfo.GetFiles("*.txt");

        using (StreamWriter sw = new StreamWriter(Path.Combine(_watcherOutputPath, htmlFileName)))
        {
            AddTopHtml(sw, pageHeading, pageLayout);

            foreach (FileInfo file in files)
            {
                using (StreamReader sr = new StreamReader(file.OpenRead()))
                {
                    content = sr.ReadToEnd();
                }

                BuildHtmlBody(sw, content, Path.GetFileNameWithoutExtension(file.FullName));
            }
            AddBottomHtml(sw);
        }
    }
    #endregion

    #region Html
    private static void AddTopHtml(StreamWriter sw, string pageHeading, PageLayout pageLayout)
    {
        sw.WriteLine("<!doctype html>");
        sw.WriteLine(@"<html lang = ""en"">");
        sw.WriteLine("<head>");
        sw.WriteLine(@"<meta charset = ""utf-8"">");
        sw.WriteLine(@"<meta name = ""viewport"" content = ""width=device-width,intial-scale=1"">");
        sw.WriteLine($"<title>{pageHeading}</title>");


        if (pageLayout == PageLayout.Accordion)
        {
            sw.WriteLine(@"<link rel = ""stylesheet"" href = ""https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css"" >");
            sw.WriteLine(@"<script src = ""https://code.jquery.com/jquery-1.12.4.js""></script>");
            sw.WriteLine(@"<script src = ""https://code.jquery.com/ui/1.12.1/jquery-ui.js""></script>");

            sw.WriteLine("<script>");
            sw.WriteLine("$(function(){");
            sw.WriteLine(@"$(""#accordion"").accordion();");
            sw.WriteLine("});");
            sw.WriteLine("</script>");
            sw.WriteLine("</head>");
            sw.WriteLine("<body>");
            sw.WriteLine($"<h1>{pageHeading}</h1> ");
            sw.WriteLine(@"<div id = ""accordion"">");
        }
        else if (pageLayout == PageLayout.Paragraph)
        {
            sw.WriteLine($"<h1>{pageHeading}</h1>");
            sw.WriteLine("<div>");
            sw.WriteLine("</head>");
            sw.WriteLine("<body>");
        }
    }

    private static void BuildHtmlBody(StreamWriter sw, string topicContent, string topicHeading)
    {
        sw.WriteLine($"<h3>{topicHeading}</h3>");
        sw.WriteLine("<div>");
        sw.WriteLine("<p>");
        sw.Write(topicContent);
        sw.WriteLine("</p>");
        sw.WriteLine("</div>");
    }

    private static void AddBottomHtml(StreamWriter sw)
    {
        sw.WriteLine("</div>");
        sw.WriteLine("</body>");
        sw.WriteLine("</html>");
    }
    #endregion
}
