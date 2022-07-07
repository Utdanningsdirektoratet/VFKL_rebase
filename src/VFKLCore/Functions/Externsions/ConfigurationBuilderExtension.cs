using System;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;

namespace VFKLCore.Functions.Externsions
{
    /// <summary>
    /// Extension class for configuration builder
    /// </summary>
    public static class ConfigurationBuilderExtension
    {
        /// <summary>
        /// Configures keyvault
        /// </summary>
        /// <param name="config">Gets configuration from keyvault</param>
        public static void ConfigureKeyVault(this IConfigurationBuilder config)
        {
            string? keyVaultEndpoint = Environment.GetEnvironmentVariable("KEYVAULT_ENDPOINT");
            if (keyVaultEndpoint is null)
            {
                throw new InvalidOperationException("Store the Key Vault endpoint in a KEYVAULT_ENDPOINT environment variable.");
            }

            var secretClient = new SecretClient(new (keyVaultEndpoint), new DefaultAzureCredential());
            config.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
        }
    }
}
