using System;
namespace Xamarin.Plugin.Firebase.RemoteConfig.Exceptions {
    public class FirebaseRemoteConfigFetchFailedException : Exception {
        public FirebaseRemoteConfigFetchFailedException(string message) : base(message) {            
        }

        public FirebaseRemoteConfigFetchFailedException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}
