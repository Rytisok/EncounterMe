using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encounter_Me.Api.Models;
using Encounter_Me.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Encounter_Me.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CapturePointController : Controller
    {
        private readonly ICapturePointRepository _capturePointRepository;

        public CapturePointController(ICapturePointRepository capturePointRepository)
        {
            _capturePointRepository = capturePointRepository;
        }

        [HttpGet("{Lat1}/{Lon1}/{Lat2}/{Lon2}")]
        public IActionResult GetCapturePointsInView(double Lat1, double Lon1, double Lat2, double Lon2)
        {
            return Ok(_capturePointRepository.GetCapturePointsInView(Lat1, Lon1, Lat2, Lon2));
        }
        
        [HttpGet("{id}")]
        public IActionResult GetCapturePointById(Guid id)
        {
            return Ok(_capturePointRepository.GetCapturePointById(id));
        }

        [HttpGet("CapturePointPercentage/{factionName}")]
        public IActionResult GetCapturePointPercentage(string factionName)
        {
            try
            {
                Factions faction; 
                Enum.TryParse(factionName, out faction);
                int pointCount = _capturePointRepository.GetCapturePointCount();
                int capturedFactionPointCount = pointCount - _capturePointRepository.GetCapturePointCountByFaction(Factions.Neutral);

                int factionPointCount = _capturePointRepository.GetCapturePointCountByFaction(faction);

                double percentage = ((double)factionPointCount / (double)capturedFactionPointCount) * 100;

                return Ok(percentage);
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpPut]
        public IActionResult UpdateCapturePoint([FromBody] CapturePoint capturePoint)
        {
            if (capturePoint == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var capturePointToUpdate = _capturePointRepository.GetCapturePointById(capturePoint.guid);

            if (capturePointToUpdate == null)
                return NotFound();

            if (_capturePointRepository.UpdateCapturePoint(capturePoint) == null)
                return BadRequest();

            return NoContent(); //success
        }
    }
}

