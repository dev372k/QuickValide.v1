using Domain;
using Shared.DTOs.UserDTOs;
using Shared.Helpers;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;
using System.Net;
using Shared.Exceptions.Messages;
using Shared.Commons;
using Google.Apis.Auth;
using Domain.Repositories.Services;

namespace Application.Implementations;

public class UserRepo
{
    private IApplicationDBContext _context;
    private IEmailService _emailService;
    private AppRepo _appRepo;

    public UserRepo(IApplicationDBContext context, IEmailService emailService, AppRepo appRepo)
    {
        _context = context;
        _emailService = emailService;
        _appRepo = appRepo;
    }

    public async Task<int> AddAsync(AddUserDTO dto)
    {
        var userExist = await _context.Set<User>().FirstOrDefaultAsync(_ => _.Email == dto.Email);
        if (userExist != null)
            throw new CustomException(HttpStatusCode.OK, ExceptionMessages.USER_ALREADY_EXIST);

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Role = enRole.User,
            Password = !String.IsNullOrEmpty(dto.Password) ? SecurityHelper.GenerateHash(dto.Password) : String.Empty,
            CreatedAt = DateTime.Now,
        };

        _context.Set<User>().Add(user);
        await _context.SaveChangesAsync();

        string guid = Guid.NewGuid().ToString().Replace("-", "");
        string sampleAppName = $"Sample App {guid}";
        string sampleAppDomain = $"sample-app-{guid}".ToLower();

        await _appRepo.AddAsync(new Shared.DTOs.AppDTOs.UpsertAppDTO() { UserId = user.Id, Name = sampleAppName, Email = dto.Email, Domain = sampleAppDomain, IsDefault = true});

        return user.Id;
    }

    public async Task<GetUserDTO> GetAsync(string email)
    {
        return await _context.Set<User>()
            .Where(_ => _.Email.ToLower().Equals(email.ToLower()))
            .Select(_ => new GetUserDTO
            {
                Id = _.Id,
                Email = _.Email,
                Name = _.Name,
                Role = _.Role.ToString(),
                Password = _.Password,
            }).FirstOrDefaultAsync() ?? throw new CustomException(HttpStatusCode.OK, ExceptionMessages.USER_DOESNOT_EXIST);
    }

    public async Task<GetUserDTO> GetAsync(int id)
    {
        return await _context.Set<User>().Where(_ => _.Id == id).Select(_ => new GetUserDTO
        {
            Id = _.Id,
            Email = _.Email,
            Name = _.Name,
            Role = _.Role.ToString(),
            Password = _.Password,
        }).FirstOrDefaultAsync() ?? throw new CustomException(HttpStatusCode.OK, ExceptionMessages.USER_DOESNOT_EXIST);
    }

    public async Task UpdateAsync(int id, UpdateUserDTO dto)
    {
        var user = await _context.Set<User>().FirstOrDefaultAsync(_ => _.Id == id);
        if (user == null)
            throw new CustomException(HttpStatusCode.OK, ExceptionMessages.USER_DOESNOT_EXIST);

        user.Name = dto.Name;
        await _context.SaveChangesAsync();

    }

    public async Task<string> LoginAsync(LoginDTO dto)
    {
        var user = await GetAsync(dto.Email);
        if (user == null)
            throw new CustomException(HttpStatusCode.OK, ExceptionMessages.USER_DOESNOT_EXIST);
        else if (!SecurityHelper.ValidateHash(dto.Password, user.Password))
            throw new CustomException(HttpStatusCode.OK, ExceptionMessages.INVALID_CREDENTIALS);

        return JWTHelper.CreateToken(new GetUserDTO
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role.ToString()
        });
    }

    public async Task<string> GoogleLoginAsync(GoogleLoginDTO dto)
    {
        var payload = await GoogleJsonWebSignature.ValidateAsync(dto.IdToken);

        var user = await _context.Set<User>().FirstOrDefaultAsync(_ => _.Email == payload.Email);
        if (user != null)
            return JWTHelper.CreateToken(new GetUserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString()
            });
        else
        {
            var userId = await AddAsync(new AddUserDTO
            {
                Name = payload.Name,
                Email = payload.Email,
            });

            return JWTHelper.CreateToken(new GetUserDTO
            {
                Id = userId,
                Name = payload.Name,
                Email = payload.Email,
                Role = enRole.User.ToString()
            });
        }
    }

    public async Task DeleteAsync(int id)
    {
        var user = await GetAsync(id);
        if (user != null)
        {
            user.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IQueryable<GetUserDTO>> GetAsync()
    {
        var users = _context.Set<User>().Select(_ => new GetUserDTO
        {
            Id = _.Id,
            Email = _.Email,
            Name = _.Name,
            Role = _.Role.ToString(),
            IsDeleted = _.IsDeleted,
            IsActive = _.IsActive,
        });

        return users;
    }

    public async Task UpdateStatusAsync(int id, bool status)
    {
        var user = await _context.Set<User>().FirstOrDefaultAsync(_ => _.Id == id);
        if (user == null)
            throw new CustomException(HttpStatusCode.OK, ExceptionMessages.USER_DOESNOT_EXIST);

        user.IsDeleted = status;
        await _context.SaveChangesAsync();

    }

    public async Task UpdatePasswordAsync(string email)
    {
        var user = await _context.Set<User>().FirstOrDefaultAsync(_ => _.Email == email);
        if (user == null)
            throw new CustomException(HttpStatusCode.OK, ExceptionMessages.USER_DOESNOT_EXIST);

        string newPassword = SecurityHelper.GeneratePassword();
        user.Password = SecurityHelper.GenerateHash(newPassword);
        await _context.SaveChangesAsync();
        await _emailService.SendEmailAsync(email, EmailTemplate.NEW_PASSWORD_SUBJECT, string.Format(EmailTemplate.NEW_PASSWORD_BODY, newPassword));
    }
}