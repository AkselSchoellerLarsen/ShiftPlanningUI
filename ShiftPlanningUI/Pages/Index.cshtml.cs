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

        public IUserService UserService { get; set; }

        public IndexModel(IUserService userService) {
            Shifts = new List<IShift>();
            UserService = userService;
        }

        public void OnGet() {
            Shifts = new ShiftCatalogue().GetShifts(UserService.GetCurrentUser());
        }
    }
}