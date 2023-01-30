using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftPlanningLibrary;
using ShiftPlanningUI.Model;
using ShiftPlanningUI.Model.Shifts;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShiftPlanningUI.Pages {
    public class IndexModel : PageModel {
        public List<IShift> Shifts { get; set; }

        [BindProperty]
        public DateTime Start { get; set; }
        [BindProperty]
        public DateTime End { get; set; }

        public IndexModel() {
            Shifts = new List<IShift>();
        }

        public void OnGet() {
            Shifts = new ShiftCatalogue().GetShifts();

            Start = DateTime.Today.AddHours(8);
            End = DateTime.Today.AddHours(16);
        }

        public async Task<IActionResult> OnPostAsync() {
            new ShiftCatalogue().PostShift(new Shift(Start, End));

            return Redirect("~/");
        }
    }
}