﻿using Android.Content;
using Android.Support.V4.Content;
using ChatDemo.Droid;
using ChatDemo.ExtensionHelpers;
using ChatDemo.Shared;
using Com.Twilio.Chat;

[assembly: Xamarin.Forms.Dependency(typeof(TwilioChatHelper))]
namespace ChatDemo.Droid
{
    public partial class TwilioChatHelper : ITwilioChatHelper
    {
        private string deviceToken;

        private TokenProvider tokenProvider;

        private Com.Twilio.Chat.ChatClient chatClient;

        private FcmMessageBroadcastReceiver fcmMessageBroadcastReceiver;

        public event LogLineEventHandler LogLine;

        protected virtual void OnLogLine(LogLineEventArgs e)
        {
            if (LogLine != null)
            {
                LogLine(this, e);
            }
        }

        public void FireLogLineEvent(LogLine logLine)
        {
            OnLogLine(new LogLineEventArgs(logLine));
        }

        public TwilioChatHelper()
        {
            this.fcmMessageBroadcastReceiver = new FcmMessageBroadcastReceiver();
            LocalBroadcastManager.GetInstance(Android.App.Application.Context).RegisterReceiver(
                this.fcmMessageBroadcastReceiver,
                new IntentFilter("com.twilio.rtd.xamarin.ChatDemo.FCM_MESSAGE"));
            
            this.fcmMessageBroadcastReceiver.FcmMessage += (sender, e) =>
            {
                Logger.Info("ChatClient", $"Got message from intent com.twilio.rtd.xamarin.ChatDemo.FCM_MESSAGE");

                if (this.chatClient != null)
                {
                    NotificationPayload notificationPayload = new NotificationPayload(e.RemoteMessage.Data);
                    this.chatClient.HandleNotification(notificationPayload);
                }
                else
                {
                    Logger.Error("ChatClient", $"There is no Twilio Chat client to process message from intent");
                }
            };
        }

        public void SetChatClient(ChatClient chatClient)
        {
            this.chatClient = chatClient;
        }

        public void CreateClient(string chatToken)
        {
            this.chatClient = null;
            ChatClient.SetLogLevel(ChatClient.LogLevel.Info);
            var properties = new ChatClient.ClientProperties.Builder()
                .SetCommandTimeout(ChatClient.ClientProperties.MinCommandTimeout)
                .CreateProperties();
            ChatClient.Create(
                Android.App.Application.Context,
                chatToken,
                properties,
                new ChatClientCallbackListener(this)
            );
        }

        public void SubscribeToClientEvents(ChatClient client)
        {
            client.ChannelAdded += (sender, args) => { this.SubscribeToChannelEvents(args.Channel); };
            client.ChannelInvited += (sender, args) => { this.SubscribeToChannelEvents(args.Channel); };
            client.ChannelJoined += (sender, args) => { this.SubscribeToChannelEvents(args.Channel); };

            client.ChannelAdded += (sender, args) => { Logger.Info($"ChatClient: {client}", $"ChannelAdded: {args.Channel.Sid}"); };
            client.ChannelDeleted += (sender, args) => { Logger.Info($"ChatClient: {client}", $"ChannelDeleted: {args.Channel.Sid}"); };
            client.ChannelInvited += (sender, args) => { Logger.Info($"ChatClient: {client}", $"ChannelInvited: {args.Channel.Sid}"); };
            client.ChannelJoined += (sender, args) => { Logger.Info($"ChatClient: {client}", $"ChannelJoined: {args.Channel.Sid}"); };
            client.ChannelSynchronizationChange += (sender, args) => { Logger.Info($"ChatClient: {client}", $"ChannelSynchronizationChange: {args.Channel.Sid}"); };
            client.ChannelUpdated += (sender, args) => {
                Logger.Info($"ChatClient: {client}", $"ChannelUpdated: {args.Channel.Sid}, reason: {args.Reason.Name()}");
                if (args.Reason == Channel.UpdateReason.Attributes)
                {
                    Logger.Info($"ChatClient: {client}", $"Channel attributes: {args.Channel.Attributes.ToDebugLog()}");
                }
            };

            client.ClientSynchronization += (sender, args) => { Logger.Info($"ChatClient: {client}", $"ClientSynchronization: {args.Status.Name()}"); };

            client.ConnectionStateChange += (sender, args) => { Logger.Info($"ChatClient: {client}", $"ConnectionStateChange: {args.State.Name()}"); };

            client.Error += (sender, args) => { Logger.Info($"ChatClient: {client}", $"Error: {args.ErrorInfo.Message}, code: {args.ErrorInfo.Code}, status: {args.ErrorInfo.Status}"); };

            client.NewMessageNotification += (sender, args) => { Logger.Info($"ChatClient: {client}", $"NewMessageNotification: ChannelSid: {args.ChannelSid}, MessageSid: {args.MessageSid}"); };
            client.AddedToChannelNotification += (sender, args) => { Logger.Info($"ChatClient: {client}", $"AddedToChannelNotification: ChannelSid: {args.ChannelSid}"); };
            client.InvitedToChannelNotification += (sender, args) => { Logger.Info($"ChatClient: {client}", $"InvitedToChannelNotification: ChannelSid: {args.ChannelSid}"); };
            client.RemovedFromChannelNotification += (sender, args) => { Logger.Info($"ChatClient: {client}", $"RemovedFromChannelNotification: ChannelSid: {args.ChannelSid}"); };
                  
            client.NotificationFailed += (sender, args) => { Logger.Info($"ChatClient: {client}", $"NotificationFailed: {args.ErrorInfo.Message}, code: {args.ErrorInfo.Code}, status: {args.ErrorInfo.Status}"); };
            client.NotificationSubscribed += (sender, args) => { Logger.Info($"ChatClient: {client}", $"NotificationSubscribed"); };

            client.UserSubscribed += (sender, args) => {
                Logger.Info($"ChatClient: {client}", $"UserSubscribed: {args.User.Identity}");
                Logger.Info($"ChatClient: {client}", $"User attributes: {args.User.Attributes.ToDebugLog()}");
            };
            client.UserUnsubscribed += (sender, args) => { Logger.Info($"ChatClient: {client}", $"UserUnsubscribed: {args.User.Identity}"); };
            client.UserUpdated += (sender, args) => {
                Logger.Info($"ChatClient: {client}", $"UserUpdated: {args.User.Identity}, reason: {args.Reason.Name()}");
                if (args.Reason == User.UpdateReason.Attributes)
                {
                    Logger.Info($"ChatClient: {client}", $"User attributes: {args.User.Attributes.ToDebugLog()}");
                }
            };

            client.TokenAboutToExpire += (sender, args) => { Logger.Info($"ChatClient: {client}", $"TokenAboutToExpire"); };
            client.TokenExpired += (sender, args) => { Logger.Info($"ChatClient: {client}", $"TokenExpired"); };
        }

