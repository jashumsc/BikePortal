using MajorProject.CustomValidation;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MajorProject.Models
{
    [Table("Purchases")]
    public class Purchase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int PurchaseId { get; set; }


        public string CustomerName { get; set; }

        #region


        [Required]
        public int BikeId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [ForeignKey(nameof(Purchase.BikeId))]
        public Bike Bike { get; set; }


        #endregion

        [Required]
        [GreateDate]
        public DateTime Date { get; set; }
    }
}
