namespace HumanResources.Business.DTOs.PuantajDtos
{
    public class UpdatePuantajDto
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }



        public DateTime Tarih { get; set; } // Örn: 01.07.2026


        public int VardiyaId { get; set; }


        // 4. GÝRÝÞ-ÇIKIÞ ZAMANLARI (Gece vardiyasý çakýþmalarýný önlemek için DateTime kullanýldý)
        public DateTime? GirisZamani { get; set; }
        public DateTime? CikisZamani { get; set; }

        // 5. SÜRE HESAPLAMALARI (Dakika cinsinden tutmak raporlamayý kolaylaþtýrýr)
        public int ToplamCalismaSuresiDk { get; set; }
        public int FazlaMesaiDk { get; set; }
        public int GecKalmaDk { get; set; }
        public int ErkenCikisDk { get; set; }

        // 6. ÖZEL DURUMLAR
        public bool Devamsiz { get; set; }//o gün hiç gelmemme durumu
        public bool ResmiTatil { get; set; }
        public bool HaftaSonuCalismasi { get; set; }
    }
}
