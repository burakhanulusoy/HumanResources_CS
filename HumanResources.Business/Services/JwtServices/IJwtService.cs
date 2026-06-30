using HumanResources.Business.DTOs.JwtTokenDto;
using HumanResources.Entity.Entities;

namespace HumanResources.Business.Services.JwtServices
{
    public interface IJwtService
    {
        Task<TokenResponseDto> GenerateTokenAsync(AppUser appUser);

    }
}
