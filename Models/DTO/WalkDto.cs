using System;

namespace NZWalks.Models.DTO;

public class WalkDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double LengthInKm { get; set; } // Length in kilometers

    public string? WalkImageUrl { get; set; } // Optional image URL for the walk

    public RegionsDto Region { get; set; }
    public DifficultyDto Difficulty { get; set; } // Navigation property to Difficulty

}
