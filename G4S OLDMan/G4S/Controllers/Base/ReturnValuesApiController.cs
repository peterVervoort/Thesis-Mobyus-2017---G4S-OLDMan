using AutoMapper;
using G4S.Business.Helpers;
using G4S.Entities.Pocos;
using G4S.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace G4S.Controllers.Base
{
    public class ReturnValuesApiController<TEntity, TModel> : ApiController 
        where TEntity : EntityBase 
        where TModel : ModelBase<TEntity>
    {

        #region ReturnValues
        protected IHttpActionResult OkEntityResult(EntityResult<TEntity> result, object value = null)
        {
            if (result == null) return BadRequest("No EntityResult found");
            switch (result.Code)
            {
                case ResultCode.Failed:
                    return InternalServerError(result.Exception);
                case ResultCode.ValidationError:
                    return BadRequest(string.Join(";", result.ValidationMessages));
                case ResultCode.Success:
                    return Ok(value ?? Mapper.Map<TModel>(result.Entity));
                default:
                    break;
            }
            return BadRequest("Unknown EntityResult code");
        }

        protected IHttpActionResult OkEntityResult(EntityResult result, object value = null)
        {
            if (result == null) return BadRequest("No EntityResult found");
            switch (result.Code)
            {
                case ResultCode.Failed:
                    return InternalServerError(result.Exception);
                case ResultCode.ValidationError:
                    return BadRequest(string.Join(";", result.ValidationMessages));
                case ResultCode.Success:
                    return Ok(value);
                default:
                    break;
            }
            return BadRequest("Unknown EntityResult code");
        }

        protected IHttpActionResult CreatedEntityResult(EntityResult<TEntity> result, string location, object value)
        {
            if (result == null) return BadRequest("No EntityResult found");
            switch (result.Code)
            {
                case ResultCode.Failed:
                    return InternalServerError(result.Exception);
                case ResultCode.ValidationError:
                    return BadRequest(string.Join(";", result.ValidationMessages));
                case ResultCode.Success:
                    return Created(location, value);
                default:
                    break;
            }
            return BadRequest("Unknown EntityResult code");
        }

        protected override ExceptionResult InternalServerError(Exception exception)
        {
            //Hier kunnen we later afhankelijk van de omgeving exceptions niet meer naar de user geven maar een algemene fout tonen
            return base.InternalServerError(exception);
        }

        protected StatusCodeResult NoContent()
        {
            return StatusCode(HttpStatusCode.NoContent);
        }

     

        #endregion

    }
}