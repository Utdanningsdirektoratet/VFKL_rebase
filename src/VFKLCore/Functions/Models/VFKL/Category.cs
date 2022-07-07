using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VFKLCore.Functions.Models.VFKL
{
    /// <summary>
    /// Enumeration for category
    /// </summary>
    public enum Category: int
    {
        /// <summary>
        /// design
        /// </summary>
        [EnumMember]
        Design = 1,

        /// <summary>
        /// pedagogical
        /// </summary>
        [EnumMember]
        Pedagogical = 2,

        /// <summary>
        /// curriculum
        /// </summary>
        [EnumMember]
        Curriculum = 3,
    }
}
