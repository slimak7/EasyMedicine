using MedicinesManagement.Exceptions;
using MedicinesManagement.RequestsModels;
using MedicinesManagement.ResponseModels;
using MedicinesManagement.Services.Medicines;
using MedicinesManagement.SyncDataServices;
using Microsoft.AspNetCore.Mvc;

namespace MedicinesManagement.Controllers
{
    [Route("[controller]")]
    public class MedicinesController : ControllerBase
    {
        private readonly IMedicinesService _medicinesService;
        private readonly IHttpDataClient _httpDataClient;

        public MedicinesController(IMedicinesService medicinesService, IHttpDataClient httpDataClient)
        {
            _medicinesService = medicinesService;    
            _httpDataClient = httpDataClient;
        }

        [HttpGet]
        [Route("GetMedicines/GetByRange/{page}/{count}")]
        public async Task<IActionResult> GetMedicinesByRange(int page, int count)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult("Invalid data provided");
            }
            try
            {
                var response = await _medicinesService.GetMedicinesByRange(page, count);

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
        public async Task<IActionResult> GetMedicinesByName(string name)
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

        [HttpGet]
        [Route("GetMedicines/GetBySubstance/{substanceID}")]
        public async Task<IActionResult> GetMedicinesBySubstance(Guid substanceID)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult("Invalid data provided");
            }
            try
            {
                var response = await _medicinesService.GetMedicinesBySubstance(substanceID);

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
        [Route("GetMedicines/GetSimilar/{medicineID}/{page}/{count}")]
        public async Task<IActionResult> GetSimilarMedicines(Guid medicineID, int page, int count)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult("Invalid data provided");
            }
            try
            {
                var response = await _medicinesService.GetSimilarMedicines(medicineID, page, count);

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
        [Route("GetMedicines/GetInteractions/{medicineID}")]
        public async Task<IActionResult> GetMedicinesInteractions(Guid medicineID)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult("Invalid data provided");
            }
            try
            {
                var response = await _httpDataClient.GetMedicineInteractions(medicineID);

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

                await _medicinesService.AddUpdateLeaflet(addUpdateLeafletRequest.MedicineID, bytes);

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
