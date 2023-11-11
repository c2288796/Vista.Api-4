using System.ComponentModel.DataAnnotations;

namespace Vista.Web.Data
{
    public class Workshop
    {
        public Workshop()
        {
        }

        public Workshop(int workshopId, string name, DateTime dateAndTime)
        {
            WorkshopId = workshopId;
            Name = name;
            DateAndTime = dateAndTime;
        }

        public int WorkshopId { get; set; }

        [Required]
        public string Name { get; set; }
        public DateTime DateAndTime { get; set; }
        public string CategoryCode { get; set; } = null!;
        public string BookingRef { get; set; } = null!;

        // Placeholder for List of Staff (many side of one-to-many)
    }
}