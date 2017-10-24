using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Surveys.Data;
using Microsoft.EntityFrameworkCore;
using Surveys.Models.Entities;
using System.Linq;

namespace Surveys.Data.Tests
{
    public static class SurveysInitializer
    {
        public static void InitializeData(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<SurveysContext>();
            InitializeData(context);
        }
        public static void InitializeData(SurveysContext context)
        {
            context.Database.EnsureCreated();
            ClearData(context);
            SeedData(context);
        }
        public static void ClearData(SurveysContext context)
        {
            ResetIdentity(context);
        }
        public static void ExecuteDeleteSQL(SurveysContext context, string tableName)
        {
            context.Database.ExecuteSqlCommand($"delete from {tableName}");
        }
        public static void ResetIdentity(SurveysContext context)
        {
        }

        public static void SeedData(SurveysContext context)
        {
            try
            {
                if (context.Surveys.Any())
                {
                    return;
                }

                var surveys = new[]
                {
                    new Survey {
                        Caption = "Sample survey #1",
                        Description = "First survey to demonstrate general validation rules and reporting features.",
                        Published = DateTime.UtcNow,
                        Questions = new []
                        {
                            new Question
                            {
                                SerialNumber = 2,
                                Sentence = "What's the color do you like the most?",
                                Comments = "Choose one option",
                                RequiredChoices = 1,
                                AllowedChoices = 1,
                                Choices = new []
                                {
                                    new Choice { Statement = "Red" },
                                    new Choice { Statement = "Blue" },
                                    new Choice { Statement = "Green" }
                                }
                            },
                            new Question
                            {
                                SerialNumber = 1,
                                Sentence = "What are your favorite shapes?",
                                Comments = "You may choose from one up to three options",
                                RequiredChoices = 2,
                                AllowedChoices = 3,
                                Choices = new []
                                {
                                    new Choice { Statement = "Square" },
                                    new Choice { Statement = "Circle" },
                                    new Choice { Statement = "Rectangle" },
                                    new Choice { Statement = "Ellipse" },
                                    new Choice { Statement = "Triangle" }
                                }
                            },
                            new Question
                            {
                                SerialNumber = 3,
                                Sentence = "The weather is fine when...?",
                                Comments = "Any number of choices allowed",
                                RequiredChoices = 0,
                                AllowedChoices = 3,
                                Choices = new []
                                {
                                    new Choice { Statement = "The sun is shining!" },
                                    new Choice { Statement = "All the clouds are gone..." },
                                    new Choice { Statement = "Sea breeze blows..." }
                                }
                            },
                        }
                    }
                };
                context.Surveys.AddRange(surveys);

                var bulletins = new []
                {
                    new Bulletin
                    {
                        Survey = surveys[0],
                        Completed = DateTime.UtcNow,
                        Votes = new []
                        {
                            new Vote { Choice = surveys[0].Questions.FirstOrDefault().Choices.FirstOrDefault() },
                            new Vote { Choice = surveys[0].Questions.Skip(1).FirstOrDefault().Choices.Skip(1).FirstOrDefault() },
                            new Vote { Choice = surveys[0].Questions.Skip(1).FirstOrDefault().Choices.Skip(2).FirstOrDefault() },
                            new Vote { Choice = surveys[0].Questions.Skip(2).FirstOrDefault().Choices.Skip(1).FirstOrDefault() },
                            new Vote { Choice = surveys[0].Questions.Skip(2).FirstOrDefault().Choices.Skip(2).FirstOrDefault() },
                        }
                    },
                    new Bulletin
                    {
                        Survey = surveys[0],
                        Completed = DateTime.UtcNow,
                        Votes = new []
                        {
                            new Vote { Choice = surveys[0].Questions.FirstOrDefault().Choices.FirstOrDefault() },
                            new Vote { Choice = surveys[0].Questions.Skip(1).FirstOrDefault().Choices.Skip(1).FirstOrDefault() },
                            new Vote { Choice = surveys[0].Questions.Skip(1).FirstOrDefault().Choices.Last()},
                            new Vote { Choice = surveys[0].Questions.Skip(2).FirstOrDefault().Choices.FirstOrDefault() },
                            new Vote { Choice = surveys[0].Questions.Skip(2).FirstOrDefault().Choices.Skip(2).FirstOrDefault() },
                        }
                    },
                    new Bulletin
                    {
                        Survey = surveys[0],
                        Completed = DateTime.UtcNow,
                        Votes = new []
                        {
                            new Vote { Choice = surveys[0].Questions.FirstOrDefault().Choices.Skip(1).FirstOrDefault() },
                            new Vote { Choice = surveys[0].Questions.Skip(1).FirstOrDefault().Choices.FirstOrDefault() },
                            new Vote { Choice = surveys[0].Questions.Skip(1).FirstOrDefault().Choices.Skip(2).FirstOrDefault() },
                            new Vote { Choice = surveys[0].Questions.Skip(2).FirstOrDefault().Choices.Skip(2).FirstOrDefault() },
                            new Vote { Choice = surveys[0].Questions.Skip(2).FirstOrDefault().Choices.Skip(2).FirstOrDefault() },
                        }
                    },
                    new Bulletin
                    {
                        Survey = surveys[0],
                        Completed = DateTime.UtcNow,
                        Votes = new []
                        {
                            new Vote { Choice = surveys[0].Questions.FirstOrDefault().Choices.Last() },
                            new Vote { Choice = surveys[0].Questions.Skip(1).FirstOrDefault().Choices.Skip(2).FirstOrDefault() },
                            new Vote { Choice = surveys[0].Questions.Skip(1).FirstOrDefault().Choices.Skip(2).FirstOrDefault() },
                            new Vote { Choice = surveys[0].Questions.Skip(2).FirstOrDefault().Choices.FirstOrDefault() },
                            new Vote { Choice = surveys[0].Questions.Skip(2).FirstOrDefault().Choices.Skip(2).FirstOrDefault() },
                        }
                    }
                };

                context.Bulletins.AddRange(bulletins);
                context.SaveChanges();               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Console.WriteLine("Data initialization complete...");
            }
        }
    }
}
