using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFKLCore.Functions.Models.VFKL
{
    /// <summary>
    /// Question
    /// </summary>
    public class Question
    {
        /// <summary>
        /// Primary id of the question
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// Question text
        /// </summary>
        public string QuestionText { get; set; }

        /// <summary>
        /// Category that the question belongsto
        /// </summary>
        public Category CategoryId { get; set; }

        /// <summary>
        /// Questions id in the form
        /// </summary>
        public int QuestionIdInForm { get; set; }
    }
}
