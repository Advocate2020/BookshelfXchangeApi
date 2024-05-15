using BCXGoogle.SecretManagerNS;
using BookXChangeApi.Constants;

namespace BookXChangeApi.Util.DeveloperAuthorizationNS
{
    public class DeveloperAuthorization
    {
        public static string GenerateHash(string apiKey)
        {
            return HashUtil.HashPassword(apiKey);
        }

        public static bool IsApiKeyValid(string passwordToVerify)
        {
            GoogleSecretManagerService _sm = new();
            string hashedPassword = _sm.GetSecret(BCXConstants.GoogleSecrete, BCXConstants.ApiSecrete);


            return HashUtil.VerifyPassword(hashedPassword, passwordToVerify);
        }

    }
}
