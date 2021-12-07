using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Encounter_Me.Api.Trail_generation_Utilities
{
    public static class RandomCoordinates
    {
        static readonly double PI = 3.141592653589;

        // Return a random double between 0 & 1
        static double uniform()
        {
            return new Random().NextDouble();
        }

        //returns 2 random coordinates, r distance apart from center and 2r distance from one another
        public static List<Coordinate> RandomPoints(double r, Coordinate coord, int n)
        {
            List<Coordinate> res = new List<Coordinate>();
            var rng = new Random(DateTime.Now.Millisecond);

            //calculate random bearing
            int randomBearing = rng.Next(-180, 180);
            //move coordinate in bearing direction by radius value
            Coordinate coordA = coord.CalculateDerivedPosition(r, randomBearing);

            //move second coordinate in completly different direction from the first one
            if (randomBearing > 0)
            {
                randomBearing = randomBearing - 180;
            }
            else
            {
                randomBearing = randomBearing + 180;
            }

            Coordinate coordB = coord.CalculateDerivedPosition(r, randomBearing);

            res.Add(coordA);
            res.Add(coordB);

            return res;
        }
    }

}
