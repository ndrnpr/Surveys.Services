using Microsoft.EntityFrameworkCore;
using Surveys.Data.Repository.Base;
using Surveys.Data.Repository.Interfaces;
using Surveys.Models.Entities;
using Surveys.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Surveys.Data.Repository
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        private readonly IChoiceRepository _choiceRepository;

        public QuestionRepository(DbContextOptions<SurveysContext> options, IChoiceRepository choiceRepository) : base(options)
        {
            _choiceRepository = choiceRepository;
        }

        public QuestionRepository(IChoiceRepository choiceRepository) : base()
        {
            _choiceRepository = choiceRepository;
        }

        public override IEnumerable<Question> GetAll()
            => Table.OrderBy(x => x.SerialNumber);

        public override IEnumerable<Question> GetRange(int skip, int take)
            => GetRange(Table.OrderBy(x => x.SerialNumber), skip, take);

        public Question GetOneWithChoices(int? id)
            => Table.Include(x => x.Choices).FirstOrDefault(x => x.ID == id);

        public IEnumerable<Question> GetAllWithChoices()
            => Table.Include(x => x.Choices).OrderBy(x => x.SerialNumber);

        public int GetCount(int surveyID)
        {
            return Table.Count(x => x.SurveyID == surveyID);
        }

        public IEnumerable<QuestionInfo> GetSurveyQuestions(int surveyID)
        {
            return Table
                .Where(x => x.SurveyID == surveyID)
                //.Include(x => x.Choices)
                //.ThenInclude(x => x.Votes)
                .OrderBy(x => x.SerialNumber)
                .Select(x =>
                    new QuestionInfo
                    {
                        QuestionID = x.ID,
                        SurveyID = x.SurveyID,
                        Sentence = x.Sentence,
                        Comments = x.Comments,
                        RequiredCount = x.RequiredChoices,
                        AllowedCount = x.AllowedChoices,
                        QuestionNumber = x.SerialNumber,
                        Choices = _choiceRepository.GetQuestionChoices(x.ID),
                        VotesCount = _choiceRepository.GetQuestionVotesCount(x.ID)
                    });
                //.OrderBy(x => x.QuestionNumber);
                   
        }

    }
}
