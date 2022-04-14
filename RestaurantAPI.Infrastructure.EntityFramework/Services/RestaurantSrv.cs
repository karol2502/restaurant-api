using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Domain.Common.Exceptions;
using RestaurantAPI.Domain.Common.Models.Dish;
using RestaurantAPI.Domain.Common.Models.Restaurant;
using RestaurantAPI.Domain.Interfaces.Infrastructure;
using RestaurantAPI.Infrastructure.EntityFramework.Entities;

namespace RestaurantAPI.Infrastructure.EntityFramework.Services;

public class RestaurantSrv : IRestaurantSrv
{
    private readonly RestaurantDbContext _restaurantDbContext;

    public RestaurantSrv(RestaurantDbContext restaurantDbContext)
    {
        _restaurantDbContext = restaurantDbContext;
    }

    public int CreateRestaurant(CreateRestaurantDTO dto)
    {
        var restaurant = new Restaurant
        {
            Name = dto.Name,
            Description = dto.Description,
            Category = dto.Description,
            HasDelivery = dto.HasDelivery,
            ContactEmail = dto.ContactEmail,
            ContactNumber = dto.ContactNumber,
            Address = new Address
            {
                City = dto.City,
                Street = dto.Street,
                PostalCode = dto.PostalCode
            }
        };

        _restaurantDbContext.Restaurants.Add(restaurant);
        _restaurantDbContext.SaveChanges();
        return restaurant.Id;
    }

    public void DeleteRestaurant(int id)
    {
        var restaurant = _restaurantDbContext
            .Restaurants
            .FirstOrDefault(r => r.Id == id);

        if (restaurant == null) throw new NotFoundException("Restaurant not found");

        _restaurantDbContext.Restaurants.Remove(restaurant);
        _restaurantDbContext.SaveChanges();
    }

    public IEnumerable<RestaurantDTO> GetAll()
    {
        var restaurants = _restaurantDbContext
            .Restaurants
            .Include(r => r.Address)
            .Include(r => r.Dishes)
            .ToList();

        var restaurantDTOs = new List<RestaurantDTO>();

        foreach (var restaurant in restaurants)
        {
            var dishes = new List<DishDTO>();

            foreach (var dish in restaurant.Dishes)
                dishes.Add(new DishDTO
                {
                    Id = dish.Id,
                    Name = dish.Name,
                    Description = dish.Description,
                    Price = dish.Price
                });
            restaurantDTOs.Add(new RestaurantDTO
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Description = restaurant.Description,
                Category = restaurant.Category,
                HasDelivery = restaurant.HasDelivery,
                City = restaurant.Address.City,
                Street = restaurant.Address.Street,
                PostalCode = restaurant.Address.PostalCode,
                Dishes = dishes
            });
        }

        return restaurantDTOs;
    }

    public RestaurantDTO GetById(int id)
    {
        var restaurant = _restaurantDbContext
            .Restaurants
            .Include(r => r.Address)
            .Include(r => r.Dishes)
            .FirstOrDefault(r => r.Id == id);

        if (restaurant == null) throw new NotFoundException("Restaurant not found");

        var dishes = new List<DishDTO>();
        foreach (var dish in restaurant.Dishes)
            dishes.Add(new DishDTO
            {
                Id = dish.Id,
                Name = dish.Name,
                Description = dish.Description,
                Price = dish.Price
            });

        return new RestaurantDTO
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Description = restaurant.Description,
            Category = restaurant.Category,
            HasDelivery = restaurant.HasDelivery,
            City = restaurant.Address.City,
            Street = restaurant.Address.Street,
            PostalCode = restaurant.Address.PostalCode,
            Dishes = dishes
        };
    }

    public void UpdateRestaurant(int id, UpdateRestaurantDTO dto)
    {
        var restaurant = _restaurantDbContext
            .Restaurants
            .FirstOrDefault(r => r.Id == id);
        if (restaurant == null) throw new NotFoundException("Restaurant not found");

        restaurant.Name = dto.Name;
        restaurant.Description = dto.Description;
        restaurant.HasDelivery = dto.HasDelivery;

        _restaurantDbContext.Restaurants.Update(restaurant);
        _restaurantDbContext.SaveChanges();
    }
}