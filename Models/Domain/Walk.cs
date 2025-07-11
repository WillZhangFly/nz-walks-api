using System;

namespace NZWalks.Models.Domain;

public class Walk
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double LengthInKm { get; set; } // Length in kilometers

    public string? WalkImageUrl { get; set; } // Optional image URL for the walk

    public Guid DifficultyId { get; set; } // Foreign key to Difficulty
    public Guid RegionId { get; set; } // Foreign key to Region

    // Navigation properties
    public Difficulty Difficulty { get; set; } // Navigation property to Difficulty
    public Region Region { get; set; } // Navigation property to Region
}
