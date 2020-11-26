using System;
using ChatDemo.Shared;
using Com.Twilio.Chat;

namespace ChatDemo.Droid
{
    public class MembersCountCallbackListener : CallbackListener<long>
    {
        private readonly Channel channel;

        public MembersCountCallbackListener(Channel channel) 
        {
            this.channel = channel;
        }

        public override void OnError(ErrorInfo errorInfo)
        {
            Logger.Error($"Channel: {channel.Sid}", errorInfo);
        }

        public override void OnSuccess(long result)
        {
            Logger.Info($"Channel: {channel.Sid}", $"Members count: {result}");
        }
    }
}
