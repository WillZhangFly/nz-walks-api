using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.Data;

public class NZWalksAuthDbContext : IdentityDbContext
{
    public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var readerId = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de");
        var writerId = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81");

        var roles = new List<IdentityRole>
        {
            new IdentityRole {
                Id = readerId.ToString(),
                ConcurrencyStamp = readerId.ToString(),
                Name = "Reader",
                NormalizedName = "READER"
         },
            new IdentityRole {
                Id = writerId.ToString(),
                ConcurrencyStamp = writerId.ToString(),
                Name = "Writer",
                NormalizedName = "WRITER" },
        };

        builder.Entity<IdentityRole>().HasData(roles);
    }
}

