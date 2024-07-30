using Domain;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Omu.ValueInjecter;
using Shared;
using Shared.DTOs.UserSubscriptionDTOs;
using Shared.Exceptions;
using Shared.Exceptions.Messages;
using System.Net;

namespace Application.Implementations
{
    public class UserSubscriptionRepo
    {
        private IApplicationDBContext _context;
        private IStateHelper _stateHelper;
        public UserSubscriptionRepo(IApplicationDBContext context, IStateHelper stateHelper) {
            _context = context;
            _stateHelper = stateHelper;
        }
        public async Task AddAsync(AddUserSubscriptionDTO dto)
        {
            //var emailExist = await _context.UserSubscriptions.FirstOrDefaultAsync(_ => _.UserId == dto.UserId );
            //if (emailExist != null)
            //    throw new CustomException(HttpStatusCode.OK, ExceptionMessages.Email_ALREADY_EXIST);


            UserSubscription userSubscription = Mapper.Map<UserSubscription>(dto);

            _context.Set<UserSubscription>().Add(userSubscription);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var userSubscription = await _context.Set<UserSubscription>().FirstOrDefaultAsync(_ => _.Id == id);
            if (userSubscription == null)
                throw new CustomException(HttpStatusCode.OK, ExceptionMessages.SUBSCRIPTION_DOESNOT_EXIST);

            userSubscription.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
        public async Task UpdateStatusAsync(int id,bool status)
        {
            var userSubscription = await _context.Set<UserSubscription>().FirstOrDefaultAsync(_ => _.Id == id);
            if (userSubscription == null)
                throw new CustomException(HttpStatusCode.OK, ExceptionMessages.SUBSCRIPTION_DOESNOT_EXIST);

            userSubscription.IsActive = status;
            await _context.SaveChangesAsync();
        }

        public async Task<GetUserSubscriptionDTO> GetAsync()
        {
            var userSubscription = await _context.Set<UserSubscription>().Where(_ => _.UserId == _stateHelper.User().Id
            ).FirstOrDefaultAsync() ?? throw new CustomException(HttpStatusCode.OK, ExceptionMessages.SUBSCRIPTION_DOESNOT_EXIST);
            return Mapper.Map<GetUserSubscriptionDTO>(userSubscription);
        }

        public async Task UpdateAsync(int id, UpdateUserSubscriptionDTO dto)
        {
            var userSubscription = await _context.Set<UserSubscription>().FirstOrDefaultAsync(_ => _.Id == id);
            if (userSubscription == null)
                throw new CustomException(HttpStatusCode.OK, ExceptionMessages.SUBSCRIPTION_DOESNOT_EXIST);

            userSubscription.Subscription = dto.Subscription;
            userSubscription.StartDate = dto.StartDate;
            userSubscription.EndDate = dto.EndDate;
            await _context.SaveChangesAsync();
        }
    }
}
