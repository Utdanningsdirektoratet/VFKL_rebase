using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;
using VFKLCore.Functions.Config;
using VFKLCore.Functions.Services.Interface;
using VFKLCore.Models.VFKL;

namespace VFKLCore.Functions.Repository
{
    /// <summary>
    /// The repository to handle assessment data
    /// </summary>
    public class AssessmentRepository : IAssessmentRepository
    {
        private readonly VFKLCoreSettings _vfklSettings;
        private readonly KeyVaultSettings _vaultSettings;
        private readonly ILogger _logger;
        private readonly IKeyVaultService _keyVaultService;
        private readonly string insertAssessmentSql = "call assessment.insert_assessment(@userid, @teachingaid, @instanceid, @groupassessmentid, @createddatetime, @usercomments, @teachingaidsupplier)";

        /// <summary>
        /// Initializes a new instnace of the <see cref="AssessmentRepository"/> class.
        /// </summary>
        public AssessmentRepository(IOptions<VFKLCoreSettings> vfklSettings, IOptions<KeyVaultSettings> vaultSettings, ILogger<AssessmentRepository> logger, IKeyVaultService keyVaultService)
        {
            _vfklSettings = vfklSettings.Value;
            _vaultSettings = vaultSettings.Value;
            _logger = logger;
            _keyVaultService = keyVaultService;
        }

        /// <inheritdoc/>
        public async Task Create(Assessment assessment)
        {
            try
            {
                string connectionString = await _keyVaultService.GetDatabaseConnectionStringAsync();
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    NpgsqlCommand command = new NpgsqlCommand(insertAssessmentSql, conn);                    
                    command.Parameters.AddWithValue("userid", assessment.UserId);
                    command.Parameters.AddWithValue("teachingaid", assessment.TeachingAid);
                    command.Parameters.AddWithValue("teachingaidsupplier", string.IsNullOrEmpty(assessment.TeachingAidSupplier) ? DBNull.Value : assessment.TeachingAidSupplier);
                    command.Parameters.AddWithValue("instanceid", assessment.InstanceId.ToString());
                    command.Parameters.AddWithValue("createddatetime", NpgsqlTypes.NpgsqlDbType.Timestamp, DateTime.Now);

                    if (string.IsNullOrEmpty(assessment.UserComments))
                    {
                        command.Parameters.AddWithValue("usercomments", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("usercomments", assessment.UserComments);
                    }

                    if (string.IsNullOrEmpty(assessment.GroupAssesmentId))
                    {
                        command.Parameters.AddWithValue("groupassessmentid", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("groupassessmentid", assessment.GroupAssesmentId);
                    }

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Assessment Repository - create");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<int> GetAssessmentId(string instanceId)
        {
            try
            {
                int assessmentId = 0;
                string selectAssessmentSql = $"SELECT assessment_id FROM assessment.assessment WHERE instance_id='{instanceId}'";

                string connectionString = await _keyVaultService.GetDatabaseConnectionStringAsync();
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    NpgsqlCommand command = new NpgsqlCommand(selectAssessmentSql, conn);
                    command.Parameters.AddWithValue("instanceid", instanceId);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            assessmentId = Convert.ToInt32(reader["assessment_id"]);
                        }
                    }

                    return Convert.ToInt32(assessmentId);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Assessment Repository - Get");
                throw;
            }
        }
    }
}
