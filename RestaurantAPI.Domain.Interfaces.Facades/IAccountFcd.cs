using RestaurantAPI.Domain.Common.Models.User;

namespace RestaurantAPI.Domain.Interfaces.Facades;

public interface IAccountFcd
{
    UserDTO RegisterUser(RegisterDTO registerDTO);
    UserDTO LoginUser(LoginDTO loginDTO);
    UserAuthorizeDTO GetById(int? id);
}