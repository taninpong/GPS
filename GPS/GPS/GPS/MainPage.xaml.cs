using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GPS
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        //protected async override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    try
        //    {
        //        DependencyService.Get<IGetGPS>().GetGPS();
        //    }
        //    catch (FeatureNotSupportedException fnsEx)
        //    {
        //        Console.WriteLine(fnsEx);
        //    }
        //    catch (FeatureNotEnabledException fneEx)
        //    {
        //        Console.WriteLine(fneEx);
        //    }
        //    catch (PermissionException pEx)
        //    {
        //        Console.WriteLine(pEx);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }
        //}


        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            //var myPage1 = new Page1();
            //var navigationPage = new NavigationPage(myPage1);
            ////MainPage = navigationPage;
            //NavigationPage
            await Navigation.PushAsync(new Page1());
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Page2());
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(6));
            var x = await Geolocation.GetLocationAsync(request);
            //var location = await Geolocation.GetLastKnownLocationAsync();
            var location2 = new Location(16.43307340526658, 102.8255601788635);// central

            //TODO open navigation 
            var options = new MapLaunchOptions { NavigationMode = NavigationMode.Driving };
            var data = Xamarin.Essentials.Map.OpenAsync(location2, options);
            //Navigation.PushAsync(new Page3());
        }

        private void Button_Clicked_3(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Page3());
        }

        private  void GetStatus(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DetailGPS());
            //DependencyService.Get<IGetGPS>().GetGPS();
            //var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            //var check = DependencyService.Get<IGetGPS>().CheckStatus();
            //if (!check || status == PermissionStatus.Denied)
            //{
            //}
            //else
            //{
            //    Navigation.PushAsync(new Getlocation());
            //}
        }
    }
}
