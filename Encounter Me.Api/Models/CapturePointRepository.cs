using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encounter_Me.Shared;

namespace Encounter_Me.Api.Models
{
    public class CapturePointRepository : ICapturePointRepository
    {
        private readonly AppDbContext _appDbContext;

        public CapturePointRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public CapturePoint GetCapturePointById(Guid guid)
        {
            return _appDbContext.CapturePoints.FirstOrDefault(t => t.guid == guid);
        }

        public IEnumerable<CapturePoint> GetCapturePointsInView(double Lat1, double Lon1, double Lat2, double Lon2)
        {
            return _appDbContext.CapturePoints.Where(t => t.Lat > Lat1 &&
                t.Lat < Lat2 &&
                t.Lon > Lon1 &&
                t.Lon < Lon2).ToList();
        }

        public CapturePoint UpdateCapturePoint(CapturePoint capturePoint)
        {
            var foundCapturePoint = _appDbContext.CapturePoints.FirstOrDefault(e => e.guid.Equals(capturePoint.guid));

            if (foundCapturePoint != null)
            {
                foundCapturePoint.faction = capturePoint.faction;
                foundCapturePoint.DefenseLevel = capturePoint.DefenseLevel;

                _appDbContext.SaveChanges();

                return foundCapturePoint;
            }

            return null;
        }

        public int GetCapturePointCount()
        {
            return _appDbContext.CapturePoints.Count();
        }

        public int GetCapturePointCountByFaction(Factions faction)
        {
            return (_appDbContext.CapturePoints.Where(e => e.faction == (int)faction)).Count();
        }
    }
}
