using System;
using System.Threading;

namespace Xamarin.Plugin.FirebaseRemoteConfig {
    public static class CrossFirebaseRemoteConfig {
        static readonly Lazy<IFirebaseRemoteConfig> _filePickerService = new Lazy<IFirebaseRemoteConfig>(() => CreateFirebaseRemoteConfig(), LazyThreadSafetyMode.PublicationOnly);

        public static IFirebaseRemoteConfig Current => Instance(_filePickerService);

        static IFirebaseRemoteConfig CreateFirebaseRemoteConfig() {
        #if __MOBILE__
            return new FirebaseRemoteConfig();
        #else
            return null;
        #endif
        }

        #region Private methods

        static T Instance<T>(Lazy<T> lazy) where T : class {
            var ret = lazy.Value;
            if (ret == null) {
                throw NotImplementedInReferenceAssembly();
            }
            return ret;
        }

        internal static Exception NotImplementedInReferenceAssembly() {
            return new NotImplementedException("This functionality is not implemented in the portable version of this assembly." +
                " You should reference the Nuget package from your main application project in order to reference the platform-specific implementation");
        }

        #endregion
    }
}
