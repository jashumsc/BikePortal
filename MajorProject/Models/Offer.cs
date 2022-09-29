using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MajorProject.Models
{
    [Table("Offers")]
    public class Offer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OfferId { get; set; }


        [Required]
        [StringLength(50)]
        [Display(Name = "Bike Name")]

        public string BikeName { get; set; }

        [Required]
        [Display(Name = "Bike Price")]

        public int BikePrice { get; set; }

        [Required]
        [Display(Name = "Offer Price")]

        public int OfferPrice { get; set; }


        public string BikeImage { get; set; }



        [Required]
        [NotMapped]
        public IFormFile BikePhoto { get; set; }
    }
}
