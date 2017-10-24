using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Surveys.Data;
using Surveys.Data.Repository;
using Surveys.Data.Repository.Base;
using Surveys.Data.Repository.Interfaces;
using Surveys.Models.Entities;
using Surveys.Services.Controllers;
using System;
using Xunit;

namespace Surveys.Tests
{
    public class BulletinTests
    {
        private IBulletinRepository GetInMemoryBulletinRepository()
        {
            DbContextOptions<SurveysContext> options;
            var builder = new DbContextOptionsBuilder<SurveysContext>();
            builder.UseInMemoryDatabase(nameof(Surveys));
            options = builder.Options;

            var voteRepository = new VoteRepository();
            var choiceRepository = new ChoiceRepository(voteRepository);
            var questionRepository = new QuestionRepository(choiceRepository);
            var surveyRepository = new SurveyRepository(questionRepository);
            return new BulletinRepository(options, surveyRepository, voteRepository);
        }

        [Fact]
        public void AddBulletinMoq()
        {
            // Arrange
            var mock = new Mock<IBulletinRepository>();
            var controller = new BulletinsController(mock.Object);
            var newBulletin = new Bulletin()
            {
                SurveyID = 1,
                Votes = new[]
                {
                    new Vote { ChoiceID = 1 },
                    new Vote { ChoiceID = 2 },
                }
            };
            // Act
            var result = controller.Create(newBulletin);
            // Assert
            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            mock.Verify(r => r.Add(newBulletin, true));

        }        
    }
}
