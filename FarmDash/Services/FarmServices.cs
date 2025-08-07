using FarmDash.Data;
using FarmDash.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace FarmDash.Services;

public class FarmServices
{
    private readonly AppDBContext _context;

    public FarmServices(AppDBContext context)
    {
        _context = context;
    }

    public async Task<List<Farm>> GetFarmsListAsync()
    {
        return await _context.Farms.ToListAsync();
    }

    public async Task UpdateFarmAsync(Farm farm)
    {
        var existingFarm = await _context.Farms.FirstOrDefaultAsync(f => f.Id == farm.Id);
        if (existingFarm != null)
        {
            existingFarm.Name = farm.Name;
            existingFarm.FarmID = farm.FarmID;
            existingFarm.Description = farm.Description;
            existingFarm.Location = farm.Location;
            existingFarm.Animal = farm.Animal;
            existingFarm.DeathRate = farm.DeathRate;
            existingFarm.SickRate = farm.SickRate;
            existingFarm.State = farm.State;
            existingFarm.Created = DateTime.UtcNow;
        }
        await _context.SaveChangesAsync();
    }

    public async Task<Farm> GetFarmByIdAsync(int id)
    {
        return await _context.Farms.FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<Farm> DeleteFarmAsync(Farm farm)
    {
        _context.Farms.Remove(farm);
        await _context.SaveChangesAsync();
        return farm;
    }
}