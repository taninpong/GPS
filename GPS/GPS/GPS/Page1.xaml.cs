using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace GPS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        Geocoder geocoder;
        public Page1()
        {
            InitializeComponent();
            geocoder = new Geocoder();
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                DateStart.Text = "DateStart : " + DateTime.UtcNow.ToString();
                //DependencyService.Get<IGetGPS>().GetGPS();
                var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));
                var location = await Geolocation.GetLocationAsync(request);
                LabelLatLong.Text = "Lat : " + location.Latitude + "Long : " + location.Longitude;
                DateEnd.Text = "DateEnd : " + DateTime.UtcNow.ToString();
                var latlong = location.Latitude + "," + location.Longitude;
                if (!string.IsNullOrWhiteSpace(latlong))
                {
                    string[] coordinates = latlong.Split(',');
                    double? latitude = Convert.ToDouble(coordinates[0]);
                    double? longitude = Convert.ToDouble(coordinates[1]);

                    if (latitude != null && longitude != null)
                    {
                        Position position = new Position(location.Latitude, location.Longitude);
                        //Position position = new Position(latitude.Value, longitude.Value);
                        try
                        {

                            IEnumerable<string> possibleAddresses = await geocoder.GetAddressesForPositionAsync(position);
                            //Geocoder;
                            string address = possibleAddresses.FirstOrDefault();
                            DataAddress.Text = address;
                        }
                        catch (Exception exx)
                        {
                            throw new Exception();
                        }


                    }
                }
                //var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                //var placemark = placemarks?.FirstOrDefault();
                //if (placemark != null)
                //{
                //    var geocodeAddress =
                //        $"AdminArea:       {placemark.AdminArea}\n" +
                //        $"CountryCode:     {placemark.CountryCode}\n" +
                //        $"CountryName:     {placemark.CountryName}\n" +
                //        $"FeatureName:     {placemark.FeatureName}\n" +
                //        $"Locality:        {placemark.Locality}\n" +
                //        $"PostalCode:      {placemark.PostalCode}\n" +
                //        $"SubAdminArea:    {placemark.SubAdminArea}\n" +
                //        $"SubLocality:     {placemark.SubLocality}\n" +
                //        $"SubThoroughfare: {placemark.SubThoroughfare}\n" +
                //        $"Thoroughfare:    {placemark.Thoroughfare}\n";
                //    Console.WriteLine(geocodeAddress);
                //}
                //DataAddress.Text = placemark.SubLocality;
                //var check = DependencyService.Get<IGetGPS>().Getvalue();
                //var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(1));
                //DateStart.Text = "DateStart : " + DateTime.UtcNow.ToString();
                //if (check == false)
                //{
                //    bool answer = await DisplayAlert("แจ้งเตือน", "กรุณาเปิดตำแหน่งของโทรศัพย์ก่อน", "เปิด", "ไม่เปิด");
                //    //Debug.WriteLine("Answer: " + answer);
                //    if (answer == true)
                //    {
                //        DependencyService.Get<IGetGPS>().GetGPS();
                //    }
                //}
                //else
                //{
                //    var location = await Geolocation.GetLocationAsync(request);
                //    LabelLatLong.Text = "Lat : " + location.Latitude + "Long : " + location.Longitude;
                //    DateEnd.Text = "DateEnd : " + DateTime.UtcNow.ToString();
                //}
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                Console.WriteLine(fnsEx);
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                Console.WriteLine(fneEx);
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                Console.WriteLine(pEx);
            }
            catch (Exception ex)
            {
                // Unable to get location
                Console.WriteLine(ex);
            }
        }
    }
}