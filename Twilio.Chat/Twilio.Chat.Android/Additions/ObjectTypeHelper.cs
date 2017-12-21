using System;
namespace Com.Twilio.Chat
{
    public static class ObjectTypeHelper
    {
        public static T Cast<T>(Java.Lang.Object obj) where T : class, Android.Runtime.IJavaObject
        {
            return Java.Lang.Object.GetObject<T>(obj.Handle, Android.Runtime.JniHandleOwnership.DoNotTransfer);
        }
}}
