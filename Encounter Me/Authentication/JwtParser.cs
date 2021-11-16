using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;

namespace Encounter_Me.Authentication
{
    public class JwtParser // TODO: testout
    {
        //jwt token - a standart format. Gives information about user. This info is needed for authentication.

        public static IEnumerable<Claim> ParseClaimsFromJWT (string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1]; /// headerinfo.payloadInfo.verification(validation)Info  - we need payloadInformation.

            var jsonBytes = ParseBase64WithoutPadding(payload);

            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            ExtractRolesFromJWT(claims, keyValuePairs); // Rolesalready in claims. 

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()))); // This ir for everything else. 

            return claims; // Claims - info about the logged in user. (To know roles, and other info - no need to add lots, we can do a separate call for that)
        }


        private static void ExtractRolesFromJWT(List<Claim> claims, Dictionary<string, object> keyValuePairs)
        {
            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles); // out object - if it finds roles in keyValue pairs, it created new object - roles

            if (roles is not null)
            {
                var parsedRoles = roles.ToString().Trim().TrimStart('[').TrimEnd(']').Split(',');

                if (parsedRoles.Length > 1) // if we find more than one role
                {
                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole.Trim('"')));  // add roleroletype to our claims, take parsed role without '"'.
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, parsedRoles[0]));
                }


                keyValuePairs.Remove(ClaimTypes.Role); //ensures, that role is note double processed.
            }
        }


        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4) // what the eftover is.
            {
                case 2:
                    base64 += "==";
                    break;
                case 3:
                    base64 += "=";
                    break;

            }
            return Convert.FromBase64String(base64); //convert to byte array.

        }
    }
}
