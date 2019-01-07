using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using CroixRouge.api.Infrastructure;
using CroixRouge.Dal;
using CroixRouge.DTO;
using AutoMapper;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.StaticFiles;

namespace api
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
            #region ....
            services.AddDbContext<bdCroixRougeContext>((options) =>
            {
                string connectionString = new ConfigurationHelper("CroixRougeDatabase").GetConnectionString();
                options.UseSqlServer(connectionString);
            });
            #endregion
            string SecretKey = new ConfigurationHelper("serveurJetonKey").GetConnectionString();
            SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = "MonSuperServeurDeJetons";
                options.Audience = "http://localhost:5000";
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });



            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = "MonSuperServeurDeJetons",

                ValidateAudience = true,
                ValidAudience = "http://localhost:5000",

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            services
                .AddAuthentication(
                    options =>
                    {

                        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Audience = "http://localhost:5000";
                    options.ClaimsIssuer = "MonSuperServeurDeJetons";
                    options.TokenValidationParameters = tokenValidationParameters;
                    options.SaveToken = true;
                });

            // services.AddCors();
            //pour ajouter CORS services (authoriser les requetes cross origin)
            //services.AddCors();
            services.AddCors(options =>{
                options.AddPolicy("AllowSpecificOrigins",
                builder =>
                {
                    builder.WithOrigins("*")
                .AllowAnyMethod()
                .AllowAnyHeader();
                });
              }
            );

            services.AddMvc((options) =>
            {
                options.Filters.Add(typeof(PersonnalExceptionFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Croix-Rouge-API", Version = "V1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            AutoMapper.Mapper.Initialize(cfg => cfg.AddProfile<CroixRouge.api.Infrastructure.MappingProfile>() );
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            /* app.UseCors(builder =>
                 builder.WithOrigins("*"));*/
            //Enable CORS with CORS Middleware
            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors(builder =>
                //builder.WithOrigins("http://localhost:4200")
                builder.WithOrigins("*") // pour toute les url
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseSwagger();
            
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Croix-Rouge-API V1");
                c.RoutePrefix = "documentation";
            });
            app.UseStaticFiles();
            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
