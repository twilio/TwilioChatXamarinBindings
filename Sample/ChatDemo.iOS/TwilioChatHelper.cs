using System;
using System.Threading.Tasks;
using ChatDemo.iOS;
using ChatDemo.iOS.Delegates;
using ChatDemo.Shared;
using Foundation;
using Twilio.Chat.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(TwilioChatHelper))]
namespace ChatDemo.iOS
{
    public class TwilioChatHelper : TwilioChatDelegate, ITwilioChatHelper
    {
        private TokenProvider tokenProvider;

        private NSData deviceToken = null;

        public event LogLineEventHandler LogLine;

        private TwilioChatClient chatClient;

        protected virtual void OnLogLine(LogLineEventArgs e)
        {
            if (LogLine != null)
            {
                LogLine(this, e);
            }
        }

        public TwilioChatHelper()
        {
        }

        void HandleChannelDescriptorPaginatorCompletion(Result result, ChannelDescriptorPaginator channelDescriptorPaginator)
        {
            if (result.IsSuccessful)
            {
                Logger.Info(
                    $"ChatClient: {chatClient}",
                    $"Processing next page of public channel descriptors... (has next page: {channelDescriptorPaginator.HasNextPage})");
                foreach (ChannelDescriptor item in channelDescriptorPaginator.Items)
                {
                    Logger.Info($"ChatClient: {chatClient}", $"public channel SID: {item.Sid}");
                }
                if (channelDescriptorPaginator.HasNextPage)
                {
                    channelDescriptorPaginator.RequestNextPageWithCompletion(HandleChannelDescriptorPaginatorCompletion);
                }
            }
            else
            {
                Logger.Error($"ChatClient: {chatClient}",
                             $"Error: {result.Error}, " +
                             $"code: {result.ResultCode}, " +
                             $"text: {result.ResultText}");
            }
        }

        public void CreateClient(string chatToken)
        {
            this.chatClient = null;
            TwilioChatClient.LogLevel = LogLevel.Info;
            var properties = new TwilioChatClientProperties();
            properties.CommandTimeout = (ulong)CommandTimeout.Min;
            TwilioChatClient.ChatClientWithToken(
                chatToken, properties, this, (result, chatClient) =>
                {
                    if (result.IsSuccessful)
                    {
                        Logger.Info("TwilioChatHelper", "Client created");
                        this.RegisterForNotificationsFromTwilioChatService(chatClient);
                        this.chatClient = chatClient;
                        chatClient.ChannelsList
                                            .PublicChannelDescriptorsWithCompletion(HandleChannelDescriptorPaginatorCompletion);

                        CreateChannel(System.Guid.NewGuid().ToString());
                    }
                    else
                    {
                        Logger.Error("TwilioChatHelper", $"Can't create client: {result.Error.Description}");
                    }

                });
        }

        public void SetTokenProvider(TokenProvider tokenProvider)
        {
            this.tokenProvider = tokenProvider;
        }

        public TokenProvider GetTokenProvider()
        {
            return this.tokenProvider;
        }

        public void FireLogLineEvent(LogLine logLine)
        {
            OnLogLine(new LogLineEventArgs(logLine));
        }

        public void SetDeviceToken(object token)
        {
            this.deviceToken = (NSData) token;
            if (this.chatClient != null)
            {
                this.RegisterForNotificationsFromTwilioChatService(this.chatClient);
            }
        }

        public object GetDeviceToken()
        {
            return this.deviceToken;
        }

        private void RegisterForNotificationsFromTwilioChatService(TwilioChatClient chatClient)
        {
            if (this.deviceToken != null)
            {
                chatClient.RegisterWithNotificationToken(
                    this.deviceToken, (Result registerResult) =>
                    {
                        if (registerResult.IsSuccessful)
                        {
                            Logger.Info("TwilioChatHelper", "Registered to remote notifications from Twilio Chat service");
                        }
                        else
                        {
                            Logger.Error("TwilioChatHelper",
                                        "Couldn't register to remote notifications " +
                                        $"from Twilio Chat service: {registerResult.Error.Description}");

                        }
                    });
            };
        }

        public void CreateChannel(string friendlyName)
        {
            NSDictionary dict = new NSDictionary("key", "value");
            var attributes = new JsonAttributes().WithDictionary(dict);

            var options = new NSDictionary(
                Constants.ChannelOptionFriendlyName, NSObject.FromObject(friendlyName),
                Constants.ChannelOptionAttributes, NSObject.FromObject(attributes));

            this.chatClient.ChannelsList.CreateChannelWithOptions(options, (Result result, Channel channel) => {
                Logger.Info($"TwilioChatHelper", $"Created channel with attributes and sid {channel.Sid}");
            });
        }
    }
}
