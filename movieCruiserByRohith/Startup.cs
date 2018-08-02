using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using movieCruiserByRohith.Data.Persistence;
using movieCruiserByRohith.Services;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace movieCruiserByRohith
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
            var connectionString = Environment.GetEnvironmentVariable("SQLSERVER_MOVIE");
            if (string.IsNullOrEmpty(connectionString)) {
                connectionString = Configuration.GetConnectionString("MovieCruiserByRohithDB");
            }
            services.AddMvc();
            services.AddDbContext<MoviesDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IMoviesDbContext, MoviesDbContext>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieService, MovieService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddOptions();

            var option = new DbContextOptionsBuilder<MoviesDbContext>().UseSqlServer(connectionString).Options;
            using (var dbContext = new MoviesDbContext(option))
            {
                dbContext.Database.EnsureCreated();
            }

            services.AddSwaggerGen(swagGen =>
            {
                swagGen.SwaggerDoc("v1", new Info
                {
                    Title = "Movie Cruiser API",
                    Version = "v1",
                    Description = "Movie Cruiser API developed in ASP.NET Core."
                });
                swagGen.DescribeAllEnumsAsStrings();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
            });
        }
    }
}
