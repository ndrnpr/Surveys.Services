using Surveys.Data.Repository.Base;
using Surveys.Models.Entities;
using Surveys.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace Surveys.Data.Repository.Interfaces
{
    public interface IQuestionRepository : IRepository<Question>
    {
        IEnumerable<Question> GetAllWithChoices();
        Question GetOneWithChoices(int? id);
        IEnumerable<QuestionInfo> GetSurveyQuestions(int surveyID);
        int GetCount(int surveyID);
    }
}
