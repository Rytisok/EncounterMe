using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Encounter_Me
{
    //public class GeoJsonTrailData
    //{ 
    //    public RooTobject AccessTrailData { get; set; }
    //}

    public class RooTobject
    {
        public string Type { get; set; }
        public Feature[] Features { get; set; }
    }
    public class Feature
    {
        public string Type { get; set; }
        public Properties Properties { get; set; }
        public Geometry Geometry { get; set; }
    }

    public class Properties
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }

    public class Geometry
    {
        public string Type { get; set; }
        public float[][] Coordinates { get; set; }
    }

}

