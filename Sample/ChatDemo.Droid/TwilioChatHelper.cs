using System;
using Android.Content;
using Android.Support.V4.Content;
using ChatDemo.Droid;
using ChatDemo.ExtensionHelpers;
using ChatDemo.Shared;
using Com.Twilio.Chat;

[assembly: Xamarin.Forms.Dependency(typeof(TwilioChatHelper))]
namespace ChatDemo.Droid
{
    public class ChatClientListener : Java.Lang.Object, IChatClientListener
    {
        private readonly ChatClient parent;

        public ChatClientListener(ChatClient parent) => this.parent = parent;

        public void OnAddedToChannelNotification(string channelSid) => this.AddedToChannelNotification(parent, new AddedToChannelNotificationEventArgs(channelSid));
        public void OnChannelAdded(Channel channel) => this.ChannelAdded(parent, new ChannelAddedEventArgs(channel));
        public void OnChannelDeleted(Channel p0) => this.ChannelDeleted(parent, new ChannelDeletedEventArgs(p0));
        public void OnChannelInvited(Channel p0) => this.ChannelInvited(parent, new ChannelInvitedEventArgs(p0));
        public void OnChannelJoined(Channel p0) => this.ChannelJoined(parent, new ChannelJoinedEventArgs(p0));
        public void OnChannelSynchronizationChange(Channel p0) => this.ChannelSynchronizationChange(parent, new ChannelSynchronizationChangeEventArgs(p0));
        public void OnChannelUpdated(Channel p0, Channel.UpdateReason p1) => this.ChannelUpdated(parent, new ChannelUpdatedEventArgs(p0, p1));
        public void OnClientSynchronization(ChatClient.SynchronizationStatus p0) => this.ClientSynchronization(parent, new ClientSynchronizationEventArgs(p0));
        public void OnConnectionStateChange(ChatClient.ClientConnectionState p0) => this.ConnectionStateChange(parent, new ConnectionStateChangeEventArgs(p0));
        public void OnError(ErrorInfo p0) => this.Error(parent, new ErrorEventArgs(p0));
        public void OnInvitedToChannelNotification(string p0) => this.InvitedToChannelNotification(parent, new InvitedToChannelNotificationEventArgs(p0));
        public void OnNewMessageNotification(string p0, string p1, long p2) => this.NewMessageNotification(parent, new NewMessageNotificationEventArgs(p0, p1, p2));
        public void OnNotificationFailed(ErrorInfo p0) => this.NotificationFailed(parent, new NotificationFailedEventArgs(p0));
        public void OnNotificationSubscribed() => this.NotificationSubscribed(parent, null);
        public void OnRemovedFromChannelNotification(string p0) => this.RemovedFromChannelNotification(parent, new RemovedFromChannelNotificationEventArgs(p0));
        public void OnTokenAboutToExpire() => this.TokenAboutToExpire(parent, null);
        public void OnTokenExpired() => this.TokenExpired(parent, null);
        public void OnUserSubscribed(User p0) => this.UserSubscribed(parent, new UserSubscribedEventArgs(p0));
        public void OnUserUnsubscribed(User p0) => this.UserUnsubscribed(parent, new UserUnsubscribedEventArgs(p0));
        public void OnUserUpdated(User p0, User.UpdateReason p1) => this.UserUpdated(parent, new UserUpdatedEventArgs(p0, p1));

        public EventHandler<ChannelSynchronizationChangeEventArgs> ChannelSynchronizationChange;
        public EventHandler<ClientSynchronizationEventArgs> ClientSynchronization;
        public EventHandler<ConnectionStateChangeEventArgs> ConnectionStateChange;
        public EventHandler<ErrorEventArgs> Error;

        public EventHandler<ChannelAddedEventArgs> ChannelAdded;
        public EventHandler<ChannelDeletedEventArgs> ChannelDeleted;
        public EventHandler<ChannelInvitedEventArgs> ChannelInvited;
        public EventHandler<ChannelJoinedEventArgs> ChannelJoined;
        public EventHandler<ChannelUpdatedEventArgs> ChannelUpdated;

        public EventHandler NotificationSubscribed;
        public EventHandler<NotificationFailedEventArgs> NotificationFailed;
        public EventHandler<NewMessageNotificationEventArgs> NewMessageNotification;
        public EventHandler<AddedToChannelNotificationEventArgs> AddedToChannelNotification;
        public EventHandler<InvitedToChannelNotificationEventArgs> InvitedToChannelNotification;
        public EventHandler<RemovedFromChannelNotificationEventArgs> RemovedFromChannelNotification;

        public EventHandler TokenAboutToExpire;
        public EventHandler TokenExpired;

        public EventHandler<UserSubscribedEventArgs> UserSubscribed;
        public EventHandler<UserUnsubscribedEventArgs> UserUnsubscribed;
        public EventHandler<UserUpdatedEventArgs> UserUpdated;
    }

    public partial class TwilioChatHelper : ITwilioChatHelper
    {
        private string deviceToken;

        private TokenProvider tokenProvider;

