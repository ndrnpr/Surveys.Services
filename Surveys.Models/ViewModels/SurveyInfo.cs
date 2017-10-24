using Surveys.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surveys.Models.ViewModels
{
    public class SurveyInfo
    {
        public int SurveyID { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }

        public int BulletinCount { get; set; }

        public IEnumerable<QuestionInfo> Questions { get; set; }
    }
}
