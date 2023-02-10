using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftPlanningLibrary;
using ShiftPlanningUI.Services;

namespace ShiftPlanningUI.Pages {
    public class LoginModel : PageModel {
        private IUserService _userService;

        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string ErrorLine { get; set; }

        public LoginModel(IUserService userService) {
            _userService = userService;

            Email = "";
            Password = "";
            ErrorLine = "";
        }

        public void OnGet() {
            _userService.Logout();
        }

        public IActionResult OnPost() {
            if(_userService.Login(new User(Email, Password))) {
                return Redirect("~/");
            }
            ErrorLine = "Login failed";
            return new EmptyResult();
        }
    }
}
