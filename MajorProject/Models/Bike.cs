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

        //Stores the name of Bike
        [Required]
        [Display(Name ="Bike Name")]
        [StringLength(50)]
        public string BikeName { get; set; }

        #region

        //Stores the name of company of the bike

        [Required]
        [Display(Name = "Company Name")]

        public int CompanyId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [ForeignKey(nameof(Bike.CompanyId))]
        public Company Company { get; set; }


        #endregion
        //Stores the Price of Bike

        [Required]
        [Display(Name = "Bike Price")]

        public int BikePrice { get; set; }

        //Stores the Price of Bike to rent
        [Required]
        [Display(Name = "Rent Price")]

        public int RentPrice { get; set; }

        //Stores the Mileage of Bike
        [Required]
        [Display(Name = "Bike Mileage")]

        public int BikeMileage { get; set; }

        public string BikeImage { get; set; }



        [Required]
        [NotMapped]
        public IFormFile BikePhoto { get; set; }

    }
}
