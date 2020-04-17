using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Consultorio.DataAccess;
using Consultorio.Models;
using Consultorio.Request;
using Consultorio.Response;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Consultorio
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
            services.AddControllers();

            #region CORDs
            services.AddCors(); 
            #endregion

            #region DbContex
            //Application DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnetion")));

            #endregion

            #region AutoMapper
            //AutoMapper
            services.AddAutoMapper(conf =>
            {
                #region Cita
                conf.CreateMap<Cita, CitaRequest>().ReverseMap();
                conf.CreateMap<Cita, CitaResponse>().ReverseMap();
                conf.CreateMap<Cita, CitaClientResponse>().ReverseMap();
                #endregion

                #region Especialidad
                conf.CreateMap<Especialidad, EspecialidadRequest>().ReverseMap();
                conf.CreateMap<Especialidad, EspecialidadResponse>().ReverseMap();
                #endregion

                #region Medicos
                conf.CreateMap<Medico, MedicoRequest>().ReverseMap();
                conf.CreateMap<Medico, MedicoResponse>().ReverseMap();
                conf.CreateMap<Medico, MedicoClientResponse>().ReverseMap();
                #endregion

                #region Pacientes
                conf.CreateMap<Paciente, PacienteRequest>().ReverseMap();
                conf.CreateMap<Paciente, PacienteResponse>().ReverseMap();
                conf.CreateMap<Paciente, PacienteClientResponse>().ReverseMap();
                #endregion

            }, typeof(Startup));
            #endregion


            #region NewtonSoftJson
            //NewtonSoftJson
            services.AddControllers().AddNewtonsoftJson(options => 
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            #endregion


            #region Swagger
            //Swagger
            services.AddSwaggerGen(conf =>
            {
                conf.SwaggerDoc("Consultorio", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Consultorio",
                    Description = "Gestion de citas un consultorio medico"
                });
            });
            #endregion


            #region Token
            //configuracion de token para usuarios y roles
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["JWT:Key"])),
                        ClockSkew = TimeSpan.Zero

                    });
            
            #endregion

            #region Roles

            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //Swagger
            app.UseSwagger();

            app.UseSwaggerUI(conf =>
            {
                conf.SwaggerEndpoint("/swagger/Consultorio/swagger.json", "Consultorio");
            });

            app.UseAuthentication();

            #region CORDs
            app.UseCors(builder => builder.AllowAnyOrigin()
                                          .AllowAnyMethod()
                                          .AllowAnyHeader());
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
