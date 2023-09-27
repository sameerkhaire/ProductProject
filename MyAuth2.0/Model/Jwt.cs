namespace MyAuth2._0.Model
{
    public class Jwt
    {
        public string Issuer { get; set; } = string.Empty;

        public string Audience { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
    }
}
