using MedicinesManagement.Helpers;
using MedicinesManagement.Repos.Medicines;
using MedicinesManagement.Services.Medicines;
using System.Runtime.InteropServices;

namespace MedicinesManagement.BackgroundServices
{
    public class LeafletsAutoProcessing : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _configuration;
        private readonly MedicineAutoCategorization _medicineAutoCategorization;

        public LeafletsAutoProcessing(IServiceScopeFactory serviceScopeFactory, IConfiguration configuration, MedicineAutoCategorization medicineAutoCategorization)
        {
            _scopeFactory = serviceScopeFactory;
            _configuration = configuration;
            _medicineAutoCategorization = medicineAutoCategorization;
        }

        protected override async Task<Task> ExecuteAsync(CancellationToken stoppingToken)
        {
            string sectionName = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "DockerDepl" : "Dev";
            bool sendLeaflets = _configuration.GetSection(sectionName).GetValue<bool>("SendLeaflets");

            
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
                            try
                            {
                                if (medicine.leafletUpdateDate == null || (DateTime.Now.Subtract((DateTime)medicine.leafletUpdateDate)).Days > _configuration.GetValue<int>("UpdateLeafletDaysPeriod"))
                                {

                                    var httpClientHandler = new HttpClientHandler
                                    {
                                        SslProtocols = System.Security.Authentication.SslProtocols.Tls12,
                                        UseDefaultCredentials = true
                                    };
                                    HttpClient client = new HttpClient(httpClientHandler);
                                    var response = await client.GetAsync(medicine.leafletURL);

                                    var leaflet = await response.Content.ReadAsByteArrayAsync();

                                    medicine.leafletUpdateDate = DateTime.Now;
                                    medicine.leafletData = leaflet;

                                    await medicinesRepo.Update(medicine);

                                    if (sendLeaflets)
                                    {
                                        await medicinesService.AddUpdateLeaflet(new List<Guid>() { medicine.MedicineID }, leaflet);
                                    }
                                }
                                else if (sendLeaflets && medicine.leafletData != null)
                                {
                                    await medicinesService.AddUpdateLeaflet(new List<Guid>() { medicine.MedicineID }, medicine.leafletData);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
