using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Route("/api/trips/{tripName}/stops")]
    [Authorize]
    public class StopsController : Controller
    {
        private GeoCoordsService _coordsService;
        private ILogger<StopsController> _logger;
        private IWorldRepository _repository;

        public StopsController(IWorldRepository repo,
            ILogger<StopsController> logger,
            GeoCoordsService coordsService)
        {
            _repository = repo;
            _logger = logger;
            _coordsService = coordsService;
        }

        [HttpGet("")]
        public IActionResult Get(string tripName)
        {
            try
            {
                var trip = _repository.GetUserTripByName(tripName, User.Identity.Name);

                return Ok(Mapper.Map<IEnumerable<StopViewModel>>(trip.Stops.OrderBy(s => s.Order).ToList()));
            } catch (Exception ex) {
                _logger.LogError($"Failed to get stops: {ex}");
            }

            return BadRequest("Couldn't find trip");
        }

        [HttpPost("")]
        public async Task<IActionResult> Post(string tripName, [FromBody]StopViewModel sm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newStop = Mapper.Map<Stop>(sm);

                    var coords = await _coordsService.GetCoordsAsync(newStop.Name);
                    if (!coords.Success)
                    {
                        _logger.LogError(coords.Message);
                    } else  {
                        newStop.Latitude = coords.Latitude;
                        newStop.Longitude = coords.Longitude;
                        _repository.AddStop(tripName, newStop, User.Identity.Name);

                        if (await _repository.SaveChangesAsync())
                        {
                            return Created($"/api/trips/{tripName}/stops/{newStop.Name}", Mapper.Map<StopViewModel>(newStop));
                        }
                    }
                }
            } catch (Exception ex) {
                _logger.LogError($"Failed to save new stop: {ex}");
            }

            return BadRequest("Didn't save stop");
        }
    }
}
