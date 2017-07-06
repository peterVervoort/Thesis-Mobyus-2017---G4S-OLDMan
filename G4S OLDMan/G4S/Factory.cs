using G4S.Business.Services;
using G4S.Services;
using Microsoft.Practices.Unity;

namespace G4S
{
    public class Factory
    {
        public static void Configure(IUnityContainer container)
        {

            //Services
            container.RegisterType<ISecurityService, SecurityService>();

        }
    }
}
