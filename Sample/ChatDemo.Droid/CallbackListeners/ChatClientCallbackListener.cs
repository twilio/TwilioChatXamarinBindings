using ChatDemo.Droid;
using ChatDemo.Shared;
using Com.Twilio.Chat;

namespace ChatDemo.Droid
{
    public class ChatClientCallbackListener : CallbackListener<ChatClient>
    {
        private TwilioChatHelper twilioChatHelper;

        public ChatClientCallbackListener(TwilioChatHelper twilioChatHelper)
        {
            this.twilioChatHelper = twilioChatHelper;
        }

        public override void OnError(ErrorInfo errorInfo)
        {
            Logger.Error("ChatClientCallbackListener", errorInfo);
        }

        public override void OnSuccess(ChatClient result)
        {
            this.twilioChatHelper.SetChatClient(result);

            Logger.Info($"ChatClient: {result}", $"Got ChatClient instance: {result}");
            Logger.Info($"ChatClient: {result}", $"ChatClient initialized with identity {result.MyIdentity}");

            this.twilioChatHelper.SubscribeToClientEvents(result);

            var subscribedChannels = result.Channels.GetSubscribedChannelsSortedBy(Channels.SortCriterion.LastMessage, Channels.SortOrder.Descending);
            foreach (Channel channel in subscribedChannels)
            {
                Logger.Info($"ChatClient: {result}", $"Got user channel: {channel.Sid}");

                if (channel.SynchronizationStatus.Equals(Channel.ChannelSynchronizationStatus.All))
                {
                    this.twilioChatHelper.SubscribeToChannelEvents(channel);
                }
                else
                {
                    channel.SynchronizationChanged += ChannelSynchronizationChanged;
                }
            }

            result.Channels.GetPublicChannelsList(new PublicChannelPaginatorCallbackListener(result));

            Logger.Info($"ChatClient: {result}", "ChatClientCallbackListener: FCM token: " + this.twilioChatHelper.GetDeviceToken());
            result.RegisterFCMToken(new ChatClient.FCMToken((string) this.twilioChatHelper.GetDeviceToken()), new StatusListeners.RegisterFCMTokenStatusListener());
        }

        void ChannelSynchronizationChanged(object sender, SynchronizationChangedEventArgs args)
        {
            if (args.Channel.SynchronizationStatus.Equals(Channel.ChannelSynchronizationStatus.All))
            {
                Logger.Info($"Channel: {args.Channel.Sid}", $"Got synchronized user channel: {args.Channel.Sid}");
                args.Channel.SynchronizationChanged -= ChannelSynchronizationChanged;
                this.twilioChatHelper.SubscribeToChannelEvents(args.Channel);
            }
        }
    }
}
