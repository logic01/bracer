using System;
using System.Collections.Generic;
using System.Text;

namespace PR.Constants.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Calculate age from birth date
        /// </summary>
        /// <param name="dob"></param>
        /// <returns></returns>
        public static string GetAge(this DateTime dob)
        {
            // Save today's date.
            var today = DateTime.Today;
            // Calculate the age.
            var age = today.Year - dob.Year;
            // Go back to the year the person was born in case of a leap year
            if (dob.Date > today.AddYears(-age)) age--;
            return age.ToString();
        }
    }
}
