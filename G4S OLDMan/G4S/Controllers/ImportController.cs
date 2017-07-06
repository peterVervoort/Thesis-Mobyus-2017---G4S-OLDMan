using G4S.Entities.Enums;
using System.Collections.Generic;
using System.Web.Http;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System;
using G4S.Business.Services;
using Microsoft.Practices.Unity;
using System.Net;
using System.Linq;
using G4S.Business.Handlers;

namespace G4S.Controllers
{
    [Authorize]
    public class ImportController : ApiController
    {
        [Dependency]
        protected ICsvService CsvService { get; set; }
        [Dependency]
        protected ICsvHandler CsvHandler { get; set; }



        [Authorize(Roles = SystemUserRole.CSVImport)]
        [ActionName("import")]
        [HttpPost]
        public async Task<IHttpActionResult> Import()
        {
            string filePath = null;
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
                }

                var provider = GetMultipartProvider();
                var result = await Request.Content.ReadAsMultipartAsync(provider);

                // On upload, files are given a generic name like "BodyPart_26d6abe1-3ae1-416a-9429-b35f15e6e5d5"
                filePath = result.FileData.First().LocalFileName;

                var models = CsvService.ReadCSV<Business.Models.CsvImportModel>(filePath, ';');

                await CsvHandler.HandleImportRecords(models);

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
            } finally
            {
                if (filePath != null) File.Delete(filePath);
            }
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