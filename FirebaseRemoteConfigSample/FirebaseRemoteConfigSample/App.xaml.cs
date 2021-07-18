using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Plugin.FirebaseRemoteConfig;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace FirebaseRemoteConfigSample {
    public partial class App : Application {
        public App() {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart() {
            CrossFirebaseRemoteConfig.Current.Init("remote_config_defaults"); //switch this to false for production builds
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}
