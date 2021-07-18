using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Plugin.FirebaseRemoteConfig;

namespace FirebaseRemoteConfigSample {
    public partial class MainPage : ContentPage {
        const string WELCOME_MESSAGE_KEY = "welcome_message";
        const string SHOW_WELCOME_MESSAGE_KEY = "show_welcome_message";

        IFirebaseRemoteConfig _config;

        public MainPage() {
            InitializeComponent();
            _config = CrossFirebaseRemoteConfig.Current;
        }

        async void HandleFetchClicked(object sender, System.EventArgs e) {
            try {
                await _config.FetchAsync(5);
                _config.ActivateFetched();
                UpdateMessage();
            } catch(Exception ex) {
                welcomeLabel.Text = "";
                errorLabel.Text = ex.Message;
            }
        }

        void UpdateMessage() {
            errorLabel.Text = "";
            if (_config.GetBool(SHOW_WELCOME_MESSAGE_KEY)) {
                welcomeLabel.Text = _config.GetString(WELCOME_MESSAGE_KEY);
            } else {
                welcomeLabel.Text = "";
            }
        }

    }
}
