using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Surveys.Models.Entities.Base;
using System.Runtime.Serialization;

namespace Surveys.Models.Entities
{
    [Table(nameof(Vote))]
    public class Vote : SurveyPrincipalEntity
    {
        [Required]
        public int BulletinID { get; set; }
        [Required]
        public int ChoiceID { get; set; }

        [ForeignKey(nameof(ChoiceID))]
        public Choice Choice { get; set; }

        [ForeignKey(nameof(BulletinID))]
        public Bulletin Bulletin { get; set; }
    }
}
