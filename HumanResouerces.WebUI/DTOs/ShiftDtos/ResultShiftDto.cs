using HumanResouerces.WebUI.Base;
using HumanResources.Business.DTOs.UserDtos;

namespace HumanResources.Business.DTOs.ShiftDtos
{
    public class ResultShiftDto:BaseDto
    {

        public string Aciklama { get; set; }

        public TimeSpan BaslangicSaati { get; set; }
        public TimeSpan BitisSaati { get; set; }

        public int AraDinlenmeSuresiDk { get; set; }

        public TimeSpan CalismaSuresi { get; set; }



        public IList<UserDto> Personeller { get; set; }



    }
}
