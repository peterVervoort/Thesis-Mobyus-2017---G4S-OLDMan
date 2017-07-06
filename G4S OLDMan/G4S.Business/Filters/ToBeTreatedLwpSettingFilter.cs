using System.Linq;
using G4S.DataAccess.Repositories;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System.Threading.Tasks;

namespace G4S.Business.Filters
{
    internal class ToBeTreatedLwpSettingFilter : EntityFilterBase<ToBeTreatedLwpSetting>, IEntityFilter<ToBeTreatedLwpSetting>
    {
        public override Task<IQueryable<ToBeTreatedLwpSetting>> FilterAsync(IQueryable<ToBeTreatedLwpSetting> query, SearchBase<ToBeTreatedLwpSetting> searchCriteria)
        {
            if (searchCriteria.GetType().Equals(typeof(ToBeTreatedLwpSettingSearchCriteria)))
            {

                ToBeTreatedLwpSettingSearchCriteria criteria = (ToBeTreatedLwpSettingSearchCriteria)searchCriteria;

                //if (!string.IsNullOrWhiteSpace(criteria.Taal))
                //{
                //    query = query.Where(x => x.Taal.Contains(criteria.Taal));
                //}
            }

            return base.FilterAsync(query, searchCriteria);
        }

        public override IQueryable<ToBeTreatedLwpSetting> Order(IQueryable<ToBeTreatedLwpSetting> query, SearchBase<ToBeTreatedLwpSetting> searchCriteria)
        {
            if (!string.IsNullOrEmpty(searchCriteria.SortField))
            {
                var descending = searchCriteria.SortDescending.HasValue && searchCriteria.SortDescending.Value;
                switch (searchCriteria.SortField)
                {
                    case "telephoneAlarmActivated":
                        query = descending ? query.OrderByDescending(x => x.TelephoneAlarmActivated) : query.OrderBy(x => x.TelephoneAlarmActivated);
                        break;
                    case "phoneNumbersForTelephoneAlarm":
                        query = descending ? query.OrderByDescending(x => x.PhoneNumbersForTelephoneAlarm) : query.OrderBy(x => x.PhoneNumbersForTelephoneAlarm);
                        break;
                    case "panicButtonActivated":
                        query = descending ? query.OrderByDescending(x => x.PanicButtonActivated) : query.OrderBy(x => x.PanicButtonActivated);
                        break;
                    case "movementDetectionActivated":
                        query = descending ? query.OrderByDescending(x => x.MovementDetectionActivated) : query.OrderBy(x => x.MovementDetectionActivated);
                        break;
                    case "timeBeforeMovementAlarmInSeconds":
                        query = descending ? query.OrderByDescending(x => x.TimeBeforeMovementAlarmInSeconds) : query.OrderBy(x => x.TimeBeforeMovementAlarmInSeconds);
                        break;
                    case "manDownAlarmActivated":
                        query = descending ? query.OrderByDescending(x => x.ManDownAlarmActivated) : query.OrderBy(x => x.ManDownAlarmActivated);
                        break;
                    case "angleOfManDownDetection":
                        query = descending ? query.OrderByDescending(x => x.AngleOfManDownDetection) : query.OrderBy(x => x.AngleOfManDownDetection);
                        break;
                    case "timeBeforeManDownAlarmInSeconds":
                        query = descending ? query.OrderByDescending(x => x.TimeBeforeManDownAlarmInSeconds) : query.OrderBy(x => x.TimeBeforeManDownAlarmInSeconds);
                        break;
                    case "schockAlarmActivated":
                        query = descending ? query.OrderByDescending(x => x.SchockAlarmActivated) : query.OrderBy(x => x.SchockAlarmActivated);
                        break;
                    case "fallAlarmActivated":
                        query = descending ? query.OrderByDescending(x => x.FallAlarmActivated) : query.OrderBy(x => x.FallAlarmActivated);
                        break;
                    case "timerAlarmActivated":
                        query = descending ? query.OrderByDescending(x => x.TimerAlarmActivated) : query.OrderBy(x => x.TimerAlarmActivated);
                        break;
                    case "timeBeforeTimerAlarmInSeconds":
                        query = descending ? query.OrderByDescending(x => x.TimeBeforeTimerAlarmInSeconds) : query.OrderBy(x => x.TimeBeforeTimerAlarmInSeconds);
                        break;
                    case "sendAlarmToExternalAlarmReciverActivated":
                        query = descending ? query.OrderByDescending(x => x.SendAlarmToExternalAlarmReciverActivated) : query.OrderBy(x => x.SendAlarmToExternalAlarmReciverActivated);
                        break;
                    case "uniqueIdentifierToSendToExternalAlarmReciever":
                        query = descending ? query.OrderByDescending(x => x.UniqueIdentifierToSendToExternalAlarmReciever) : query.OrderBy(x => x.UniqueIdentifierToSendToExternalAlarmReciever);
                        break;
                    case "exitGeofenceAreaWhenUserSignsOff":
                        query = descending ? query.OrderByDescending(x => x.ExitGeofenceAreaWhenUserSignsOff) : query.OrderBy(x => x.ExitGeofenceAreaWhenUserSignsOff);
                        break;
                    default:
                        break;
                }
            }

            return base.Order(query, searchCriteria);
        }
    }
}
