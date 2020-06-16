using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;
using Android.Content;
using Xamarin.Essentials;
using Android.Net;
using Com.Xamarin.Formsviewgroup;
using Xamarin.Forms;

namespace GPS.Droid
{
    [Activity(Label = "GPS", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        public async override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            LocationManager LM = (LocationManager)Android.App.Application.Context.GetSystemService(Context.LocationService);
            //|| grantResults[0] == Permission.Denied
            if (LM.IsProviderEnabled(LocationManager.GpsProvider) == false)
            {
                Intent intent = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
                intent.AddFlags(ActivityFlags.NewTask);
                intent.AddFlags(ActivityFlags.MultipleTask);
                Android.App.Application.Context.StartActivity(intent);
            }
            //string permission = Android.Manifest.Permission.AccessFineLocation;
            if (grantResults[0] == Permission.Denied)
            {
                //var x = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                Intent intent = new Intent(Android.Provider.Settings.ActionApplicationDetailsSettings,
                    Android.Net.Uri.Parse("package:" + Android.App.Application.Context.PackageName)
                    );
                intent.AddFlags(ActivityFlags.NewTask);
                intent.AddFlags(ActivityFlags.MultipleTask);
                Android.App.Application.Context.StartActivity(intent);
            }
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}