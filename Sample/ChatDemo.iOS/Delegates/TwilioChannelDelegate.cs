using ChatDemo.Shared;
using Twilio.Chat.iOS;

namespace ChatDemo.iOS.Delegates
{
    internal class TwilioChannelDelegate : ChannelDelegate
    {
        public override void ChannelUpdated(TwilioChatClient client, Channel channel, ChannelUpdate updated)
        {
            Logger.Info($"Channel: {channel.Sid}", $"ChannelUpdated reason: {updated}");
        }

        public override void ChannelSynchronizationStatusUpdated(TwilioChatClient client, Channel channel, ChannelSynchronizationStatus status)
        {
            Logger.Info($"Channel: {channel.Sid}", $"SynchronizationStatusUpdated: {status}");
        }

        public override void ChannelDeleted(TwilioChatClient client, Channel channel)
        {
            Logger.Info($"Channel: {channel.Sid}", $"ChannelDeleted");
        }

        public override void MemberJoined(TwilioChatClient client, Channel channel, Member member)
        {
            Logger.Info($"Channel: {channel.Sid}", $"MemberJoined: {member.Sid}");
        }

        public override void MemberUpdated(TwilioChatClient client, Channel channel, Member member, MemberUpdate updated)
        {
            Logger.Info($"Channel: {channel.Sid}", $"MemberUpdated: {member.Sid}, reason: {updated}");
        }

        public override void MemberLeft(TwilioChatClient client, Channel channel, Member member)
        {
            Logger.Info($"Channel: {channel.Sid}", $"MemberLeft: {member.Sid}");
        }

        public override void MessageAdded(TwilioChatClient client, Channel channel, Message message)
        {
            Logger.Info($"Channel: {channel.Sid}", $"MessageAdded: {message.Sid}");
        }

        public override void MessageUpdated(TwilioChatClient client, Channel channel, Message message, MessageUpdate updated)
        {
            Logger.Info($"Channel: {channel.Sid}", $"MessageUpdated: {message.Sid}, reason: {updated}");
        }

        public override void MessageDeleted(TwilioChatClient client, Channel channel, Message message)
        {
            Logger.Info($"Channel: {channel.Sid}", $"MessageDeleted: {message.Sid}");
        }

        public override void TypingStartedOnChannel(TwilioChatClient client, Channel channel, Member member)
        {
            Logger.Info($"Channel: {channel.Sid}", $"TypingStartedOnChannel: {member.Sid}");
        }

        public override void TypingEndedOnChannel(TwilioChatClient client, Channel channel, Member member)
        {
            Logger.Info($"Channel: {channel.Sid}", $"TypingEndedOnChannel: {member.Sid}");
        }

        public override void UserUpdated(TwilioChatClient client, Channel channel, Member member, User user, UserUpdate updated)
        {
            Logger.Info($"Channel: {channel.Sid}", $"UserUpdated: {user.Identity}, member: {member.Sid}, reason: {updated}");
        }

        public override void UserSubscribed(TwilioChatClient client, Channel channel, Member member, User user)
        {
            Logger.Info($"Channel: {channel.Sid}", $"UserSubscribed: {user.Identity}, member: {member.Sid}");
        }

        public override void UserUnsubscribed(TwilioChatClient client, Channel channel, Member member, User user)
        {
            Logger.Info($"Channel: {channel.Sid}", $"UserUnsubscribed: {user.Identity}, member: {member.Sid}");
        }

    }
}