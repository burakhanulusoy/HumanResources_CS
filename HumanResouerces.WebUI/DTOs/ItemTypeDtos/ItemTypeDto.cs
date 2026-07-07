using HumanResouerces.WebUI.Base;
using HumanResources.WebUI.DTOs.ItemDtos;

namespace HumanResources.WebUI.DTOs.ItemTypeDtos
{
    public class ItemTypeDto:BaseDto
    {
        public string Ad { get; set; } // Örn: Laptop, Monitör, ESD Bileklik, Ýþ Ayakkabýsý

        public IList<ResultItemDto> Zimmetler { get; set; }







    }
}
