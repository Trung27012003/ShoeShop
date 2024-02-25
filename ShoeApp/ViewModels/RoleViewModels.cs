using Microsoft.AspNetCore.Identity;

namespace ShoeApp.ViewModels
{
    public class RoleViewModels:IdentityRole
    {
        public IEnumerable<string> Claims { get; set; }
    }
}
