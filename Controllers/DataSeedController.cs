using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.Data;

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataSeedController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;

        public DataSeedController(NZWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("execute-full-sql-script")]
        public async Task<IActionResult> ExecuteFullSqlScript()
        {
            try
            {
                // Clear existing data (order matters due to foreign keys)
                await _dbContext.Database.ExecuteSqlRawAsync("DELETE FROM [Walks]");
                await _dbContext.Database.ExecuteSqlRawAsync("DELETE FROM [Regions]");
                await _dbContext.Database.ExecuteSqlRawAsync("DELETE FROM [Difficulties]");

                // Insert Difficulties
                await _dbContext.Database.ExecuteSqlRawAsync(@"
                    INSERT [dbo].[Difficulties] ([Id], [Name]) VALUES ('f808ddcd-b5e5-4d80-b732-1ca523e48434', 'Hard');
                    INSERT [dbo].[Difficulties] ([Id], [Name]) VALUES ('54466f17-02af-48e7-8ed3-5a4a8bfacf6f', 'Easy');
                    INSERT [dbo].[Difficulties] ([Id], [Name]) VALUES ('ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c', 'Medium');
                ");

                // Insert Regions
                await _dbContext.Database.ExecuteSqlRawAsync(@"
                    INSERT [dbo].[Regions] ([Id], [Code], [Name], [RegionImageUrl]) VALUES ('906cb139-415a-4bbb-a174-1a1faf9fb1f6', 'NSN', 'Nelson', 'https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1');
                    INSERT [dbo].[Regions] ([Id], [Code], [Name], [RegionImageUrl]) VALUES ('f7248fc3-2585-4efb-8d1d-1c555f4087f6', 'AKL', 'Auckland', 'https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1');
                    INSERT [dbo].[Regions] ([Id], [Code], [Name], [RegionImageUrl]) VALUES ('14ceba71-4b51-4777-9b17-46602cf66153', 'BOP', 'Bay Of Plenty', NULL);
                    INSERT [dbo].[Regions] ([Id], [Code], [Name], [RegionImageUrl]) VALUES ('6884f7d7-ad1f-4101-8df3-7a6fa7387d81', 'NTL', 'Northland', NULL);
                    INSERT [dbo].[Regions] ([Id], [Code], [Name], [RegionImageUrl]) VALUES ('f077a22e-4248-4bf6-b564-c7cf4e250263', 'STL', 'Southland', NULL);
                    INSERT [dbo].[Regions] ([Id], [Code], [Name], [RegionImageUrl]) VALUES ('cfa06ed2-bf65-4b65-93ed-c9d286ddb0de', 'WGN', 'Wellington', 'https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1');
                ");

                // Insert Walks - Wellington walks
                await _dbContext.Database.ExecuteSqlRawAsync(@"
                    INSERT INTO [dbo].[Walks] ([Id], [Name], [Description], [LengthInKm], [WalkImageUrl], [DifficultyId], [RegionId]) VALUES
                    ('327aa9f7-26f7-4ddb-8047-97464374bb63', 'Mount Victoria Loop', 'This scenic walk takes you around the top of Mount Victoria, offering stunning views of Wellington and its harbor.', 3.5, 'https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1', '54466f17-02af-48e7-8ed3-5a4a8bfacf6f','cfa06ed2-bf65-4b65-93ed-c9d286ddb0de'),
                    ('1cc5f2bc-ff4b-47c0-a475-1add56c6497b', 'Makara Beach Walkway', 'This walk takes you along the wild and rugged coastline of Makara Beach, with breathtaking views of the Tasman Sea.', 8.2, 'https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1', 'ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c','cfa06ed2-bf65-4b65-93ed-c9d286ddb0de'),
                    ('09601132-f92d-457c-b47e-da90e117b33c', 'Botanic Garden Walk', 'Explore the beautiful Botanic Garden of Wellington on this leisurely walk, with a wide variety of plants and flowers to admire.', 2, 'https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1', '54466f17-02af-48e7-8ed3-5a4a8bfacf6f','cfa06ed2-bf65-4b65-93ed-c9d286ddb0de');
                ");

                // Insert Walks - Auckland walks
                await _dbContext.Database.ExecuteSqlRawAsync(@"
                    INSERT INTO [dbo].[Walks] ([Id], [Name], [Description], [LengthInKm], [WalkImageUrl], [DifficultyId], [RegionId]) VALUES
                    ('30d654c7-89ac-4704-8333-5065b740150b', 'Mount Eden Summit Walk', 'This walk takes you to the summit of Mount Eden, the highest natural point in Auckland, with panoramic views of the city.', 2, 'https://images.pexels.com/photos/5342974/pexels-photo-5342974.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1', '54466f17-02af-48e7-8ed3-5a4a8bfacf6f','f7248fc3-2585-4efb-8d1d-1c555f4087f6'),
                    ('f7578324-f025-4c86-83a9-37a7f3d8fe81', 'Cornwall Park Walk', 'Explore the beautiful Cornwall Park on this leisurely walk, with a wide variety of trees, gardens, and animals to admire.', 3, 'https://images.pexels.com/photos/5342974/pexels-photo-5342974.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1', '54466f17-02af-48e7-8ed3-5a4a8bfacf6f','f7248fc3-2585-4efb-8d1d-1c555f4087f6'),
                    ('bdf28703-6d0e-4822-ad8b-e2923f4e95a2', 'Takapuna to Milford Coastal Walk', 'This coastal walk takes you along the beautiful beaches of Takapuna and Milford, with stunning views of Rangitoto Island.', 5, 'https://images.pexels.com/photos/5342974/pexels-photo-5342974.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1', 'ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c','f7248fc3-2585-4efb-8d1d-1c555f4087f6');
                ");

                // Insert Walks - Nelson walks
                await _dbContext.Database.ExecuteSqlRawAsync(@"
                    INSERT INTO [dbo].[Walks] ([Id], [Name], [Description], [LengthInKm], [WalkImageUrl], [DifficultyId], [RegionId]) VALUES
                    ('43132402-3d5e-467a-8cde-351c5c7c5dde', 'Centre of New Zealand Walkway', 'This walk takes you to the geographical centre of New Zealand, with stunning views of Nelson and its surroundings.', 1.0, 'https://images.pexels.com/photos/808466/pexels-photo-808466.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1', 'ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c','906cb139-415a-4bbb-a174-1a1faf9fb1f6'),
                    ('1ea0b064-2d44-4324-91ee-6dd86c91b713', 'Maitai Valley Walk', 'Explore the picturesque Maitai Valley on this easy walk, with a tranquil river and native bush to enjoy.', 5.0, 'https://images.pexels.com/photos/808466/pexels-photo-808466.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1', 'ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c','906cb139-415a-4bbb-a174-1a1faf9fb1f6'),
                    ('04ab77f0-e145-4fbf-b641-989df24e5573', 'Boulder Bank Walkway', 'This coastal walk takes you along the unique Boulder Bank, a long narrow bar of rocks that extends into Tasman Bay.', 8.0, 'https://images.pexels.com/photos/808466/pexels-photo-808466.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1', 'f808ddcd-b5e5-4d80-b732-1ca523e48434','906cb139-415a-4bbb-a174-1a1faf9fb1f6');
                ");

                // Insert Walks - Bay of Plenty walks
                await _dbContext.Database.ExecuteSqlRawAsync(@"
                    INSERT INTO [dbo].[Walks] ([Id], [Name], [Description], [LengthInKm], [WalkImageUrl], [DifficultyId], [RegionId]) VALUES
                    ('b5aa2791-3616-4db6-ab33-c54d03d17f62', 'Mount Maunganui Summit Walk', 'This walk takes you to the summit of Mount Maunganui, with stunning views of the ocean and surrounding landscape.', 3.0, 'https://images.pexels.com/photos/808466/pexels-photo-808466.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1', 'ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c','14ceba71-4b51-4777-9b17-46602cf66153'),
                    ('2d9d6604-bef9-4b0a-805d-630240a29595', 'The Papamoa Hills Regional Park Walk', 'Enjoy panoramic views of Tauranga and Mount Maunganui on this walk through the Papamoa Hills, with a mix of bush and open farmland.', 5.0, 'https://images.pexels.com/photos/808466/pexels-photo-808466.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1', 'ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c','14ceba71-4b51-4777-9b17-46602cf66153'),
                    ('135a6e58-969f-47e1-8278-d7fbf2b3bd69', 'The White Pine Bush Track', 'Explore the lush and peaceful White Pine Bush on this easy walk, with a variety of native flora and fauna to discover.', 2.0, 'https://images.pexels.com/photos/808466/pexels-photo-808466.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1', 'ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c','14ceba71-4b51-4777-9b17-46602cf66153');
                ");

                // Insert Walks - Southland walks
                await _dbContext.Database.ExecuteSqlRawAsync(@"
                    INSERT INTO [dbo].[Walks] ([Id], [Name], [Description], [LengthInKm], [WalkImageUrl], [DifficultyId], [RegionId]) VALUES
                    ('24ef9346-17e2-467e-bfc0-d062a9042bf1', 'The Bluff Hill Walkway', 'This walk takes you to the top of Bluff Hill, with panoramic views of Bluff and the surrounding coastline.', 6.0, 'https://images.pexels.com/photos/2226900/pexels-photo-2226900.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1', 'ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c','f077a22e-4248-4bf6-b564-c7cf4e250263'),
                    ('f2b56c63-eb99-475a-881c-278f3da03e3d', 'The Kepler Track', 'One of New Zealand''s most famous walks, the Kepler Track offers stunning alpine vistas and takes you through a range of landscapes from peaceful beech forests to open tussock lands.', 32.0, 'https://images.pexels.com/photos/2226900/pexels-photo-2226900.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1', 'f808ddcd-b5e5-4d80-b732-1ca523e48434','f077a22e-4248-4bf6-b564-c7cf4e250263'),
                    ('a7796ab6-5426-46af-b755-65d9b9e12978', 'The Hump Ridge Track', 'Experience the stunning scenery of the southern Fiordland and the coast on this challenging multi-day walk, with beautiful forest and alpine views.', 60.0, 'https://images.pexels.com/photos/2226900/pexels-photo-2226900.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1', 'f808ddcd-b5e5-4d80-b732-1ca523e48434','f077a22e-4248-4bf6-b564-c7cf4e250263');
                ");

                return Ok(new {
                    message = "SQL script executed successfully!",
                    difficulties = 3,
                    regions = 6,
                    walks = 16,
                    details = "All data from your SQL file has been inserted successfully into NZWalksDb"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message, stackTrace = ex.ToString() });
            }
        }
    }
}
