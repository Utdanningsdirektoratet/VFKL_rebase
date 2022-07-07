using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFKLCore.Models.VFKL;

namespace VFKLCore.Functions.Repository
{
    /// <summary>
    /// The repository to handle assessment data
    /// </summary>
    public interface IAssessmentRepository
    {
        /// <summary>
        /// insert new assessment
        /// </summary>
        /// <param name="assessment">the instance to base the new one on</param>
        /// <returns>The instance id</returns>
        Task Create(Assessment assessment);

        /// <summary>
        /// Get assessment id from postgres
        /// </summary>
        /// <param name="instanceId">instance id</param>
        Task<int> GetAssessmentId(string instanceId);
    }
}
