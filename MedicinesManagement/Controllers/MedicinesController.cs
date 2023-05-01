using MedicinesManagement.Exceptions;
using MedicinesManagement.RequestsModels;
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
        [Route("GetMedicines/GetByRange/{index}/{count}")]
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
                return BadRequest(new MedicinesListResponse(e.Message));
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet]
        [Route("GetMedicines/GetByName/{name}")]
        public async Task<IActionResult> GetMedicinesByRange(string name)
        {
            if (!ModelState.IsValid || name.Length < 3)
            {
                return new BadRequestObjectResult("Invalid data provided");
            }
            try
            {
                var response = await _medicinesService.GetMedicinesByName(name);

                return Ok(response);
            }
            catch (DataAccessException e)
            {
                return BadRequest(new MedicinesListResponse(e.Message));
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost]
        [Route("GetMedicines/AddUpdateLeaflet")]
        public async Task<IActionResult> AddUpdateLeaflet([FromBody] AddUpdateLeafletRequest addUpdateLeafletRequest)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult("Invalid data provided");
            }
            try
            {
                return Ok();
            }
            catch (DataAccessException e)
            {
                return BadRequest(new MedicinesListResponse(e.Message));
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
