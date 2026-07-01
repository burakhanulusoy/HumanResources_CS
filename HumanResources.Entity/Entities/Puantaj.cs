using HumanResources.Entity.Entities.Common;
using System;

namespace HumanResources.Entity.Entities
{
    public class Puantaj : BaseEntity
    {
        // 1. BU KAYIT KÝME AÝT?
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        // 2. HANGÝ GÜNÜN ÇALIÞMA KAYDI?
        public DateTime Tarih { get; set; } // Örn: 01.07.2026

        // 3. O GÜN HANGÝ VARDÝYADAYDI?
        public int VardiyaId { get; set; }
        public Vardiya Vardiya { get; set; }

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