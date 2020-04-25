using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xamarin.Plugin.FirebaseRemoteConfig {
    public interface IFirebaseRemoteConfig {
        /// <summary>
        /// Initializes the service.
        /// </summary>
        /// <param name="defaultConfigResourceName">If set, load defaults from this resource</param>
        /// <param name="developerModeEnabled">If set to <c>true</c> developer mode is enabled.</param>
        void Init(string defaultConfigResourceName = null, bool developerModeEnabled = false);

        /// <summary>
        /// Initializes the service without default config.
        /// </summary>
        /// <param name="developerModeEnabled">If set to <c>true</c> developer mode is enabled.</param>
        void Init(bool developerModeEnabled = false);

        /// <summary>
        /// Fetchs the remote config.
        /// </summary>
        /// <param name="cacheExpiration">Cache expiration in seconds.</param>
        /// <exception cref="FirebaseRemoteConfigFetchFailedException">when fetch fails.</exception>
        Task FetchAsync(long cacheExpiration);

        /// <summary>
        /// Activates the last fetched config.
        /// </summary>
        void ActivateFetched();

        /// <summary>
        /// Gets the value with specified key as string.
        /// </summary>
        string GetString(string key);

        /// <summary>
        /// Gets the value with specified key as byte array.
        /// </summary>
        byte[] GetBytes(string key);

        /// <summary>
        /// Gets the value with specified key as boolean.
        /// </summary>
        bool GetBool(string key);

        /// <summary>
        /// Gets the value with specified key as long.
        /// </summary>
        long GetLong(string key);

        /// <summary>
        /// Gets the value with specified key as double.
        /// </summary>
        double GetDouble(string key);

        /// <summary>
        /// Gets all keys by prefix.
        /// </summary>
        ICollection<string> GetKeysByPrefix(string prefix);

        /// <summary>
        /// Get json and parse to specific class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetObject<T>(string key) where T : class;
    }

}
