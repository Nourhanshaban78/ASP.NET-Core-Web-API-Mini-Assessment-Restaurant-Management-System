namespace UserIdentityProject.Helpers
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double Lifetime { get; set; }
        public string Key { get; set; }
    }
}
