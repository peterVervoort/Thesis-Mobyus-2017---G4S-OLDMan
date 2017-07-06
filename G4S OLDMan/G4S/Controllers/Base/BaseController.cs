using AutoMapper;
using G4S.Business.Helpers;
using G4S.Business.Repositories;
using G4S.Business.Services;
using G4S.Business.Writers;
using G4S.Controllers.Base;
using G4S.Entities.Enums;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using G4S.Models;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace G4S.Controllers
{
    public class BaseController<TEntity, TModel, TPostModel, TSearchModel> : ReturnValuesApiController<TEntity, TModel>
        where TEntity : EntityBase
        where TModel : ModelBase<TEntity>
        where TPostModel : PostModelBase<TEntity>
        where TSearchModel : SearchModelBase<TEntity>
    {
        [Dependency]
        protected IReader<TEntity> EntityReader { get; set; }
        [Dependency]
        protected IWriter<TEntity> EntityWriter { get; set; }
        [Dependency]
        protected ICsvService CsvService { get; set; }

        protected string[] IncludeFields = null;

        // GET: api/TEntity
        [Route("", Order = 0)]
        [HttpGet]
        public virtual async Task<IHttpActionResult> Get()
        {
            try
            {
                var entities = await EntityReader.GetAllAsync(IncludeFields);
                if (Request.Headers.Accept.Contains(new MediaTypeWithQualityHeaderValue("application/csv")))
                {
                    return CsvDownload(Mapper.Map<IEnumerable<TModel>>(entities));
                }
                return Ok(Mapper.Map<IEnumerable<TModel>>(entities));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/TEntity/5
        [Route("{id:int}")]
        [HttpGet]
        public virtual async Task<IHttpActionResult> GetById(int id)
        {
            try
            {
                var entity = await EntityReader.GetById(id , IncludeFields);
                if (entity == null) return NotFound();
                return Ok(Mapper.Map<TModel>(entity));
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


        // POST: api/TEntity/search
        [HttpPost]
        [ActionName("search")]
        public virtual async Task<IHttpActionResult> Search([FromBody]TSearchModel model)
        {
            try
            {
                if (model == null) return BadRequest("Model not found");
                if (ModelState.IsValid)
                {
                    var searchPoco = model.Map();
                    searchPoco.Deleted = model.IncludeDeleted ? DeleteOption.Both : DeleteOption.NotDeleted;
                    var entities = await EntityReader.Search(searchPoco, searchPoco.Deleted.Value, IncludeFields);
                    var models = Mapper.Map<IEnumerable<TModel>>(entities);
                    return Ok(models);
                }
                else
                {
                    return BadRequest(ModelState);
                }
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

        // POST: api/TEntity/searchcount
        [HttpPost]
        [ActionName("searchcount")]
        public virtual async Task<IHttpActionResult> SearchCount([FromBody]TSearchModel model)
        {
            try
            {
                if (model == null) return BadRequest("Model not found");
                if (ModelState.IsValid)
                {
                    var searchPoco = model.Map();
                    searchPoco.Deleted = model.IncludeDeleted ? DeleteOption.Both : DeleteOption.NotDeleted;
                    int count = await EntityReader.SearchCount(searchPoco, searchPoco.Deleted.Value);
                    return Ok(count);
                }
                else
                {
                    return BadRequest(ModelState);
                }
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

        // POST: api/TEntity
        [Route("")]
        [HttpPost]
        public virtual async Task<IHttpActionResult> Post([FromBody]TPostModel model)
        {
            try
            {
                if (model == null) return BadRequest("Model not found");
                if (ModelState.IsValid)
                {
                    var entity = Mapper.Map<TEntity>(model);
                    var result = await EntityWriter.InsertAsync(entity);
                    return OkEntityResult(result);
                } else
                {
                    return BadRequest(ModelState);
                }
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

        // PUT: api/TEntity/5
        [Route("{id:int}")]
        [HttpPut]
        public virtual async Task<IHttpActionResult> Put(int id, [FromBody]TPostModel model)
        {
            try
            {
                if (model == null) return BadRequest("Model not found");
                if (id != model.Id) return BadRequest("Id not matching");
                if (ModelState.IsValid)
                {
                    var entity = Mapper.Map<TEntity>(model);
                    var result = await EntityWriter.UpdateAsync(entity);
                    return OkEntityResult(result);
                } else
                {
                    return BadRequest(ModelState);
                }
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


        //TODO :: Patch

        // DELETE: api/TEntity/5
        [Route("{id:int}")]
        [HttpDelete]
        public virtual async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                var result = await EntityWriter.DeleteAsync(id);
                if (result.Code == Business.Helpers.ResultCode.Failed)
                {
                    return InternalServerError(result.Exception);
                }
                return NoContent();
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

        [Authorize(Roles = SystemUserRole.CSVImport)]
        [ActionName("import")]
        [HttpPost]
        public async Task<IHttpActionResult> Import()
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
                }

                var provider = GetMultipartProvider();
                var result = await Request.Content.ReadAsMultipartAsync(provider);

                // On upload, files are given a generic name like "BodyPart_26d6abe1-3ae1-416a-9429-b35f15e6e5d5"
                var filePath = result.FileData.First().LocalFileName;

                var models = CsvService.ReadCSV<TPostModel>(filePath);

                List<IHttpActionResult> results = new List<IHttpActionResult>();

                //TODO import method in writer
                foreach (var model in models)
                {
                    results.Add(await this.Post(model));
                }

                File.Delete(filePath);

                return Ok(models);
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

        private IHttpActionResult CsvDownload(IEnumerable<TModel> entities)
        {
            var fileInfo = CsvService.GetCSV(entities);
            var result = new FileStream(fileInfo.FullName, FileMode.Open)
            {
                Position = 0
            };
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StreamContent(result)
            };
            response.Content.Headers.ContentDisposition =
                       new ContentDispositionHeaderValue("attachment")
                       {
                           FileName = $"{typeof(TEntity)}.csv"
                       };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return ResponseMessage(response);
        }

        private MultipartFormDataStreamProvider GetMultipartProvider()
        {
            var uploadFolder = "~/App_Data/Tmp/FileUploads"; // you could put this to web.config
            var root = System.Web.HttpContext.Current.Server.MapPath(uploadFolder);
            Directory.CreateDirectory(root);
            return new MultipartFormDataStreamProvider(root);
        }

    }
}