using Surveys.Data.Repository.Base;
using Surveys.Models.Entities;
using Surveys.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace Surveys.Data.Repository.Interfaces
{
    public interface IVoteRepository : IRepository<Vote>
    {
        int GetCount(int choiceID);
        IEnumerable<VoteInfo> GetBulletinVotes(int bulletinID);
    }
}
