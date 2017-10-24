using System;
using System.Collections.Generic;
using System.Text;

namespace Surveys.Models.ViewModels
{
    public class QuestionInfo
    {
        public int QuestionID { get; set; }
        public int QuestionNumber { get; set; }
        public int SurveyID { get; set; }
        public string Sentence { get; set; }
        public string Comments { get; set; }

        public int VotesCount { get; set; }
        public int RequiredCount { get; set; }
        public int AllowedCount { get; set; }

        public IEnumerable<ChoiceInfo> Choices { get; set; }
    }
}
