using System.Collections.Generic;
using UI.Web.Models;

namespace UI.Web.Models.View
{
    public class ManageUsersViewModel
    {
        public IEnumerable<ApplicationUser> Administrators { get; set; }
        public IEnumerable<ApplicationUser> Everyone { get; set; }
    }
}
