using System;
using System.ComponentModel.DataAnnotations;

namespace NZWalks.Models.DTO;

public class RegionsDto
{



    [Required]
    [MinLength(3, ErrorMessage = "Code must be at least 3 characters long")]
    [MaxLength(3, ErrorMessage = "Code must be at most 3 characters long")]
    public Guid Id { get; set; }
    [Required]
    [MaxLength(100, ErrorMessage = "Name must be at most 100 characters long")]
    public string Name { get; set; }
    public string Code { get; set; }
    public string? RegionImageUrl { get; set; }
}
