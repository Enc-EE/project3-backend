using Newtonsoft.Json;

namespace project3_backend.Models
{
    public class VerifiedJwt
    {
        [JsonProperty("azp")]
        public string AuthorizedParty { get; set; }

        [JsonProperty("aud")]
        public string Audience { get; set; }

        [JsonProperty("sub")]
        public string Subject { get; set; }

        [JsonProperty("email")]
        public string EMail { get; set; }

        [JsonProperty("email_verified")]
        public bool EMailVerified { get; set; }

        [JsonProperty("at_hash")]
        public string AccessTokenHash { get; set; }

        [JsonProperty("exp")]
        public string ExpirationTime { get; set; }

        [JsonProperty("iss")]
        public string Issuer { get; set; }

        [JsonProperty("jti")]
        public string JwtId { get; set; }

        [JsonProperty("iat")]
        public string IssuedAt { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }

        [JsonProperty("given_name")]
        public string GivenName { get; set; }

        [JsonProperty("family_name")]
        public string FamilyName { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("alg")]
        public string Algorithm { get; set; }

        [JsonProperty("kid")]
        public string KeyId { get; set; }
    }
}