using Microsoft.AspNetCore.Identity;

namespace GymApp.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<ApplicationUserGymClass> AttendinClasses { get; set; } = new List<ApplicationUserGymClass>();
    }
    
}
