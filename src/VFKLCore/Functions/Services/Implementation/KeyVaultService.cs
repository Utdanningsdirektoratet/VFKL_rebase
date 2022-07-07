using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Azure;
using Azure.Identity;
using Azure.Security.KeyVault.Certificates;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VFKLCore.Functions.Config;
using VFKLCore.Functions.Services.Interface;

namespace VFKLCore.Functions.Services.Implementation
{
    /// <summary>
    /// Wrapper implementation for a KeyVaultClient. The wrapped client is created with a principal obtained through configuration.
    /// </summary>
    /// <remarks>This class is excluded from code coverage because it has no logic to be tested.</remarks>
    [ExcludeFromCodeCoverage]
    public class KeyVaultService : IKeyVaultService
    {
        private readonly ILogger _logger;
        private readonly VFKLCoreSettings _vfklSettings;
        private readonly KeyVaultSettings _vaultSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyVaultService"/> class.
        /// </summary>
        public KeyVaultService(ILogger<KeyVaultService> logger, IOptions<VFKLCoreSettings> vfklSettings, IOptions<KeyVaultSettings> vaultSettings)
        {
            _logger = logger;
            _vfklSettings = vfklSettings.Value;
            _vaultSettings = vaultSettings.Value;
        }

        /// <inheritdoc/>
        public async Task<string> GetCertificateAsync()
        {
            CertificateClient certificateClient = new CertificateClient(new Uri(_vaultSettings.KeyVaultURI), new DefaultAzureCredential());
            AsyncPageable<CertificateProperties> certificatePropertiesPage = certificateClient.GetPropertiesOfCertificateVersionsAsync(_vaultSettings.MaskinPortenCertSecretId);
            await foreach (CertificateProperties certificateProperties in certificatePropertiesPage)
            {
                if (certificateProperties.Enabled == true &&
                    (certificateProperties.ExpiresOn == null || certificateProperties.ExpiresOn >= DateTime.UtcNow))
                {                    
                    SecretClient secretClient = new SecretClient(new Uri(_vaultSettings.KeyVaultURI), new DefaultAzureCredential());

                    KeyVaultSecret secret = await secretClient.GetSecretAsync(certificateProperties.Name, certificateProperties.Version);
                    return secret.Value;
                }
            }

            return null;
        }

        /// <inheritdoc/>
        public async Task<string> GetJWKAsync()
        {
            SecretClient secretClient = new SecretClient(new Uri(_vaultSettings.KeyVaultURI), new DefaultAzureCredential());
            KeyVaultSecret secret = await secretClient.GetSecretAsync(_vaultSettings.MaskinportenClientSecretId);
            return secret.Value;
        }

        /// <inheritdoc/>
        public async Task<string> GetDatabaseConnectionStringAsync()
        {
            string vaultUri = _vaultSettings.KeyVaultURI;
            string secretId = _vfklSettings.DatabaseSecretId;
            SecretClient secretClient = new SecretClient(new Uri(vaultUri), new DefaultAzureCredential());

            KeyVaultSecret secret = await secretClient.GetSecretAsync(secretId);
            return secret.Value;
        }
    }
}
