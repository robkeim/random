using System.Web.Http;
using MyShuttle.Api.ServiceContainer;
using MyShuttle.Data;

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
