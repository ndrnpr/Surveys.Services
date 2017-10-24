using System;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;

namespace Surveys.Data
{
    public class SurveysExecutionStrategy : ExecutionStrategy
    {
        public SurveysExecutionStrategy(DbContext context) : base(context, DefaultMaxRetryCount, DefaultMaxDelay)
        {
        }

        public SurveysExecutionStrategy(DbContext context, int maxRetryCount, TimeSpan maxRetryDelay) : base(context, maxRetryCount, maxRetryDelay)
        {
        }

        protected override bool ShouldRetryOn(Exception exception)
        {
            return true;
        }
    }

}