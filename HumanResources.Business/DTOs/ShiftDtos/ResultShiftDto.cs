using HumanResources.Business.Base;
using HumanResources.Business.DTOs.UserDtos;
using HumanResources.Entity.Entities;

namespace HumanResources.Business.DTOs.ShiftDtos
{
    public class ResultShiftDto:BaseDto
    {

        public string Aciklama { get; set; }

        public TimeSpan BaslangicSaati { get; set; }
        public TimeSpan BitisSaati { get; set; }

        public int AraDinlenmeSuresiDk { get; set; }

        public TimeSpan CalismaSuresi { get; set; }

        public int? YoneticiId { get; set; }
        public UserDto? Yonetici { get; set; }

        public IList<UserDto> Personeller { get; set; }



    }
}
