using RestaurantAPI.Infrastructure.EntityFramework.Entities;

namespace RestaurantAPI.Infrastructure.EntityFramework;

public class DbSeeder
{
    private readonly RestaurantDbContext _dbContext;

    public DbSeeder(RestaurantDbContext restaurantDbContext)
    {
        _dbContext = restaurantDbContext;
    }
    
    public void Seed()
    {
        if (_dbContext.Database.CanConnect())
            if (!_dbContext.Roles.Any())
            {
                var roles = GetRoles();
                _dbContext.Roles.AddRange(roles);
                _dbContext.SaveChanges();
            }
    }

    private IEnumerable<Role> GetRoles()
    {
        var roles = new List<Role>
        {
            new()
            {
                Name = "User"
            },
            new()
            {
                Name = "Admin"
            }
        };
        return roles;
    }
}