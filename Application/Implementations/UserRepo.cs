using Domain;
using Shared.DTOs.UserDTOs;
using Shared.Helpers;
using Domain.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;
using System.Net;
using Shared.Exceptions.Messages;

namespace Application.Implementations;

public class UserRepo : IUserRepo
{
    private ApplicationDBContext _context;

    public UserRepo(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task AddAsync(AddUserDTO dto)
    {
        var userExist = GetAsync(dto.Email);
        if (userExist != null)
            throw new CustomException(HttpStatusCode.OK, ExceptionMessages.USER_ALREADY_EXIST);

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Password = SecurityHelper.GenerateHash(dto.Password),
            CreatedAt = DateTime.Now,
        };

        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public async Task<GetUserDTO> GetAsync(string email)
    {
        return await _context.Users.Where(_ => _.Email.ToLower().Equals(email.ToLower())).Select(_ => new GetUserDTO
        {
            Id = _.Id,
            Email = _.Email,
            Name = _.Name,
            Password = SecurityHelper.GenerateHash(_.Password),
        }).FirstOrDefaultAsync() ?? throw new CustomException(HttpStatusCode.OK, ExceptionMessages.USER_DOESNOT_EXIST);
    }

    public async Task<GetUserDTO> GetAsync(int id)
    {
        return await _context.Users.Where(_ => _.Id == id).Select(_ => new GetUserDTO
        {
            Id = _.Id,
            Email = _.Email,
            Name = _.Name,
            Password = SecurityHelper.GenerateHash(_.Password),
        }).FirstOrDefaultAsync() ?? throw new CustomException(HttpStatusCode.OK, ExceptionMessages.USER_DOESNOT_EXIST);
    }

    public async Task UpdateAsync(int id, UpdateUserDTO dto)
    {
        var user = await GetAsync(id);
        if (user == null)
            throw new Exception(ExceptionMessages.USER_DOESNOT_EXIST);

        if (user != null)
        {
            user.Name = dto.Name;
            _context.SaveChanges();
        }
    }
    
    public async Task DeleteAsync(int id)
    {
        var user = await GetAsync(id);
        if (user == null)
            throw new Exception(ExceptionMessages.USER_DOESNOT_EXIST);

        if (user != null)
        {
            user.IsDeleted = true;
            _context.SaveChanges();
        }
    }

    public async Task<IQueryable<GetUserDTO>> GetAsync()
    {
        var users = _context.Users.Select(_ => new GetUserDTO
        {
            Id = _.Id,
            Email = _.Email,
            Name = _.Name,
            IsDeleted = _.IsDeleted,
            IsActive = _.IsActive,

        });

        return users;
    }

    public async Task UpdateStatusAsync(int id, bool status)
    {
        var user = await _context.Users.FirstOrDefaultAsync(_ => _.Id == id);
        if (user != null)
        {
            user.IsDeleted = status;
            _context.SaveChanges();
        }
    }
}