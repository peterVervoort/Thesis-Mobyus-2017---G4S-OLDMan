using System.Collections.Generic;
using System.Threading.Tasks;
using G4S.Business.Repositories;
using G4S.Business.Services;
using G4S.Entities.Pocos;

namespace G4S.Business.Handlers
{
    public interface IDeviceStateHistoryHandler
    {
        Task<IEnumerable<StateChange>> GetPossibleStateChanges(int mobileDeviceId);
    }
}