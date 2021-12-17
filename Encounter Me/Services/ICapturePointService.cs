using Encounter_Me.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Encounter_Me.Services
{
    public interface ICapturePointService
    {
        Task<IEnumerable<CapturePoint>> GetCapturePointsInView(double Lat1, double Lon1, double Lat2, double Lon2);

        Task<CapturePoint> GetCapturePoint(Guid Id);

        Task UpdateCapturePoint(CapturePoint capturePoint);
        Task<double> GetCapturePointPercentage(Factions faction);
    }
}
