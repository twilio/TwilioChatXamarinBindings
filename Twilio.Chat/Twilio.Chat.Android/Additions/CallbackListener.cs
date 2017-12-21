using System;
using System.Collections.Generic;

namespace Com.Twilio.Chat
{
    public abstract class CallbackListener<T> : CallbackListener
    {
        public abstract void OnSuccess(T result);

        public override void OnSuccess(Java.Lang.Object result) => this.OnSuccess(Cast(result));

        private static T Cast(Java.Lang.Object javaObject)
        {
            if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition().Equals(typeof(Paginator<>)) &&
                javaObject.GetType().Equals(typeof(InternalPaginator)))
            {
                return (T)(Activator.CreateInstance(typeof(T), Convert.ChangeType(javaObject, typeof(InternalPaginator))));
            }

            if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition().Equals(typeof(IList<>)) &&
                typeof(T).GetGenericArguments().Length.Equals(1) && 
                javaObject.GetType().Equals(typeof(Android.Runtime.JavaList))) 
            {
                Type genericArgument = typeof(T).GetGenericArguments()[0];
                Type genericType = typeof(Android.Runtime.JavaList<>).MakeGenericType(genericArgument);
                return (T)Activator.CreateInstance(genericType, javaObject.Handle, Android.Runtime.JniHandleOwnership.DoNotRegister);
            }

            return (T)Convert.ChangeType(javaObject, typeof(T));
        }
    }
}
