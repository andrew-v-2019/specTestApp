using specTestApp.Services;
using specTestApp.Services.Interfaces;
using specTestApp.Services.Services;
using specTestApp.ViewModels;
using System.Collections.Generic;
using System.Web.Http;

namespace specTestApp.Web.Controllers
{
    [Authorize(Roles = "Manager")]
    public class RequestsApiController : ApiController
    {

        private IFileService _fileService;
        private IRequestsService _requestsService;

        public RequestsApiController()
        {
            _fileService = new FileService();
            _requestsService = new RequestsService();
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<IEnumerable<RequestListItemViewModel>> Get([FromUri]FilterViewModel filter)
        {
            
            var items = await _requestsService.GetRequestsAsync(filter);
            var folder = System.Web.Hosting.HostingEnvironment.MapPath("~/"); 
            _fileService.DecorateModelWithFileUrls(items, folder);
            return items;
        }

        //[HttpPut]
        //[ActionName("restore")]
        //public async System.Threading.Tasks.Task RestoreAsync(int id)
        //{
        //    await _requestsService.ActivateRequestAsync(id);
        //}

        // DELETE: api/RequestsApi/5
        [HttpPut]
        public async System.Threading.Tasks.Task Delete(int id)
        {
            await _requestsService.DeactivateRequestAsync(id);
        }
    }
}
