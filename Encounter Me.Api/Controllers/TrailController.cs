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
    public class TrailController : Controller
    {
        private readonly ITrailRepository _trailRepository;

        public TrailController(ITrailRepository trailRepository)
        {
            _trailRepository = trailRepository;
        }

        [HttpGet]
        public IActionResult GetAllTrails()
        {
            return Ok(_trailRepository.GetAllTrails());
        }
        [HttpGet("{id}")]
        public IActionResult GetTrailById(int id)
        {
            return Ok(_trailRepository.GetTrailById(id));
        }
        [HttpPost]
        public IActionResult CreateTrail([FromBody] TrailContainer trail)
        {
            if (trail == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdTrail = _trailRepository.AddTrail(trail);

            return Created("trail", createdTrail);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTrail(int id)
        {
            if (id == 0)
                return BadRequest();

            var trailToDelete = _trailRepository.GetTrailById(id);
            if (trailToDelete == null)
                return NotFound();

            _trailRepository.DeleteTrail(id);

            return NoContent();//success
        }

    }
}
