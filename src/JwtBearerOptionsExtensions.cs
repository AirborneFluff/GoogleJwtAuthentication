using API.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace API.Authentication
{
    public static class JwtBearerOptionsExtensions
    {

        public static JwtBearerOptions UseGoogle(this JwtBearerOptions options, string clientId)
        {
            if (clientId == null)
            {
                throw new ArgumentNullException(nameof(clientId));
            }

            if (clientId.Length == 0)
            {
                throw new ArgumentException("ClientId cannot be empty.", nameof(clientId));
            }

            options.Audience = clientId;
            options.Authority = GoogleJwtBearerDefaults.Authority;

            options.SecurityTokenValidators.Clear();
            options.SecurityTokenValidators.Add(new JwtSecurityTokenHandler());

            options.TokenValidationParameters = new TokenValidationParameters
            {
                // - The ID token is properly signed by Google. Use Google's public keys to verify the token's signature.
                ValidateIssuerSigningKey = true,

                // - The value of aud in the ID token is equal to one of your app's client IDs.
                ValidateAudience = true,
                ValidAudience = clientId,

                // - The value of iss in the ID token is equal to accounts.google.com or https://accounts.google.com.
                ValidateIssuer = true,
                ValidIssuers = new[] { GoogleJwtBearerDefaults.Authority, "accounts.google.com" },

                // - The expiry time (exp) of the ID token has not passed.
                ValidateLifetime = true,

                NameClaimType = GoogleClaimTypes.Name,
                AuthenticationType = GoogleJwtBearerDefaults.AuthenticationScheme,
            };

            return options;
        }
    }
}
