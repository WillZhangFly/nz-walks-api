using System;
using AutoMapper;
using NZWalks.Models;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;

namespace NZWalks.Mapping;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Region, RegionsDto>()
        .ReverseMap();

        CreateMap<AddRegionRequestDto, Region>().ReverseMap();

        CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();

        CreateMap<AddWalkRequestDto, Walk>().ReverseMap();

        CreateMap<Walk, WalkDto>()
            .ReverseMap();

        CreateMap<Difficulty, DifficultyDto>()
            .ReverseMap();

        CreateMap<UpdateWalkRequestDto, Walk>()
            .ReverseMap();

    }
}
