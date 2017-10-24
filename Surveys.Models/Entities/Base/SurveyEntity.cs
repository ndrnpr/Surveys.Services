using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Surveys.Models.Entities.Base
{
    public abstract class SurveyEntity
    {
        [Timestamp]
        public byte[] TimeStamp { get; set; }
    }
}
