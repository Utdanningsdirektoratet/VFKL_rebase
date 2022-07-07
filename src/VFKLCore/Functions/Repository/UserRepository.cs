using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;
using VFKLCore.Functions.Config;
using VFKLCore.Functions.Models.VFKL;
using VFKLCore.Functions.Services.Interface;

namespace VFKLCore.Functions.Repository
{
    /// <summary>
    /// Repository to handle user data
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly VFKLCoreSettings _vfklSettings;
        private readonly KeyVaultSettings _vaultSettings;
        private readonly ILogger _logger;
        private readonly IKeyVaultService _keyVaultService;
        private readonly string insertUserSql = "call userprofile.insert_user(@feideid, @username)";

        /// <summary>
        /// Initializes a new instnace of the <see cref="UserRepository"/> class.
        /// </summary>
        public UserRepository(IOptions<VFKLCoreSettings> vfklSettings, IOptions<KeyVaultSettings> vaultSettings, ILogger<AnswerRepository> logger, IKeyVaultService keyVaultService)
        {
            _vfklSettings = vfklSettings.Value;
            _vaultSettings = vaultSettings.Value;
            _logger = logger;
            _keyVaultService = keyVaultService;
        }

        /// <inheritdoc/>
        public async Task Create(UserProfile user)
        {
            try
            {
                string connectionString = await _keyVaultService.GetDatabaseConnectionStringAsync();
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    NpgsqlCommand command = new NpgsqlCommand(insertUserSql, conn);

                    command.Parameters.AddWithValue("feideid", user.FeideId);
                    command.Parameters.AddWithValue("username", user.Name);

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "User Repository - create");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<UserProfile> Get(string feideId)
        {
            try
            {
                UserProfile user = null;
                string selectAssessmentSql = $"SELECT * FROM userprofile.userprofile WHERE feide_id='{feideId}'";

                string connectionString = await _keyVaultService.GetDatabaseConnectionStringAsync();
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    NpgsqlCommand command = new NpgsqlCommand(selectAssessmentSql, conn);
                    command.Parameters.AddWithValue("feideid", feideId);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = new UserProfile();
                            user.UserID = Convert.ToInt32(reader["user_id"]);
                            user.FeideId = reader["feide_id"].ToString();
                            user.Name = reader["name"].ToString();
                        }
                    }

                    return user;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "User Repository - Get");
                throw;
            }
        }
    }
}
