using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFKLCore.Functions.Models.VFKL
{
    /// <summary>
    /// User profile
    /// </summary>
    public class UserProfile
    {
        /// <summary>
        /// Primary Id assigned for the user in VFKL
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Equivalent feide id for the user
        /// </summary>
        public string FeideId { get; set; }

        /// <summary>
        /// Equivalent SSN of the user
        /// </summary>
        public int SocialSecurityNumber { get; set; }

        /// <summary>
        /// Name of the user
        /// </summary>
        public string Name { get; set; }
    }
}
