public enum TrailType{Simple, Nature, Historic};

public class TrailContainer
{
	public int Id { get; set; }
	public double Lat { get; set; }
	public double Lng { get; set; }
	public float Length { get; set; }
	public int Diff { get; set; }
	public string GeoJsonData { get; set; }
	public TrailType trailType { get; set; }
}