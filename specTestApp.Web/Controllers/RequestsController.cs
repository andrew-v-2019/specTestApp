using specTestApp.Services;
using specTestApp.Services.Interfaces;
using specTestApp.Services.Services;
using specTestApp.Web.Models;
using System;
using System.Web.Mvc;
using specTestApp.Web.Infrastructure;

namespace specTestApp.Web.Controllers
{
    [LogException]
    public class RequestsController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IRequestsService _requestsService;
        private readonly IUserService _userService;
        private readonly EmailService _emailService;

        public RequestsController()
        {
            IConfigurationProvider configurationProvider = new ConfigurationProvider();
            _fileService = new FileService();
            _requestsService = new RequestsService();
            _userService = new UserService();
            _emailService = new EmailService(configurationProvider);
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

                //Отправка новой заявки мэнэджэрам
                var managerEmails = await _userService.GetManagersEmails();
                await _emailService.SendAsync(model.Caption, model.Message, managerEmails);

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

            var hours = await _requestsService.GetHoursFromLastRequestForUserAsync(user.Id);
            if (hours.HasValue && hours < 24)
            {
                model.DenyCreation = true;
                model.DenyCreationReason = $"{24 - hours.Value} ч. до создания новой заявки";
            }
        }
    }
}