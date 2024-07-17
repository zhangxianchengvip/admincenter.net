using Auto.Options;

namespace AdminCenter.Web;

[AutoOptions]
public record JwtOptions(string Issuer, string Audience, string SecretKey);
