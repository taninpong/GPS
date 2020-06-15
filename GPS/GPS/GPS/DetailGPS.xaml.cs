﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GPS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailGPS : ContentPage
    {
        public DetailGPS()
        {
            InitializeComponent();
        }

        private async void OpenSetting(object sender, EventArgs e)
        {

            // DependencyService.Get<IGetGPS>().GetGPS();
            var checkdevice = DependencyService.Get<IGetGPS>().CheckStatus();
            var checkapp = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            var x = Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (checkdevice == false || checkapp != PermissionStatus.Granted)
            {
                if (!checkdevice)
                {
                    DependencyService.Get<IGetGPS>().GetGPS();
                }
                await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }
            else
            {
                Navigation.PushAsync(new Getlocation());
            }
        }

    }
}
