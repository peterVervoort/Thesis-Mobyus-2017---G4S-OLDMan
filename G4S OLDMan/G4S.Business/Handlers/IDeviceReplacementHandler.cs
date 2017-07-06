using System.Threading.Tasks;
using G4S.Business.Helpers;

namespace G4S.Business.Handlers
{
    public interface IDeviceReplacementHandler
    {
        Task<EntityResult> ReplaceDevice(int oldDeviceId, int newDeviceId, int oldStateId, int newStateId);
    }
}