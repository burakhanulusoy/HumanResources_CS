namespace HumanResources.Entity.Enums
{
    public enum ApplicationStatus //basþvuru durumu
    {
        Bekliyor = 1,
        Onaylandi = 2,
        Reddedildi = 3,
        Tamamlandi = 4, 
        IptalEdildi = 5
    }
    public enum TrainingStatus //eðitim durumu
    {
        Planlandi = 1,
        DevamEdiyor = 2,
        Tamamlandi = 3,
        IptalEdildi = 4
    }
    public enum CertificateStatus //sertifika olayý
    {
        Gecerli = 1,      // Süresi devam ediyor
        SuresiDolu = 2,   // Geçerlilik tarihi geçmiþ, yenilenmesi lazým
        IptalEdildi = 3   // Herhangi bir disiplin suçu vb. sebeple iptal edilmiþ
    }


    public enum ZimmetDurumu
    {
        Aktif = 1,        // Personele teslim edildi, þu an kullanýyor
        IadeEdildi = 2,   // Þirkete saðlam bir þekilde geri teslim edildi
        Arizali = 3,      // Bozuldu veya hasar gördü
        Kayip = 4         // Kayboldu veya çalýndý
    }

}
