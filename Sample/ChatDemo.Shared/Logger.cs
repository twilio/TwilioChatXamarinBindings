using System;
using Xamarin.Forms;

namespace ChatDemo.Shared
{
    public static class Logger
    {
        public static void Info(object source, object line)
        {
            var logLine = new LogLine(LogLine.LogLevel.Info, source.ToString(), line.ToString());
            ToConsole(logLine);
            var twilioChatHelper = DependencyService.Get<ITwilioChatHelper>();
            twilioChatHelper.FireLogLineEvent(new LogLine(LogLine.LogLevel.Info, source.ToString(), line.ToString()));
        }

        public static void Error(object source, object line)
        {
            var logLine = new LogLine(LogLine.LogLevel.Error, source.ToString(), line.ToString());
            ToConsole(logLine);
            var twilioChatHelper = DependencyService.Get<ITwilioChatHelper>();
            twilioChatHelper.FireLogLineEvent(logLine);
        }

        public static void ToConsole(LogLine logLine)
        {
            Console.WriteLine($"[{logLine.DateTime}] [{logLine.Level.ToString().ToUpper()}] [{logLine.Source}] {logLine.Text}");
        }
    }
}
