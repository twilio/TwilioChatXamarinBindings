using System;
using ChatDemo.Shared;
using Com.Twilio.Chat;

namespace ChatDemo.Droid.StatusListeners
{
    public class SetAttributesStatusListener : Com.Twilio.Chat.StatusListener
    {
        private readonly Channel channel;

        public SetAttributesStatusListener(Channel channel)
        {
            this.channel = channel;
        }
        public override void OnSuccess()
        {
            Logger.Info("SetAttributesStatusListener", $"Created channel with attributes and sid {channel.Sid}");
        }
    }
}
