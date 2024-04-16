using Microsoft.EntityFrameworkCore;

using DataModel.Repository;
using Application.Services;

namespace Domain
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		protected virtual void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			services
				.AddEndpointsApiExplorer()
				.AddSwaggerGen()
				.AddDbContext<AbsanteeContext>(options =>
				{
					options.UseSqlServer(Configuration["ConnectionString"]);
				});
			services.AddScoped<AssociationService>();
			services.AddScoped<ProjectService>();
			services.AddScoped<ColaboratorIdService>();
		}

		public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
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