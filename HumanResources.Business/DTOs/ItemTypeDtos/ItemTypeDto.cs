using HumanResources.Business.Base;
using HumanResources.Business.DTOs.DemirbasDtos;
namespace HumanResources.Business.DTOs.ItemTypeDtos
{
    public class ItemTypeDto : BaseDto
    {
        public string Ad { get; set; }
        public IList<ResultDemirbasDto> Demirbaslar { get; set; }
    }
}