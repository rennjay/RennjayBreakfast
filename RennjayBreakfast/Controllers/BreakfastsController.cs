using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using RennjayBreakfast.Contracts.Breakfast;
using RennjayBreakfast.Models;
using RennjayBreakfast.Services.Breakfast;

namespace RennjayBreakfast.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BreakfastsController : ControllerBase
    {
        private IBreakfastService _breakfastService;
        public BreakfastsController(IBreakfastService breakfastService)
        {
            _breakfastService = breakfastService;
        }

        [HttpPost]
        public IActionResult CreateBreakfast(CreateBreakfastRequest request)
        {
            var breakfast = new Breakfast(
                Guid.NewGuid(),
                request.Name,
                request.Description,
                request.StartDateTime,
                request.EndDateTime,
                DateTime.UtcNow,
                request.Savory,
                request.Sweet
            );

            _breakfastService.CreateBreakfast(breakfast);

            var response = new BreakfastResponse(
                breakfast.Id,
                breakfast.Name,
                breakfast.Description,
                breakfast.StartDateTime,
                breakfast.EndDateTime,
                breakfast.LastModifiedTime,
                breakfast.Savory,
                breakfast.Sweet);

            return CreatedAtAction(actionName: nameof(GetBreakfast),
                                   routeValues: new { id = response.Id },
                                   value: response);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetBreakfast(Guid id)
        {
            ErrorOr<Breakfast> breakfastResult = _breakfastService.GetBreakfast(id);

            breakfastResult.Match(
                breakfast => Ok(MapToBreakfastResponse(breakfastResult.Value)),
                errors => Problem());

            BreakfastResponse response = MapToBreakfastResponse(breakfast);

            return Ok(response);
        }

        private static BreakfastResponse MapToBreakfastResponse(ErrorOr<Breakfast> breakfast)
        {
            return new BreakfastResponse(
                breakfast.Id,
                breakfast.Name,
                breakfast.Description,
                breakfast.StartDateTime,
                breakfast.EndDateTime,
                breakfast.LastModifiedTime,
                breakfast.Savory,
                breakfast.Sweet
            );
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
        {
            var breakfast = _breakfastService.GetBreakfast(id);

            if (breakfast is null)
            {
                breakfast = new Breakfast(Guid.NewGuid(),
                                          request.Name,
                                          request.Description,
                                          request.StartDateTime,
                                          request.EndDateTime,
                                          DateTime.UtcNow,
                                          request.Savory,
                                          request.Sweet);

                _breakfastService.CreateBreakfast(breakfast);
            }

            var response = new BreakfastResponse(breakfast.Id,
                                                 breakfast.Name,
                                                 breakfast.Description,
                                                 breakfast.StartDateTime,
                                                 breakfast.EndDateTime,
                                                 breakfast.LastModifiedTime,
                                                 breakfast.Savory,
                                                 breakfast.Sweet);

            //TODO: Return 201 if new breakfast is created
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteBreakfast(Guid id)
        {
            _breakfastService.DeleteBreakfast(id);
            return NoContent();
        }
    }
}