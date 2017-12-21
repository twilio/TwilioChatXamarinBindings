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
        Fatal = 0,
        Critical,
        Warning,
        Info,
        Debug
    }

    [Native]
    public enum ChannelUpdate : ulong
    {
        Status = 1,
        LastConsumedMessageIndex,
        UniqueName,
        FriendlyName,
        Attributes
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
        ublic = 0,
        rivate
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
        MemberUpdateLastConsumedMessageIndex = 0
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
}