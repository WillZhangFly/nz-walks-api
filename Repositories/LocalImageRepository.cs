using System;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories;

public class LocalImageRepository: IImageRepository
{
    private readonly IWebHostEnvironment webHostEnvironment;
    private readonly IHttpContextAccessor httpContextAccessor;

    private readonly NZWalksDbContext dbContext;

    public LocalImageRepository(IWebHostEnvironment webHostEnvironment,
    IHttpContextAccessor httpContextAccessor, NZWalksDbContext dbContext)
    {
        this.webHostEnvironment = webHostEnvironment;
        this.httpContextAccessor = httpContextAccessor;
        this.dbContext = dbContext;
    }
    public async Task<Image> Upload(Image image)
    {
        //Implement local file storage logic here
        var localImagePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", image.FileName + image.FileExtension);
        using var fileStream = new FileStream(localImagePath, FileMode.Create);
        await image.File.CopyToAsync(fileStream);

        var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

        image.FilePath = urlFilePath;


        //add logic to save image details to database if needed
        await dbContext.Images.AddAsync(image);
        await dbContext.SaveChangesAsync();

        return image;

    }
}

