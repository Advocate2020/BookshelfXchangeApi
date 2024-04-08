using Google.Cloud.SecretManager.V1;

namespace BCXGoogle.SecretManagerNS
{
    public class GoogleSecretManagerService
    {
        private readonly SecretManagerServiceClient _client;

        public GoogleSecretManagerService()
        {
            _client = SecretManagerServiceClient.Create();
        }

        public string GetSecret(string projectId, string secretId, string versionId = "latest")
        {
            SecretVersionName secretVersionName = new(projectId, secretId, versionId);
            AccessSecretVersionResponse result = _client.AccessSecretVersion(secretVersionName);
            return result.Payload.Data.ToStringUtf8();
        }
    }
}



