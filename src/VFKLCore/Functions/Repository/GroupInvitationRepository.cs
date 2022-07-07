using System;
using System.Threading.Tasks;
using AltinnApplicationsOwnerSystem.Functions.VFKL.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;
using VFKLCore.Functions.Config;
using VFKLCore.Functions.Models.VFKLInvitation;
using VFKLCore.Functions.Services.Interface;

namespace VFKLCore.Functions.Repository
{
    /// <summary>
    /// The repository to handle invitation data
    /// </summary>
    public class GroupInvitationRepository : IGroupInvitationRepository
    {
        private readonly VFKLCoreSettings _vfklSettings;
        private readonly KeyVaultSettings _vaultSettings;
        private readonly ILogger _logger;
        private readonly IKeyVaultService _keyVaultService;
        private readonly string insertGroupInvitationSql = "call invitations.insert_invitation(@userid, @gvid, @frist, @laeremiddel, @laereplan, @mottakereposter, @opprettetdato, @assessmenttypeid,@laereplankode,@bortvalgtsporsmaal, @laeremiddelleverandor)";

        /// <summary>
        /// Initializes a new instnace of the <see cref="GroupInvitationRepository"/> class.
        /// </summary>
        public GroupInvitationRepository(IOptions<VFKLCoreSettings> vfklSettings, IOptions<KeyVaultSettings> vaultSettings, ILogger<GroupInvitationRepository> logger, IKeyVaultService keyVaultService)
        {
            _vfklSettings = vfklSettings.Value;
            _vaultSettings = vaultSettings.Value;
            _logger = logger;
            _keyVaultService = keyVaultService;
        }

        /// <inheritdoc/>
        public async Task Create(GruppeInvitasjon invitation)
        {
            try
            {
                string connectionString = await _keyVaultService.GetDatabaseConnectionStringAsync();

                // using (NpgsqlConnection conn = new NpgsqlConnection(_vfklSettings.DatabaseConnectionString))
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    NpgsqlCommand command = new NpgsqlCommand(insertGroupInvitationSql, conn);
                    command.Parameters.AddWithValue("userid", int.Parse(invitation.BrukerID));
                    command.Parameters.AddWithValue("gvid", invitation.GruppeVurderingsID);
                    command.Parameters.AddWithValue("frist", invitation.VurderingsFrist);
                    command.Parameters.AddWithValue("laeremiddel", invitation.Læremiddel);
                    command.Parameters.AddWithValue("laeremiddelleverandor", string.IsNullOrEmpty(invitation.LæremiddelLeverandør) ? DBNull.Value : invitation.LæremiddelLeverandør);
                    command.Parameters.AddWithValue("mottakereposter", invitation.MottakerEposter);
                    command.Parameters.AddWithValue("opprettetdato", DateTime.Now.ToString());
                    command.Parameters.AddWithValue("assessmenttypeid", int.Parse(invitation.VurderingsType));
                    command.Parameters.AddWithValue("laereplan", invitation.Læreplan);
                    command.Parameters.AddWithValue("laereplankode", invitation.LæreplanKode);
                    command.Parameters.AddWithValue("bortvalgtsporsmaal", string.IsNullOrEmpty(invitation.BortvalgteSpørsmål) ? DBNull.Value : invitation.BortvalgteSpørsmål);
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GroupInvitation Repository - create");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<GruppeInvitasjon> GetInvitation(string groupAssessmentId)
        {
           try
           {
               GruppeInvitasjon invitation = null;
               string selectGroupInvitationSql = $"SELECT * FROM invitations.invitations WHERE gv_id='{groupAssessmentId}'";
               string connectionString = await _keyVaultService.GetDatabaseConnectionStringAsync();
               using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
               {
                   conn.Open();
                   NpgsqlCommand command = new NpgsqlCommand(selectGroupInvitationSql, conn);

                   using (NpgsqlDataReader reader = command.ExecuteReader())
                   {
                       while (reader.Read())
                       {
                           invitation = new GruppeInvitasjon();
                           invitation.GruppeVurderingsID = Convert.ToString(reader["gv_id"]);
                           invitation.Læremiddel = Convert.ToString(reader["laeremiddel"]);
                           invitation.LæremiddelLeverandør = Convert.ToString(reader["laeremiddel_leverandor"]);
                           invitation.Læreplan = Convert.ToString(reader["laereplan"]);
                           invitation.VurderingsFrist = Convert.ToString(reader["frist"]);
                           AssessmentType vurderingsType = (AssessmentType)Convert.ToInt32(reader["assessmenttype_id_fk"]);
                           invitation.VurderingsType = vurderingsType.ToString();
                           invitation.LæreplanKode = Convert.ToString(reader["laereplankode"]);
                           invitation.BortvalgteSpørsmål = Convert.ToString(reader["bortvalgtsporsmaal"]);

                        }
                   }

                   return invitation;
               }
           }
           catch (Exception e)
           {
               _logger.LogError(e, "GroupInvitation Repository - Get");
               throw;
           }
        }

        // <inheritdoc/>
        // public async Task<int> GetGroupInvitationId(string instanceId)
        // {
        //    try
        //    {
        //        int invitationId = 0;
        //        string selectGroupInvitationSql = $"SELECT invitation_id FROM invitation WHERE instance_id='{instanceId}'";

        // using (NpgsqlConnection conn = new NpgsqlConnection(_vfklSettings.DatabaseConnectionString))
        //        {
        //            conn.Open();
        //            NpgsqlCommand command = new NpgsqlCommand(selectGroupInvitationSql, conn);
        //            command.Parameters.AddWithValue("instanceid", instanceId);

        // using (NpgsqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    invitationId = Convert.ToInt32(reader["invitation_id"]);
        //                }
        //            }

        // return Convert.ToInt32(invitationId);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError(e, "GroupInvitation Repository - Get");
        //        throw;
        //    }
        // }
    }
}
