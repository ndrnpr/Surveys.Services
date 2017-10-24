using Surveys.Data.Repository.Base;
using Surveys.Models.Entities;
using Surveys.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surveys.Data.Repository.Interfaces
{
    public interface ISurveyRepository : IRepository<Survey>
    {
        Survey GetOneWithQuestions(int? id);

        IEnumerable<SurveyStatsInfo> GetAllWithStatsInfo();        
        SurveyInfo GetSurveyInfo(int? id);
    }
}
