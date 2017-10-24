using Surveys.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Surveys.Models.Entities
{
    [Table(nameof(Bulletin))]
    public class Bulletin : SurveyPrincipalEntity
    {
        [Required]
        public int SurveyID { get; set; }
        public DateTime? Completed { get; set; }

        [ForeignKey(nameof(SurveyID))]
        public Survey Survey { get; set; }

        [InverseProperty(nameof(Vote.Bulletin))]
        public IEnumerable<Vote> Votes { get; set; }
    }
}
