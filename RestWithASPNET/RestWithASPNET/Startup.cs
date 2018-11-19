﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using RestWithASPNET.Business;
using RestWithASPNET.Business.Implementations;
using RestWithASPNET.Hypermedia;
using RestWithASPNET.Models.Context;
using RestWithASPNET.Repository;
using RestWithASPNET.Repository.Generic;
using RestWithASPNET.Repository.Implementations;
using System;
using System.Collections.Generic;
using Tapioca.HATEOAS;

namespace RestWithASPNET
{
    public class Startup
    {
        public readonly ILogger _logger;
        public IConfiguration _configuration { get; }
        public IHostingEnvironment _environment;

        public Startup(ILogger<Startup> logger, IConfiguration configuration, IHostingEnvironment environment)
        {
            _logger = logger;
            _configuration = configuration;
            _environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration["MySqlConnection:MySqlConnectionString"];
            services.AddDbContext<MySQLContext>(options => {
                options.UseMySql(connectionString);
            });


            //configurações para o evolve
            if (_environment.IsDevelopment())
            {
                try
                {
                    var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

                    var evolve = new Evolve.Evolve("evolve.json", evolveConnection, msg => _logger.LogInformation(msg))
                    {
                        Locations = new List<string> { "db/migrations" },
                        IsEraseDisabled = true
                    };

                    evolve.Migrate();
                }
                catch (Exception ex)
                {
                    _logger.LogCritical("Database migration failed.", ex);
                    throw ex;
                }
            }

            services.AddMvc(options => {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("text/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            })
            .AddXmlSerializerFormatters()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ObjectContentResponseEnricherList.Add(new PersonEnticher());

            services.AddSingleton(filterOptions);

            services.AddApiVersioning();

            //Injeção de dependencias
            services.AddScoped<IPersonBusiness, PersonBusiness>();
            services.AddScoped<IBookBusiness, BookBusiness>();
            services.AddScoped<IPersonRepository, PersonRepository>();

            //modo de adição para classes genericas
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "DefaultApi",
                    template: "{controller=Values}/{id}"
                );
            });
        }
    }
}
