namespace VFKLCore.Functions.Config
{
    /// <summary>
    /// Configuration object used to hold settings for the KeyVault.
    /// </summary>
    public class KeyVaultSettings
    {
        /// <summary>
        /// Uri to keyvault
        /// </summary>
        public string KeyVaultURI { get; set; }

        /// <summary>
        /// Name of the certificate secret
        /// </summary>
        public string MaskinPortenCertSecretId { get; set; }

        /// <summary>
        /// Name of the secret in keyvault
        /// </summary>
        public string MaskinportenClientSecretId { get; set; }

        /// <summary>
        /// Jwk
        /// </summary>
        public string MaskinportenKey { get; set; }
    }
}
