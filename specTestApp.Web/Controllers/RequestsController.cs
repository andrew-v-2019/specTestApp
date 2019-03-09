using specTestApp.Services;
using specTestApp.Services.Interfaces;
using specTestApp.Services.Services;
using specTestApp.Web.Models;
using System;
using System.Web.Mvc;

namespace specTestApp.Web.Controllers
{
    public class RequestsController : ControllerBase
    {

        private IFileService _fileService;
        private IRequestsService _requestsService;

        public RequestsController()
        {
            _fileService = new FileService();
            _requestsService = new RequestsService();
        }

        [Authorize(Roles = "Manager")]
        public ActionResult RequestsList()
        {
            return View();
        }


        [Authorize(Roles = "Client")]
        public async System.Threading.Tasks.Task<ActionResult> Create()
        {
            var model = new CreateRequestViewModel();
            await CheckAbilityForCeationAsync(model);

            return View(model);
        }



        [HttpPost]
        [Authorize(Roles = "Client")]
        public async System.Threading.Tasks.Task<ActionResult> Create(CreateRequestViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                await CheckAbilityForCeationAsync(model);

                if (model.DenyCreation)
                {
                    ModelState.AddModelError("", model.DenyCreationReason);
                    return View(model);
                }

                var userName = User.Identity.Name;
                var user = await UserManager.FindByEmailAsync(userName);
                var folder = Server.MapPath("~/");

                var fileSaveResult = _fileService.SaveFile(model.File, folder);
                await _requestsService.CreateRequestAsync(model, fileSaveResult, user.Id);

                return RedirectToAction("Create");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(model);
            }
        }

        private async System.Threading.Tasks.Task CheckAbilityForCeationAsync(CreateRequestViewModel model)
        {
            var userName = User.Identity.Name;
            var user = await UserManager.FindByEmailAsync(userName);

            if (!user.EmailConfirmed)
            {
                model.DenyCreation = true;
                model.DenyCreationReason = "Подтвердите Email перед созданием заявки";
                return;
            }
            else
            {
                var hours = await _requestsService.GetHoursFromLastRequestForUserAsync(user.Id);
                if (hours.HasValue && hours < 24)
                {
                    model.DenyCreation = true;
                    model.DenyCreationReason = $"{24 - hours.Value} ч. до создания новой заявки";
                }
            }
        }
    }
}