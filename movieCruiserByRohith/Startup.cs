using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using movieCruiserByRohith.Data.Persistence;
using movieCruiserByRohith.Services;

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
