using Domain;
using Domain.Entities;
using Domain.IRepositories;
using Domain.Repositories.Services;
using Microsoft.EntityFrameworkCore;
using Omu.ValueInjecter;
using Shared.DTOs.AppDTOs;
using Shared.Exceptions;
using Shared.Exceptions.Messages;
using System.Net;

namespace Application.Implementations;

public class AppRepo : IAppRepo
{
    private readonly ApplicationDBContext _context;

    private readonly ICloudflareService _cloudflareService;
    public AppRepo(ApplicationDBContext context, ICloudflareService cloudflareService)
    {
        _context = context;
        _cloudflareService = cloudflareService;
    }

    public async Task<List<GetAppNameDTO>> GetNames(int id)
    {
        var apps = await _context.Apps
        .Where(app => app.Id == id)
        .Select(app => new GetAppNameDTO
        {
            Id = app.Id,
            Name = app.Name
        })
        .ToListAsync();

        if (apps == null)
            throw new CustomException(HttpStatusCode.OK, ExceptionMessages.RECORD_NOT_FOUND);

        return apps;

    }
    public async Task DeleteAsync(int id)
    {
        var app = await _context.Apps.FirstOrDefaultAsync(_ => _.Id == id);
        if (app == null)
            throw new Exception(ExceptionMessages.APP_DOESNOT_EXIST);

        app.IsDeleted = true;
        _context.SaveChanges();
    }

    public async Task AddAsync(AddAppDTO request)
    {
        App app = Mapper.Map<App>(request);
        var appExist = await _context.Apps.FirstOrDefaultAsync(_ => _.Domain == app.Domain);

        if (appExist != null)
            throw new CustomException(HttpStatusCode.BadRequest, ExceptionMessages.DOMAIN_ALREADY_EXIST);

        bool isDomainConfig = await _cloudflareService.DomainConfig(app.Domain, "www.quickvalide.com/" + Guid.NewGuid());
        if (!isDomainConfig)
            throw new CustomException(HttpStatusCode.BadRequest, ExceptionMessages.DOMAIN_CONFIGURATION_ISSUE);

        _context.Apps.Add(app);
        _context.SaveChanges();
    }

    public async Task UpdateAsync(UpdateAddAppDTO request)
    {
        App app = Mapper.Map<App>(request);
        string? Domain = _context.Apps.Where(_ => _.Id == app.Id).FirstOrDefault().Domain;

        if (app.Domain != Domain)
        {
            bool isDomainConfig = await _cloudflareService.DomainConfig(app.Domain, "www.quickvalide.com/" + Guid.NewGuid());
        }

        _context.Apps.Update(app);
        _context.SaveChanges();
    }

    public async Task<GetAppDTO> GetAsync(int id)
    {
        var app = await _context.Apps.Where(_ => _.Id == id).FirstOrDefaultAsync() ?? throw new CustomException(HttpStatusCode.OK, ExceptionMessages.APP_DOESNOT_EXIST);
        return Mapper.Map<GetAppDTO>(app);
    }

    public async Task UpdateGoogleURLAsync(int id, string url)
    {
        var app = await _context.Apps.Where(_ => _.Id == id).FirstOrDefaultAsync()
            ?? throw new CustomException(HttpStatusCode.OK, ExceptionMessages.APP_DOESNOT_EXIST);

        app.GoogleURL = url;
        _context.SaveChanges();
    }

    public async Task<string> GetGoogleURLAsync(int id)
    {
        var app = await _context.Apps.Where(_ => _.Id == id).FirstOrDefaultAsync()
            ?? throw new CustomException(HttpStatusCode.OK, ExceptionMessages.APP_DOESNOT_EXIST);

        return app.GoogleURL
    }
}
