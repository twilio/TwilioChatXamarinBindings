using System;
using ObjCRuntime;

namespace Twilio.Chat.iOS
{
    // => Enums attributed with [Native] attribute must have an underlying type of `long` or `ulong`
    [Native]
    public enum ClientConnectionState : ulong
    {
        Unknown,
        Disconnected,
        Connected,
        Connecting,
        Denied,
        Error
    }

    [Native]
    public enum ClientSynchronizationStatus : ulong
    {
        Started = 0,
        ChannelsListCompleted,
        Completed,
        Failed
    }

    [Native]
    public enum LogLevel : ulong
    {
		Silent = 0,
        Fatal,
        Critical,
        Warning,
        Info,
        Debug,
        Trace
    }

    [Native]
    public enum ChannelUpdate : ulong
    {
        Status = 1,
        LastConsumedMessageIndex,
        UniqueName,
        FriendlyName,
		Attributes,
        LastMessage,
        UserNotificationLevel
    }

    [Native]
    public enum ChannelSynchronizationStatus : ulong
    {
        None = 0,
        Identifier,
        Metadata,
        All,
        Failed
    }

    [Native]
    public enum ChannelStatus : ulong
    {
        Invited = 0,
        Joined,
        NotParticipating,
        Unknown
    }

    [Native]
    public enum ChannelType : ulong
    {
        Public = 0,
        Private
    }


    [Native]
    public enum ChannelNotificationLevel : ulong
    {
        Default = 0,
        Muted
    }

    [Native]
    public enum ChannelSortingCriteria : ulong
    {
        LastMessage = 0,
        FriendlyName,
        UniqueName
    }

    [Native]
	public enum ChannelSortingOrder : ulong
    {
        Ascending = 0,
        Descending
    }

    [Native]
    public enum UserUpdate : ulong
    {
        FriendlyName = 0,
        Attributes,
        ReachabilityOnline,
        ReachabilityNotifiable
    }

    [Native]
    public enum MemberUpdate : ulong
    {
        LastConsumedMessageIndex = 0,
        Attributes = 1
    }

    [Native]
    public enum MessageUpdate : ulong
    {
        Body = 0,
        Attributes
    }

    [Native]
    public enum MessageType : ulong
    {
        Text = 0,
        Media
    }

    [Native]
    public enum MemberType : ulong
    {
        Unset = 0,
        Other,
        Chat,
        Sms,
        Whatsapp
    }

}