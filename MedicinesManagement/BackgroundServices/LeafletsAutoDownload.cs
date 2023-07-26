using MedicinesManagement.Repos.Medicines;
using MedicinesManagement.Services.Medicines;

namespace MedicinesManagement.BackgroundServices
{
    public class LeafletsAutoDownload : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public LeafletsAutoDownload(IServiceScopeFactory serviceScopeFactory)
        {
            _scopeFactory = serviceScopeFactory;

        }

        protected override async Task<Task> ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var medicinesRepo = scope.ServiceProvider.GetRequiredService<IMedicinesRepo>();
                    var httpClient = scope.ServiceProvider.GetRequiredService<HttpClient>();
                    var medicinesService = scope.ServiceProvider.GetRequiredService<IMedicinesService>();   

                    var medicines = await medicinesRepo.GetAll();

                    foreach (var medicine in medicines)
                    {
                        if (medicine.leafletURL != "")
                        {
                            var response = await httpClient.GetAsync(medicine.leafletURL);

                            var leaflet = await response.Content.ReadAsByteArrayAsync();

                            await medicinesService.AddUpdateLeaflet(new List<Guid>() { medicine.MedicineID }, leaflet);
                        }
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
