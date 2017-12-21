using System;
using Android.App;
using Firebase.Iid;
using Android.Util;
using ChatDemo.Shared;
using Xamarin.Forms;

namespace ChatDemo.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class FirebaseIIDService : FirebaseInstanceIdService
    {
        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            var twilioChatHelper = DependencyService.Get<ITwilioChatHelper>();
            twilioChatHelper.SetDeviceToken(refreshedToken);
        }
    }
}