        private Com.Twilio.Chat.ChatClient chatClient;
        private ChatClientListener delegateForwarder;

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

        public void SubscribeToClientEvents()
        {
            var client = this.chatClient;
            this.delegateForwarder = new ChatClientListener(this.chatClient);

            this.delegateForwarder.ChannelAdded += (sender, args) => { this.SubscribeToChannelEvents(args.Channel); };
            this.delegateForwarder.ChannelInvited += (sender, args) => { this.SubscribeToChannelEvents(args.Channel); };
            this.delegateForwarder.ChannelJoined += (sender, args) => { this.SubscribeToChannelEvents(args.Channel); };

            this.delegateForwarder.ChannelAdded += (sender, args) => { Logger.Info($"ChatClient: {client}", $"ChannelAdded: {args.Channel.Sid}"); };
            this.delegateForwarder.ChannelDeleted += (sender, args) => { Logger.Info($"ChatClient: {client}", $"ChannelDeleted: {args.Channel.Sid}"); };
            this.delegateForwarder.ChannelInvited += (sender, args) => { Logger.Info($"ChatClient: {client}", $"ChannelInvited: {args.Channel.Sid}"); };
            this.delegateForwarder.ChannelJoined += (sender, args) => { Logger.Info($"ChatClient: {client}", $"ChannelJoined: {args.Channel.Sid}"); };
            this.delegateForwarder.ChannelSynchronizationChange += (sender, args) => { Logger.Info($"ChatClient: {client}", $"ChannelSynchronizationChange: {args.Channel.Sid}"); };
            this.delegateForwarder.ChannelUpdated += (sender, args) => {
                Logger.Info($"ChatClient: {client}", $"ChannelUpdated: {args.Channel.Sid}, reason: {args.Reason.Name()}");
                if (args.Reason == Channel.UpdateReason.Attributes)
                {
                    Logger.Info($"ChatClient: {client}", $"Channel attributes: {args.Channel.Attributes.ToDebugLog()}");
                }
            };

            this.delegateForwarder.ClientSynchronization += (sender, args) => { Logger.Info($"ChatClient: {client}", $"ClientSynchronization: {args.Status.Name()}"); };

            this.delegateForwarder.ConnectionStateChange += (sender, args) => { Logger.Info($"ChatClient: {client}", $"ConnectionStateChange: {args.State.Name()}"); };

            this.delegateForwarder.Error += (sender, args) => { Logger.Info($"ChatClient: {client}", $"Error: {args.ErrorInfo.Message}, code: {args.ErrorInfo.Code}, status: {args.ErrorInfo.Status}"); };

            this.delegateForwarder.NewMessageNotification += (sender, args) => { Logger.Info($"ChatClient: {client}", $"NewMessageNotification: ChannelSid: {args.ChannelSid}, MessageSid: {args.MessageSid}"); };
            this.delegateForwarder.AddedToChannelNotification += (sender, args) => { Logger.Info($"ChatClient: {client}", $"AddedToChannelNotification: ChannelSid: {args.ChannelSid}"); };
            this.delegateForwarder.InvitedToChannelNotification += (sender, args) => { Logger.Info($"ChatClient: {client}", $"InvitedToChannelNotification: ChannelSid: {args.ChannelSid}"); };
            this.delegateForwarder.RemovedFromChannelNotification += (sender, args) => { Logger.Info($"ChatClient: {client}", $"RemovedFromChannelNotification: ChannelSid: {args.ChannelSid}"); };

            this.delegateForwarder.NotificationFailed += (sender, args) => { Logger.Info($"ChatClient: {client}", $"NotificationFailed: {args.ErrorInfo.Message}, code: {args.ErrorInfo.Code}, status: {args.ErrorInfo.Status}"); };
            this.delegateForwarder.NotificationSubscribed += (sender, args) => { Logger.Info($"ChatClient: {client}", $"NotificationSubscribed"); };

            this.delegateForwarder.UserSubscribed += (sender, args) => {
                Logger.Info($"ChatClient: {client}", $"UserSubscribed: {args.User.Identity}");
                Logger.Info($"ChatClient: {client}", $"User attributes: {args.User.Attributes.ToDebugLog()}");
            };
            this.delegateForwarder.UserUnsubscribed += (sender, args) => { Logger.Info($"ChatClient: {client}", $"UserUnsubscribed: {args.User.Identity}"); };
            this.delegateForwarder.UserUpdated += (sender, args) => {
                Logger.Info($"ChatClient: {client}", $"UserUpdated: {args.User.Identity}, reason: {args.Reason.Name()}");
                if (args.Reason == User.UpdateReason.Attributes)
                {
                    Logger.Info($"ChatClient: {client}", $"User attributes: {args.User.Attributes.ToDebugLog()}");
                }
            };

            this.delegateForwarder.TokenAboutToExpire += (sender, args) => { Logger.Info($"ChatClient: {client}", $"TokenAboutToExpire"); };
            this.delegateForwarder.TokenExpired += (sender, args) => { Logger.Info($"ChatClient: {client}", $"TokenExpired"); };
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
