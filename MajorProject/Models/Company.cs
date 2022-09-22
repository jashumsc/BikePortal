using Microsoft.AspNetCore.Http;
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
        [StringLength(10,ErrorMessage =" {0} Cannot be empty")]
        public string CompanyName { get; set; }


        public string CompanyImage { get; set; }

        [Required]
        [NotMapped]
        public IFormFile CompanyPhoto { get; set; }

    }
}
