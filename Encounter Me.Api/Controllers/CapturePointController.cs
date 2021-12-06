using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encounter_Me.Api.Models;
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

        [HttpGet]
        public IActionResult GetAllTokens()
        {
            return Ok("Here you go."); // For testing only
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
    }
}

