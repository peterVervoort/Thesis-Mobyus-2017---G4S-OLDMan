using AutoMapper;
using G4S.Business.Repositories;
using G4S.Entities.Pocos;
using G4S.Models;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace G4S.Controllers
{
    [Authorize]
    public class StatekindsController : ApiController
    {
        [Dependency]
        public IReader<StateKind> EntityReader { get; set; }

        // GET: api/TEntity
        [HttpGet]
        public virtual async Task<IHttpActionResult> Get()
        {
            try
            {
                var entities = await EntityReader.GetAllAsync();
                return Ok(Mapper.Map<IEnumerable<StateKindModel>>(entities));
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
