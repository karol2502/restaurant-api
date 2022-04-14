using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Domain.Common.Models.Restaurant;
using RestaurantAPI.Domain.Interfaces.Facades;

namespace RestaurantAPI.UI.ASP.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RestaurantController : ControllerBase
{
    private readonly IRestaurantFcd _restaurantFcd;

    public RestaurantController(IRestaurantFcd restaurantFcd)
    {
        _restaurantFcd = restaurantFcd;
    }

    [HttpGet("{id}")]
    public ActionResult GetById([FromRoute] int id)
    {
        return Ok(_restaurantFcd.GetById(id));
    }

    [HttpGet]
    public ActionResult GetAll()
    {
        return Ok(_restaurantFcd.GetAll());
    }

    [HttpPost]
    public ActionResult CreateRestaurant([FromBody] CreateRestaurantDTO dto)
    {
        var id = _restaurantFcd.CreateRestaurant(dto);
        return Created($"/api/restaurant/{id}", null);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute] int id)
    {
        _restaurantFcd.DeleteRestaurant(id);

        return NoContent();
    }


    [HttpPut("{id}")]
    public ActionResult Update([FromRoute] int id, [FromBody] UpdateRestaurantDTO dto)
    {
        _restaurantFcd.UpdateRestaurant(id, dto);

        return Ok();
    }
}