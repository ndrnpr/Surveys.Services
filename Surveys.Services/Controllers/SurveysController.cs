using Microsoft.AspNetCore.Mvc;
using Surveys.Data.Repository.Interfaces;
using Surveys.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Surveys.Services.Controllers
{
    [Route("api/[controller]")]
    public class SurveysController : Controller
    {
        private ISurveyRepository Repository { get; set; }

        public SurveysController(ISurveyRepository repository)
        {
            Repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Repository.GetAllWithStatsInfo());
        }

        [HttpGet("{id}", Name = "GetSurveyInfo")]
        public IActionResult Get(int id)
        {
            var item = Repository.GetSurveyInfo(id);
            if (item == null)
            {
                return NotFound();
            }
            return Json(item);
        }
    }
}
