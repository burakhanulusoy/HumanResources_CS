namespace HumanResouerces.WebUI.Enums
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
    public enum CertificateStatus
    {
        Gecerli = 1,
        SuresiDolu = 2,
        IptalEdildi = 3,
        Sinirsiz = 4
    }


    public enum ZimmetDurumu
    {
        Aktif = 1,        // Personele teslim edildi, þu an kullanýyor
        IadeEdildi = 2,   // Þirkete saðlam bir þekilde geri teslim edildi
        Arizali = 3,      // Bozuldu veya hasar gördü
        Kayip = 4         // Kayboldu veya çalýndý
    }
    public enum DemirbasDurumu
    {
        Musait = 0,
        Zimmetli = 1,
        Arizali = 2,
        HizmetDisi = 3
    }
}
