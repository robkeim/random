using Microsoft.AspNet.Identity.EntityFramework;

namespace MyShuttle.Model
{
    public class ApplicationUser : IdentityUser
    {
        public int CarrierId { get; set; }
    }
}