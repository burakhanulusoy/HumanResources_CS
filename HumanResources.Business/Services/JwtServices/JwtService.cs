using HumanResources.Business.DTOs.JwtTokenDto;
using HumanResources.Business.Options;
using HumanResources.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HumanResources.Business.Services.JwtServices
{
    public class JwtService(IOptions<JwtTokenOptions> _tokenOptions
                             , UserManager<AppUser> _userManager,
                             IConfiguration configuration) : IJwtService
    {

        //Secret.Josndakileri alýyoruz 
        private readonly JwtTokenOptions jwtTokenOptions = _tokenOptions.Value;

        //        private readonly JwtTokenOptions jwtTokenOptions = configuration.GetSection(nameof(JwtTokenOptions)).Get<JwtTokenOptions>();


        public async Task<TokenResponseDto> GenerateTokenAsync(AppUser appUser)
        {
            //imza signature olsutur
            SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(jwtTokenOptions.Key));

            var userRoles = await _userManager.GetRolesAsync(appUser);

            List<Claim> claims = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.Sub, appUser.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Name, appUser.UserName ?? string.Empty),
                    new Claim("FullName", string.Join(" ", appUser.Ad, appUser.Soyad).Trim()),
                    new Claim("ImageUrl", appUser.FotografUrl ?? string.Empty), 
                    new Claim(JwtRegisteredClaimNames.Email, appUser.Email ?? string.Empty),
                    new Claim(JwtRegisteredClaimNames.PhoneNumber, appUser.PhoneNumber ?? string.Empty)
                };
                
            foreach (var role in userRoles)
            {

                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            JwtSecurityToken jwtSecurityToken = new
                (
                   issuer: jwtTokenOptions.Issuer,
                   audience: jwtTokenOptions.Audience,
                   claims: claims,
                   notBefore: DateTime.UtcNow,
                   expires: DateTime.UtcNow.AddMinutes(jwtTokenOptions.ExpireInMinutes),
                   signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
                );

            var responseDto = new TokenResponseDto()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                ExpireTime = DateTime.UtcNow.AddMinutes(jwtTokenOptions.ExpireInMinutes)


            };

            return responseDto;




        }


    }
}
