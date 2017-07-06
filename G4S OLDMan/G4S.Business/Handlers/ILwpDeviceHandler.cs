using System.Threading.Tasks;
using G4S.Business.Helpers;
using G4S.Business.Repositories;
using G4S.Business.Writers;
using G4S.Entities.Pocos;

namespace G4S.Business.Handlers
{
    public interface ILwpDeviceHandler
    {
        Task<EntityResult> Create(MobileDevice device, LwpSetting lwp);
        Task<EntityResult> Update(int id, MobileDevice device, LwpSetting lwp);
        Task<EntityResult> Delete(int id);
    }
}