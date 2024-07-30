using Domain;
using Domain.Entities;
using Domain.IRepositories;
using Domain.Repositories.Services;
using Microsoft.EntityFrameworkCore;
using Shared.Commons;
using Shared.DTOs.WaitlistDTOs;
using Shared.Exceptions.Messages;
using Shared.Exceptions;
using Shared.Helpers;
using System.Net;
using Omu.ValueInjecter;
using Shared.DTOs.AppDTOs;

namespace Application.Implementations
{
    public class WaitlistRepo : IWaitlistRepo
    {
        private ApplicationDBContext _context;
        public WaitlistRepo(ApplicationDBContext context) { 
        _context = context; 
        }

        public async Task<int> AddAsync(AddWaitlistDTO dto)
        {
            var emailExist = await _context.Waitlists.FirstOrDefaultAsync(_ => _.Email == dto.Email && _.AppId == dto.AppId);
            if (emailExist != null)
                throw new CustomException(HttpStatusCode.OK, ExceptionMessages.Email_ALREADY_EXIST);


            Waitlist waitlist = Mapper.Map<Waitlist>(dto);

            _context.Waitlists.Add(waitlist);
            _context.SaveChanges();

            return waitlist.Id;
        }

        public async Task<GetWaitlistDTO> GetAsync(int appid)
        {
            var waitList = await _context.Waitlists.Where(_ => _.AppId == appid).FirstOrDefaultAsync() ?? throw new CustomException(HttpStatusCode.OK, ExceptionMessages.APP_DOESNOT_EXIST);
            return Mapper.Map<GetWaitlistDTO>(waitList);
        }
    }
}
