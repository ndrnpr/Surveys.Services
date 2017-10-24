using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Surveys.Models.Entities.Base;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Surveys.Models.Entities
{
    [Table(nameof(Question))]
    public class Question : SurveyPrincipalEntity
    {
        public int SurveyID { get; set; }

        public int SerialNumber { get; set; }

        [DataType(DataType.Text), MinLength(1), MaxLength(100)]
        public string Sentence { get; set; }
        [DataType(DataType.Text), MaxLength(250)]        
        public string Comments { get; set; }
        [Required]
        public int RequiredChoices { get; set; }
        [Required]
        public int AllowedChoices { get; set; }

        [ForeignKey(nameof(SurveyID))]
        [DataMember(EmitDefaultValue = false)]
        public Survey Survey { get; set; }

        [InverseProperty(nameof(Choice.Question))]
        [DataMember(EmitDefaultValue = false)]
        public IEnumerable<Choice> Choices { get; set; }
    }
}
