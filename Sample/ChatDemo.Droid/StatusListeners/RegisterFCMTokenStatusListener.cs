using System;
using ChatDemo.Shared;

namespace ChatDemo.Droid.StatusListeners
{
    public class RegisterFCMTokenStatusListener : Com.Twilio.Chat.StatusListener
    {
        public override void OnError(Com.Twilio.Chat.ErrorInfo errorInfo) => Logger.Error("RegisterFCMTokenStatusListener", errorInfo);

        public override void OnSuccess()
        {
            Logger.Info("RegisterFCMTokenStatusListener", "Success");
        }
    }
}
