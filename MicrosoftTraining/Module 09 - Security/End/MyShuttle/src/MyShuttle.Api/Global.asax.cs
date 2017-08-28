using MyShuttle.Api.ServiceContainer;
using MyShuttle.Data;
using System.Web.Http;

namespace MyShuttle.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new AppServiceContainer();

            container.AddService<IDriverRepository, DriverRepository>();
            container.AddService<IVehicleRepository, VehicleRepository>();
            container.AddService<ICarrierRepository, CarrierRepository>();
            container.AddService<IRidesRepository, RidesRepository>();

            GlobalConfiguration.Configuration.DependencyResolver = new AppDependencyResolver(container);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}