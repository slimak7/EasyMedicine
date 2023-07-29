using MedicinesManagement.AsyncDataServices;
using MedicinesManagement.Exceptions;
using MedicinesManagement.Repos.ActiveSubstances;
using MedicinesManagement.Repos.Medicines;
using MedicinesManagement.RequestsModels;
using MedicinesManagement.ResponseModels;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace MedicinesManagement.Services.Medicines
{
    public class MedicinesService : IMedicinesService
    {
        private IMedicinesRepo _medicinesRepo;
        private IMedicineActiveSubstancesRepo _activeSubstancesRepo;
        private IMessageBusClient _messageBusClient;

        public MedicinesService(IMedicinesRepo medicinesRepo, IMedicineActiveSubstancesRepo activeSubstancesRepo, IMessageBusClient messageBusClient)
        {
            _medicinesRepo = medicinesRepo;
            _activeSubstancesRepo = activeSubstancesRepo;  
            _messageBusClient = messageBusClient;
        }

        public async Task AddUpdateLeaflet(List<Guid> medicineID, byte[] leaflet)
        {
            var substances = await _activeSubstancesRepo.GetAllByCondition(x => x.Medicine.MedicineID == medicineID[0]);

            _messageBusClient.PublishNewLeaflet(new Dtos.MedicineUpdateInfoDto()
            {
                MedicineID = medicineID.ToArray(),
                SubstancesID = substances.Select(x => x.ActiveSubstance.SubstanceID).ToArray(),
                Leaflet = leaflet,
                EventName = "AddUpdateLeaflet"
            });

        }

        public async Task<MedicinesListResponse> GetMedicinesByName(string name)
        {
            var match = new Regex(@$".*?{name}.*?", RegexOptions.IgnoreCase);
            var medicines = await _medicinesRepo.GetAllByCondition(x => match.IsMatch(x.MedicineName));

            if (medicines.IsNullOrEmpty())
            {
                throw new DataAccessException("No medicines found with given name");
            }
            else
            {
                List<MedicineResponse> medicineResponses = new List<MedicineResponse>();

                foreach (var medicine in medicines)
                {
                    var activeSubstancesForMedicine = await _activeSubstancesRepo.GetAllByCondition(x => x.Medicine.MedicineID == medicine.MedicineID);

                    medicineResponses.Add(new MedicineResponse()
                    {
                        MedicineName = medicine.MedicineName,
                        MedicineID = medicine.MedicineID.ToString(),
                        CompanyName = medicine.CompanyName,
                        Power = medicine.Power,
                        ActiveSubstances = activeSubstancesForMedicine.Select(x => x.ActiveSubstance.SubstanceName).ToList()
                    });
                }

                return new MedicinesListResponse(medicineResponses);
            }
        }

        public async Task<MedicinesListResponse> GetMedicinesByRange(int index, int count)
        {
            var medicines = await _medicinesRepo.GetAllByIndex(index, count);

            if (medicines.IsNullOrEmpty())
            {
                throw new DataAccessException("No medicines with given index and count number");
            }
            else
            {
                List<MedicineResponse> medicineResponses = new List<MedicineResponse>();

                foreach(var medicine in medicines)
                {
                    var activeSubstancesForMedicine = await _activeSubstancesRepo.GetAllByCondition(x => x.Medicine.MedicineID == medicine.MedicineID);

                    medicineResponses.Add(new MedicineResponse()
                    {
                        MedicineName = medicine.MedicineName,
                        MedicineID = medicine.MedicineID.ToString(),
                        CompanyName = medicine.CompanyName,
                        Power = medicine.Power,
                        ActiveSubstances = activeSubstancesForMedicine.Select(x => x.ActiveSubstance.SubstanceName).ToList()
                    });
                }

                return new MedicinesListResponse(medicineResponses);
            }
        }
    }
}
