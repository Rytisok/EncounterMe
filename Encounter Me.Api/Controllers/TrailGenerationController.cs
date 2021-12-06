using Encounter_Me.Api.Models;
using Encounter_Me.Api.Trail_generation_Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Encounter_Me.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrailGenerationController : Controller
    {
        [HttpGet("{Lat}/{Lon}/{Diff}")]
        public IActionResult GenerateTrail(double Lat, double Lon, int Diff)
        {
            Coordinate coord = new Coordinate(Lat, Lon);
            return Ok(GenerateTrail(coord, Diff));
        }

        public static string Get(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            HttpWebResponse response = null;

            response = (HttpWebResponse)request.GetResponse();

            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public static string GenerateTrail(Coordinate center, int difficulty)
        {
            IEnumerable<Coordinate> coordinates = null;
            List<Coordinate> randomPoints = new List<Coordinate>();
            string polyline = "";

            double maxDist = 450 + 300 * difficulty;
            randomPoints = RandomCoordinates.RandomPoints(maxDist, center, 2);

            string directionData = Get("https://maps.googleapis.com/maps/api/directions/json?origin=" +
                    randomPoints[0].ToString() +
                    "&destination=" +
                    randomPoints[1].ToString() +
                    "&mode=walking&key=AIzaSyBnfp5LXgEH9hhE55Q5xVbxDu_X89taYOs" +
                    "&waypoints=optimize:true|" + center.ToString() +
                    "&avoid=highways");

            polyline = GooglePoints.ParsePolyline(directionData);
            coordinates = GooglePoints.Decode(polyline);

            string trailResultGeojson = "{\"type\": \"FeatureCollection\",\"features\": [{\"type\": \"Feature\",\"properties\": { },\"geometry\": { \"type\": \"LineString\",\"coordinates\":[";

            for (int i = 0; i < coordinates.Count(); i++)
            {
                Coordinate coord = coordinates.ToArray()[i];

                trailResultGeojson += ("[" + coord.Lon.ToString() + ", " + coord.Lat.ToString() + "]");

                if (i != coordinates.Count()-1)
                {
                    trailResultGeojson += ",";
                }
            }

            trailResultGeojson += "]}}]}";

            return trailResultGeojson;
        }
    }
}