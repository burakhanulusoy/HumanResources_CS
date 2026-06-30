using HumanResources.Entity.Entities.Common;

namespace HumanResources.Entity.Entities
{
    public class Izin : BaseEntity
    {
        public int PersonelId { get; set; }
        public AppUser Personel { get; set; }

        public int IzinTuruId { get; set; }
        public IzinTuru IzinTuru { get; set; }

        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public string Aciklama { get; set; } // Ýzin nedeni?


        //nulable olma nedeni en baþta null olacak false deðil onun için direk onaylanmmaýsgibi olmasýný istemiyorum 
        public bool? AmirOnayi { get; set; }
        public bool? IkOnayi { get; set; }

        
    }
}