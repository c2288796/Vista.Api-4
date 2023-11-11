using System.ComponentModel.DataAnnotations;

namespace Vista.Web.Data
{
    public class WorkshopStaff
    {
        public WorkshopStaff()
        {
        }

        public WorkshopStaff(int workshopId, int staffId)
        {
            WorkshopId = workshopId;
            StaffId = staffId;
        }

        public int WorkshopId { get; set; }
        public Workshop Workshop { get; set; }
        public int StaffId { get; set; }
    }
}

