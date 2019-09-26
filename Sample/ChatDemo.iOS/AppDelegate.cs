using System;
using ChatDemo.Shared;
using Foundation;
using UIKit;
using UserNotifications;
using Xamarin.Forms;

namespace ChatDemo.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, UserNotifications.IUNUserNotificationCenterDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();


            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                // iOS 10 or later
                var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
                UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) => {
                    Console.WriteLine($"[AppDelegate] UNUserNotificationCenter.Current.RequestAuthorization: { granted }");
                    if (error != null)
                    {
                        Console.WriteLine($"[AppDelegate] UNUserNotificationCenter.Current.RequestAuthorization error: { error.LocalizedDescription }");
                    }
                });
                UNUserNotificationCenter.Current.Delegate = this;
            }
            else
            {
                var allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
                var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);
                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            }

            UIApplication.SharedApplication.RegisterForRemoteNotifications();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            Console.WriteLine($"[AppDelegate] RegisteredForRemoteNotifications Got device token { deviceToken.Description}");
            Logger.Info($"AppDelegate", $"RegisteredForRemoteNotifications Got device token {deviceToken.Description}");
            var twilioChatHelper = DependencyService.Get<ITwilioChatHelper>();
            twilioChatHelper.SetDeviceToken(deviceToken);
        }


        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            Console.WriteLine($"[AppDelegate] FailedToRegisterForRemoteNotifications {error.Description}");
            Logger.Error($"AppDelegate", $"FailedToRegisterForRemoteNotifications {error.Description}");
        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            Logger.Info($"AppDelegate", $"DidReceiveRemoteNotification {userInfo}");
        }

    }
}
