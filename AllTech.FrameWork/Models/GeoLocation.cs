using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllTech.FrameWork.Models
{
    public class GeoLocation
    {
        public GeoLocation()
        { }

        public GeoLocation(string newName, double newLatitute, double newLongitute)
        {
            Name = newName;
            Latitude = newLatitute;
            Longitude = newLongitute;
        }

        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
