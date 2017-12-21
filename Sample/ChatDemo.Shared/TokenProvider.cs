using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChatDemo.Shared
{
    public class TokenProvider
    {
        private string tokenProviderUrl;
        private string lastIdentityUsed;

        public TokenProvider(string tokenProviderUrl)
        {
            this.tokenProviderUrl = tokenProviderUrl;
        }

        public async Task<string> GetTokenWithLastIdentityUsed()
        {
            return await this.GetToken(this.lastIdentityUsed);
        }
        public async Task<string> GetToken(string identity)
        {
            this.lastIdentityUsed = identity;
            var client = new System.Net.Http.HttpClient();
            string url = $"{tokenProviderUrl}?identity={identity}";
            if (Device.RuntimePlatform.Equals(Device.Android))
            {
                url += $"&pushChannel=fcm";
            }
            else if (Device.RuntimePlatform.Equals(Device.iOS))
            {
                url += $"&pushChannel=apn";
            };

            try
            {
                var response = await client.GetAsync(url);
                if (response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
                {
                    var token = System.Text.Encoding.UTF8.GetString(await response.Content.ReadAsByteArrayAsync());
                    Logger.Info("TokenProvider", $"Got chat token: {token}");
                    return token;
                }
                else
                {
                    Logger.Error("TokenProvider", $"Can't get token, token generator responded with {response}");
                    throw new TokenProviderException(response.ToString());
                }
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                Logger.Error("TokenProvider", $"Can't get token, token generator encountered exception {ex.Message}");
                throw new TokenProviderException(ex.Message, ex);
            }
        }
    }

    [Serializable]
    public class TokenProviderException : Exception
    {
        public TokenProviderException()
        {
        }

        public TokenProviderException(string message) : base(message)
        {
        }

        public TokenProviderException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TokenProviderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
