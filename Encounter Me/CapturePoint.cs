using System;
using System.ComponentModel.DataAnnotations;

namespace Encounter_Me
{
	public class CapturePoint
	{
		public string Name { get; set; }
		[Key]
		public Guid guid { get; set; }
		public double Lat { get; set; }
		public double Lon { get; set; }
		//public enum Faction { Neutral, FactionA, FactionB, FactionC };
		public int faction { get; set; }
		public int DefenseLevel { get; set; }

		public CapturePoint(double Lat, double Lon, int faction)
		{
			this.Lat = Lat;
			this.Lon = Lon;
			this.faction = faction;
			this.guid = Guid.NewGuid();
		}
	}
}
