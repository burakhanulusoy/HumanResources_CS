using HumanResouerces.WebUI.Base;
using HumanResources.WebUI.DTOs.DemirbasDtos;
namespace HumanResources.WebUI.DTOs.ItemTypeDtos
{
    public class ItemTypeDto : BaseDto
    {
        public string Ad { get; set; }
        public IList<ResultDemirbasDto> Demirbaslar { get; set; }
    }
}