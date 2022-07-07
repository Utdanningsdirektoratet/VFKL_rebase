using System;
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
    /// The repository to handle answer data
    /// </summary>
    public class AnswerRepository : IAnswerRepository
    {
        private readonly VFKLCoreSettings _vfklSettings;
        private readonly KeyVaultSettings _vaultSettings;
        private readonly ILogger _logger;
        private readonly IKeyVaultService _keyVaultService;        
        private readonly string insertAnswerSql = "call assessment.insert_answers(@assessmentid, @questionid, @answertypeid, @reason)";

        /// <summary>
        /// Initializes a new instnace of the <see cref="AnswerRepository"/> class.
        /// </summary>
        public AnswerRepository(IOptions<VFKLCoreSettings> vfklSettings, IOptions<KeyVaultSettings> vaultSettings, ILogger<AnswerRepository> logger, IKeyVaultService keyVaultService)
        {
            _vfklSettings = vfklSettings.Value;
            _vaultSettings = vaultSettings.Value;
            _logger = logger;
            _keyVaultService = keyVaultService;
        }

        /// <inheritdoc/>
        public async Task Create(Answers answer)
        {
            try
            {
                string connectionString = await _keyVaultService.GetDatabaseConnectionStringAsync();
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    NpgsqlCommand command = new NpgsqlCommand(insertAnswerSql, conn);

                    command.Parameters.AddWithValue("assessmentid", answer.AssessmentId);
                    command.Parameters.AddWithValue("questionid", answer.QuestionId);
                    command.Parameters.AddWithValue("answertypeid", Convert.ToInt32(answer.AnswerTypeId));
                    if (string.IsNullOrEmpty(answer.Reason))
                    {
                        command.Parameters.AddWithValue("reason", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("reason", answer.Reason);
                    }

                    await command.ExecuteScalarAsync();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Answer Repository - create");
                throw;
            }
        }
    }
}
