using AssetManager.API.Context.Models;
using AssetManager.Shared.Dtos;
using AutoMapper;

namespace AssetManager.API.Extensions
{
    public class AutoMapperProFile:Profile
    {
        public AutoMapperProFile()
        {
            CreateMap<PlatformDto, Platform>().ReverseMap();

            CreateMap<PlatformAssetDto, PlatformAsset>().ReverseMap()
                .ForMember(dest=>dest.TargetPlatform,opt=>opt.MapFrom(src=>src.TargetPlatform));

            CreateMap<AssetPackageDto, AssetPackage>().ReverseMap();

            CreateMap<ProjectDto, Project>().ReverseMap();
        }
    }
}
