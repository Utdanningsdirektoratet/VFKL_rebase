using System;

namespace VFKLCore.Models.VFKL
{
    /// <summary>
    /// Assessment meta data
    /// </summary>
    public class Assessment
    {
        /// <summary>
        /// Assessment Id
        /// </summary>
        public int AssessmentId { get; set; }

        /// <summary>
        /// The teaching aid used for teaching
        /// </summary>
        public string TeachingAid { get; set; }

        /// <summary>
        /// The teaching aid supplier used for teaching
        /// </summary>
        public string TeachingAidSupplier { get; set; }

        /// <summary>
        /// Id of the user that took the assessment
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Id of the instance data from Altinn 3
        /// </summary>
        public Guid InstanceId { get; set; }

        /// <summary>
        /// Group id of an assesment that was set during the invitation
        /// </summary>
        public string GroupAssesmentId { get; set; }

        /// <summary>
        /// Optional comments given by user
        /// </summary>
        public string UserComments { get; set; }
    }
}