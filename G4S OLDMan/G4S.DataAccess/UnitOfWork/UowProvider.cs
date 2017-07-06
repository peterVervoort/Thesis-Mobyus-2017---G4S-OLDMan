using Microsoft.Practices.Unity;
using System;

namespace G4S.DataAccess.UnitOfWork
{
    public class UowProvider : IUowProvider
    {
        public UowProvider()
        { }

        public UowProvider(IUnityContainer container)
        {
            _container = container;
        }

        private readonly IUnityContainer _container;

        public IUnitOfWork CreateUnitOfWork(bool trackChanges = true)
        {
            var _context = new EntityContext();

            if (!trackChanges) _context.Configuration.AutoDetectChangesEnabled = false;

            var uow = new UnitOfWork(_context, _container);
            return uow;
        }
    }
}
