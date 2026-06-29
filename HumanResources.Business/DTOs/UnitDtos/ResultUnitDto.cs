using HumanResources.Business.Base;
using HumanResources.Business.DTOs.DepartmentDtos;

namespace HumanResources.Business.DTOs.UnitDtos
{
    public class ResultUnitDto : BaseDto
    {
        public string Ad { get; set; }

        public int DepartmanId { get; set; }
        public ResultDepartmentDto  Departman { get; set; }
        public string DepartmanAd { get; set; }

        public int PersonellerCount { get; set; }
    }
}