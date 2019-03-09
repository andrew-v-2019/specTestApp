using specTestApp.Services;
using specTestApp.Services.Interfaces;
using specTestApp.Services.Services;
using specTestApp.ViewModels;
using System.Collections.Generic;
using System.Web.Http;

namespace specTestApp.Web.Controllers
{
    //[Authorize(Roles = "Manager")]
    public class RequestsApiController : ApiController
    {

        private IFileService _fileService;
        private IRequestsService _requestsService;

        public RequestsApiController()
        {
            _fileService = new FileService();
            _requestsService = new RequestsService();
        }


        // GET: api/RequestsApi
       // [Route("api/requests/get")]
        [HttpGet]
        public async System.Threading.Tasks.Task<IEnumerable<RequestListItemViewModel>> Get()
        {
            var filter = new FilterViewModel();
            var items = await _requestsService.GetRequestsAsync(filter);
            _fileService.DecorateModelWithFileUrls(items);
            return items;
        }

        // PUT: api/RequestsApi/restore/5
        [HttpPut]
        public async System.Threading.Tasks.Task RestoreAsync(int id)
        {
            await _requestsService.ActivateRequestAsync(id);
        }

        // DELETE: api/RequestsApi/5
        [HttpDelete]
        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            await _requestsService.DeactivateRequestAsync(id);
        }
    }
}
