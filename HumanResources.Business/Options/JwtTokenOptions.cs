namespace HumanResources.Business.Options
{
    public class JwtTokenOptions
    {
        public string Issuer { get; set; } // Tokeni daðýtan sunucu   // ani api tarafý  https://api.arkitektur.com
        public string Audience { get; set; }  //Tokeni dinleyen taraf /// https://www.arkitektur.com
        public string Key { get; set; }
        public int ExpireInMinutes { get; set; } //Token hayatta kalma suresi


    }
}
