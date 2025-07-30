using System;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories;

public class LocalImageRepository: IImageRepository
{
    private readonly IWebHostEnvironment webHostEnvironment;
    public LocalImageRepository(IWebHostEnvironment webHostEnvironment)
    {
        this.webHostEnvironment = webHostEnvironment;
    }
    public async Task<Image> Upload(Image image)
    {
        //Implement local file storage logic here
        var localImagePath = Path.Combine(webHostEnvironment.WebRootPath, "images", image.FileName, image.FileExtension);
        using var fileStream = new FileStream(localImagePath, FileMode.Create);
        await image.File.CopyToAsync(fileStream);

    }
}

