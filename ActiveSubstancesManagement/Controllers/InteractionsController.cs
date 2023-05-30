using ActiveSubstancesManagement.Exceptions;
using ActiveSubstancesManagement.ResponseModels;
using ActiveSubstancesManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace ActiveSubstancesManagement.Controllers
{
    [Route("[controller]")]
    public class InteractionsController : ControllerBase
    {
        private readonly IInteractionsService _interactionsService;

        public InteractionsController(IInteractionsService interactionsService)
        {
            _interactionsService = interactionsService;
        }

        [HttpGet]
        [Route("GetInteractions/{medicineID}")]
        public async Task<IActionResult> GetInteractions(Guid medicineID)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult("Invalid data provided");
            }
            try
            {
                var interactions = await _interactionsService.GetInteractionsForMedicine(medicineID);

                return Ok(interactions);
            }
            catch (DataAccessException e)
            {
                return BadRequest(new InteractionsForMedicineResponse(e.Message));
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

        }       
    }
}
