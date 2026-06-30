using HumanResources.Business.Base;
using HumanResources.Business.DTOs.UserDtos;

namespace HumanResources.Business.DTOs.PermissionDtos
{
    public class PermissionDto:BaseDto
    {
        public int PersonelId { get; set; }
        public int IzinTuruId { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public string Aciklama { get; set; } // Ýzin nedeni?
        //nulable olma nedeni en baþta null olacak false deðil onun için direk onaylanmmaýsgibi olmasýný istemiyorum 
        public bool? AmirOnayi { get; set; }
        public bool? IkOnayi { get; set; }
    }
}
