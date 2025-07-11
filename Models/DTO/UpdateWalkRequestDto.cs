using System;

namespace NZWalks.Models.DTO;

public class UpdateWalkRequestDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double LengthInKm { get; set; } // Length in kilometers

    public string? WalkImageUrl { get; set; } // Optional image URL for the walk

    public Guid DifficultyId { get; set; } // Foreign key to Difficulty
    public Guid RegionId { get; set; } // Foreign key to Region

}
