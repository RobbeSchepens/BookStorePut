using BookStore.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore
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
            services.AddMvcCore();
            services.AddDbContext<BookStoreContext>(opt => opt.UseInMemoryDatabase("BookLists"));
            services.AddApiVersioning();
            services.AddOData().EnableApiVersioning();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, VersionedODataModelBuilder modelBuilder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var models = modelBuilder.GetEdmModels();

            app.UseMvc(routes => routes.MapVersionedODataRoutes("odata", "odata", models));
        }
    }

    /// <summary>
    /// Defines the configuration of the <see cref="AccountAddressesController" />.
    /// </summary>
    public class ModelConfigurationAccountAddress : IModelConfiguration
    {
        /// <summary>
        /// Applies the configuration for the given <see cref="ApiVersion" />.
        /// </summary>
        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion)
        {
            switch (apiVersion.MajorVersion)
            {
                case 1:
                    ConfigureV1(builder);
                    break;
                default:
                    ConfigureV1(builder);
                    break;
            }
        }

        /// <summary>
        /// Applies the configuration for version 1 of the controller.
        /// </summary>
        private void ConfigureV1(ODataModelBuilder builder)
        {
            builder.EntitySet<Account>("Accounts");
            builder.EntityType<PaymentInstrument>();
        }
    }
}
