using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Events.Web.Validators
{
    public class StartDateTimeAttribute : RangeAttribute
    {
        private string maxValue = string.Empty;

        public StartDateTimeAttribute() :
            base(typeof(DateTime), DateTime.UtcNow.ToString("yyyy-MM-dd"), DateTime.MaxValue.ToUniversalTime().ToString("yyyy-MM-dd"))
        {
        }
    }
}