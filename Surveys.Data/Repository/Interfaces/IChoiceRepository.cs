using Surveys.Data.Repository.Base;
using Surveys.Models.Entities;
using Surveys.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace Surveys.Data.Repository.Interfaces
{
    public interface IChoiceRepository : IRepository<Choice>
    {
        IEnumerable<Choice> GetAllWithVotes();
        Choice GetOneWithVotes(int? id);
        IEnumerable<ChoiceInfo> GetQuestionChoices(int questionID);

        int GetQuestionVotesCount(int questionID);

    }
}
