namespace Vista.Web.Models
{
    public class WorkshopVM
    {
        public int WorkshopId { get; set; }
        public string Name { get; set; }
        public DateTime DateAndTime { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string BookingRef { get; set; }
    }
}
