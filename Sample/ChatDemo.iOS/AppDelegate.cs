using System;
using ChatDemo.Shared;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace ChatDemo.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();


            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                UIApplication.SharedApplication.RegisterUserNotificationSettings(
                    UIUserNotificationSettings.GetSettingsForTypes(
                        UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
                        null));
                UIApplication.SharedApplication.RegisterForRemoteNotifications();
            }
            else
            {
                UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(
                    UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound);
            }


            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            Logger.Info($"AppDelegate", $"RegisteredForRemoteNotifications Got device token {deviceToken.Description}");
            var twilioChatHelper = DependencyService.Get<ITwilioChatHelper>();
            twilioChatHelper.SetDeviceToken(deviceToken);
        }


        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            Logger.Error($"AppDelegate", $"FailedToRegisterForRemoteNotifications {error.Description}");
        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            Logger.Info($"AppDelegate", $"DidReceiveRemoteNotification {userInfo}");
        }

    }
}
