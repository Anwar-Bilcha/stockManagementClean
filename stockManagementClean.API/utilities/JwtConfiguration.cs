namespace stockManagementClean.API.utilities
{
    public static class JwtConfiguration
    {
        public static string JwtSigningKey { get; set; }
        public static string JwtAudience { get; set; }
        public static string JwtIssuer { get; set; }
        public static int TokenExpirationMinutes { get; set; } = 60;

        public static void Initialize (IConfiguration configuration)
        {
            JwtSigningKey = configuration["Jwt:Key"];
            JwtAudience = configuration["Jwt:Audience"];
            JwtIssuer = configuration["Jwt:Issuer"];
            TokenExpirationMinutes = TokenExpirationMinutes * 1;
        }

    }
}
