using System;
namespace Com.Twilio.Chat
{
    public sealed partial class ChatClient
    {
        public enum LogLevel { Silent, Fatal, Critical, Warning, Info, Debug, Trace }

        public static void SetLogLevel(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Silent:
                    ChatClient.SetLogLevel(6);
                    break;
                case LogLevel.Fatal:
                    ChatClient.SetLogLevel(5);
                    break;
                case LogLevel.Critical:
                    ChatClient.SetLogLevel(4);
                    break;
                case LogLevel.Warning:
                    ChatClient.SetLogLevel(3);
                    break;
                case LogLevel.Info:
                    ChatClient.SetLogLevel(2);
                    break;
                case LogLevel.Debug:
                    ChatClient.SetLogLevel(1);
                    break;
                case LogLevel.Trace:
                    ChatClient.SetLogLevel(0);
                    break;
            }
        }
    }
}