        public void SubscribeToChannelEvents(Channel channel)
        {
            Logger.Info($"Channel: {channel.Sid}", $"SynchronizationChanged: {channel.SynchronizationStatus.Name()}");
            Logger.Info($"Channel: {channel.Sid}", $"Type: { channel.Type.Name() }");
            Logger.Info($"Channel: {channel.Sid}", $"Status: { channel.Status.Name() }");
            Logger.Info($"Channel: {channel.Sid}", $"LastConsumedMessageIndex: {channel.Messages.LastConsumedMessageIndex}");
            Logger.Info($"Channel: {channel.Sid}", $"NotificationLevel: {channel.NotificationLevel.Name()}");
            Logger.Info($"Channel: {channel.Sid}", $"Attributes: {channel.Attributes.ToDebugLog()}");

            channel.GetMessagesCount(new MessagesCountCallbackListener(channel));
            channel.GetMembersCount(new MembersCountCallbackListener(channel));
            channel.Messages.GetLastMessages(10, new MessagesCallbackListener(channel));

            foreach (Member member in channel.Members.MembersList)
            {
                Logger.Info($"Channel: {channel.Sid}", $"Got member: {member.Sid} with type {member.Type}");
            }

            channel.SynchronizationChanged += (sender, args) => { Logger.Info($"Channel: {channel.Sid}", $"SynchronizationChanged: {args.Channel.SynchronizationStatus.Name()}"); };

            channel.MemberAdded += (sender, args) => {
                Logger.Info($"Channel: {channel.Sid}", $"MemberAdded: {args.Member.Sid}");
                Logger.Info($"Channel: {channel.Sid}", $"Member attributes: {args.Member.Attributes.ToDebugLog()}");
            };
            channel.MemberDeleted += (sender, args) => { Logger.Info($"Channel: {channel.Sid}", $"MemberDeleted: {args.Member.Sid}"); };
            channel.MemberUpdated += (sender, args) => {
                Logger.Info($"Channel: {channel.Sid}", $"MemberUpdated: {args.Member.Sid}, reason: {args.Reason.Name()}");
                if (args.Reason == Member.UpdateReason.Attributes)
                {
                    Logger.Info($"Channel: {channel.Sid}", $"Member attributes: {args.Member.Attributes.ToDebugLog()}");
                }
            };

            channel.MessageAdded += (sender, args) => {
                Logger.Info($"Channel: {channel.Sid}", $"MessageAdded: {args.Message.Sid}");
                Logger.Info($"Channel: {channel.Sid}", $"Message attributes: {args.Message.Attributes.ToDebugLog()}");
            };
            channel.MessageDeleted += (sender, args) => { Logger.Info($"Channel: {channel.Sid}", $"MessageDeleted: {args.Message.Sid}"); };
            channel.MessageUpdated += (sender, args) => {
                Logger.Info($"Channel: {channel.Sid}", $"MessageUpdated: {args.Message.Sid}, reason: {args.Reason.Name()}");
                if (args.Reason == Message.UpdateReason.Attributes)
                {
                    Logger.Info($"Channel: {channel.Sid}", $"Message attributes: {args.Message.Attributes.ToDebugLog()}");
                }
            };


            channel.TypingEnded += (sender, args) => { Logger.Info($"Channel: {args.Channel.Sid}", $"TypingEnded: {args.Member.Sid}"); };
            channel.TypingStarted += (sender, args) => { Logger.Info($"Channel: {args.Channel.Sid}", $"TypingStarted: {args.Member.Sid}"); };
        }

        public TokenProvider GetTokenProvider()
        {
            return this.tokenProvider;
        }

        public void SetTokenProvider(TokenProvider tokenProvider)
        {
            this.tokenProvider = tokenProvider;
        }

        public void SetDeviceToken(object token)
        {
            deviceToken = (string) token;

            if (this.chatClient != null && deviceToken != null)
            {
                Logger.Info("ChatClient", "ChatClientCallbackListener: FCM token: " + deviceToken);
                this.chatClient.RegisterFCMToken(new ChatClient.FCMToken((string)deviceToken), new StatusListeners.RegisterFCMTokenStatusListener());
            }
        }

        public object GetDeviceToken()
        {
            return deviceToken;
        }

        public void CreateChannel(string friendlyName)
        {
            this.chatClient.Channels.CreateChannel(friendlyName, Channel.ChannelType.Private, new CreateChannelCallbackListener());
        }
    }
}
