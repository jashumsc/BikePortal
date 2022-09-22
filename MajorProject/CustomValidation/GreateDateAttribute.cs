using System;
using System.ComponentModel.DataAnnotations;

namespace MajorProject.CustomValidation
{
    public class GreateDateAttribute : ValidationAttribute
    {
        public GreateDateAttribute() : base("{0} Date should be More than current date")
        {

        }

        public override bool IsValid(object value)
        {
            DateTime propValue = Convert.ToDateTime(value);
            if (propValue >= DateTime.Now)
                return true;
            else
                return false;
        }
    }
}
