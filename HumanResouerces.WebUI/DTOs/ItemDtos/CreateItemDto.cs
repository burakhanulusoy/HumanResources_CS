namespace HumanResources.WebUI.DTOs.ItemDtos
{
    public class CreateItemDto 
    {
        public int AppUserId { get; set; }

        public int ZimmetTuruId { get; set; }

        public string SeriNumarasi { get; set; }

        public DateTime TeslimTarihi { get; set; }

        public DateTime IadeTarihi { get; set; }


        public string Aciklama { get; set; }
    }
}
