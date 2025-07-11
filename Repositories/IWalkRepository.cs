using System;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories;

public interface IWalkRepository
{
    Task<Walk> CreateAsync(Walk walk);

    Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, string? sortOrder = null, int pageNumber = 1, int pageSize = 3);

    Task<Walk?> GetByIdAsync(Guid id);

    Task<Walk?> UpdateAsync(Guid id, Walk walk);

    Task<Walk?> DeleteAsync(Guid id);
}
