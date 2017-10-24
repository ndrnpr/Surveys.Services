using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Surveys.Models.Entities.Base;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Surveys.Models.Entities
{
    [Table(nameof(Choice))]
    public class Choice : SurveyPrincipalEntity
    {
        public int QuestionID { get; set; }
        [DataType(DataType.Text), MinLength(1), MaxLength(100)]
        public string Statement { get; set; }

        [ForeignKey(nameof(QuestionID))]
        public Question Question { get; set; }

        [InverseProperty(nameof(Vote.Choice))]
        public IEnumerable<Vote> Votes { get; set; }
    }
}
