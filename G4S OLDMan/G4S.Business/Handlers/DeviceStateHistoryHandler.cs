using G4S.Business.Repositories;
using G4S.Business.Services;
using G4S.Entities.Pocos;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G4S.Business.Handlers
{
    class DeviceStateHistoryHandler : IDeviceStateHistoryHandler
    {
        [Dependency]
        public IReader<StateChange> StateChangeReader { get; set; }
        [Dependency]
        public IReader<MobileDevice> MobileDeviceReader { get; set; }
        [Dependency]
        public ISecurityService SecurityService { get; set; }

        public async Task<IEnumerable<StateChange>> GetPossibleStateChanges(int mobileDeviceId)
        {
            var currentUser = await SecurityService.GetCurrentUser();
            if (currentUser == null) throw new UnauthorizedAccessException();
            
            var mobileDevice = await MobileDeviceReader.GetById(mobileDeviceId, nameof(MobileDevice.RepairChanges));
            var stateFromId = mobileDevice.RepairChanges?.OrderBy(rc => rc.ChangeDate).LastOrDefault()?.RepairStateChange?.StateToId;

            var possibleStates = await StateChangeReader.Search(sc => sc.StateFromId == stateFromId
                                    && sc.SystemStateChange == false
                                    && (!sc.AcceptedRoleGroups.Any() || sc.AcceptedRoleGroups.Any(arg => arg.Id == currentUser.RoleGroupId)));

            return possibleStates;

        }
    }
}
