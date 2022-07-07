using System;
using AltinnApplicationsOwnerSystem.Functions.VFKL.Models;

namespace VFKLCore.Functions.Models.VFKL
{
    /// <summary>
    /// Answer model
    /// </summary>
    public class Answers
    {
        /// <summary>
        /// Primarky Id of the answer
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of the assessment that the answer belongs to
        /// </summary>
        public int AssessmentId { get; set; }
        
        /// <summary>
        /// Id of the question that the user anwered for
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// User's answer
        /// </summary>
        public AnswerType AnswerTypeId { get; set; }

        /// <summary>
        /// Additional comments or reason for the assessment answer
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Timestamp on when the user answered
        /// </summary>
        public DateTime AnsweredDateTime { get; set; }

    }
}
