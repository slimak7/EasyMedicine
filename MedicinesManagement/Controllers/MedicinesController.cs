using MedicinesManagement.AsyncDataServices;
using MedicinesManagement.Exceptions;
using MedicinesManagement.RequestsModels;
using MedicinesManagement.ResponseModels;
using MedicinesManagement.Services.Medicines;
using Microsoft.AspNetCore.Mvc;
using System.IO;

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
            string extension = Path.GetExtension(addUpdateLeafletRequest.Leaflet.FileName).ToLower();
            if (extension != ".pdf")
            {
                return new BadRequestObjectResult("Invalid file extension");
            }
            try
            {
                byte[] bytes = null;
                using (var memoryStream = new MemoryStream())
                {
                    addUpdateLeafletRequest.Leaflet.CopyTo(memoryStream);
                    bytes = memoryStream.ToArray();
                }

                _messageBusClient.PublishNewLeaflet(new Dtos.MedicineUpdateInfoDto()
                {
                    MedicineID = addUpdateLeafletRequest.MedicineID,
                    Leaflet = bytes,
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
