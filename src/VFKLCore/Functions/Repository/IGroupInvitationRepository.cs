using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFKLCore.Functions.Models.VFKLInvitation;

namespace VFKLCore.Functions.Repository
{
    /// <summary>
    /// The repository to handle GroupInvitation data
    /// </summary>
    public interface IGroupInvitationRepository
    {
        /// <summary>
        /// insert new GroupInvitation
        /// </summary>
        /// <param name="invitation">the invitation</param>
        /// <returns>The instance id</returns>
        Task Create(GruppeInvitasjon invitation);

        /// <summary>
        /// Get invitation information for a given group assessment id
        /// </summary>
        /// <param name="groupAssessmentId">group assessment id</param>
        /// <returns></returns>
        Task<GruppeInvitasjon> GetInvitation(string groupAssessmentId);
    }
}
