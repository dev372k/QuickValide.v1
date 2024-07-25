using AutoMapper;
using Domain;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs.AppDTOs;
using Shared.Exceptions;
using Shared.Exceptions.Messages;
using System.Net;

namespace Application.Implementations
{
    public class AppRepo : Repository<App>, IAppRepo
    {
        private ApplicationDBContext _context;
        private readonly IMapper _mapper;
        public AppRepo(ApplicationDBContext context,IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;

        }
        public Task<List<GetAppNameDTO>> GetNames(int id)
        {
            var apps =  _context.Apps
            .Where(app => app.Id == id)
            .Select(app => new GetAppNameDTO
            {
                Id = app.Id,
                Name = app.Name
            })
            .ToListAsync();

            if (apps == null)
            {
                throw new CustomException(HttpStatusCode.OK, ExceptionMessages.RECORD_NOT_FOUND);
            }

            return apps;

        }
        public async Task DeleteAsync(int id)
        {
            var app = await GetByIdAsync(id);
            if (app == null)
                throw new Exception(ExceptionMessages.USER_DOESNOT_EXIST);

            if (app != null)
            {
                app.IsDeleted = true;
                _context.SaveChanges();
            }
        }
        public async Task AddAsync(AddAppDTO request)
        {
            App app = _mapper.Map<App>(request);
            var appExist = _context.Apps.Where<App>(_ => _.Domain ==  app.Domain);
            if (appExist != null)
                throw new CustomException(HttpStatusCode.OK, ExceptionMessages.Domain_ALREADY_EXIST);

            _context.Apps.Add(app);
            _context.SaveChanges();
        }
        public async Task UpdateAsync(UpdateAddAppDTO request)
        {
            App app = _mapper.Map<App>(request);
            _context.Apps.Update(app);
            _context.SaveChanges();
        }
        public async Task<GetAppDTO> GetById(int id)
        {
            var app = await _context.Apps.Where(_ => _.Id == id).FirstOrDefaultAsync() ?? throw new CustomException(HttpStatusCode.OK, ExceptionMessages.APP_DOESNOT_EXIST);
            return _mapper.Map<GetAppDTO>(app);

        }

        
    }
}
