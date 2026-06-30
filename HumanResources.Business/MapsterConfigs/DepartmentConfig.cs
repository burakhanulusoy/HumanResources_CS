using HumanResources.Business.DTOs.DepartmentDtos;
using HumanResources.Entity.Entities;
using Mapster;

namespace HumanResources.Business.MapsterConfigs
{
    public static class DepartmentConfig
    {
        public static void Configure()
        {
            TypeAdapterConfig<Departman, ResultDepartmentDto>.NewConfig()
                .Map(dest => dest.YoneticiAdSoyad,
                     src => src.Yonetici != null ? src.Yonetici.Ad + " " + src.Yonetici.Soyad : string.Empty)

                .Map(dest => dest.BirimlerCount, src => src.Birimler.Count)
                .Map(dest => dest.PersonellerCount, src => src.Personeller.Count);
        }


    }
}
