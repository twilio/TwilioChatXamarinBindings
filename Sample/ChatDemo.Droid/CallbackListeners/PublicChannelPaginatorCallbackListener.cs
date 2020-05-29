using ChatDemo.Droid;
using ChatDemo.ExtensionHelpers;
using ChatDemo.Shared;
using Com.Twilio.Chat;

namespace ChatDemo.Droid
{
    public class PublicChannelPaginatorCallbackListener : CallbackListener<Paginator<ChannelDescriptor>>
    {
        private ChatClient chatClient;

        public PublicChannelPaginatorCallbackListener(ChatClient result)
        {
            this.chatClient = result;
        }

        public override void OnError(ErrorInfo errorInfo)
        {
            Logger.Error($"ChatClient: {chatClient}", errorInfo);
        }

        public override void OnSuccess(Paginator<ChannelDescriptor> result)
        {
            Logger.Info($"ChatClient: {chatClient}", $"Processing next page (page size {result.PageSize}) of public channel descriptors...");
            foreach (ChannelDescriptor item in result.Items)
            {
                Logger.Info($"ChatClient: {chatClient}", $"public channel SID: {item.Sid}");
                item.GetChannel(new PublicChannelCallbackListener());
            }

            if (result.HasNextPage)
            {
                result.RequestNextPage(new PublicChannelPaginatorCallbackListener(chatClient));
            }
        }
    }

    internal class PublicChannelCallbackListener : CallbackListener<Channel>
    {
        public override void OnSuccess(Channel result) {
            Logger.Info($"ChannelDescriptor: {result.Sid}", $"Got public channel from descriptor: {result.Sid}");
            Logger.Info($"ChannelDescriptor: {result.Sid}", $"attributes: {result.Attributes.ToDebugLog()}");
        }
    }
}
