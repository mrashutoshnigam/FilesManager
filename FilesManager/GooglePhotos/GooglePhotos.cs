using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FilesManager.GooglePhotos
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class CreationTime
    {
        public string Timestamp { get; set; }
        public string Formatted { get; set; }
    }

    public class GeoData
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public double LatitudeSpan { get; set; }
        public double LongitudeSpan { get; set; }
    }

    public class GeoDataExif
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public double LatitudeSpan { get; set; }
        public double LongitudeSpan { get; set; }
    }

    public class GooglePhotosOrigin
    {
        public ThirdPartyApp ThirdPartyApp { get; set; }
    }

    public class Person
    {
        public string Name { get; set; }
    }

    public class PhotoTakenTime
    {
        public string Timestamp { get; set; }
        public string Formatted { get; set; }
    }

    public class GooglePhoto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageViews { get; set; }
        public CreationTime CreationTime { get; set; }
        public PhotoTakenTime PhotoTakenTime { get; set; }
        public GeoData GeoData { get; set; }
        public GeoDataExif GeoDataExif { get; set; }
        public List<Person> People { get; set; }
        public string Url { get; set; }
        public GooglePhotosOrigin GooglePhotosOrigin { get; set; }
    }

    public class ThirdPartyApp
    {
        public string AppName { get; set; }
    }




}
