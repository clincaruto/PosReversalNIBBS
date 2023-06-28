using Microsoft.IdentityModel.Tokens;
using PosReversalNIBBS_API.Models.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace PosReversalNIBBS_API.Utilities
{
    public static class JWTDecryption
    {
        public static bool JWTChecker(string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJmcmVzaCI6dHJ1ZSwiaWF0IjoxNjg3ODc2Mjc2LCJqdGkiOiI5OWJmY2E4Mi02NjRlLTQ2MDctYTdjMi05NWZjMTdhZDRiYzIiLCJ0eXBlIjoiYWNjZXNzIiwic3ViIjoia2VoaW5kZS5vbW9zZWhpbkB1YmFncm91cC5jb20iLCJuYmYiOjE2ODc4NzYyNzYsImV4cCI6MTY4Nzg3Njg3Nn0.ISu6rhVNR6_vJ5-RxIF8C8I46qXUrtBksoiMnmUJrLo")
        {

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(token);
            DateTime expiration = jwtToken.ValidTo;
            DateTime currentDateTime = DateTime.UtcNow;
            if (expiration > currentDateTime)
            {
                return true;
            }
            return false;
        }
        public static bool GetRoles(string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJmcmVzaCI6dHJ1ZSwiaWF0IjoxNjg3ODc2Mjc2LCJqdGkiOiI5OWJmY2E4Mi02NjRlLTQ2MDctYTdjMi05NWZjMTdhZDRiYzIiLCJ0eXBlIjoiYWNjZXNzIiwic3ViIjoia2VoaW5kZS5vbW9zZWhpbkB1YmFncm91cC5jb20iLCJuYmYiOjE2ODc4NzYyNzYsImV4cCI6MTY4Nzg3Njg3Nn0.ISu6rhVNR6_vJ5-RxIF8C8I46qXUrtBksoiMnmUJrLo", string secretKey = "UnitedBankOfAfricaPLC")
        {


            // Create a JWT token handler
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            // Set the validation parameters
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,  // Set to true if you want to validate the issuer
                ValidateAudience = false,  // Set to true if you want to validate the audience
                ValidateLifetime = true,  // Set to true if you want to validate the lifetime
                IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(secretKey))  // Set the secret key used for decryption
            };

            // Read and validate the token with signature validation
            ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

            // Get the claims from the validated token
            var claims = claimsPrincipal.Claims;

            // Check if the token contains a specific role
            string initiatorRole = "Initiator";
            string authorizerRole = "Authorizer";
            bool hasRole = claims.Any(c => c.Type == "role" && (c.Value == authorizerRole || c.Value == initiatorRole));
            return hasRole;
        }
       
    }
}
