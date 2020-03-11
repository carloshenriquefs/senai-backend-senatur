using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;

namespace Senai.Senatur.WebApi
{
    public class Startup
    {

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                // Adiciona o MVC ao projeto
                .AddMvc()

                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
                 {
                     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Senai.Senatur.WebApi", Version = "v1" });

                     var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                     var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                     c.IncludeXmlComments(xmlPath);
                 });


            services
                //Define a forma de autenticação
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "JwtBearer";
                    options.DefaultChallengeScheme = "JwtBearer";
                })

                //Define os parâmetros de validação do token
                .AddJwtBearer("JwtBearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // Quem está solicitando
                        ValidateIssuer = true,

                        // Quem está validando
                        ValidateAudience = true,

                        // Definindo o tempo de expiração
                        ValidateLifetime = true,

                        // Forma de criptografia
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("Senatur-chave-autenticacao")),

                        // Tempo de expiração do token
                        ClockSkew = TimeSpan.FromMinutes(30),

                        // Nome da issuer, de onde está vindo
                        ValidIssuer = "Senatur.WebApi",

                        // Nome da audience, de onde está vindo
                        ValidAudience = "Senatur.WebApi"
                    };
                });

                // Adiciona as opções do json 
                //.AddJsonOptions(options => {
                    // Ignora valores nulos ao fazer junções nas consultas
                  //  options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    // Ignora os loopings nas consultas
                    //options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //})

                // Define a versão do .NET Core
                //.SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Senatur.WebApi");
            });

            app.UseAuthentication();

            app.UseMvc();
            
        }
    }
}
