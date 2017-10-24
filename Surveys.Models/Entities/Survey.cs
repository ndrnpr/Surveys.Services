using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Surveys.Models.Entities.Base;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Surveys.Models.Entities
{
    [Table(nameof(Survey))]
    public class Survey : SurveyPrincipalEntity
    {
        //[Required, MaxLength(10)]
        //public string Culture { get; set; }
        [Required, MaxLength(100)]
        public string Caption { get; set; }
        [Required, MaxLength(100)]
        public string Description { get; set; }
        [Required]
        public DateTime Published { get; set; }
        [Required]
        public DateTime Expires { get; set; }

        [InverseProperty(nameof(Question.Survey))]
        public IEnumerable<Question> Questions { get; set; }
        [InverseProperty(nameof(Bulletin.Survey))]
        public IEnumerable<Bulletin> Bulletins { get; set; }
    }
}
