namespace Com.Twilio.Chat
{
    public partial class JsonAttributes : Java.Lang.Object
    {
        public bool IsNull => ValueType == AttributeType.Null;
        public bool IsString => ValueType == AttributeType.String;
        public bool IsNumber => ValueType == AttributeType.Number;
        public bool IsArray => ValueType == AttributeType.Array;
        public bool IsDictionary => ValueType == AttributeType.Object;
        public bool IsBoolean => ValueType == AttributeType.Boolean;
    }
}
