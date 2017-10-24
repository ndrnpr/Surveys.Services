using Microsoft.EntityFrameworkCore;
using Surveys.Data.Repository.Base;
using Surveys.Data.Repository.Interfaces;
using Surveys.Models.Entities;
using Surveys.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Surveys.Data.Repository
{
    public class BulletinRepository : Repository<Bulletin>, IBulletinRepository
    {
        private readonly IVoteRepository _voteRepository;
        private readonly ISurveyRepository _surveyRepository;

        public BulletinRepository(DbContextOptions<SurveysContext> options, ISurveyRepository surveyRepository, IVoteRepository voteRepository) : base(options)
        {
            _voteRepository = voteRepository;
            _surveyRepository = surveyRepository;
        }

        public BulletinRepository(ISurveyRepository surveyRepository, IVoteRepository voteRepository) : base()
        {
            _voteRepository = voteRepository;
            _surveyRepository = surveyRepository;
        }

        public override IEnumerable<Bulletin> GetAll()
            => Table.OrderByDescending(x => x.Completed);

        public override IEnumerable<Bulletin> GetRange(int skip, int take)
            => GetRange(Table.OrderByDescending(x => x.Completed), skip, take);

        public Bulletin GetOneWithVotes(int? id)
            => Table.Include(x => x.Votes).ThenInclude(v => v.Choice).FirstOrDefault(x => x.ID == id);

        public IEnumerable<Bulletin> GetAllWithVotes()
            => Table.Include(x => x.Votes).ThenInclude(v => v.Choice);

        public int GetCount(int surveyID)
        {
            return Table.Where(x => x.SurveyID == surveyID).Count();
        }

        //public BulletinInfo GetOneWithVoteInfo(int id)
        //{
        //    return Table
        //        .Where(x => x.ID == id)                
        //        .Select(x => new BulletinInfo { Survey = _surveyRepository.GetSurveyInfo(x.SurveyID), Votes = _voteRepository.GetBulletinVotes(x.ID) })
        //        .FirstOrDefault();
        //}
    }
}
