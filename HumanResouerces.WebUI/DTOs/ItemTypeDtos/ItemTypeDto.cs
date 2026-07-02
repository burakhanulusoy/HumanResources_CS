using HumanResouerces.WebUI.Base;
using HumanResources.Business.DTOs.ItemDtos;

namespace HumanResources.Business.DTOs.ItemTypeDtos
{
    public class ItemTypeDto:BaseDto
    {
        public string Ad { get; set; } // Örn: Laptop, Monitör, ESD Bileklik, Ýþ Ayakkabýsý

        public IList<ResultItemDto> Zimmetler { get; set; }







    }
}
