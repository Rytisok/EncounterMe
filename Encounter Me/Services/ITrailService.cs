using BrowserInterop.Geolocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encounter_Me.Services
{
    public interface ITrailService
    {
        Task<IEnumerable<TrailContainer>> GetAllTrails();

        Task<TrailContainer> GetTrailDetails(int Id);

        Task<TrailContainer> AddTrail(TrailContainer trail);

        Task DeleteTrail(int Id);

        Task<RooTobject> GenerateTrail(double Lat, double Lon, int difficulty);
    }
}
