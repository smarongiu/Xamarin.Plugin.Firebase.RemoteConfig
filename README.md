# Xamarin.Plugin.Firebase.RemoteConfig
Cross-platform library for using Firebase RemoteConfig in Xamarin Forms applications.
The plugin provides a simple facade to official Xamarin bindings for Firebase native libs.

## Platform support
This plugin is compatible with iOS and Android.

## Quickstart

### Setup
- Setup your Firebase project on https://console.firebase.google.com
- NuGet package available: https://www.nuget.org/packages/Xam.Plugin.FirebaseRemoteConfig
- Install the nuget package into your PCL/Forms project and client projects.

### API Usage
Call **CrossFirebaseRemoteConfig.Current** from PCL or client code to use APIs.
```csharp
/// <summary>
/// Initializes the service.
/// </summary>
/// <param name="defaultConfigResourceName">If set, load defaults from this resource</param>
void Init(string defaultConfigResourceName = null);

/// <summary>
/// Initializes the service without default config.
/// </summary>
void Init();

/// <summary>
/// Fetchs the remote config.
/// </summary>
/// <param name="cacheExpiration">Cache expiration in seconds.</param>
/// <exception cref="FirebaseRemoteConfigFetchFailedException">when fetch fails.</exception>
Task FetchAsync(long cacheExpiration);

/// <summary>
/// Activates the last fetched config.
/// </summary>
Task ActivateFetched();

/// <summary>
/// Fetch and active last fetched config
/// </summary>
/// <returns></returns>
Task FetchAndActiveAsync();

/// <summary>
/// Gets the value with specified key as string.
/// </summary>
string GetString(string key);

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
```

### iOS
- From the Firebase Console, add your iOS app to your project. 
- Download and add the generated GoogleService-Info.plist file to your app in the root folder and mark it as "BundleResource".
- Add this line to AppDelegate.cs: 
```csharp
Firebase.Core.App.Configure();
CrossFirebaseRemoteConfig.Current.Init("my_config_defaults");
``` 
- you can add a default config file as a plist, put it in the root folder and mark it as "BundleResource".

### Android
- From the Firebase Console, add your Android app to your project. 
- Download and add the generated google-services.json file to your app in the root folder and mark it as "GoogleServicesJson".
- Add the nuget package https://www.nuget.org/packages/Plugin.CurrentActivity and follow installation guide.
- Add these lines to MainActivity.cs, after <code>base.OnCreate(bundle)</code>:
```csharp
Firebase.FirebaseApp.InitializeApp(this);
CrossFirebaseRemoteConfig.Current.Init("my_config_defaults");
```
- you can add a default config file as xml resource (under Resources/xml). 

## License
Licensed under MIT, see license file.
