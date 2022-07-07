using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFKLCore.Functions.Models.VFKL;

namespace VFKLCore.Functions.Repository
{
    /// <summary>
    /// The repository to handle answer data
    /// </summary>
    public interface IAnswerRepository
    {
        /// <summary>
        /// insert answer
        /// </summary>
        /// <param name="answer">the instance to base the new one on</param>
        /// <returns>The instance id</returns>
        Task Create(Answers answer);
    }
}
