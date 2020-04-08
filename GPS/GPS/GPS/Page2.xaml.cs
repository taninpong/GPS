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
    public partial class Page2 : ContentPage
    {
        Geocoder geoCoder;
        public Page2()
        {
            InitializeComponent();
            geoCoder = new Geocoder();
            var latlong = "";
            mapapi.MapClicked += async (s, arg) =>
            {
                var x = arg.Position.Latitude;
                var y = arg.Position.Longitude;
                latlong = x + "," + y;
                if (!string.IsNullOrWhiteSpace(latlong))
                {
                    string[] coordinates = latlong.Split(',');
                    double? latitude = Convert.ToDouble(coordinates[0]);
                    double? longitude = Convert.ToDouble(coordinates[1]);

                    if (latitude != null && longitude != null)
                    {
                        Position position = new Position(latitude.Value, longitude.Value);
                        IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(position);
                        //Geocoder;
                        string address = possibleAddresses.FirstOrDefault();
                        reverseGeocodedOutputLabel.Text = address;
                    }
                }
            };
        }

        async void OnReverseGeocodeButtonClicked(object sender, EventArgs e)
        {
            var latlong2 = "16.43307340526658, 102.8255601788635";
            mapapi.MapClicked += (s, arg) =>
            {
                var x = arg.Position.Latitude;
                var y = arg.Position.Longitude;
                latlong2 = x + "," + y;
            };
            if (!string.IsNullOrWhiteSpace(latlong2))
            {
                string[] coordinates = latlong2.Split(',');
                double? latitude = Convert.ToDouble(coordinates[0]);
                double? longitude = Convert.ToDouble(coordinates[1]);

                if (latitude != null && longitude != null)
                {
                    Position position = new Position(latitude.Value, longitude.Value);
                    IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(position);
                    IEnumerable<Position> possibleAddresses2 = await geoCoder.GetPositionsForAddressAsync(possibleAddresses.FirstOrDefault());
                    //Geocoder;
                    string address = possibleAddresses.FirstOrDefault();
                    reverseGeocodedOutputLabel.Text = address;
                }
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(6));
                var location = await Geolocation.GetLocationAsync(request);
                //var location = await Geolocation.GetLastKnownLocationAsync();
                Xamarin.Forms.Maps.Position position = new Xamarin.Forms.Maps.Position(location.Latitude, location.Longitude);
                Xamarin.Forms.Maps.MapSpan mapSpan = new Xamarin.Forms.Maps.MapSpan(position, 0.01, 0.01);
                Xamarin.Forms.Maps.Map map = new Xamarin.Forms.Maps.Map(mapSpan)
                {
                    IsShowingUser = true
                };
                var latnow = location.Latitude;
                var longnow = location.Longitude;
                reverseGeocodeEntry.Text = location.Latitude + "," + location.Longitude;
                //< x:Double x:Name = "latnow" ></ x:Double >
                //< x:Double x:Name = "longnow" ></ x:Double >
                Xamarin.Forms.Maps.Pin boardwalkPin = new Xamarin.Forms.Maps.Pin
                {
                    Label = "U Plaza",
                    Address = "The city with a KhonKean",
                    Type = Xamarin.Forms.Maps.PinType.SearchResult,
                    Position = new Xamarin.Forms.Maps.Position(16.480157, 102.818123)
                    //16.43307340526658, 102.8255601788635  16.480157,102.818123
                };
                Xamarin.Forms.Maps.Pin boardwalkPin2 = new Xamarin.Forms.Maps.Pin
                {
                    Label = "โจ๊กเผาหม้อ",
                    Address = "โจ๊กกกกกกกก",
                    Type = Xamarin.Forms.Maps.PinType.SearchResult,
                    Position = new Xamarin.Forms.Maps.Position(16.483848, 102.818624)
                    //16.43307340526658, 102.8255601788635  16.480157,102.818123
                };
                map.Pins.Add(boardwalkPin);
                map.Pins.Add(boardwalkPin2);
                Position positionx = new Position(location.Latitude, location.Longitude);
                map.MoveToRegion(new MapSpan(positionx, 0.1, 0.1));
                //this.mapapi = map;
                Content = map;

                //map.Pins.Add(new Pin
                //{
                //    Label = "Xamarin",
                //    Position = positionx
                //});
                //var polyline = new Xamarin.Forms.GoogleMaps.Polyline();
                //Xamarin.Forms.Maps.Pin pin = new Xamarin.Forms.Maps.Pin
                //{
                //    Type = PinType.Place,
                //    Position = new Position(16.480157, 102.818123),
                //    Label = "First",
                //    Address = "First",
                //    //Icon = Xamarin.Forms.GoogleMaps.BitmapDescriptorFactory.FromView(new Image() { Source = "ic_taxi.png", WidthRequest = 25, HeightRequest = 25 })
                //};
                //map.Pins.Add(pin);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                Console.WriteLine(fnsEx);
            }
            catch (FeatureNotEnabledException fneEx)
            {
                Console.WriteLine(fneEx);
            }
            catch (PermissionException pEx)
            {
                Console.WriteLine(pEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }




        }
    }
}