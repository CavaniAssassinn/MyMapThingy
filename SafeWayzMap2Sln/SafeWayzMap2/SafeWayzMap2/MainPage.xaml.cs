using MapApp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace SafeWayzMap2
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer

    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
      
       
    
        public MainPage()
        {
            InitializeComponent();
            // Task.Delay(2000);
            //UpdateMap();
           // Map map = new Map();
           // myMap = map;


           // myMap.MapClicked = map_MapClicked();

        }
       

        private async void map_MapClicked(object sender, MapClickedEventArgs e)
        {
            Geocoder geoCoder = new Geocoder();
            Position posi = e.Position;
            IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(posi);
            string address = possibleAddresses.FirstOrDefault();
            
            Pin pin = new Pin
            {
                Label = "Location",
                Address = address,
                Type = PinType.Place,
                Position = new Position(posi.Latitude, posi.Longitude)
            };
            myMap.Pins.Clear();
            myMap.Pins.Add(pin);
            

            var response = await DisplayAlert("Location",  address, "OK", "CANCEL");

            if (response == true)
            {

                await Navigation.PushAsync(new Page1(address));
            }
           

        }


        // List<Place> placesList = new List<Place>();

        /* private void UpdateMap()
         {
             try
             {
                 var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;   // uses the mainpage as the assembly
                 Stream stream = assembly.GetManifestResourceStream("MapApp.Places.json");  // reference the json file
                 string text = string.Empty;

                 using (var reader = new StreamReader(stream))
                 {
                     text = reader.ReadToEnd();
                 }

                 var resultObject = JsonConvert.DeserializeObject<Places>(text);

                 foreach (var place in resultObject.results)
                 {
                     placesList.Add(new Place
                     {
                         PlaceName = place.name,
                         Address = place.vicinity,
                         Location = place.geometry.location,
                         //Position = new Position(place.geometry.location.lat, place.geometry.location.lng),
                         //Icon = place.icon,
                         //Distance = $"{GetDistance(lat1, lon1, place.geometry.location.lat, place.geometry.location.lng, DistanceUnit.Kiliometers).ToString("N2")}km",
                         //OpenNow = GetOpenHours(place?.opening_hours?.open_now)
                     });
                 }

                 /* MyMap.ItemsSource = placesList;
                  //PlacesListView.ItemsSource = placesList;
                  //var loc = await Xamarin.Essentials.Geolocation.GetLocationAsync();  //my current location
                  MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(47.6370891183, -122.123736172), Distance.FromKilometers(100)));

             }

             catch (Exception ex)
             {
                 Debug.WriteLine(ex);
             }
         }*/
    }
}