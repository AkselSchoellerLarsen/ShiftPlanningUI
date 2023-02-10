using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftPlanningLibrary;
using ShiftPlanningUI.Model;
using ShiftPlanningUI.Model.Shifts;
using ShiftPlanningUI.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShiftPlanningUI.Pages {
    public class IndexModel : PageModel {
        public List<IShift> Shifts { get; set; }

        [BindProperty]
        public DateTime Start { get; set; }
        [BindProperty]
        public DateTime End { get; set; }

        public IUserService UserService { get; set; }

        public IndexModel(IUserService userService) {
            Shifts = new List<IShift>();
            UserService = userService;
        }

        public void OnGet() {
            Shifts = new ShiftCatalogue().GetShifts(UserService.GetCurrentUser());

            Start = DateTime.Today.AddHours(8);
            End = DateTime.Today.AddHours(16);
        }

        public IActionResult OnPost() {
            new ShiftCatalogue().PostShift(new Shift(Start, End), UserService.GetCurrentUser());

            return Redirect("~/");
        }
    }
}