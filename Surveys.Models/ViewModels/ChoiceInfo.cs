using System;
using System.Collections.Generic;
using System.Text;

namespace Surveys.Models.ViewModels
{
    public class ChoiceInfo
    {
        public int QuestionID { get; set; }
        
        public int ChoiceID { get; set; }

        public string Statement { get; set; }

        public int VotesCount { get; set; }
    }
}
