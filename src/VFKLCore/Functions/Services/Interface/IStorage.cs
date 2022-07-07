using System.IO;
using System.Threading.Tasks;
using VFKLCore.Functions.Models.VFKL;
using VFKLCore.Functions.Models.VFKLInvitation;

namespace VFKLCore.Functions.Services.Interface
{
    /// <summary>
    /// Interface for Storage where Application Owner system store received data
    /// </summary>
    public interface IStorage
    {
        /// <summary>
        /// Saves a specific blob
        /// </summary>
        Task SaveBlob(string name, string data);

        /// <summary>
        /// Saves a stream to blob
        /// </summary>
        Task<long> UploadFromStreamAsync(string name, Stream stream);

        /// <summary>
        /// Save registered subscription information
        /// </summary>
        Task SaveRegisteredSubscription(string name, Subscription subscription);

        /// <summary>
        /// Delete blob from container
        /// </summary>
        Task DeleteBlobFromContainer(string name, string container);

        /// <summary>
        /// Save assessment answer to postgres
        /// </summary>
        /// <param name="vurdering">assessment metadata</param>
        Task SaveAssessment(Vurdering vurdering);

        /// <summary>
        /// Save  answer to postgres
        /// </summary>
        /// <param name="paastand">assessment answers</param>
        /// <param name="assessmentId">assessment id</param>
        /// <param name="questionId">question id</param>
        Task SaveAnswer(Paastand paastand, int assessmentId,int questionId);

        /// <summary>
        /// Get assessment id from postgres
        /// </summary>
        /// <param name="instanceId">instance id</param>
        Task<int> GetAssessmentId(string instanceId);

        /// <summary>
        /// Save invitation to postgres
        /// </summary>
        /// <param name="invitation">invitation metadata</param>
        Task SaveGroupInvitation(GruppeInvitasjon invitation);

        /// <summary>
        /// Save  user to postgres
        /// </summary>
        /// <param name="user">user profile</param>
        Task SaveUser(UserProfile user);

        /// <summary>
        /// Get user from postgres
        /// </summary>
        /// <param name="feideId">feide id</param>
        Task<UserProfile> GetUser(string feideId);

        /// <summary>
        /// Get invitation information
        /// </summary>
        /// <param name="groupAssessmentId">feide id</param>
        Task<GruppeInvitasjon> GetInvitation(string groupAssessmentId);
    }
}
