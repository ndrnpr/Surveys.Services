using Surveys.Data.Repository.Base;
using Surveys.Models.Entities;
using Surveys.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace Surveys.Data.Repository.Interfaces
{
    public interface IBulletinRepository : IRepository<Bulletin>
    {
        IEnumerable<Bulletin> GetAllWithVotes();
        Bulletin GetOneWithVotes(int? id);

        int GetCount(int surveyID);

        //BulletinInfo GetOneWithVoteInfo(int id);
    }
}
