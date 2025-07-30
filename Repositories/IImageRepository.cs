using System;
using System.Net;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories;

public interface IImageRepository
{
    Task<Image> Upload(Image image);
}
