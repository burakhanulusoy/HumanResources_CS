using HumanResources.Business.DTOs.DepartmentDtos;
using HumanResources.Entity.Entities;
using Mapster;

namespace HumanResources.Business.Mappings
{
    public class DepartmentMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Departman, DepartmentUnitsDto>()
                  .Map(dest => dest.YoneticiAdSoyad,
                       src => src.Yonetici != null
                              ? src.Yonetici.Ad + " " + src.Yonetici.Soyad
                              : null);
            // DepartmentUnitItemDto.PersonellerCount -> Birim.Personeller.Count
            // Mapster bunu otomatik flatten eder, ekstra satýr gerekmez.
        }
    }
}