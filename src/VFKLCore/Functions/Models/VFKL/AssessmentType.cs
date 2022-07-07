using System.Runtime.Serialization;

namespace AltinnApplicationsOwnerSystem.Functions.VFKL.Models
{
    /// <summary>
    /// Enumeration for AssessmentType
    /// </summary>
    public enum AssessmentType : int
    {
        /// <summary>
        /// User fully agrees
        /// </summary>
        [EnumMember]
        AlleFag = 1,

        /// <summary>
        /// User somewhat agrees
        /// </summary>
        [EnumMember]
        Norsk = 2,

        /// <summary>
        /// User somewhat disagrees
        /// </summary>
        [EnumMember]
        Engelsk = 3,

        /// <summary>
        /// User fully disagrees
        /// </summary>
        [EnumMember]
        Matte = 4
    }
}
