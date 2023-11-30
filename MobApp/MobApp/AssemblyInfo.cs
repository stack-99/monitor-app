using Android.App;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
#if DEBUG
[assembly: Application(UsesCleartextTraffic = true, Debuggable = true)]
#else
[assembly: Application(UsesCleartextTraffic=true, Icon = "@drawable/logo")]
#endif
