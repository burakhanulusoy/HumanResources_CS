namespace HumanResources.Business.DTOs.JwtTokenDto
{
    public class TokenResponseDto
    {
        public string Token { get; set; }
        public DateTime ExpireTime { get; set; }
    }
}
