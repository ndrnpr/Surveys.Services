using Microsoft.EntityFrameworkCore;
using Surveys.Data.Repository.Base;
using Surveys.Data.Repository.Interfaces;
using Surveys.Models.Entities;
using Surveys.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Surveys.Data.Repository
{
    public class ChoiceRepository : Repository<Choice>, IChoiceRepository
    {
        private readonly IVoteRepository _voteRepository;

        public ChoiceRepository(DbContextOptions<SurveysContext> options, IVoteRepository voteRepository) : base(options)
        {
            _voteRepository = voteRepository;
        }

        public ChoiceRepository(IVoteRepository voteRepository) : base()
        {
            _voteRepository = voteRepository;
        }

        public override IEnumerable<Choice> GetAll()
            => Table.OrderBy(x => x.ID);

        public override IEnumerable<Choice> GetRange(int skip, int take)
            => GetRange(Table.OrderBy(x => x.ID), skip, take);

        public Choice GetOneWithVotes(int? id)
            => Table.Include(x => x.Votes).FirstOrDefault(x => x.ID == id);

        public IEnumerable<Choice> GetAllWithVotes()
            => Table.Include(x => x.Votes).OrderBy(x => x.ID);

        public IEnumerable<ChoiceInfo> GetQuestionChoices(int questionID)
        {
            return Table
                .Where(x => x.QuestionID == questionID)
                .Select(x => 
                    new ChoiceInfo
                    {
                        ChoiceID = x.ID,
                        QuestionID = x.QuestionID,
                        Statement = x.Statement,
                        VotesCount = _voteRepository.GetCount(x.ID)
                    });
        }

        public int GetQuestionVotesCount(int questionID)
        {
            return Table
                .Where(x => x.QuestionID == questionID)
                .Include(x => x.Votes)
                .Sum(x => x.Votes.Count() /*_voteRepository.GetCount(x.ID)*/);
        }

    }
}
