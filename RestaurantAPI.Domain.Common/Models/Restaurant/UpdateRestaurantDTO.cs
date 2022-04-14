using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Domain.Common.Models.Restaurant;

public class UpdateRestaurantDTO
{
    [Required]
    [MaxLength(25)]
    public string Name { get; set; }
    public string Description { get; set; }
    public bool HasDelivery { get; set; }
}