using AutoMapper;
using KapitalMedyaBooking.AppService.AppServices;
using KapitalMedyaBooking.AppService.Interfaces;
using KapitalMedyaBooking.AppService.Mapper;
using KapitalMedyaBooking.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace KapitalMedyaBooking.API
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
            #region AutoMapper

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            #endregion

            #region Service Providing

            services.AddDbContext<KapitalDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("KapitalMedyaConn")));
            services.AddSingleton(mapper);
            services.AddScoped<IAppartmentAppService, AppartmentAppService>();
            services.AddScoped<ICompanyAppService, CompanyAppService>();
            services.AddScoped<IBookingAppService, BookingAppService>();
            services.AddScoped<IUserAppService, UserAppService>();
            #endregion


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "KapitalMedyaBooking.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KapitalMedyaBooking.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
