using System;

namespace Lab0.Models
{
    public class Birth
    {
        public string Name { get; set; }
        public DateTime? Date { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Name)
                   && Date.HasValue
                   && Date.Value < DateTime.Today;
        }

        public int CalculateAge()
        {
            if (!Date.HasValue)
                return 0;

            var today = DateTime.Today;
            var age = today.Year - Date.Value.Year;
            if (Date.Value.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}