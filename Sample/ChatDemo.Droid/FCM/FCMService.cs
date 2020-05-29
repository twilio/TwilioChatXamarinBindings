using System;
using Android.App;
using Android.Content;
using Android.Media;
using Android.Support.V4.Content;
using Android.Util;
using ChatDemo.Shared;
using Com.Twilio.Chat;
using Xamarin.Forms;
using Firebase.Messaging;

namespace ChatDemo.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT", "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class FCMService : FirebaseMessagingService
    {
        public override void OnNewToken(string refreshedToken)
        {
            Logger.ToConsole(new LogLine(LogLine.LogLevel.Info, "FCMService", $"received new device token: {refreshedToken}"));
            var twilioChatHelper = DependencyService.Get<ITwilioChatHelper>();
            twilioChatHelper.SetDeviceToken(refreshedToken);
        }

        public override void OnMessageReceived(RemoteMessage message)
        {
            Logger.ToConsole(new LogLine(LogLine.LogLevel.Info, "FCMService", $"received new message: {message}"));
            if (message.Data != null && message.Data.Count > 0)
            {
                NotificationPayload chatNotificationPayload = new NotificationPayload(message.Data);

                if (chatNotificationPayload.Type == NotificationPayload.NotificationPayloadType.Unknown)
                {
                    Logger.ToConsole(new LogLine(LogLine.LogLevel.Error, "FCMService", "received notification with unknown type"));
                    return;
                }

                // broadcast intent with message (to be caught by TwilioChatHelper and processed to the lib
                Intent messageIntent = new Intent("com.twilio.rtd.xamarin.ChatDemo.FCM_MESSAGE");
                var bundle = new Android.OS.Bundle();
                messageIntent.PutExtra("message", message);
                LocalBroadcastManager.GetInstance(this).SendBroadcast(messageIntent);

                // show local notification
                var intent = new Intent(this, typeof(MainActivity));
                intent.AddFlags(ActivityFlags.ClearTop);
                var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

                var notificationBuilder = new Notification
                    .Builder(this)
                    .SetSmallIcon(Resource.Drawable.icon)
                    .SetContentTitle("ChatDemo.Droid")
                    .SetContentText(chatNotificationPayload.Body)
                    .SetAutoCancel(true)
                    .SetContentIntent(pendingIntent);

                var notificationManager = NotificationManager.FromContext(this);
                notificationManager.Notify(0, notificationBuilder.Build());
            }

            if (message.GetNotification() != null)
            {
                Logger.ToConsole(new LogLine(LogLine.LogLevel.Info, "FCMService", $"We do not parse notification body - leave it to system. Notification Message Body: ${message.GetNotification().Body}"));
            }
        }
    }
}
