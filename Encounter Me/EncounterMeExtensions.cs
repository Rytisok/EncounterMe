using System;
using System.Text;

namespace ExtensionMethods
{
    public static class EncounterMeExtensions
    {
        public static string ToTrailStars(this int stars)
        {
            string starString = "";

            for (int i = 0; i < stars; i++)
            {
                starString += "★";
            }
            for (int i = 0; i < 5 - stars; i++)
            {
                starString += '☆';
            }

            return starString;
        }
    }
}