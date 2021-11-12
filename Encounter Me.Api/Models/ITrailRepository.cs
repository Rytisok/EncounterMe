using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encounter_Me;
using Encounter_Me.Shared;


namespace Encounter_Me.Api.Models
{
    public interface ITrailRepository
    {
        IEnumerable<TrailContainer> GetAllTrails();
        TrailContainer GetTrailById(int Id);
        TrailContainer AddTrail(TrailContainer trail);
        void DeleteTrail(int id);
    }
}
