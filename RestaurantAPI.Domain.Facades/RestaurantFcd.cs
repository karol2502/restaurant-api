using RestaurantAPI.Domain.Common.Models.Restaurant;
using RestaurantAPI.Domain.Interfaces.Facades;
using RestaurantAPI.Domain.Interfaces.Infrastructure;

namespace RestaurantAPI.Domain.Facades;

public class RestaurantFcd : IRestaurantFcd
{
    private readonly IRestaurantSrv _restaurantSrv;

    public RestaurantFcd(IRestaurantSrv restaurantSrv)
    {
        _restaurantSrv = restaurantSrv;
    }

    public int CreateRestaurant(CreateRestaurantDTO dto)
    {
        return _restaurantSrv.CreateRestaurant(dto);
    }

    public void DeleteRestaurant(int id)
    {
        _restaurantSrv.DeleteRestaurant(id);
    }

    public IEnumerable<RestaurantDTO> GetAll()
    {
        return _restaurantSrv.GetAll();
    }

    public RestaurantDTO GetById(int id)
    {
        return _restaurantSrv.GetById(id);
    }

    public void UpdateRestaurant(int id, UpdateRestaurantDTO dto)
    {
        _restaurantSrv.UpdateRestaurant(id, dto);
    }
}