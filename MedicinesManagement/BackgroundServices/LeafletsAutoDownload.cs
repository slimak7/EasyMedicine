using MedicinesManagement.Repos.Medicines;
using MedicinesManagement.Services.Medicines;
using System.Runtime.InteropServices;

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
            string sectionName = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "DockerDepl" : "Dev";
            bool sendLeaflets = _configuration.GetSection(sectionName).GetValue<bool>("LeafletsAutoProcessing");

            
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
                            try
                            {
                                var httpClientHandler = new HttpClientHandler
                                {
                                    SslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls13 | System.Security.Authentication.SslProtocols.Tls11,
                                    UseDefaultCredentials = true
                                };
                                HttpClient client = new HttpClient(httpClientHandler);
                                var response = await client.GetAsync(medicine.leafletURL);

                                var leaflet = await response.Content.ReadAsByteArrayAsync();

                                await medicinesService.AddUpdateLeaflet(new List<Guid>() { medicine.MedicineID }, leaflet);
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
