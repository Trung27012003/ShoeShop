using ShoeApp.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ShoeApp.ViewModels
{
    public class UserViewModel : User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        [Display(Name = "Role")]
        public string SelectedRole { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
