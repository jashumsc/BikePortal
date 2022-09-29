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

        //Stores the name of company
        [Required]
        [Display(Name ="Company Name")]
        [StringLength(50)]
        public string CompanyName { get; set; }

        //Stores the Description of company

        [Required]
        [StringLength(2000)]
        [Display(Name = "Company Description")]

        public string CompanyDescription { get; set; }

        //Stores the Logo of company

        public string CompanyImage { get; set; }

        [Required]
        [NotMapped]
        public IFormFile CompanyPhoto { get; set; }

        public ICollection<Bike> Bikes { get; set; } 
    }
}
