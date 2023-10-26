using MedicinesManagement.AsyncDataServices;
using MedicinesManagement.BackgroundServices;
using MedicinesManagement.Context;
using MedicinesManagement.Helpers;
using MedicinesManagement.Repos.ActiveSubstances;
using MedicinesManagement.Repos.MedicineATCCategory;
using MedicinesManagement.Repos.Medicines;
using MedicinesManagement.Services.Medicines;
using MedicinesManagement.SyncDataServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace MedicinesManagement
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddControllers();
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "EasyMedicine app API",
                    Version = "v1"
                });

            });

            services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy",
                            builder => builder.WithOrigins("http://localhost:4200")
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowCredentials()

                            );
            });

            services.AddScoped<IMedicinesRepo, MedicinesRepo>();
            services.AddScoped<IMedicineActiveSubstancesRepo, MedicineActiveSubstancesRepo>();
            services.AddScoped<IMedicinesService, MedicinesService>();
            services.AddScoped<IHttpDataClient,  HttpDataClient>();
            services.AddScoped<IActiveSubstancesRepo, ActiveSubstancesRepo>();
            services.AddScoped<IMedicineATCCategoryRepo, MedicineATCCategoryRepo>();
            services.AddScoped<HttpClient, HttpClient>();
            services.AddSingleton<IMessageBusClient, MessageBusClient>();
            services.AddSingleton<MedicineAutoCategorization>();
            services.AddHostedService<LeafletsAutoProcessing>();


            services.AddDbContext<AppDbContext>(options => options
            .UseSqlServer(Configuration.GetConnectionString("Default"), options => 
            options.EnableRetryOnFailure(
                maxRetryCount: 2,
                maxRetryDelay: System.TimeSpan.FromSeconds(20),
                errorNumbersToAdd: null)).UseLazyLoadingProxies());

        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EasyMedicine app API"));
            }
            app.UseCors("MyPolicy");

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });

        }

    }
}
