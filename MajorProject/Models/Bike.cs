using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MajorProject.Models
{
    [Table("Bikes")]
    public class Bike
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int BikeId { get; set; }


        [Required]
        [Display(Name ="Bike Name")]
        [StringLength(50)]
        public string BikeName { get; set; }

        #region


        [Required]
        public int CompanyId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [ForeignKey(nameof(Bike.CompanyId))]
        public Company Company { get; set; }


        #endregion

        [Required]
        public int BikePrice { get; set; }


        [Required]
        public int RentPrice { get; set; }

        [Required]
        public int BikeMileage { get; set; }

        public string BikeImage { get; set; }



        [Required]
        [NotMapped]
        public IFormFile BikePhoto { get; set; }

    }
}
