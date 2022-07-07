using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFKLCore.Functions.Models.VFKL;

namespace VFKLCore.Functions.Repository
{
    /// <summary>
    /// Repository to handle user data
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// insert user if not exists
        /// </summary>
        /// <param name="user">information about user</param>
        /// <returns>user id</returns>
        Task Create(UserProfile user);

        /// <summary>
        /// insert user if not exists
        /// </summary>
        /// <param name="feideId">Feide user id</param>
        /// <returns>user id</returns>
        Task<UserProfile> Get(string feideId);
    }
}
