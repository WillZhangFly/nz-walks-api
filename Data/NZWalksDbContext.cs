using System;
using Microsoft.EntityFrameworkCore;
using NZWalks.Models.Domain;

namespace NZWalks.Data;

public class NZWalksDbContext : DbContext
{
    public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions)
        : base(dbContextOptions)
    {

    }

    public DbSet<Difficulty> Difficulties { get; set; }

    public DbSet<Region> Regions { get; set; }
    public DbSet<Walk> Walks { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        var difficulties = new List<Difficulty>()
        {
            new Difficulty
            {
                Id = Guid.Parse("54466f17-02af-48e7-8ed3-5a4a8bfacf6f"),
                Name = "Easy"
            },
            new Difficulty
            {
                Id = Guid.Parse("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                Name = "Medium"
            },
            new Difficulty
            {
                Id = Guid.Parse("f808ddcd-b5e5-4d80-b732-1ca523e48434"),
                Name = "Hard"
            }
        };

        modelBuilder.Entity<Difficulty>().HasData(difficulties);


        // seed data for Regions
        var regions = new List<Region>()
        {
            new Region
            {
                Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                Name = "North Island",
                Code = "NI",
                RegionImageUrl = "https://example.com/north-island.jpg"
            },
            new Region
            {
                Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                Name = "South Island",
                Code = "SI",
                RegionImageUrl = "https://example.com/south-island.jpg"
            },
            new Region
            {
                Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                Name = "Stewart Island",
                Code = "ST",
                RegionImageUrl = "https://example.com/stewart-island.jpg"
            },
            new Region
            {
                Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                Name = "Chatham Islands",
                Code = "CI",
                RegionImageUrl = "https://example.com/chatham-islands.jpg"
            },
            new Region
            {
                Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                Name = "Auckland",
                Code = "AKL",
                RegionImageUrl = "https://example.com/auckland.jpg"
            },
            new Region
            {
                Id = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                Name = "Wellington",
                Code = "WLG",
                RegionImageUrl = "https://example.com/wellington.jpg"
            },
            new Region
            {
                Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f7"),
                Name = "Christchurch",
                Code = "CHC",
                RegionImageUrl = "https://example.com/christchurch.jpg"
            }
        };

        modelBuilder.Entity<Region>().HasData(regions);
    }
}
