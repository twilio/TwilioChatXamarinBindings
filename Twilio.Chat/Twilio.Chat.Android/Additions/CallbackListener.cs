using System;
using System.Collections.Generic;
using System.Reflection;

namespace Com.Twilio.Chat
{
    public abstract class CallbackListener<T> : CallbackListener
    {
        public abstract void OnSuccess(T result);

        public override void OnSuccess(Java.Lang.Object result) => this.OnSuccess(Cast(result));

        private static T Cast(Java.Lang.Object javaObject)
        {
            if (typeof(T).IsGenericType)
            {
                if (typeof(T).GetGenericTypeDefinition().Equals(typeof(Paginator<>)) &&
                    javaObject.GetType().Equals(typeof(InternalPaginator)))
                {
                    Type genericType = typeof(Paginator<>).MakeGenericType(typeof(T).GetGenericArguments());
                    ConstructorInfo constructor = genericType.GetConstructor(new Type[] { typeof(InternalPaginator) });
                    if (constructor == null)
                    {
                        throw new InvalidOperationException("Type " + genericType.Name + " does not contain an appropriate constructor");
                    }
                    return (T)constructor.Invoke(new object[] { Convert.ChangeType(javaObject, typeof(InternalPaginator)) });
                 }

                if (typeof(T).GetGenericTypeDefinition().Equals(typeof(IList<>)) &&
                    javaObject.GetType().Equals(typeof(Android.Runtime.JavaList)))
                {
                    Type genericType = typeof(Android.Runtime.JavaList<>).MakeGenericType(typeof(T).GetGenericArguments());
                    return (T)Activator.CreateInstance(genericType, javaObject.Handle, Android.Runtime.JniHandleOwnership.DoNotRegister);
                }
            }
            return (T)Convert.ChangeType(javaObject, typeof(T));
        }
    }
}
