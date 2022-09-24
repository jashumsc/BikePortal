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

        [Required]
        public string CustomerName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered phone format is not valid.")]

        public string CustomerPhone { get; set; }


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
