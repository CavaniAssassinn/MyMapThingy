using System.Net.Http;
using System.Threading.Tasks;
using Android.Gms.Maps.Model;
using Newtonsoft.Json;


namespace GeoMap.Droid.Helpers
{
    public class MapFunctionHelper
    {
        string mapkey;

        public MapFunctionHelper(string mMapkey)
        {
            mapkey = mMapkey;
        }

        public string GetGeoUrl ( double lat, double lng)
        {
            string url = "https://maps.googleapis.com/maps/api/geocode/json?latlng=" + lat + "," + lng + "&key=" + mapkey;
            return url;
        }

        public async Task<string> GetGeoJsonAsync(string url)
        {
            var handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            string result = await client.GetStringAsync(url);
            return result;  
        }
    public async Task<string> FindCoordinateAddress(LatLng position)
        {
            string url = GetGeoUrl(position.Latitude, position.Longitude);
            string json = "";
            string placeAddress = "";

            json = await GetGeoJsonAsync(url);

            if (!string.IsNullOrEmpty(json))
            {
                var geoCodeData = JsonConvert.DeserializeObject<RootObject>(json);
                if (!geoCodeData.status.Contains("ZERO"))
                {
                    if (geoCodeData.results[0] != null)
                    {
                        placeAddress = geoCodeData.results[0].formatted_address;
                    }
                }
            }
            return placeAddress;
        }
    
    }

}