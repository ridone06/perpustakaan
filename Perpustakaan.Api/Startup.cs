using AutoWrapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Perpustakaan.Api.Data.Repository.Interfaces;
using Perpustakaan.Api.Data.Services.Auth;
using Perpustakaan.Api.Data.Services.Interfaces;
using Perpustakaan.Api.Extensions;
using Perpustakaan.Api.Helpers;
using Perpustakaan.Api.Models;
using Perpustakaan.Api.Models.Entity;
using Serilog;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;

namespace Perpustakaan
{
    public class Startup
    {
        IWebHostEnvironment _environment;
        SwaggerConfig swaggerConfig;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
            swaggerConfig = new SwaggerConfig();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
            })
            .AddNewtonsoftJson(options =>
            {
                // Use the default property (Pascal) casing
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
            });

            services.AddHttpContextAccessor();

            RegisterIIS(services);
            RegisterDBContext(services);
            RegisterSwagger(services);

            services.AddAuthentication("BasicAuthentication")
                    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddAutoMapper(typeof(Startup));

            services.AddLogging();

            services.AddCors();

            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (_environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseResponseCaching();
            app.UseResponseCompression();
            app.UseMiddleware<RequestResponseLoggingMiddleware>();
            app.UseSerilogRequestLogging(opts => opts.EnrichDiagnosticContext = LogHelper.EnrichFromRequest);
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(swaggerConfig.SwaggerEndpoint, "Perpustakaan API v1");
                c.EnableDeepLinking();
                c.OAuthAppName(swaggerConfig.OAuthAppName);
                c.RoutePrefix = string.Empty;
            });

            app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions
            {
                UseApiProblemDetailsException = true,
                ShowApiVersion = true,
                ShowStatusCode = true,
                UseCustomSchema = true,
                IgnoreNullValue = false,
                IsDebug = _environment.IsDevelopment(),
                ApiVersion = "1.0.0",
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RegisterIIS(IServiceCollection services)
        {
            // configures IIS out-of-proc settings (see https://github.com/aspnet/AspNetCore/issues/14882)
            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });

            // configures IIS in-proc settings
            services.Configure<IISServerOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
                iis.AllowSynchronousIO = true;
            });
        }

        private void RegisterDBContext(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            Log.Information("ConnectionString : ", connectionString);

            var dataPoolingDB = Configuration.GetConnectionString("DataPoolingDB");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<PerpustakaanContext>((options) =>
            {
                options.UseSqlServer(connectionString, sql =>
                {
                    sql.MigrationsAssembly(migrationsAssembly);
                    //sql.CommandTimeout(30); // default 30 seconds
                });
                options.EnableSensitiveDataLogging(true);
            });
        }

        private void RegisterSwagger(IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Perpustakaan API",
                    Version = "1.0.0"
                });
                options.DocInclusionPredicate((name, api) => true);
                options.TagActionsBy(api => new[] { api.GroupName });
                options.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}");
                options.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "basic"
                                }
                            },
                            new string[] {}
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.EnableAnnotations();
            });

            services.AddSwaggerGenNewtonsoftSupport();
        }

        private void RegisterServices(IServiceCollection services)
        {
            var swaggerConfigSection = Configuration.GetSection("SwaggerConfig");

            swaggerConfigSection.Bind(swaggerConfig);

            services.Configure<SwaggerConfig>(swaggerConfigSection);
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            //for Compression respon JSON
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes =
                    ResponseCompressionDefaults.MimeTypes.Concat(
                        new[] { "image/svg+xml", "application/json", "text/json" });
            });
            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            services.AddClassesAsImplementedInterface(Assembly.GetEntryAssembly(), typeof(IRepository<>));
            services.AddClassesAsImplementedInterface(Assembly.GetEntryAssembly(), typeof(IRepositoryWithoutId<>));

            services.AddScoped<IUserService, UserService>();
            services.AddClassesAsImplementedInterface(Assembly.GetEntryAssembly(), typeof(IServices<>));
        }

    }
}
