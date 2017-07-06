using AutoMapper;
using G4S.Business.Handlers;
using G4S.Business.Repositories;
using G4S.Business.Writers;
using G4S.Entities.Enums;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using G4S.Models;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace G4S.Controllers
{
    [Authorize(Roles = SystemUserRole.DasBoardX)]
    public class DashboardController : ApiController
    {
        [Dependency]
        public IReader<Translation> TranslationReader { get; set; }
        [Dependency]
        public IReader<State> StateReader { get; set; }
        [Dependency]
        public IReader<MobileDevice> DeviceReader { get; set; }


        [Route("api/dashboard/missingtranslation")]
        [HttpGet]
        public async Task<IHttpActionResult> MissingTranslation()
        {
            try
            {
                var missingCount = await TranslationReader.SearchCount(t => t.Value.Contains("[TBT]"));
                var totalCount = await TranslationReader.SearchCount(t => true);
                decimal percentage = ((decimal)missingCount / (decimal)totalCount) * 100;
                var returnModel = new
                {
                    value = missingCount,
                    total = totalCount,
                    percentage = (int)percentage
                };
                return Ok(returnModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/dashboard/tags")]
        [HttpGet]
        public async Task<IHttpActionResult> GetStateTags()
        {
            try
            {
                var states = await StateReader.GetAllAsync();
                var groups = states.GroupBy(s => s.Tag);
                IEnumerable<string> tags = groups.Select(g => g.Key);
                return Ok(tags);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/dashboard/tagdetails/{tagName}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetStateTagDetails(string tagName)
        {
            try
            {
                var devices = await DeviceReader.Search(new MobileDeviceSearchCriteria { TagName = tagName, Deleted = DeleteOption.NotDeleted });
                var timespans = devices.Select(d =>
                {
                    var lastState = d.RepairChanges?.OrderByDescending(rc => rc.ChangeDate)?.FirstOrDefault();
                    if (lastState == null) return 0;
                    return (DateTime.Now - lastState.ChangeDate).TotalDays;
                });

                var group = timespans.GroupBy(ts => (int)ts);
                var select = group.Select(g => new { Days = -g.Key, count = g.Count() });

                return Ok(select.OrderBy(x => x.Days));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/dashboard/tag/{tagName}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetStateTag(string tagName)
        {
            try
            {
                var deviceCount = await DeviceReader.SearchCount(s =>
                            s.RepairChanges.OrderByDescending(rc => rc.ChangeDate).FirstOrDefault() != null
                                ? s.RepairChanges.OrderByDescending(rc => rc.ChangeDate).FirstOrDefault().RepairStateChange.StateTo.Tag == tagName
                                : false);
                var totalCount = await DeviceReader.SearchCount(t => true);
                decimal percentage = ((decimal)deviceCount / (decimal)totalCount) * 100;
                var returnModel = new
                {
                    value = deviceCount,
                    total = totalCount,
                    percentage = (int)(percentage)
                };
                return Ok(returnModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


    }
}
