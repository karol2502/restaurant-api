namespace RestaurantAPI.Domain.Common.Models.Dish;

public class DishDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}