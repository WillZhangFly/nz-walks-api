using System;
using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories;

public class SQLWalkRepository : IWalkRepository
{
    private readonly NZWalksDbContext dbContext;


    public SQLWalkRepository(NZWalksDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<Walk> CreateAsync(Walk walk)
    {
        await dbContext.Walks.AddAsync(walk);
        await dbContext.SaveChangesAsync();
        return walk;
    }

    public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, string? sortOrder = null)
    {
        // return await dbContext.Walks.Include(x => x.Difficulty).Include(x => x.Region).ToListAsync();
        var walks = dbContext.Walks.Include(x => x.Difficulty).Include(x => x.Region).AsQueryable();

        if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
        {
            if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                walks = walks.Where(x => x.Name.Contains(filterQuery));
            }
        }

        // Apply sorting if specified
        if (!string.IsNullOrWhiteSpace(sortBy) && !string.IsNullOrWhiteSpace(sortOrder))
        {
            var isAscending = sortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase);

            walks = sortBy.ToLower() switch
            {
                "name" => isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name),
                "description" => isAscending ? walks.OrderBy(x => x.Description) : walks.OrderByDescending(x => x.Description),
                "lengthinkm" or "length" => isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm),
                "region" => isAscending ? walks.OrderBy(x => x.Region.Name) : walks.OrderByDescending(x => x.Region.Name),
                "difficulty" => isAscending ? walks.OrderBy(x => x.Difficulty.Name) : walks.OrderByDescending(x => x.Difficulty.Name),
                _ => walks // Default: no sorting if sortBy field is not recognized
            };
        }

        return await walks.ToListAsync();
    }

    public async Task<Walk?> GetByIdAsync(Guid id)
    {
        return await dbContext.Walks.Include(x => x.Difficulty).Include(x => x.Region).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
    {
        var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
        if (existingWalk == null)
        {
            return null;
        }

        existingWalk.Name = walk.Name;
        existingWalk.Description = walk.Description;
        existingWalk.LengthInKm = walk.LengthInKm;
        existingWalk.WalkImageUrl = walk.WalkImageUrl;
        existingWalk.RegionId = walk.RegionId;
        existingWalk.DifficultyId = walk.DifficultyId;

        await dbContext.SaveChangesAsync();
        return existingWalk;
    }

    public async Task<Walk?> DeleteAsync(Guid id)
    {
        var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
        if (existingWalk == null)
        {
            return null;
        }

        dbContext.Walks.Remove(existingWalk);
        await dbContext.SaveChangesAsync();
        return existingWalk;
    }

}
