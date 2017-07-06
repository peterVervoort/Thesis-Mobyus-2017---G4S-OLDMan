using G4S.Business.Repositories;
using G4S.Entities.Pocos;
using System;
using System.Threading.Tasks;
using G4S.Business.Writers;
using G4S.Business.Helpers;
using Microsoft.Practices.Unity;

namespace G4S.Business.Handlers
{
    internal class LwpDeviceHandler : ILwpDeviceHandler
    {

        [Dependency]
        public IWriter<MobileDevice> MobileDeviceWriter { get; set; }
        [Dependency]
        public IWriter<LwpSetting> LwpSettingWriter { get; set; }
        [Dependency]
        public IReader<MobileDevice> MobileDeviceReader { get; set; }
        [Dependency]
        public IReader<LwpSetting> LwpSettingReader { get; set; }


        public async Task<EntityResult> Create(MobileDevice device, LwpSetting lwp)
        {
            var result = new EntityResult(ResultCode.Failed);
            if (device == null) return result;
            if (lwp == null) return result;

            device.LwpSetting = lwp;

            return (EntityResult)await MobileDeviceWriter.InsertAsync(device);
        }

        public async Task<EntityResult> Update(int id, MobileDevice device, LwpSetting lwp)
        {
            var result = new EntityResult(ResultCode.Failed);
            if (device == null) return result;
            if (lwp == null) return result;

            var existingDevice = await MobileDeviceReader.GetById(id);
            if (existingDevice == null) return result;
            var existingLwp = await LwpSettingReader.GetById(id);
            device.Id = id;
            lwp.Id = id;
            result = (EntityResult)await MobileDeviceWriter.UpdateAsync(device);
            if (result.Code == ResultCode.Success)
            {
                if (existingLwp != null) result = (EntityResult)await LwpSettingWriter.UpdateAsync(lwp);
                else
                {
                    result = (EntityResult)await LwpSettingWriter.InsertAsync(lwp);
                }
            }

            return result;
        }

        public async Task<EntityResult> Delete(int id)
        {
            var result = (EntityResult)await MobileDeviceWriter.DeleteAsync(id);
            if (result.Code == ResultCode.Success) result = (EntityResult)await LwpSettingWriter.DeleteAsync(id);

            return result;
        }

    }

}

