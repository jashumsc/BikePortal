using MajorProject.CustomValidation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MajorProject.Models
{
    [Table("Rents")]
    public class Rent
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RentId { get; set; }

        public string CustomerName { get; set; }

        #region


        [Required]
        public int BikeId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [ForeignKey(nameof(Rent.BikeId))]
        public Bike Bike { get; set; }


        #endregion

        [Required]
        [GreateDate]

        public DateTime RentDate { get; set; }

    }
}
