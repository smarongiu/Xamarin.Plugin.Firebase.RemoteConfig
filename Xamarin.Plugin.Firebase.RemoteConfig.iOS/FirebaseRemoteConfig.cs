using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.RemoteConfig;
using Newtonsoft.Json;
using Xamarin.Plugin.Firebase.RemoteConfig.Exceptions;

namespace Xamarin.Plugin.FirebaseRemoteConfig
{
    public class FirebaseRemoteConfig : IFirebaseRemoteConfig
    {
        private readonly RemoteConfig _config;

        public FirebaseRemoteConfig()
        {
            _config = RemoteConfig.SharedInstance;
        }


        #region Public methods

        public void Init() => Init(null);

        public void Init(string defaultConfigResourceName = null)
        {
            _config.ConfigSettings = new RemoteConfigSettings();
            if (!string.IsNullOrWhiteSpace(defaultConfigResourceName))
            {
                _config.SetDefaults(defaultConfigResourceName);
            }
        }

        public async Task FetchAsync(long cacheExpiration)
        {
            var status = await _config.FetchAsync(cacheExpiration);
            if (status != RemoteConfigFetchStatus.Success)
            {
                throw new FirebaseRemoteConfigFetchFailedException($"status: {status}");
            }
        }

        public async Task ActivateFetched()
        {
            try
            {
                await _config.ActivateAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task FetchAndActiveAsync()
        {
            try
            {
                await _config.FetchAndActivateAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        public bool GetBool(string key) => _config[key].BoolValue;

        public double GetDouble(string key) => _config[key].NumberValue.DoubleValue;

        public long GetLong(string key) => _config[key].NumberValue.LongValue;

        public string GetString(string key) => _config[key].StringValue;

        public ICollection<string> GetKeysByPrefix(string prefix)
        {
            return _config.GetKeys(prefix).ToArray().Select(x => x.ToString()).ToList();
        }

        public T GetObject<T>(string key) where T : class
        {
            try
            {
                var data = _config[key].StringValue;
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