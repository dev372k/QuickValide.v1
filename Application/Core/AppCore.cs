using Domain.ICore;
using Domain.IRepositories;
using Shared.DTOs.AppDTOs;
using Domain.Entities;
using AutoMapper;
using Azure.Core;
using Application.Implementations;
using Shared.Exceptions.Messages;
using Shared.Extensions;
using Shared.Exceptions;
using System.Net;

namespace Application.Core
{
    public class AppCore : IAppCore
    {
        private readonly IAppRepo _iAppRepo;
        private readonly IMapper _mapper;
        public AppCore(IAppRepo iAppRepo, IMapper mapper)
        {
            iAppRepo = _iAppRepo;
            _mapper = mapper;
        }
        public async Task Add(AddAppDTO request)
        {
                App app = _mapper.Map<App>(request);
                await _iAppRepo.AddAsync(app);
        }
        public async Task Update(UpdateAddAppDTO request)
        {
            App app = _mapper.Map<App>(request);
            _iAppRepo.AddAsync(app);
        }
        public async Task Delete(int Id)
        {
            await _iAppRepo.DeleteAsync(Id);
        }

        public async Task<GetAppDTO> GetById(int id)
        {
            App app = await _iAppRepo.GetByIdAsync(id);
            return _mapper.Map<GetAppDTO>(app);
        }

        public async Task<List<GetAppNameDTO>> GetNames(int AppId)
        {
            return await _iAppRepo.GetNames(AppId);
            
        }

        public Task<List<GetAppDTO>> Get()
        {
            throw new NotImplementedException();
        }
    }
}
