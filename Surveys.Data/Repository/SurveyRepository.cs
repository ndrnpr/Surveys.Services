using Surveys.Data.Repository.Interfaces;
using Surveys.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Surveys.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Surveys.Models.ViewModels;

namespace Surveys.Data.Repository
{
    public class SurveyRepository : Repository<Survey>, ISurveyRepository
    {
        private readonly IQuestionRepository _questionRepository;

        public SurveyRepository(DbContextOptions<SurveysContext> options, IQuestionRepository questionRepository) : base(options)
        {
            _questionRepository = questionRepository;
        }

        public SurveyRepository(IQuestionRepository questionRepository) : base()
        {
            _questionRepository = questionRepository;
        }

        public override IEnumerable<Survey> GetAll()
            => Table.OrderByDescending(x => x.Published);

        public override IEnumerable<Survey> GetRange(int skip, int take)
            => GetRange(Table.OrderByDescending(x => x.Published), skip, take);

        public Survey GetOneWithQuestions(int? id)
            => Table.Include(x => x.Questions).ThenInclude(q => q.Choices).FirstOrDefault(x => x.ID == id);

        internal SurveyStatsInfo GetStatsInfoRecord(Survey survey)
        {
            return new SurveyStatsInfo
            {
                SurveyID = survey.ID,
                Caption = survey.Caption,
                Description = survey.Description,
                BulletinCount = survey.Bulletins.Count(),
                QuestionCount = survey.Questions.Count()
            };
        }

        public IEnumerable<SurveyStatsInfo> GetAllWithStatsInfo()
        {
            return Table
                .OrderByDescending(x => x.Published)
                .Include(x => x.Bulletins)
                .Include(x => x.Questions)
                .Select(x => GetStatsInfoRecord(x));
        }

        internal SurveyInfo GetInfoRecord(Survey survey, bool detailed)
        {
            return new SurveyInfo {
                SurveyID = survey.ID,
                Caption = survey.Caption,
                Description = survey.Description,
                BulletinCount = survey.Bulletins.Count(),
                Questions = (detailed) ? _questionRepository.GetSurveyQuestions(survey.ID) : null
            };
        }

        public SurveyInfo GetSurveyInfo(int? id)
            => Table            
            .Where(x => x.ID == id)
            .Include(x => x.Bulletins)
            .Select(x => GetInfoRecord(x, true))
            .FirstOrDefault();
    }
}
