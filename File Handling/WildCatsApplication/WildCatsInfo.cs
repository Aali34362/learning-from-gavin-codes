using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WildCatsApplication;

public static class WildCatsInfo
{
    public static void WildCatsInfoMain()
    {
        string content = string.Empty;

        string rootPath = AppDomain.CurrentDomain.BaseDirectory;

        try
        {
            DirectoryInfo sourceDirectory = InitializeSourceDirectory(rootPath);

            string htmlOutputFilePath = Path.Combine(rootPath, "WildCats.html");

            FileInfo[] textFiles = sourceDirectory.GetFiles("*.txt");

            using (var htmlFile = new StreamWriter(htmlOutputFilePath))
            {
                //Todo: Write Top Part of Output Html File
                AddTopHtml(htmlFile);

                foreach (FileInfo textFile in textFiles) 
                { 
                    using(StreamReader sr = new StreamReader(textFile.OpenRead()))
                    {
                        content = sr.ReadToEnd();
                    }
                    //Todo: Build Body of HTML File
                    BuildHtmlBody(htmlFile, content, Path.GetFileNameWithoutExtension(textFile.FullName));
                }
                //Todo: Write Bottom Part of Output HTML File
                AddBottomHtml(htmlFile);
            }
            //Todo: Launch Default Browser and Display Output Html file in Browser
            LaunchHtmlFileInBrowser(htmlOutputFilePath);
        }
        catch (UnauthorizedAccessException ex)
        {
            LogExceptionMessageToScreen(ex);
        }
        catch (DirectoryNotFoundException ex)
        {
            LogExceptionMessageToScreen(ex);
        }
        catch (FileNotFoundException ex)
        {
            LogExceptionMessageToScreen(ex);
        }
        catch (IOException ex)
        {
            LogExceptionMessageToScreen(ex);
        }
        catch (NotImplementedException ex)
        {
            LogExceptionMessageToScreen(ex);
        }
    }

    private static void LogExceptionMessageToScreen(Exception ex)
    {
        Console.BackgroundColor = ConsoleColor.DarkRed;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(ex.Message);
        Console.ResetColor();
    }

    private static void AddTopHtml(StreamWriter streamWriter)
    {
        streamWriter.WriteLine("<!doctype html>");
        streamWriter.WriteLine(@"<html lang = ""en"">");
        streamWriter.WriteLine("<head>");
        streamWriter.WriteLine(@"<meta charset = ""utf-8"">");
        streamWriter.WriteLine(@"<meta name = ""viewport"" content = ""width=device-width,intial-scale=1"">");
        streamWriter.WriteLine("<title>Wild Cats</title>");
        streamWriter.WriteLine(@"<link rel = ""stylesheet"" href = ""https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css"" >");
        streamWriter.WriteLine(@"<script src = ""https://code.jquery.com/jquery-1.12.4.js""></script>");
        streamWriter.WriteLine(@"<script src = ""https://code.jquery.com/ui/1.12.1/jquery-ui.js""></script>");

        streamWriter.WriteLine("<script>");
        streamWriter.WriteLine("$(function(){");
        streamWriter.WriteLine(@"$(""#accordion"").accordion();");
        streamWriter.WriteLine("});");
        streamWriter.WriteLine("</script>");
        streamWriter.WriteLine("</head>");
        streamWriter.WriteLine("<body>");

        streamWriter.WriteLine(@"<h1 style=""text-align:center;font-family:arial"">Wild Cats</h1> ");
        streamWriter.WriteLine(@"<div id = ""accordion"">");
    }

    private static void BuildHtmlBody(StreamWriter streamWriter, string topicContent, string topicHeading)
    {
        streamWriter.WriteLine($"<h3>{topicHeading}</h3>");
        streamWriter.WriteLine("<div>");
        streamWriter.WriteLine("<p>");
        streamWriter.Write(topicContent);
        streamWriter.WriteLine("</p>");
        streamWriter.WriteLine("</div>");
    }

    private static void AddBottomHtml(StreamWriter streamWriter)
    {
        streamWriter.WriteLine("</div>");
        streamWriter.WriteLine("</body>");
        streamWriter.WriteLine("</html>");
    }

    private static DirectoryInfo InitializeSourceDirectory(string rootPath)
    {
        string wildCatsDirectoryPath = Path.Combine(rootPath,"WildCats");

        string infoFilePath = Path.Combine(wildCatsDirectoryPath, "Information.txt");

        if(!Directory.Exists(wildCatsDirectoryPath))
            Directory.CreateDirectory(wildCatsDirectoryPath);

        DirectoryInfo sourceDirectory = new(wildCatsDirectoryPath);

        int numTextFileInDirectory = sourceDirectory.GetFiles("*.txt").Length;

        if(numTextFileInDirectory == 0)
        {
            using (StreamWriter sw = File.CreateText(infoFilePath))
            {
                sw.WriteLine($"Text files have not yet been added to this directory, {wildCatsDirectoryPath}");
            }
        }
        else if(numTextFileInDirectory > 1 && File.Exists(infoFilePath))
        {
            File.Delete(infoFilePath);
        }

        return sourceDirectory!;
    }

    private static void LaunchHtmlFileInBrowser(string url)
    {
        if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            url = url.Replace("&", "^&");
            Process.Start(new ProcessStartInfo("cmd", $"/d start {url}") { CreateNoWindow = true });
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            Process.Start("xdg-open", url);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            Process.Start("open", url);
        }
        else
        {
            throw new NotImplementedException("Unable to launch your default browser through this application.");
        }
    }
}
