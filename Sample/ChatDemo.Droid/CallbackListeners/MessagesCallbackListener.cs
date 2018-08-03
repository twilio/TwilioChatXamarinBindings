using ChatDemo.Droid;
using ChatDemo.Shared;
using Com.Twilio.Chat;

namespace ChatDemo.Droid
{
    public class MessagesCallbackListener : CallbackListener<System.Collections.Generic.IList<Message>>
    {

        private Channel channel;

        public MessagesCallbackListener(Channel channel)
        {
            this.channel = channel;
        }

        public override void OnError(ErrorInfo errorInfo)
        {
            Logger.Error($"Channel: {channel.Sid}", errorInfo);
        }

        public override void OnSuccess(System.Collections.Generic.IList<Message> result)
        {
            Logger.Info($"Channel: {channel.Sid}", $"Messages: {result}");
            foreach (Message message in result)
            {
                Logger.Info($"Channel: {channel.Sid}", $"Got message: {message.Sid} created on {message.DateCreatedAsDate}");
            };
        }

    }
}
