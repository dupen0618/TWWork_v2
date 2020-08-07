using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TWWork_v2.Dao;
using TWWork_v2.Reps.IRepository;
using TWWork_v2.Reps.Repository;

namespace TWWork_v2
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
			services.AddDbContextPool<AppDbContext>(
				options => options.UseOracle(Configuration.GetConnectionString("TWWorkDbConnection"))
			);
			services.AddControllersWithViews();
			services.AddScoped<IOBTRFIDRecordCountRepository, OBTRFIDRecordCountRepository>();
			services.AddScoped<IOBTRFIDCountModelRepository, OBTRFIDCountModelRepository>();
			services.AddScoped<IDataChangeModelRepository, DataChangeModelRepository>();
			services.AddScoped<IOBTRFIDRecordRepository, OBTRFIDRecordRepository>();
			services.AddScoped<ISuccessRateModelRepository, SuccessRateModelRepository>();
			services.AddScoped<ISiteReadQualityRepository, SiteReadQualityRepository>();
			services.AddScoped<IMissingRecordRepository, MissingRecordRepository>();
			services.AddScoped<IRFIDReadCountsRepository, RFIDReadCountsRepository>();
			services.AddScoped<IInOutCompareDataModelRepository, InOutCompareDataModelRepository>();
			services.AddScoped<IDeviceWorkRecordRepository, DeviceWorkRecordRepository>();
			services.AddScoped<ITableInfoRepository, TableInfoRepository>();
			services.AddScoped<IRetrospectiveInquiryRepository, RetrospectiveInquiryRepository>();
			services.AddScoped<Idev_TraceRecordRepository, dev_TraceRecordRepository>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=ComparedStatistics}/{action=Index}/{id?}");
			});
		}
	}
}