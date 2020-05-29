using Com.Twilio.Chat;

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
            if (attributes.IsBoolean)
                return attributes.Boolean.ToString();
            if (attributes.IsNumber)
                return attributes.Number.ToString();
            if (attributes.IsArray)
                return attributes.JSONArray.ToString();
            if (attributes.IsDictionary)
                return attributes.JSONObject.ToString();
            throw new System.Exception("<unknown json type>");
        }
    }
}
