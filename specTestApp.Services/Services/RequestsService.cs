using specTestApp.Data;
using specTestApp.Data.Entities;
using specTestApp.Services.Interfaces;
using specTestApp.ViewModels;
using specTestApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace specTestApp.Services.Services
{
    public class RequestsService : IRequestsService
    {

        public async System.Threading.Tasks.Task<List<RequestListItemViewModel>> GetRequestsAsync(FilterViewModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                var query = context.Requests.OrderByDescending(x => x.CreatedDate).Select(x => x);

                if (!model.ShowInActive)
                {
                    query = query.Where(x => !x.IsDeleted).Select(x => x);
                }
                query = query.Skip(model.Skip).Take(model.Take);

                var models = await query.Select(x => Project(x)).ToListAsync();

                var usersIds = models.Select(x => x.UserId).ToList();
                var users = await context.Users.Where(x => usersIds.Contains(x.Id))
                    .ToDictionaryAsync(x => x.Id, x => x.UserName);
                models.ForEach(x => DecorateModel(x, users));

                return models;
            }
        }

        private void DecorateModel(RequestListItemViewModel model, IDictionary<string, string> users)
        {
            var modelUserId = model.UserId;

            if (users.ContainsKey(modelUserId))
            {
                model.UserName = users[modelUserId];
            }

            model.CreationDateString = model.CreationDate.ToString("g");
        }

        private RequestListItemViewModel Project(Request request)
        {
            var model = new RequestListItemViewModel()
            {
                Caption = request.Caption,
                CreationDate = request.CreatedDate,
                Message = request.Message,
                RequestId = request.RequestId,
                OrigFileName = request.OriginalFileName,
                FileUrl = request.FileName,
                UserId = request.CreatedBy,
            };
            return model;
        }

        public async System.Threading.Tasks.Task DeactivateRequestAsync(int requestId)
        {
            using (var context = new ApplicationDbContext())
            {
                var request = await context.Requests.FirstOrDefaultAsync(x => x.RequestId == requestId);

                if (request == null)
                {
                    return;
                }

                request.IsDeleted = true;
                await context.SaveChangesAsync();
            }
        }

        public async System.Threading.Tasks.Task ActivateRequestAsync(int requestId)
        {
            using (var context = new ApplicationDbContext())
            {
                var request = await context.Requests.FirstOrDefaultAsync(x => x.RequestId == requestId);

                if (request == null)
                {
                    return;
                }

                request.IsDeleted = false;
                await context.SaveChangesAsync();
            }
        }

        public async System.Threading.Tasks.Task<int?> GetHoursFromLastRequestForUserAsync(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var lastRequest = await context.Requests.Where(x => x.CreatedBy.Equals(userId))
                      .OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync();
                if (lastRequest == null)
                {
                    return null;
                }

                var lastDate = lastRequest.CreatedDate;
                var utcNow = DateTime.UtcNow;
                var diff = utcNow - lastDate;
                var hours = (int)Math.Round(diff.TotalHours);

                return hours;
            }
        }

        private Request Project(CreateRequestViewModel model, FileSaveResult file, string currentUserId)
        {
            var request = new Request();
            request.Caption = model.Caption.Trim();
            request.Message = model.Message.Trim();
            request.FileName = file.FileName;
            request.OriginalFileName = file.OrigFileName;
            request.CreatedDate = DateTime.UtcNow;
            request.CreatedBy = currentUserId;
            request.IsDeleted = false;
            return request;
        }

        public async System.Threading.Tasks.Task<CreateRequestViewModel> CreateRequestAsync(CreateRequestViewModel model, FileSaveResult file, string currentUserId)
        {
            var request = Project(model, file, currentUserId);

            using (var context = new ApplicationDbContext())
            {
                context.Requests.Add(request);
                await context.SaveChangesAsync();
            }

            return model;
        }
    }
}
