using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MajorProject.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }


        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }


        [Required]
        public int ProductPrice { get; set; }

        public string ProductImage { get; set; }



        [Required]
        [NotMapped]
        public IFormFile ProductPhoto { get; set; }

    }
}
