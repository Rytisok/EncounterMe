using System;
using System.Collections.Generic;
using BrowserInterop.Geolocation;
using System.Threading.Tasks;
using Encounter_Me;

public static class CoordinateConverter
{
    public static async Task<List<GeolocationCoordinates>> ConvertGeojsonToGeolocationCoordinates(RooTobject geoJsonTrailData)
    {
        var coordinatesArr = geoJsonTrailData.Features[0].Geometry.Coordinates;

        var coordinatesList = new List<GeolocationCoordinates>();

        for (int i = 0; i < coordinatesArr.GetLength(0); i++)
        {
            var coordinate = new GeolocationCoordinates();
            coordinate.Latitude = coordinatesArr[i][1];
            coordinate.Longitude = coordinatesArr[i][0];
            coordinatesList.Add(coordinate);
        }

        return coordinatesList;
    }
    public static double[] GeolocationCoordinateToArray(GeolocationCoordinates coords)
    {
        double[] coordinates = new double[2];
        coordinates[0] = coords.Latitude;
        coordinates[1] = coords.Longitude;

        return coordinates;
    }
    public static double[][] GeolocationCoordinateListToCoordinateArray(List<GeolocationCoordinates> list)
    {
        double[][] coordinatesArray = new double[list.Count][];

        for (int i = 0; i < list.Count; i++)
        {
            coordinatesArray[i] = GeolocationCoordinateToArray(list[i]);
        }
        return coordinatesArray;
    }
}
