using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Surveys.Data.Repository.Interfaces;
using Surveys.Models.Entities;

namespace Surveys.Services.Controllers
{
    [Route("api/[controller]")]
    public class BulletinsController : Controller
    {
        private IBulletinRepository Repository { get; set; }

        public BulletinsController(IBulletinRepository repository)
        {
            Repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Repository.GetAllWithVotes());
        }

        //[HttpGet("{id}", Name = "GetBulletinDetails")]
        //public IActionResult Get(int id)
        //{
        //    var item = Repository.GetOneWithVoteInfo(id);
        //    if (item == null)
        //    {
        //        return NotFound();
        //    }
        //    return Json(item);
        //}

        [HttpPost]
        public IActionResult Create([FromBody]Bulletin item)
        {
            if (item == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            item.Completed = null;
            Repository.Add(item);

            return Ok();
        }
    }
}