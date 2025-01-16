namespace stockManagementClean.API.Utilities
{
    public static class StockJwtConfiguration
    {
        //public static readonly IConfiguration _configuration;
        public static string JwtSigningKey { get; private set; }
        public  static string JwtAudience { get; private set; }
        public static string JwtIssuer { get; private set; }
        public static int tokenExpirationMinutes { get; private set; } = 60;
        public static void Initialize(IConfiguration configuration)
        { 
        //_configuration = configuration;
        JwtSigningKey = configuration["Jwt:Key"];
        JwtAudience = configuration["Jwt:Audience"];
        JwtIssuer = configuration["Jwt:Issuer"];
        }

    }
}
