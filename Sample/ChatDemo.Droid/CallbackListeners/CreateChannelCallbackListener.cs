using System;
using ChatDemo.Shared;
using Com.Twilio.Chat;

namespace ChatDemo.Droid
{
    public class CreateChannelCallbackListener : CallbackListener<Channel>
    {
        public CreateChannelCallbackListener()
        {}

        public override void OnError(ErrorInfo errorInfo)
        {
            Logger.Error($"CreateChannel", errorInfo);
        }

        public override void OnSuccess(Channel result)
        {
            Logger.Info($"CreateChannel", $"Channel created");
            using var attrs = new JsonAttributes("hello");
            result.SetAttributes(attrs, new StatusListeners.SetAttributesStatusListener(result));
        }
    }
}
