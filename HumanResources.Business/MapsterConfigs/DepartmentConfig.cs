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
                // Yöneticinin Ad ve Soyadýný birleþtiriyoruz
                .Map(dest => dest.YoneticiAdSoyad,
                     src => src.Yonetici != null ? src.Yonetici.Ad + " " + src.Yonetici.Soyad : string.Empty)

                // Entity Framework'ün COUNT sorgusu atmasý için Mapster'a garanti bir þekilde öðretiyoruz
                .Map(dest => dest.BirimlerCount, src => src.Birimler.Count)
                .Map(dest => dest.PersonellerCount, src => src.Personeller.Count);
        }


    }
}
