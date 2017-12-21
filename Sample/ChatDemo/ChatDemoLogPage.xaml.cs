using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ChatDemo
{
    public partial class ChatDemoLogPage : ContentPage
    {
        public ObservableCollection<LogLine> LogLines { get; set; } = new ObservableCollection<LogLine>();

        public ChatDemoLogPage()
        {
            InitializeComponent();
            LogListView.ItemsSource = LogLines;

            var twilioChatHelper = DependencyService.Get<ITwilioChatHelper>();
            twilioChatHelper.LogLine += (object sender, LogLineEventArgs e) => 
            {
                LogLines.Insert(0, e.LogLine);
            };
        }
    }
}
