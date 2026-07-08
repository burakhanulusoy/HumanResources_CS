using HumanResources.WebUI.DTOs.ItemDtos;
using HumanResources.WebUI.DTOs.UserDtos;

namespace HumanResouerces.WebUI.Areas.Admin.Models
{
    public class UserItemsViewModel
    {
        public ResultUserDto Kullanici { get; set; }
        public List<ItemDto> Zimmetler { get; set; } = new();
    }
}