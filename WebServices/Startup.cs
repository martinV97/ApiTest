using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using WebService.Core.CustomEntities;
using WebService.Core.Entities;
using WebService.Core.Interfaces;
using WebService.Core.Interfaces.MongoDBInterfaces;
using WebService.Core.Interfaces.RepositoryInterfaces;
using WebService.Core.Interfaces.ServicesInterfaces;
using WebService.Core.Services;
using WebService.Infrastructure.Data;
using WebService.Infrastructure.Repositories;

namespace WebServices
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
            services.AddMvc().AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });
            services.AddControllers();
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "WebServices",
                    Description = "ASP.Net core Web API"
                });
                c.AddSecurityDefinition("Bearer", //Name the security scheme
                    new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme.",
                        Type = SecuritySchemeType.Http, //We set the scheme type to http since we're using bearer authentication
                        Scheme = "bearer" //The name of the HTTP Authorization scheme to be used in the Authorization header. In this case "bearer".
                    });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = "Bearer", //The name of the previously defined security scheme.
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                var xmlPath2 = Path.Combine(AppContext.BaseDirectory, "WebService.Core.xml");
                c.IncludeXmlComments(xmlPath2);
            });

            services.AddAuthentication(options => 
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Authentication:Issuer"],
                    ValidAudience = Configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:SecretKey"])),
                    RequireExpirationTime = false
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.Configure<PasswordOptions>(Configuration.GetSection("PasswordOptions"));
            services.Configure<Collections>(Configuration.GetSection("DataBaseSettings").GetSection("Collection"));

            services.AddSingleton(s =>
            {
                var mongoClient = new MongoClient(Configuration.GetSection("DataBaseSettings").GetSection("ConnectionString").Value);
                var database = mongoClient.GetDatabase(Configuration.GetSection("DataBaseSettings").GetSection("DatabaseName").Value);
                return database;
            });

            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IOfferService, OfferService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IPublicationService, PublicationService>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPlaceService, PlaceService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IPasswordService, PasswordService>();

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IOfferRepository, OfferRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IPublicationRepository, PublicationRepository>();
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPlaceRepository, PlaceRepository>();

            services.AddTransient<ICategoryMongoDBContext, CategoryMongoDBContext>();
            services.AddTransient<IOfferMongoDBContext, OfferMongoDBContext>();
            services.AddTransient<IPaymentMongoDBContext, PaymentMongoDBContext>();
            services.AddTransient<IProductMongoDBContext, ProductMongoDBContext>();
            services.AddTransient<IPublicationMongoDBContext, PublicationMongoDBContext>();
            services.AddTransient<IQuestionMongoDBContext, QuestionMongoDBContext>();
            services.AddTransient<IUserMongoDBContext, UserMongoDBContext>();
            services.AddTransient<IPlaceMongoDBContext, PlaceMongoDBContext>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebServices");
                c.RoutePrefix = "swagger";
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
