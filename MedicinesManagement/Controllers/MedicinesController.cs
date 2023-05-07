using MedicinesManagement.AsyncDataServices;
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
        private IMessageBusClient _messageBusClient;

        public MedicinesController(IMedicinesService medicinesService, IMessageBusClient messageBusClient)
        {
            _medicinesService = medicinesService;
            _messageBusClient = messageBusClient;
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
        public async Task<IActionResult> AddUpdateLeaflet(AddUpdateLeafletRequest addUpdateLeafletRequest)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult("Invalid data provided");
            }
            try
            {
                _messageBusClient.PublishNewLeaflet(new Dtos.MedicineUpdateInfoDto()
                {
                    MedicineID = addUpdateLeafletRequest.MedicineID,
                    Leaflet = addUpdateLeafletRequest.Leaflet,
                    EventName = "AddUpdateLeaflet"
                });

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
