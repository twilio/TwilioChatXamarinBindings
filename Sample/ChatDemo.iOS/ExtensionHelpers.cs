using Twilio.Chat.iOS;

namespace ChatDemo.ExtensionHelpers
{
    public static class JsonAttributesExtension
    {
        public static string ToDebugLog(this JsonAttributes attributes)
        {
            if (attributes.IsNull)
                return "null";
            if (attributes.IsString)
                return attributes.String;
            if (attributes.IsNumber)
                return attributes.Number.ToString();
            if (attributes.IsArray)
                return attributes.Array.ToString();
            if (attributes.IsDictionary)
                return attributes.Dictionary.ToString();
            throw new System.Exception("<unknown json type>");
        }
    }
}
