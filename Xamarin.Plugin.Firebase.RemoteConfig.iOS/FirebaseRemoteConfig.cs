using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.RemoteConfig;
using Xamarin.Plugin.Firebase.RemoteConfig.Exceptions;

namespace Xamarin.Plugin.FirebaseRemoteConfig {
    public class FirebaseRemoteConfig : IFirebaseRemoteConfig {
        RemoteConfig _config;

        public FirebaseRemoteConfig() {
            _config = RemoteConfig.SharedInstance;
        }


        #region Public methods

        public void Init(bool developerModeEnabled = false) => Init(null, developerModeEnabled);

        public void Init(string defaultConfigResourceName = null, bool developerModeEnabled = false) {
            _config.ConfigSettings = new RemoteConfigSettings(developerModeEnabled);
            if (!string.IsNullOrWhiteSpace(defaultConfigResourceName)) {
                _config.SetDefaults(defaultConfigResourceName);
            }
        }

        public async Task FetchAsync(long cacheExpiration) {
            var status = await _config.FetchAsync(cacheExpiration);
            if (status != RemoteConfigFetchStatus.Success) {
                throw new FirebaseRemoteConfigFetchFailedException($"status: {status}");
            }
        }

        public void ActivateFetched() => _config.ActivateFetched();


        public bool GetBool(string key) => _config[key].BoolValue;

        public byte[] GetBytes(string key) => _config[key].DataValue.ToArray();

        public double GetDouble(string key) => _config[key].NumberValue.DoubleValue;

        public long GetLong(string key) => _config[key].NumberValue.LongValue;

        public string GetString(string key) => _config[key].StringValue;

        public ICollection<string> GetKeysByPrefix(string prefix) {
            return _config.GetKeys(prefix).ToArray().Select(x => x.ToString()).ToList();
        }

        #endregion

    }
}
