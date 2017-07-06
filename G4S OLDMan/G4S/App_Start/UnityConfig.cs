using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace G4S
{
    public static class UnityConfig
    {
        public static IUnityContainer RegisterComponents()
        {
            IUnityContainer container = new UnityContainer();

            container.RegisterInstance<IUnityContainer>(container);
            Factory.Configure(container);

            G4S.Business.Factory.Configure(container);
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

            return container;

        }
    }
}