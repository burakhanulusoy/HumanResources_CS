using HumanResouerces.WebUI.Base;

namespace HumanResources.Business.DTOs.ShiftDtos
{
    public class ShiftDto:BaseDto
    {

        public string Aciklama { get; set; }

        public TimeSpan BaslangicSaati { get; set; }
        public TimeSpan BitisSaati { get; set; }

        public int AraDinlenmeSuresiDk { get; set; }

        public TimeSpan CalismaSuresi { get; set; }
    }
}
