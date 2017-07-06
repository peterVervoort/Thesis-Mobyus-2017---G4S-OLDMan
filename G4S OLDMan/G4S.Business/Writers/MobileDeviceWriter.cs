using System.Threading.Tasks;
using G4S.Business.Helpers;
using G4S.Entities.Pocos;
using System;

namespace G4S.Business.Writers
{
    public class MobileDeviceWriter : Writer<MobileDevice>, IWriter<MobileDevice>
    {

        public override async Task<EntityResult<MobileDevice>> UpdateAsync(MobileDevice entity)
        {
            if (entity != null)
            {
                //entity.LwpSetting = null;
            }
            return await base.UpdateAsync(entity);
        }

    }
}
