using MedicinesManagement.Exceptions;
using MedicinesManagement.ResponseModels;
using MedicinesManagement.Services.Medicines;
using Microsoft.AspNetCore.Mvc;

namespace MedicinesManagement.Controllers
{
    [Route("[controller]")]
    public class MedicinesController : ControllerBase
    {
        private IMedicinesService _medicinesService;

        public MedicinesController(IMedicinesService medicinesService)
        {
            _medicinesService = medicinesService;
        }

        [HttpGet]
        [Route("GetMedicines/{index}/{count}")]
        public async Task<IActionResult> GetMedicinesByRange(int index, int count)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult("Invalid data provided");
            }
            try
            {
                var response = await _medicinesService.GetMedicinesByRange(index, count);

                return Ok(response);
            }
            catch (DataAccessException e)
            {
                return BadRequest(new AllMedicinesByRangeResponse(e.Message));
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
