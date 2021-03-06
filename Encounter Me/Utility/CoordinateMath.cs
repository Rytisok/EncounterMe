using System;
using System.Collections.Generic;
using BrowserInterop.Geolocation;
using System.Threading.Tasks;
using Encounter_Me;

public static class CoordinateMath
{
    public static double GetDistanceBetweenCoords(GeolocationCoordinates posA, GeolocationCoordinates posB)
    {
        return Haversine(posA, posB);
    }

    public static double CalculateOverallDistance(GeolocationCoordinates[] coordinateArray)
    {
        double finalDist = 0;

        for (int i = 0; i < coordinateArray.Length-1; i++)
        {
            finalDist += GetDistanceBetweenCoords(coordinateArray[i], coordinateArray[i + 1]);
        }

        return finalDist;
    }

    private static double Haversine(GeolocationCoordinates pos1, GeolocationCoordinates pos2)
    {
        double R = 6371;

        double dLat = ToRadian(pos2.Latitude - pos1.Latitude);
        double dLon = ToRadian(pos2.Longitude - pos1.Longitude);

        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
            Math.Cos(ToRadian(pos1.Latitude)) * Math.Cos(ToRadian(pos2.Latitude)) *
            Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        double c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
        double d = R * c;

        return d;
    }

    private static double ToRadian(double val)
    {
        return (Math.PI / 180) * val;
    }
}
