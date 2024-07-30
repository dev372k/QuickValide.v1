using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs.AppDTOs;
using Shared.Exceptions.Messages;
using Shared.Exceptions;
using System.Net;
using Domain;
using Shared.DTOs.SettingDTOs;
using Domain.Repositories.Services;

namespace Application.Services
{
    public class SettingRepo
    {
        private readonly IApplicationDBContext _context;
        private readonly ICloudflareService _cloudflareService;
        public SettingRepo(IApplicationDBContext context, ICloudflareService cloudflareService)
        {
            _context = context;
            _cloudflareService = cloudflareService;
        }

        public async Task<GetSettingDTO> GetAsync(int appid)
        {
            var apps = await _context.Set<App>()
            .Where(app => app.Id == appid)
            .Select(app => new GetSettingDTO
            {
                Name = app.Name,
                Domain = app.Domain,
                IsLive = app.IsLive

            })
            .FirstOrDefaultAsync();

            if (apps == null)
                throw new CustomException(HttpStatusCode.OK, ExceptionMessages.RECORD_NOT_FOUND);

            return apps;

        }

        public async Task UpdateAsync(int appid,UpdateSettingDTO request)
        {
            var app = await _context.Set<App>().FirstOrDefaultAsync(_ => _.Id == appid);
            if (app == null)
                throw new CustomException(HttpStatusCode.OK, ExceptionMessages.USER_DOESNOT_EXIST);

            if(app.Domain != request.Domain)
                 await _cloudflareService.UpdateDomain(app.RecordId, request.Domain);
            app.Name = request.Name;
            app.Domain = request.Domain;
           
           
            await _context.SaveChangesAsync();

        }

        public async Task UpdateAppStatusAsync(int appid, bool IsLive)
        {
            var app = await _context.Set<App>().FirstOrDefaultAsync(_ => _.Id == appid);
            if (app == null)
                throw new CustomException(HttpStatusCode.OK, ExceptionMessages.USER_DOESNOT_EXIST);

            app.IsLive = IsLive;
            await _context.SaveChangesAsync();

        }
    }
}
