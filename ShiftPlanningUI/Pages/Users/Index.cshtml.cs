using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftPlanningLibrary;
using ShiftPlanningUI.Model.Users;
using ShiftPlanningUI.Services;

namespace ShiftPlanningUI.Pages.Users {
    public class IndexModel : PageModel {
        private readonly IUserCatalogue _catalogue;
        private readonly IUserService _service;

        [BindProperty]
        public List<IUser> Users { get; set; }
        [BindProperty]
        public string Email { get; set; }

        public IndexModel(IUserCatalogue catalogue, IUserService service) {
            _catalogue = catalogue;
            _service = service;

            Users = _catalogue.GetUsers(_service.GetCurrentUser() ?? new User("", ""));
        }

        public void OnGet() { }

        public IActionResult OnPostMakeAdmin() {
            _service.MakeUserAdmin(Email);
            return Redirect("/Users");
        }
        public IActionResult OnPostDelete() {
            _catalogue.DeleteUser(Email, _service.GetCurrentUser() ?? new User("", ""));
            return Redirect("/Users");
        }
    }
}
