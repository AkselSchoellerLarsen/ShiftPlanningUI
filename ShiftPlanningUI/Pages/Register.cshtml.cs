using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftPlanningLibrary;
using ShiftPlanningUI.Model.Users;
using ShiftPlanningUI.Services;

namespace ShiftPlanningUI.Pages {
    public class RegisterModel : PageModel {
        private IUserService _userService;

        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        public string ErrorLine { get; set; }

        public RegisterModel(IUserService userService) {
            _userService = userService;

            Email = "";
            Password = "";
            ConfirmPassword = "";
            ErrorLine = "";
        }

        public void OnGet() { }

        public IActionResult OnPost() {
            if(Password != ConfirmPassword) {
                ErrorLine = "Passwords were not the same";
                return new EmptyResult();
            }

            _userService.Register(new User(Email, Password));
            _userService.Login(new User(Email, Password));
            return Redirect("~/");
        }
    }
}
