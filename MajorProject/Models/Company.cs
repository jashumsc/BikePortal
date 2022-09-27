using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MajorProject.Models
{
    [Table("Companies")]
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyId { get; set; }


        [Required]
        [Display(Name ="Company Name")]
        [StringLength(50)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(2000)]
        public string CompanyDescription { get; set; }
        public string CompanyImage { get; set; }

        [Required]
        [NotMapped]
        public IFormFile CompanyPhoto { get; set; }

        public ICollection<Bike> Bikes { get; set; } 
    }
}
