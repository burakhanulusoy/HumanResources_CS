using HumanResources.Business.DTOs.UnitDtos;
using HumanResources.Entity.Entities;
using Mapster;

namespace HumanResources.Business.Mappings
{
    public class UnitMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Birim, UnitWithUserDto>()
                  .Map(dest => dest.DepartmanAd, src => src.Departman.Ad)
                  .Map(dest => dest.YoneticiAdSoyad,
                       src => src.Departman.Yonetici != null
                              ? src.Departman.Yonetici.Ad + " " + src.Departman.Yonetici.Soyad
                              : "Atanmamýþ");
        }
    }
}