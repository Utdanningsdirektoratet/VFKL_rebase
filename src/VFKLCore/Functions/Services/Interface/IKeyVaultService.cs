using System.Threading.Tasks;

namespace VFKLCore.Functions.Services.Interface
{
    /// <summary>
    /// Interface for interacting with key vault
    /// </summary>
    public interface IKeyVaultService
    {
        /// <summary>
        /// Gets the value of a secret from the given key vault.
        /// </summary>
        /// <returns>The secret value.</returns>
        Task<string> GetCertificateAsync();

        /// <summary>
        /// Gets the value of jwk from secret in key vault
        /// </summary>
        /// <returns>JWK</returns>
        Task<string> GetJWKAsync();

        /// <summary>
        /// Gets the value of jwk from secret in key vault
        /// </summary>
        /// <returns>JWK</returns>
        Task<string> GetDatabaseConnectionStringAsync();
    }
}
