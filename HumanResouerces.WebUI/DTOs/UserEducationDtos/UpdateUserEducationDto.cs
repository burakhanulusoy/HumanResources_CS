using HumanResources.Entity.Enums;

namespace HumanResources.Business.DTOs.UserEducationDtos
{
    public class UpdateUserEducationDto
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public int EgitimId { get; set; }
        public ApplicationStatus BasvuruDurumu { get; set; }
        public string? AdminAciklamasi { get; set; } // Reddedilirse neden reddedildi?
      }
}
