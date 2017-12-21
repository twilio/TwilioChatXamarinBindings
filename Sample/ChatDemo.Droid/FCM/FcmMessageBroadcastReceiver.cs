using System;
using Android.Content;
using ChatDemo.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(TwilioChatHelper))]
namespace ChatDemo.Droid
{
    public partial class TwilioChatHelper
    {
        public class FcmMessageBroadcastReceiver : BroadcastReceiver
        {

            public FcmMessageEventHandler FcmMessage;

            protected virtual void OnFcmMessage(FcmMessageEventArgs e)
            {
                if (FcmMessage != null)
                {
                    FcmMessage(this, e);
                }
            }

            public override void OnReceive(Context context, Intent intent)
            {

                Firebase.Messaging.RemoteMessage remoteMessage = (Firebase.Messaging.RemoteMessage)intent.GetParcelableExtra("message");

                OnFcmMessage(new FcmMessageEventArgs(remoteMessage));
            }


            public delegate void FcmMessageEventHandler(object sender, FcmMessageEventArgs e);


            public class FcmMessageEventArgs : EventArgs
            {
                public FcmMessageEventArgs(Firebase.Messaging.RemoteMessage remoteMessage)
                {
                    this.RemoteMessage = remoteMessage;
                }

                public Firebase.Messaging.RemoteMessage RemoteMessage { get; }
            }
        }
    }
}
