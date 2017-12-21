using System;
namespace Com.Twilio.Chat
{
    public sealed partial class ChatClient
    {
        public enum LogLevel { Fatal, Critical, Warning, Info, Debug }

        public static void SetLogLevel(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Fatal:
                    ChatClient.SetLogLevel(0);
                    break;
                case LogLevel.Critical:
                    ChatClient.SetLogLevel(1);
                    break;
                case LogLevel.Warning:
                    ChatClient.SetLogLevel(2);
                    break;
                case LogLevel.Info:
                    ChatClient.SetLogLevel(3);
                    break;
                case LogLevel.Debug:
                    ChatClient.SetLogLevel(4);
                    break;
            }
        }
    }
}
