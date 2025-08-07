namespace FarmDash.Controllers;

using Microsoft.AspNetCore.Mvc;
using FarmDash.Models;
using FarmDash.Data;

[ApiController]
[Route("api/[controller]")]
public class FarmController : ControllerBase
{
    private readonly AppDBContext _context;

    public FarmController(AppDBContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult GetAll() => Ok(_context.Farms.ToList());

    [HttpPost]
    public IActionResult AddFarm(Farm farm)
    {
        _context.Farms.Add(farm);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetAll), new { id = farm.Id }, farm);
    }
    
    public IActionResult DeleteFarm(int id)
    {
        var farm = _context.Farms.Find(id);
        _context.Farms.Remove(farm);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFarm(int id, [FromBody] Farm updateFarm)
    {
        var farm = _context.Farms.Find(id);
        if (farm == null) return NotFound();
        
        farm.Id = updateFarm.Id;
        farm.FarmID = updateFarm.FarmID;
        farm.Name = updateFarm.Name;
        farm.Location = updateFarm.Location;
        farm.Description = updateFarm.Description;
        farm.Animal = updateFarm.Animal;
        farm.DeathRate = updateFarm.DeathRate;
        farm.SickRate = updateFarm.SickRate;
        farm.State = updateFarm.State;
        farm.Created = DateTime.UtcNow;
        
        _context.SaveChanges();

        return Ok(farm);
    }
}