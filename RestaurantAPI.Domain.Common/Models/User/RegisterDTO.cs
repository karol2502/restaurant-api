using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Domain.Common.Models.User;

public class RegisterDTO
{
    [Required] public string Email { get; set; }

    [Required] public string FirstName { get; set; }

    [Required] public string LastName { get; set; }

    [Required][MinLength(6)] public string Password { get; set; }

    public int RoleId { get; set; } = 1;
}