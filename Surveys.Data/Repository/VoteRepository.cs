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
    public class VoteRepository : Repository<Vote>, IVoteRepository
    {       
        public VoteRepository(DbContextOptions<SurveysContext> options) : base(options)
        {            
        }

        public VoteRepository() : base()
        {            
        }

        public override IEnumerable<Vote> GetAll()
            => Table.OrderBy(x => x.ID);

        public override IEnumerable<Vote> GetRange(int skip, int take)
            => GetRange(Table.OrderBy(x => x.ID), skip, take);
                

        public int GetCount(int choiceID)
        {
            return Table.Where(x => x.ChoiceID == choiceID).Count();
        }

        public IEnumerable<VoteInfo> GetBulletinVotes(int bulletinID)
        {
            return Table.Where(x => x.BulletinID == bulletinID).Select(x => new VoteInfo { ChoiceID = x.ChoiceID });
        }
    }
}
