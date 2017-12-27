﻿using System;
using ChatDemo.Shared;
using Twilio.Chat.iOS;

namespace ChatDemo.iOS.Delegates
{
    public class TwilioChatDelegate : TwilioChatClientDelegate
    {
        public TwilioChatDelegate()
        {
        }

        public override void ConnectionStateUpdated(TwilioChatClient client, ClientConnectionState state)
        {
            Logger.Info($"ChatClient: {client}", $"ConnectionStateChange: {state}");
        }

        public override void SynchronizationStatusUpdated(TwilioChatClient client, ClientSynchronizationStatus status)
        {
            Logger.Info($"ChatClient: {client}", $"SynchronizationStatusUpdated: {status}");
        }

        public override void ChannelAdded(TwilioChatClient client, Channel channel)
        {
            Logger.Info($"ChatClient: {client}", $"ChannelAdded: {channel.Sid}");
        }

        public override void ChannelUpdated(TwilioChatClient client, Channel channel, ChannelUpdate updated)
        {
            Logger.Info($"ChatClient: {client}", $"ChannelUpdated: {channel.Sid}, reason: {updated}");
        }

        public override void ChannelSynchronizationStatusUpdated(TwilioChatClient client, Channel channel, ChannelSynchronizationStatus status)
        {
            Logger.Info($"ChatClient: {client}", $"ChannelSynchronizationStatusUpdated for channel: {channel.Sid}, status: {status}");

            if (status.Equals(ChannelSynchronizationStatus.All) && channel.Status.Equals(ChannelStatus.Joined))
            {
                Logger.Info($"ChatClient: {client}", $"Got joined channel: {channel.Sid}");
                channel.Delegate = new TwilioChannelDelegate();
                channel.GetMessagesCountWithCompletion((result, count) =>
                {
                    if (result.IsSuccessful)
                    {
                        Logger.Info($"Channel: {channel.Sid}", $"Messages count: {count}");
                    }
                    else
                    {
                        Logger.Error($"Channel: {channel.Sid}",
                                     $"Error: {result.Error}, " +
                                     $"code: {result.ResultCode}, " +
                                     $"text: {result.ResultText}");
                    }
                });
                channel.GetMembersCountWithCompletion((result, count) =>
                {
                    if (result.IsSuccessful)
                    {
                        Logger.Info($"Channel: {channel.Sid}", $"Members count: {count}");
                    }
                    else
                    {
                        Logger.Error($"Channel: {channel.Sid}",
                                     $"Error: {result.Error}, " +
                                     $"code: {result.ResultCode}, " +
                                     $"text: {result.ResultText}");
                    }
                });
                channel.Messages.GetLastMessagesWithCount(10, (result, messages) =>
                {
                    if (result.IsSuccessful)
                    {
                        Logger.Info($"Channel: {channel.Sid}", $"Messages: {result}");
                        foreach (Message message in messages)
                        {
                            Logger.Info($"Channel: {channel.Sid}", $"Got message: {message.Sid}");
                        };
                    }
                    else
                    {
                        Logger.Error($"Channel: {channel.Sid}",
                                     $"Error: {result.Error}, " +
                                     $"code: {result.ResultCode}, " +
                                     $"text: {result.ResultText}");
                    }

                });
            }
            else if (status.Equals(ChannelSynchronizationStatus.All) && channel.Status.Equals(ChannelStatus.Invited))
            {
                Logger.Info($"ChatClient: {client}", $"Got invited channel: {channel.Sid}");
            }

        }

        public override void ChannelDeleted(TwilioChatClient client, Channel channel)
        {
            Logger.Info($"ChatClient: {client}", $"ChannelDeleted: {channel.Sid}");
        }

        public override void MemberJoined(TwilioChatClient client, Channel channel, Member member)
        {
            Logger.Info($"ChatClient: {client}", $"Channel: {channel.Sid} MemberJoined: {member.Sid}");
        }

        public override void MemberUpdated(TwilioChatClient client, Channel channel, Member member, MemberUpdate updated)
        {
            Logger.Info($"ChatClient: {client}", $"Channel: {channel.Sid} MemberUpdated: {member.Sid}, reason: {updated}");
        }

