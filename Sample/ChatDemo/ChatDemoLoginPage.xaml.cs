using System;
using Xamarin.Forms;
using ChatDemo.Shared;

namespace ChatDemo
{
    public partial class ChatDemoLoginPage : ContentPage
    {
        public ChatDemoLoginPage()
        {
            InitializeComponent();
            //TODO: change this to your token provider url or leave blank
            this.tokenProviderUrlEntry.Text = "";
            identityEntry.Completed += (s, e) =>
            {
                tokenProviderUrlEntry.Focus();
            };

            tokenProviderUrlEntry.Completed += (s, e) =>
            {
                loginButton.Focus();
            };

            loginButton.Clicked += OnLoginButtonClicked;
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            DisableLoginElements(true);
            string errors = "";
            if (string.IsNullOrEmpty(identityEntry.Text))
            {
                errors = "Please provide identity";
            }

            if (string.IsNullOrEmpty(tokenProviderUrlEntry.Text))
            {
                errors += "\nPlease provide token provider URL";
            }

            if (!string.IsNullOrEmpty(errors))
            {
                await DisplayAlert("Can't login", errors, "OK");
                DisableLoginElements(false);
                return;
            }

            activityIndicator.IsRunning = true;
            var tokenProvider = new TokenProvider(tokenProviderUrlEntry.Text);

            var twilioChatHelper = DependencyService.Get<ITwilioChatHelper>();
            twilioChatHelper.SetTokenProvider(new TokenProvider(tokenProviderUrlEntry.Text));
            try
            {
                string chatToken = await twilioChatHelper.GetTokenProvider().GetToken(identityEntry.Text);
                Navigation.InsertPageBefore(new ChatDemoLogPage(), this);
                twilioChatHelper.CreateClient(chatToken);
                await Navigation.PopAsync();

            }
            catch (TokenProviderException ex)
            {
                await DisplayAlert("Can't get token", $"{ex}", "OK");
                DisableLoginElements(false);
                activityIndicator.IsRunning = false;
            }
        }

        private void DisableLoginElements(bool disable)
        {
            identityEntry.IsEnabled = !disable;
            tokenProviderUrlEntry.IsEnabled = !disable;
            loginButton.IsEnabled = !disable;
        }
    }
}
