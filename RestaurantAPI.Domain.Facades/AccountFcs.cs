using RestaurantAPI.Domain.Common.Models.User;
using RestaurantAPI.Domain.Interfaces.Facades;
using RestaurantAPI.Domain.Interfaces.Infrastructure;

namespace RestaurantAPI.Domain.Facades;

public class AccountFcd : IAccountFcd
{
    private readonly IAccountSrv _accountSrv;

    public AccountFcd(IAccountSrv accountSrv)
    {
        _accountSrv = accountSrv;
    }

    public UserDTO LoginUser(LoginDTO loginDTO)
    {
        return _accountSrv.LoginUser(loginDTO);
    }

    public UserDTO RegisterUser(RegisterDTO registerDTO)
    {
        return _accountSrv.RegisterUser(registerDTO);
    }

    public UserAuthorizeDTO GetById(int? id)
    {
        return _accountSrv.GetById(id);
    }
}