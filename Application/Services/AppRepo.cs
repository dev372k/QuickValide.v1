using Domain;
using Domain.Entities;
using Domain.Repositories.Services;
using Microsoft.EntityFrameworkCore;
using Omu.ValueInjecter;
using Shared.DTOs.AppDTOs;
using Shared.Exceptions;
using Shared.Exceptions.Messages;
using System.Net;
using Shared;
using Newtonsoft.Json;

namespace Application.Implementations;

public class AppRepo
{
    private readonly IApplicationDBContext _context;
    private readonly ICloudflareService _cloudflareService;
    private readonly IStateHelper _stateHelper;

    public AppRepo(IApplicationDBContext context, ICloudflareService cloudflareService, IStateHelper stateHelper)
    {
        _context = context;
        _cloudflareService = cloudflareService;
        _stateHelper = stateHelper;
    }

    public async Task<List<GetAppNameDTO>> GetAsync()
    {
        var apps = await _context.Set<App>()
        .Where(app => app.UserId == _stateHelper.User().Id)
        .Select(app => new GetAppNameDTO
        {
            Id = app.Id,
            Name = app.Name,
            Domain = app.Domain!,
            CreatedAt = app.CreatedAt,
            IsDefault = app.IsDefault
        })
        .ToListAsync();

        if (apps == null)
            throw new CustomException(HttpStatusCode.OK, ExceptionMessages.RECORD_NOT_FOUND);

        return apps;

    }
    public async Task DeleteAsync(int id)
    {
        var app = await _context.Set<App>().FirstOrDefaultAsync(_ => _.Id == id);
        if (app == null)
            throw new Exception(ExceptionMessages.APP_DOESNOT_EXIST);
        else if(app.IsDefault)
            throw new Exception(ExceptionMessages.DEFAULT_APP_CANNOT_DELETE);

        await _cloudflareService.DeleteDomain(app.RecordId);
        app.IsDeleted = true;
        await _context.SaveChangesAsync();
    }

    public async Task AddAsync(UpsertAppDTO request)
    {
        App app = Mapper.Map<App>(request);
        request.SEO.Title = request.Name;
        app.SEO = JsonConvert.SerializeObject(request.SEO);
        app.Style = JsonConvert.SerializeObject(request.Style);
        var appExist = _context.Set<App>().Any(_ => _.Domain == app.Domain);

        if (appExist)
            throw new CustomException(HttpStatusCode.BadRequest, ExceptionMessages.DOMAIN_ALREADY_EXIST);

        var recordId = await _cloudflareService.AddDomain(app.Domain);
        app.RecordId = recordId;
        _context.Set<App>().Add(app);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, UpsertAppDTO request)
    {
        var app = _context.Set<App>()
            .Where(_ => _.Id == id)
            .FirstOrDefault();

        if (app == null)
            throw new Exception(ExceptionMessages.APP_DOESNOT_EXIST);
        else if (app.Domain != request.Domain)
            await _cloudflareService.UpdateDomain(app.RecordId!, app.Domain!);

        app = Mapper.Map<App>(request);
        _context.Set<App>().Update(app);
        await _context.SaveChangesAsync();
    }

    public async Task<GetAppDTO> GetAsync(int id)
    {
        GetAppDTO getAppDTO = null;
        var app = await _context.Set<App>().Where(_ => _.Id == id).FirstOrDefaultAsync();

        if (app != null)
        {
            getAppDTO = Mapper.Map<GetAppDTO>(app);
            getAppDTO.SEO = !String.IsNullOrEmpty(app?.SEO) ? JsonConvert.DeserializeObject<SEO>(app.SEO) : null; 
            getAppDTO.Style = !String.IsNullOrEmpty(app?.Style) ? JsonConvert.DeserializeObject<Style>(app.Style) : null;
        }

        return getAppDTO;
    }
    public async Task<GetAppDTO> GetAsync(string name)
    {
        GetAppDTO getAppDTO = null;
        var app = await _context.Set<App>().Where(_ => _.Name == name).FirstOrDefaultAsync();

        if (app != null)
        {
            getAppDTO = Mapper.Map<GetAppDTO>(app);
            getAppDTO.SEO = !String.IsNullOrEmpty(app?.SEO) ? JsonConvert.DeserializeObject<SEO>(app.SEO) : null;
            getAppDTO.Style = !String.IsNullOrEmpty(app?.Style) ? JsonConvert.DeserializeObject<Style>(app.Style) : null;
        }

        return getAppDTO;
    }
    public async Task UpdateGoogleURLAsync(int id, string url)
    {
        var app = await _context.Set<App>().Where(_ => _.Id == id).FirstOrDefaultAsync()
            ?? throw new CustomException(HttpStatusCode.OK, ExceptionMessages.APP_DOESNOT_EXIST);

        app.GoogleURL = url;
        await _context.SaveChangesAsync();
    }

    public async Task<GetAnalyticsDTO> GetGoogleURLAsync(int id)
    {
        var app = await _context.Set<App>().Where(_ => _.Id == id).FirstOrDefaultAsync()
            ?? throw new CustomException(HttpStatusCode.OK, ExceptionMessages.APP_DOESNOT_EXIST);

        return new GetAnalyticsDTO { URL = app.GoogleURL };
    }
    public async Task UpdateSEOAsync(int id, UpdateSEODTO dto)
    {
        var app = await _context.Set<App>().Where(_ => _.Id == id).FirstOrDefaultAsync()
            ?? throw new CustomException(HttpStatusCode.OK, ExceptionMessages.APP_DOESNOT_EXIST);

        app.SEO = JsonConvert.SerializeObject(dto);
        await _context.SaveChangesAsync();
    }

    public async Task<GetSEODTO> GetSEOAsync(int id)
    {
        var app = await _context.Set<App>().Where(_ => _.Id == id).FirstOrDefaultAsync()
            ?? throw new CustomException(HttpStatusCode.OK, ExceptionMessages.APP_DOESNOT_EXIST);

        return JsonConvert.DeserializeObject<GetSEODTO>(app.SEO);
    }
}
