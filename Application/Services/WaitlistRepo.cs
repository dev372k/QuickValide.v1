using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs.WaitlistDTOs;
using Shared.Exceptions.Messages;
using Shared.Exceptions;
using System.Net;
using Omu.ValueInjecter;

namespace Application.Implementations;

public class WaitlistRepo
{
    private IApplicationDBContext _context;
    public WaitlistRepo(IApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(AddWaitlistDTO dto)
    {
        var emailExist = await _context.Set<Waitlist>().FirstOrDefaultAsync(_ => _.Email == dto.Email && _.AppId == dto.AppId);
        if (emailExist != null)
            throw new CustomException(HttpStatusCode.OK, ExceptionMessages.Email_ALREADY_SUBSCRIBED);


        Waitlist waitlist = Mapper.Map<Waitlist>(dto);
        waitlist.CreatedAt = DateTime.Now;
        _context.Set<Waitlist>().Add(waitlist);
        await _context.SaveChangesAsync();

        return waitlist.Id;
    }

    public async Task<List<GetWaitlistDTO>> GetAsync(int appid) => await _context.Set<Waitlist>().Where(_ => _.AppId == appid).Select(_ => new GetWaitlistDTO
    {
        Id = _.Id,
        Email = _.Email,
        SelectedPlan = _.SelectedPlan,
        CreatedOn = _.CreatedAt.ToString("dd MMM, yyyy"),
    }).ToListAsync() ?? throw new CustomException(HttpStatusCode.OK, ExceptionMessages.APP_DOESNOT_EXIST);
}
