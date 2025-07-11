using System;
using System.ComponentModel.DataAnnotations;

namespace NZWalks.Models.DTO;

public class AddWalkRequestDto
{

    [Required]
    [MaxLength(100, ErrorMessage = "Name must be at most 100 characters long")]
    public string Name { get; set; }

    [Required]
    [MaxLength(500, ErrorMessage = "Description must be at most 500 characters long")]
    public string Description { get; set; }

    [Required]
    [MaxLength(50, ErrorMessage = "Difficulty must be at most 50 characters long")]
    public double LengthInKm { get; set; } // Length in kilometers

    public string? WalkImageUrl { get; set; } // Optional image URL for the walk

    [Required]
    public Guid DifficultyId { get; set; } // Foreign key to Difficulty

    [Required]
    public Guid RegionId { get; set; } // Foreign key to Region
}
