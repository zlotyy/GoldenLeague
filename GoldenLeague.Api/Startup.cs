using GoldenLeague.Api.Commands;
using GoldenLeague.Api.Queries;
using GoldenLeague.Api.Utils;
using GoldenLeague.Common.Extensions;
using GoldenLeague.Database;
using GoldenLeague.Database.Queries;
using LinqToDB.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace GoldenLeague.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            DataConnection.DefaultSettings = new LinqToDBSettings(configuration.GetConnectionString("GoldenLeagueDB"));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            services.AddSingleton<IDbContextFactory, DbContextFactory>();

            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Insert(0, new DateTimeJsonConverter());
                });

            services.AddAutoMapper(typeof(Startup));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GoldenLeague.Api", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
                c.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic Authorization for API"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Basic" }},
                        new string[] { }
                    }
                });
            });

            // queries
            services.AddTransient<IBaseQueries, BaseQueries>();
            services.AddTransient<IUserQueries, UserQueries>();
            services.AddTransient<IBookmakerBetQueries, BookmakerBetQueries>();
            services.AddTransient<IBookmakerLeagueQueries, BookmakerLeagueQueries>();
            services.AddTransient<IMatchQueries, MatchQueries>();
            services.AddTransient<ITeamQueries, TeamQueries>();
            services.AddTransient<ICompetitionsQueries, CompetitionsQueries>();

            // commands
            services.AddTransient<IBookmakerBetCommands, BookmakerBetCommands>();
            services.AddTransient<IBookmakerLeagueCommands, BookmakerLeagueCommands>();
            services.AddTransient<IMatchCommands, MatchCommands>();
            services.AddTransient<IUserCommands, UserCommands>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GoldenLeague.Api v1"));
            }

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
