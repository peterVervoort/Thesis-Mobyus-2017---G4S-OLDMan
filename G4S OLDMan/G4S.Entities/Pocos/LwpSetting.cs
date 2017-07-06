using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G4S.Entities.Pocos
{
    public class LwpSetting : EntityBase
    {
        public virtual MobileDevice MobileDevice { get; set; }
        public bool? TelephoneAlarmActivated { get; set; }
        public string PhoneNumbersForTelephoneAlarm { get; set; }
        public bool? PanicButtonActivated { get; set; }
        public bool? MovementDetectionActivated { get; set; }
        public int? TimeBeforeMovementAlarmInSeconds { get; set; }
        public bool? ManDownAlarmActivated { get; set; }
        public int? AngleOfManDownDetection { get; set; }
        public int? TimeBeforeManDownAlarmInSeconds { get; set; }
        public bool? SchockAlarmActivated { get; set; }
        public bool? FallAlarmActivated { get; set; }
        public bool? TimerAlarmActivated { get; set; }
        public int? TimeBeforeTimerAlarmInSeconds { get; set; }
        public bool? SendAlarmToExternalAlarmReciverActivated { get; set; }
        public int? UniqueIdentifierToSendToExternalAlarmReciever { get; set; }
        public bool? ExitGeofenceAreaWhenUserSignsOff { get; set; }
    }
}
