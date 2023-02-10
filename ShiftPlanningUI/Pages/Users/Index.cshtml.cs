using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftPlanningLibrary;
using ShiftPlanningUI.Model.Users;
using ShiftPlanningUI.Services;

namespace ShiftPlanningUI.Pages.Users {
    public class IndexModel : PageModel {
        private readonly IUserCatalogue _catalogue;
        private readonly IUserService _service;

        public List<IUser> Users { get; set; }
        [BindProperty]
        public string Email { get; set; }

        public IndexModel(IUserCatalogue catalogue, IUserService service) {
            _catalogue = catalogue;
            _service = service;

            Users = _catalogue.GetUsers(_service.GetCurrentUser());
        }

        public void OnGet() { }

        public IActionResult OnPost() {
            _service.MakeUserAdmin(Email);
            return Redirect("/Users");
        }
    }
}