        public override void MemberLeft(TwilioChatClient client, Channel channel, Member member)
        {
            Logger.Info($"ChatClient: {client}", $"Channel: {channel.Sid} MemberLeft: {member.Sid}");
        }

        public override void MessageAdded(TwilioChatClient client, Channel channel, Message message)
        {
            Logger.Info($"ChatClient: {client}", $"Channel: {channel.Sid} MessageAdded: {message.Sid}");
        }

        public override void MessageUpdated(TwilioChatClient client, Channel channel, Message message, MessageUpdate updated)
        {
            Logger.Info($"ChatClient: {client}", $"Channel: {channel.Sid} MessageUpdated: {message.Sid}, reason: {updated}");
        }

        public override void MessageDeleted(TwilioChatClient client, Channel channel, Message message)
        {
            Logger.Info($"ChatClient: {client}", $"Channel: {channel.Sid} MessageDeleted: {message.Sid}");
        }

        public override void ErrorReceived(TwilioChatClient client, Error error)
        {
            if (error.UserInfo != null && error.UserInfo.ContainsKey(Constants.ErrorMsgKey))
            {
                Logger.Info($"ChatClient: {client}",
                            $"Error: {error}, " +
                            $"userInfo: {error.UserInfo.ObjectForKey(Constants.ErrorMsgKey)} " +
                            $"code: {error.Code}, " +
                            $"domain: {error.Domain}");
            }
            else
            {
                Logger.Info($"ChatClient: {client}", $"Error: {error}, code: {error.Code}, domain: {error.Domain}");
            }
        }

        public override void TypingStartedOnChannel(TwilioChatClient client, Channel channel, Member member)
        {
            Logger.Info($"ChatClient: {client}", $"Channel: {channel.Sid} TypingStartedOnChannel: {member.Sid}");
        }

        public override void TypingEndedOnChannel(TwilioChatClient client, Channel channel, Member member)
        {
            Logger.Info($"ChatClient: {client}", $"Channel: {channel.Sid} TypingEndedOnChannel: {member.Sid}");
        }

        public override void NotificationNewMessageReceivedForChannelSid(TwilioChatClient client, string channelSid, nuint messageIndex)
        {
            Logger.Info($"ChatClient: {client}", $"NotificationNewMessageReceivedForChannelSid: ChannelSid: {channelSid}, MessageIndex: {messageIndex}");
        }

        public override void NotificationAddedToChannelWithSid(TwilioChatClient client, string channelSid)
        {
            Logger.Info($"ChatClient: {client}", $"NotificationAddedToChannelWithSid: ChannelSid: {channelSid}");
        }

        public override void NotificationInvitedToChannelWithSid(TwilioChatClient client, string channelSid)
        {
            Logger.Info($"ChatClient: {client}", $"NotificationInvitedToChannelWithSid: ChannelSid: {channelSid}");
        }

        public override void NotificationRemovedFromChannelWithSid(TwilioChatClient client, string channelSid)
        {
            Logger.Info($"ChatClient: {client}", $"NotificationRemovedFromChannelWithSid: ChannelSid: {channelSid}");
        }

        public override void NotificationUpdatedBadgeCount(TwilioChatClient client, nuint badgeCount)
        {
            Logger.Info($"ChatClient: {client}", $"NotificationUpdatedBadgeCount:  {badgeCount}");
        }

        public override void UserUpdated(TwilioChatClient client, User user, UserUpdate updated)
        {
            Logger.Info($"ChatClient: {client}", $"UserUpdated: {user.Identity}, reason: {updated}");
        }

        public override void UserSubscribed(TwilioChatClient client, User user)
        {
            Logger.Info($"ChatClient: {client}", $"UserSubscribed: {user.Identity}");
        }

        public override void UserUnsubscribed(TwilioChatClient client, User user)
        {
            Logger.Info($"ChatClient: {client}", $"UserUnsubscribed: {user.Identity}");
        }

    }
}
