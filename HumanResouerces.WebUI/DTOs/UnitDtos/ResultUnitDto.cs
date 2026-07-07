using HumanResouerces.WebUI.Base;

namespace HumanResources.WebUI.DTOs.UnitDtos
{
    public class ResultUnitDto : BaseDto
    {
        public string Ad { get; set; }

        public int DepartmanId { get; set; }
        public string DepartmanAd { get; set; }

        public int PersonellerCount { get; set; }
    }
}