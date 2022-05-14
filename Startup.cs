using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using MediatR;

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

using OnlineLearning.Common;
using OnlineLearning.EntitiesValidators;
using OnlineLearning.Models;
using OnlineLearning.PipelineBehaviors;
using OnlineLearning.Services;
using OnlineLearning.Settings;
using OnlineLearning.Utilities;
using OnlineLearning.Utilities.Stemmer;

using Swashbuckle.AspNetCore.Swagger;

namespace OnlineLearning
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

            services.AddDbContext<AppDbContext>(item => item.UseSqlServer(Configuration.GetConnectionString("OnlineLearningDb")));
            services.AddIdentity<ApplicationUser, IdentityRole>(policy => {
                policy.Password.RequireUppercase = false;
                policy.Password.RequireNonAlphanumeric = false;
                policy.Password.RequireLowercase = false;
                policy.Password.RequiredLength =8;
            
            })
                .AddRoles<IdentityRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<AppDbContext>();
            #region Services
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IInterestService, InterestService>();
            services.AddScoped<IInterestService, InterestService>();
            services.AddScoped<IMeetingService, MeetingService>();
            services.AddScoped<IZoomService, ZoomService>();
            services.AddScoped<IFileManager, FileManager>();
            //services.AddScoped<IConstatQueries, ConstatQueries>();
            services.AddScoped<IStemmer, EnglishPorter2Stemmer>();
            #endregion
            services.AddScoped(typeof( ILoggerService<>), typeof( LoggerService<>));
            #region Validators
            services.AddScoped<IUserValidator, UserValidator>();
            services.AddScoped<IRoomValidator, RoomValidator>();
            services.AddScoped<IInterestValidator, InterestValidator>();
            services.AddScoped<IMeetingValidator, MeetingValidator>();
            #endregion
            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
            services.AddMediatR(typeof(Startup));
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LogBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            var jwtSettings = new JwtSettings();
            var paginationSettings = new PaginationSettings();
            Configuration.Bind(nameof(jwtSettings), jwtSettings);
            Configuration.Bind(nameof(paginationSettings), paginationSettings);
            services.AddSingleton(jwtSettings);
            services.AddSingleton(paginationSettings);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


            }).AddJwtBearer(x =>
                {
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = true,
                        ClockSkew = TimeSpan.FromMilliseconds(jwtSettings.ExpirationInDays),
                    };
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OnlineLearning", Version = "v1" });
              
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Insert the token here",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                    {
                            Reference = new OpenApiReference
                            {
                               Type = ReferenceType.SecurityScheme,
                               Id = "Bearer"
                            }
                    },
                        new string[]{}
                    },
                    
                });
               
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "OnlineLearning v1");
                    c.OAuthAppName("Test Auth JWT with swagger");
                });
            }
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
