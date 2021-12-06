using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BrowserInterop.Geolocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encounter_Me.Services
{
    public interface ICapturePointService
    {
        Task<IEnumerable<CapturePoint>> GetCapturePointsInView(double Lat1, double Lon1, double Lat2, double Lon2);

        Task<CapturePoint> GetCapturePoint(Guid Id);

        Task UpdateCapturePoint(CapturePoint capturePoint);
    }
}
