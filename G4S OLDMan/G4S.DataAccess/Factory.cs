using G4S.DataAccess.Repositories;
using G4S.DataAccess.UnitOfWork;
using Microsoft.Practices.Unity;

namespace G4S.DataAccess
{
    public class Factory
    {
        public static void Configure(IUnityContainer container)
        {
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>));
            container.RegisterType(typeof(IEntityFilter<>), typeof(EntityFilterBase<>));
            container.RegisterType<IUowProvider, UowProvider>(new PerResolveLifetimeManager());
            container.RegisterType<IEntityContext, EntityContext>(new PerResolveLifetimeManager());
        }
    }
}
