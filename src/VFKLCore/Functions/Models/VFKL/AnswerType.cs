using System.Runtime.Serialization;

namespace AltinnApplicationsOwnerSystem.Functions.VFKL.Models
{
    /// <summary>
    /// Enumeration for AnswerType
    /// </summary>
    public enum AnswerType : int
    {
        /// <summary>
        /// User fully agrees
        /// </summary>
        [EnumMember]
        TotallyAgree = 1,

        /// <summary>
        /// User somewhat agrees
        /// </summary>
        [EnumMember]
        PartlyAgree = 2,

        /// <summary>
        /// User somewhat disagrees
        /// </summary>
        [EnumMember]
        PartlyDisagree = 3,

        /// <summary>
        /// User fully disagrees
        /// </summary>
        [EnumMember]
        TotallyDisagree = 4,

        /// <summary>
        /// User chose not to answer the question
        /// </summary>
        [EnumMember]
        NotApplicable = 5
    }
}
