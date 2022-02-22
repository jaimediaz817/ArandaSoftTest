using ArandaSoftTest.CORE.Interfaces;
using ArandaSoftTest.CORE.Services;
using ArandaSoftTest.INFRASTRUCTURE.Data;
using ArandaSoftTest.INFRASTRUCTURE.Filters;
using ArandaSoftTest.INFRASTRUCTURE.Repositories;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using Microsoft.Extensions.FileProviders;
using ArandaSoftTest.INFRASTRUCTURE.Interfaces;
using Microsoft.AspNetCore.Http;
using ArandaSoftTest.INFRASTRUCTURE.Services;
using ArandaSoftTest.CORE.CustomEntities;

namespace ArandaSoftTest.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // ENABLE CORS
            services.AddCors(c => {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            // TODO: Configuración del autoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // TODO: irgnorar referencias circulares por la relación
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                // NO MOSTRAR OBJETOS NULOS al intentar serializar: Ignore
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include;
            })
            .ConfigureApiBehaviorOptions(options => {
                options.SuppressModelStateInvalidFilter = true;
            });

            // TODO: configurar que se desactiven por defecto los filtros para validar los requests

            // Trayendo del fichero de configuración appsettings.json la clave Pagination
            services.Configure<PaginationOptions>(Configuration.GetSection("Pagination"));

            // TODO: registrar cadena de conexión
            services.AddDbContext<PruebaArandaSoftContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("PruebaArandaSoft"))
            );

            // TODO: INYECCIÓN DE DEPENDENCIAS ::: C O N T R O L L E R S 
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductRepository, ProductRepository>();
            // por cada solicitud no se requiere una instancia
            services.AddSingleton<IUriService>(provider => 
            {
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(absoluteUri);
            }); 

            // registrar el repo generico e implementación genérica
            services.AddScoped(typeof (IRepository<>), typeof(BaseRepository<>));

            // Registrar Unidad de trabajo para unificar repositorios
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            // aplicar el filtro a nivel de toda la app y registrando validadores para ProductoDto
            services.AddMvc(options =>
            {
                options.Filters.Add<ValidationFilter>();
            }).AddFluentValidation(options => {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options => 
                options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // TODO: Configuración de subida de imágenes
            app.UseStaticFiles(new StaticFileOptions {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "images")),
                    RequestPath="/images"
            });
        }
    }
}
