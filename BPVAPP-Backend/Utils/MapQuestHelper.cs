using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BPVAPP_Backend.Utils
{
    public static class MapQuestHelper
    {
        /// <summary>
        /// Returns a MapQuest object with the search result of a given adress
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static GeoLocation GetGeoLocation(string address)
        {
            ErrorMessage = string.Empty;
            Success = false;
            return Get(address);
        }

        public static GeoLocation GetGeoLocationGoogle(string address)
        {
            ErrorMessage = string.Empty;
            Success = false;
            return GetGoogle(address);
        }

        public static object ErrorMessage { get; private set; } = "";

        public static bool Success { get; private set; } = false;

        private static GeoLocation GetGoogle(string addres)
        {
            var key = "AIzaSyBxivCXKvjDevE0puAhsCHfJGDUmmzkVqk";
            var url = $@"https://maps.googleapis.com/maps/api/geocode/json?address={addres.Replace(" ","")}&key={key}&country=NL";
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url); // Send the API call
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                // Handles response
                using (var res = (HttpWebResponse)request.GetResponse())
                using (var strm = res.GetResponseStream())
                using (var reader = new StreamReader(strm))
                {
                    // Data is given back in JSON formar
                    var response = JsonConvert.DeserializeObject(reader.ReadToEnd());

                    var json = JObject.FromObject(response);

                    // Checks if we got an error back
                    if (json["status"].ToString() != "OK")
                    {
                        Success = false;
                        ErrorMessage = "Response Code: not oke";
                        return null;
                    }

                    return new GeoLocation
                    {
                        Latitude = json["results"][0]["geometry"]["location"]["lat"].ToString().Replace(",","."),
                        Longitude = json["results"][0]["geometry"]["location"]["lng"].ToString().Replace(",", ".")
                    };
                }
            }
            catch (Exception e)
            {
                ErrorMessage = e.ToString();
                Success = false;
                return new GeoLocation {
                    Latitude = "null",
                    Longitude = "null"
                };
            }
        }

        /// <summary>
        /// Makes an API request to mapquestapi to get the geo locationof an adress
        /// </summary>
        /// <param name="addres"></param>
        private static GeoLocation Get(string addres)
        {
            var key = "h9HsdxSmGwHRFNhbCR6oGp5fbs2nbYRJ"; // API key 
            var url = $@"https://www.mapquestapi.com/geocoding/v1/address?key={key}&location='{addres}'"; // Search URL


            // Some fancy http request magic
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url); // Send the API call
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                // Handles response
                using (var res = (HttpWebResponse)request.GetResponse())
                using (var strm = res.GetResponseStream())
                using (var reader = new StreamReader(strm))
                {
                    // Data is given back in JSON formar
                    var response = JObject.Parse(reader.ReadToEnd());

                    var status = int.Parse(JObject.Parse(response["info"].ToString())["statuscode"].ToString());

                    // Checks if we got an error back
                    if (status != 0)
                    {
                        Success = false;
                        ErrorMessage = "Response Code: " + status;
                        return null;
                    }

                    var results = JArray.Parse(response["results"].ToString());

                    var locations = JArray.Parse(results[0]["locations"].ToString());

                    var street = JObject.Parse(locations[0].ToString())["street"];


                    // Checks if there is a location
                    if (string.IsNullOrWhiteSpace(street.ToString()))
                    {
                        Success = false;
                        ErrorMessage = "Locatie niet gevonden";
                        return null;
                    }

                    // The data we really want
                    var dataIwant = JObject.Parse(locations[0].ToString())["displayLatLng"];

                    // Data that gets stored in database
                    var lat = dataIwant["lat"].ToString().Replace(",", "."); // The reason , is replaced by . is because other wise when you open maps and pass in the values with , it will bring you to same plaace over and over again (random place in belguim)
                    var lon = dataIwant["lng"].ToString().Replace(",", ".");

                    Success = true;

                    return new GeoLocation {
                        Latitude = lat,
                        Longitude = lon
                    };
                }
            }
            catch
            {
                Success = false;
                return null;
            }
        }
    }
}
