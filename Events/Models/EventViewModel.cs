using Events.Web.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Events.Models
{
    public class EventViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(100)]
        public string Title { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(100)]
        public string Description { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(100)]
        public string Location { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Date)]
        [StartDateTime(ErrorMessage = "Start Date must be a valid future date.")]
        public DateTime? StartDate { get; set; }

        public DateTime EndDate
        {
            get
            {
                return this.StartDate.Value.AddDays(this.Days.Value);
            }
        }

        public string AudiencePluralName { get; set; }

        [Required(ErrorMessage = "*")]
        [Range(0, 100, ErrorMessage = "Days must be a non-decimal value between 1 and 100")]
        public int? Days { get; set; }

        public string Duration
        {
            get
            {
                return this.Days + (this.Days == 1 ? " Day" : " Days");
            }
        }

        public string OwnerId { get; set; }

        public string Owner { get; set; }

        public int Audience { get; set; }
    }
}