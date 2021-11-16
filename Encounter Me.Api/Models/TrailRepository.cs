using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encounter_Me.Shared;

namespace Encounter_Me.Api.Models
{
    public class TrailRepository : ITrailRepository
    {
        private readonly AppDbContext _appDbContext;

        public TrailRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public TrailContainer AddTrail(TrailContainer trail)
        {
            var addedEntity = _appDbContext.Trails.Add(trail);
            _appDbContext.SaveChanges();
            return addedEntity.Entity;
        }

        public void DeleteTrail(int Id)
        {
            var foundTrail = _appDbContext.Trails.FirstOrDefault(t => t.Id == Id);
            if (foundTrail == null) return;

            _appDbContext.Trails.Remove(foundTrail);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<TrailContainer> GetAllTrails()
        {
            return _appDbContext.Trails;
        }

        public TrailContainer GetTrailById(int Id)
        {
            return _appDbContext.Trails.FirstOrDefault(t => t.Id == Id);
        }
    }
}
