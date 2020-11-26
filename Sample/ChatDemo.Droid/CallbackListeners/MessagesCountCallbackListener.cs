using System;
using ChatDemo.Shared;
using Com.Twilio.Chat;

namespace ChatDemo.Droid
{
    public class MessagesCountCallbackListener : CallbackListener<long>
    {
        private readonly Channel channel;

        public MessagesCountCallbackListener(Channel channel)
        {
            this.channel = channel;
        }

        public override void OnError(ErrorInfo errorInfo)
        {
            Logger.Error($"Channel: {channel.Sid}", errorInfo);
        }

        public override void OnSuccess(long result)
        {
            Logger.Info($"Channel: {channel.Sid}", $"Messages count: {result}");
        }
    }
}
