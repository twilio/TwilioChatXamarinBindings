using System;
using ChatDemo.Shared;

namespace ChatDemo
{
    public interface ITwilioChatHelper
    {
        TokenProvider GetTokenProvider();
        void SetTokenProvider(TokenProvider tokenProvider);
        void CreateClient(string chatToken);

        event LogLineEventHandler LogLine;
        void FireLogLineEvent(LogLine logLine);

        void SetDeviceToken(object token);
        object GetDeviceToken();

        void CreateChannel(string friendlyName);
    }

    public delegate void LogLineEventHandler(object sender, LogLineEventArgs e);

    public class LogLineEventArgs : EventArgs
    {
        public LogLineEventArgs(LogLine logLine)
        {
            this.LogLine = logLine;
        }

        public LogLine LogLine { get; }
    }
}
