using System.Linq;
using Microsoft.AspNetCore.Identity;
using RestaurantAPI.Domain.Common.Exceptions;
using RestaurantAPI.Domain.Common.Models.User;
using RestaurantAPI.Domain.Common.Utils;
using RestaurantAPI.Domain.Interfaces.Infrastructure;
using RestaurantAPI.Infrastructure.EntityFramework.Entities;

namespace RestaurantAPI.Infrastructure.EntityFramework.Services;

public class AccountSrv : IAccountSrv
{
    private readonly IJwtUtils _jwtUtils;
    private readonly RestaurantDbContext _dbContext;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AccountSrv(RestaurantDbContext dbContext, IJwtUtils jwtUtils, IPasswordHasher<User> passwordHasher)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _jwtUtils = jwtUtils;
    }

    public UserDTO LoginUser(LoginDTO loginDTO)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Email == loginDTO.Email);
        if (user == null) throw new LoginException();
        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDTO.Password);
        if (result == PasswordVerificationResult.Failed) throw new LoginException();

        var userAuth = new UserAuthorizeDTO
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PasswordHash = user.PasswordHash
        };
        var token = _jwtUtils.GenerateJWT(userAuth);
        return new UserDTO
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Token = token
        };
    }

    public UserDTO RegisterUser(RegisterDTO registerDTO)
    {
        var emailInUse = _dbContext.Users.Any(u => u.Email == registerDTO.Email);
        if (emailInUse) throw new Exception("That email is taken");

        var nameUserCount = _dbContext.Users.Count(u => u.FirstName == registerDTO.FirstName).ToString();
        var tag = nameUserCount
            .PadLeft(4, '0')
            .Insert(0, "#");
        var newUser = new User
        {
            Email = registerDTO.Email,
            FirstName = registerDTO.FirstName,
            LastName = registerDTO.LastName,
            RoleId = registerDTO.RoleId
        };

        var hashedPassword = _passwordHasher.HashPassword(newUser, registerDTO.Password);
        newUser.PasswordHash = hashedPassword;

        _dbContext.Users.Add(newUser);
        _dbContext.SaveChanges();

        var userAuth = new UserAuthorizeDTO
        {
            Id = newUser.Id,
            Email = newUser.Email,
            FirstName = newUser.FirstName,
            LastName = newUser.LastName,
            PasswordHash = newUser.PasswordHash
        };
        var token = _jwtUtils.GenerateJWT(userAuth);

        return new UserDTO
        {
            Id = userAuth.Id,
            FirstName = userAuth.FirstName,
            LastName = userAuth.LastName,
            Email = userAuth.Email,
            Token = token
        };
    }

    public UserAuthorizeDTO GetById(int? id)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
       
        return new UserAuthorizeDTO
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PasswordHash = user.PasswordHash
        };
    }
}