namespace GymApp.Models.Entities
{
    public class GymClass
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime EndTime => StartDate + Duration;
        public string Description { get; set; } = string.Empty;

        public ICollection<ApplicationUserGymClass> AttendingMembers { get; set; } = new List<ApplicationUserGymClass>();
    }
    
}
