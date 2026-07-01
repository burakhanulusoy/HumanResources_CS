using HumanResources.Business.Base;
using HumanResources.Business.DTOs.ItemDtos;
using HumanResources.Entity.Entities;

namespace HumanResources.Business.DTOs.ItemTypeDtos
{
    public class ItemTypeDto:BaseDto
    {
        public string Ad { get; set; } // Örn: Laptop, Monitör, ESD Bileklik, Ýþ Ayakkabýsý

        public IList<ResultItemDto> Zimmetler { get; set; }







    }
}
