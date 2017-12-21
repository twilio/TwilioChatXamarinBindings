using System;
namespace Com.Twilio.Chat
{
    public sealed partial class Messages
    {
        public long? LastConsumedMessageIndex {
            get
            {
                Java.Lang.Long value = this.GetLastConsumedMessageIndex();
                if (value == null) 
                {
                    return null;
                }
                return value.LongValue();
            }
        }
    }
}
