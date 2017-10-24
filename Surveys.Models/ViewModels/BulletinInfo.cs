using System;
using System.Collections.Generic;
using System.Text;

namespace Surveys.Models.ViewModels
{
    public class BulletinInfo
    {
        public SurveyInfo Survey { get; set; }
        public IEnumerable<VoteInfo> Votes { get;set; }
    }
}
