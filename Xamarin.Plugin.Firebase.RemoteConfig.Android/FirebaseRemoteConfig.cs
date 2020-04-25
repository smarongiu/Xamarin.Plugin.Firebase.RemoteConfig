using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.CurrentActivity;
using Xamarin.Plugin.Firebase.RemoteConfig.Exceptions;
using AndroidFirebaseRemoteConfig = Firebase.RemoteConfig.FirebaseRemoteConfig;
using AndroidFirebaseRemoteConfigSettings = Firebase.RemoteConfig.FirebaseRemoteConfigSettings;

namespace Xamarin.Plugin.FirebaseRemoteConfig {
    public class FirebaseRemoteConfig : IFirebaseRemoteConfig {
        AndroidFirebaseRemoteConfig _config;

        public FirebaseRemoteConfig() {
            _config = AndroidFirebaseRemoteConfig.Instance;
        }


        #region Public methods

        public void Init(bool developerModeEnabled = false) => Init(null, developerModeEnabled);

        public void Init(string defaultConfigResourceName = null, bool developerModeEnabled = false) {
            var settings = new AndroidFirebaseRemoteConfigSettings.Builder().SetDeveloperModeEnabled(developerModeEnabled).Build();
            _config.SetConfigSettings(settings);
            if (!string.IsNullOrWhiteSpace(defaultConfigResourceName)) {
                var ctx = CrossCurrentActivity.Current.AppContext;
                var resId = ctx.Resources.GetIdentifier(defaultConfigResourceName, "xml", ctx.PackageName);
                _config.SetDefaults(resId);
            }
        }

        public async Task FetchAsync(long cacheExpiration) {
            try {
                await _config.FetchAsync(cacheExpiration);
            } catch(Exception ex) {
                throw new FirebaseRemoteConfigFetchFailedException("[FirebaseRemoteConfig] Fetch failed", ex);
            }
        }

        public void ActivateFetched() => _config.ActivateFetched();

        public bool GetBool(string key) => _config.GetBoolean(key);

        public byte[] GetBytes(string key) => _config.GetByteArray(key);

        public double GetDouble(string key) => _config.GetDouble(key);

        public long GetLong(string key) => _config.GetLong(key);

        public string GetString(string key) => _config.GetString(key);

        public ICollection<string> GetKeysByPrefix(string prefix) => _config.GetKeysByPrefix(prefix);

        public T GetObject<T>(string key) where T : class
        {
            try
            {
                var data = _config.GetString(key);
                return JsonConvert.DeserializeObject<T>(data, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                    Formatting = Formatting.Indented
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }

        #endregion
    }
}
