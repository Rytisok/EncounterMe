using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Encounter_Me.Api.Trail_generation_Utilities
{
    public static class CoordinateMath
    {
        public static double GetDistanceBetweenCoords(Coordinate posA, Coordinate posB)
        {
            return Haversine(posA, posB);
        }

        private static double Haversine(Coordinate pos1, Coordinate pos2)
        {
            double R = 6371;

            double dLat = ToRadian(pos2.Lat - pos1.Lat);
            double dLon = ToRadian(pos2.Lon - pos1.Lon);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadian(pos1.Lat)) * Math.Cos(ToRadian(pos2.Lat)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
            double d = R * c;

            return d;
        }

        private static double ToRadian(double val)
        {
            return (Math.PI / 180) * val;
        }

        public static Coordinate CalculateDerivedPosition(this Coordinate source, double range, double bearing)
        {
            var latA = source.Lat * DegreesToRadians;
            var lonA = source.Lon * DegreesToRadians;
            var angularDistance = range / EarthRadius;
            var trueCourse = bearing * DegreesToRadians;

            var lat = Math.Asin(
                Math.Sin(latA) * Math.Cos(angularDistance) +
                Math.Cos(latA) * Math.Sin(angularDistance) * Math.Cos(trueCourse));

            var dlon = Math.Atan2(
                Math.Sin(trueCourse) * Math.Sin(angularDistance) * Math.Cos(latA),
                Math.Cos(angularDistance) - Math.Sin(latA) * Math.Sin(lat));

            var lon = ((lonA + dlon + Math.PI) % (Math.PI * 2)) - Math.PI;

            return new Coordinate(
                lat * RadiansToDegrees,
                lon * RadiansToDegrees);
        }

        private const double DegreesToRadians = Math.PI / 180.0;
        private const double RadiansToDegrees = 180.0 / Math.PI;
        private const double EarthRadius = 6378137.0;
    }

}
