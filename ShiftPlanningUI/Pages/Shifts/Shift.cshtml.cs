using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftPlanningLibrary;
using ShiftPlanningUI.Model.Shifts;

namespace ShiftPlanningUI.Pages.Shifts {
    public class ShiftModel : PageModel {
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public DateTime Start { get; set; }
        [BindProperty]
        public DateTime End { get; set; }

        public ShiftModel() { }

        public void OnGet(int id) {
            IShift shift = new ShiftCatalogue().GetShifts().Find((s) => {
                return s.Id == id;
            }) ?? new Shift();
            Id = shift.Id;
            Start = shift.Start;
            End = shift.End;
        }

        public IActionResult OnPost() {
            new ShiftCatalogue().PutShift(new Shift(Id, Start, End));

            return Redirect("~/");
        }

        public IActionResult OnPostDelete() {
            new ShiftCatalogue().DeleteShift(new Shift(Id, Start, End));

            return Redirect("~/");
        }
    }
}
