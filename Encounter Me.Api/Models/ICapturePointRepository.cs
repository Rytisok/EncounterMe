using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encounter_Me;
using Encounter_Me.Shared;


namespace Encounter_Me.Api.Models
{
    public interface ICapturePointRepository
    {
        IEnumerable<CapturePoint> GetCapturePointsInView(double Lat1, double Lon1, double Lat2, double Lon2);
        CapturePoint GetCapturePointById(Guid guid);
        CapturePoint UpdateCapturePoint(CapturePoint capturePoint);
        int GetCapturePointCount();
        int GetCapturePointCountByFaction(Factions faction);
    }
}
