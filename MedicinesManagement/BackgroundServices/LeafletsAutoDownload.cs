using MedicinesManagement.Repos.Medicines;
using MedicinesManagement.Services.Medicines;

namespace MedicinesManagement.BackgroundServices
{
    public class LeafletsAutoDownload : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _configuration;

        public LeafletsAutoDownload(IServiceScopeFactory serviceScopeFactory, IConfiguration configuration)
        {
            _scopeFactory = serviceScopeFactory;
            _configuration = configuration;
        }

        protected override async Task<Task> ExecuteAsync(CancellationToken stoppingToken)
        {
            bool sendLeaflets = _configuration.GetSection("Leaflets").GetValue<bool>("LeafletsAutoProcessing");

            
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
                        if (sendLeaflets && medicine.leafletURL != "")
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
