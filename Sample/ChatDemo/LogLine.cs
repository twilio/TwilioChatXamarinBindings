using Xamarin.Forms;

namespace ChatDemo
{
    public class LogLine
    {
        public LogLevel Level { get; }
        public string Source { get; }
        public string Text { get; }
        public string DateTime { get; }

        public LogLine(LogLevel level, string source, string text)
        {
            this.Level = level;
            this.Source = source;
            this.Text = text;
            this.DateTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        public enum LogLevel
        {
            Info, Error
        }
    }
}
