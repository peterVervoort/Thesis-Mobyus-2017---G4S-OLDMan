using G4S.Entities.Pocos;
using System.Threading.Tasks;
using G4S.Business.Writers;
using G4S.Business.Helpers;
using Microsoft.Practices.Unity;
using System;
using G4S.Business.Repositories;
using System.Linq;

namespace G4S.Business.Handlers
{
    internal class DeviceReplacementHandler : IDeviceReplacementHandler
    {

        [Dependency]
        public IReader<MobileDevice> MobileDeviceReader{ get; set; }
        [Dependency]
        public IWriter<MobileDevice> MobileDeviceWriter { get; set; }
        [Dependency]
        public IWriter<DeviceStateHistory> StateHistoryWriter { get; set; }


        public async Task<EntityResult> ReplaceDevice(int oldDeviceId, int newDeviceId, int oldStateId, int newStateId)
        {
            var oldDevice = await MobileDeviceReader.GetById(oldDeviceId);
            var newDevice = await MobileDeviceReader.GetById(newDeviceId);

            if (oldDevice == null || newDevice == null) return new EntityResult(ResultCode.Failed, new[] { "Device not found" });
            if (oldDevice.OrderItem?.AnnulationDate.HasValue == true) return new EntityResult(ResultCode.ValidationError, new[] { "Device has been cancelled" });
            if (newDevice.OrderItem?.AnnulationDate.HasValue == true) return new EntityResult(ResultCode.ValidationError, new[] { "Device has been cancelled" });

            newDevice.DeviceTypeId = oldDevice.DeviceTypeId;
            newDevice.DeviceName = oldDevice.DeviceName;
            newDevice.PhoneNumber = oldDevice.PhoneNumber;
            newDevice.PlatformId = oldDevice.PlatformId;
            newDevice.Country = oldDevice.Country;
            newDevice.OrderItemId = oldDevice.OrderItemId;
            newDevice.LwpSetting = oldDevice.LwpSetting;

            oldDevice.OrderItemId = null;

            var result = await MobileDeviceWriter.UpdateAsync(oldDevice);
            if (result.Code != ResultCode.Success) return (EntityResult)result;
            result = await MobileDeviceWriter.UpdateAsync(newDevice);
            if (result.Code != ResultCode.Success) return (EntityResult)result;

            var oldDeviceChange = new DeviceStateHistory()
            {
                MobileDeviceId = oldDeviceId,
                RepairStateChange = new StateChange
                {
                    SystemStateChange = true,
                    StateFromId = oldDevice.RepairChanges?.OrderByDescending(rc => rc.ChangeDate)?.FirstOrDefault()?.RepairStateChange?.StateToId,
                    StateToId = oldStateId
                },
                Comment = $"Replaced by {newDevice.Reference} (id:{newDeviceId})"
            };

            var stateResult = await StateHistoryWriter.InsertAsync(oldDeviceChange);
            if (result.Code != ResultCode.Success) return (EntityResult)result;

            var newDeviceChange = new DeviceStateHistory()
            {
                MobileDeviceId = newDeviceId,
                RepairStateChange = new StateChange
                {
                    SystemStateChange = true,
                    StateFromId = oldDevice.RepairChanges?.OrderByDescending(rc => rc.ChangeDate)?.FirstOrDefault()?.RepairStateChange?.StateToId,
                    StateToId = newStateId
                },
                Comment = $"Replaced {oldDevice.Reference} (id:{oldDeviceId})"
            };

            stateResult = await StateHistoryWriter.InsertAsync(newDeviceChange);

            if (result.Code != ResultCode.Success) return (EntityResult)stateResult;
            return result;
        }


    }
}

