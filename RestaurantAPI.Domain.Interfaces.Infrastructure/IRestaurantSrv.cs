using RestaurantAPI.Domain.Common.Models.Restaurant;

namespace RestaurantAPI.Domain.Interfaces.Infrastructure;

public interface IRestaurantSrv
{
    RestaurantDTO GetById(int id);
    IEnumerable<RestaurantDTO> GetAll();
    int CreateRestaurant(CreateRestaurantDTO dto);
    void DeleteRestaurant(int id);
    void UpdateRestaurant(int id, UpdateRestaurantDTO dto);
}