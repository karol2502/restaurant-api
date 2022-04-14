using RestaurantAPI.Domain.Common.Models.User;

namespace RestaurantAPI.Domain.Interfaces.Infrastructure;

public interface IAccountSrv
{
    UserDTO RegisterUser(RegisterDTO registerDTO);
    UserDTO LoginUser(LoginDTO loginDTO);
    UserAuthorizeDTO GetById(int? id);
}