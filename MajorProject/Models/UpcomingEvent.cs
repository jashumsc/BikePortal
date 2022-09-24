using MajorProject.CustomValidation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace MajorProject.Models
{
    [Table("UpcomingEvents")]
    public class UpcomingEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventId { get; set; }



        [Required]
        [StringLength(50)]
        public string EventName { get; set; }



        [Required]
        [StringLength(50)]
        public string EventDescription { get; set; }

       

        [Required]
        [GreateDate]

        public DateTime EventDate { get; set; }

        public string EventImage { get; set; }



        [Required]
        [NotMapped]
        public IFormFile EventPhoto { get; set; }


    }
}
