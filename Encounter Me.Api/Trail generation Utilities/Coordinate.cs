using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Encounter_Me.Api.Trail_generation_Utilities
{
    public class Coordinate
    {
        public double Lat, Lon;

        public Coordinate(double Lat,
                    double Lon)
        {
            this.Lat = Lat;
            this.Lon = Lon;
        }

        public override string ToString()
        {
            return Lat.ToString().Replace(',', '.') + "," + Lon.ToString().Replace(',', '.');
        }

        public override bool Equals(object obj)
        {
            Coordinate coordB = obj as Coordinate;

            if (coordB == null)
            {
                return false;
            }
            else
            {
                return (coordB.Lat == this.Lat && coordB.Lon == this.Lon);
            }
        }
    }

}
