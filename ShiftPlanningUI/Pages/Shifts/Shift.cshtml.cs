using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftPlanningLibrary;
using ShiftPlanningUI.Model.Shifts;
using ShiftPlanningUI.Services;

namespace ShiftPlanningUI.Pages.Shifts {
    public class ShiftModel : PageModel {
        private readonly IShiftCatalogue _shiftCatalogue;
        private readonly IUserService _userService;

        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public DateTime Start { get; set; }
        [BindProperty]
        public DateTime End { get; set; }
        [BindProperty]
        public string? UserEmail { get; set; }
        [BindProperty]
        public bool IsNew { get; set; }
        [BindProperty]
        public string ErrorLine { get; set; }

        public ShiftModel(IShiftCatalogue shiftCatalogue, IUserService userService) {
            _shiftCatalogue = shiftCatalogue;
            _userService = userService;
        }

        public void OnGet(int id, bool isNew) {
            IsNew = isNew;
            if (IsNew) {
                Start = DateTime.Today.AddHours(8);
                End = DateTime.Today.AddHours(16);
                return;
            }

            IShift shift = _shiftCatalogue.GetShifts(_userService.GetCurrentUser()).Find((s) => {
                return s.Id == id;
            }) ?? new Shift();
            Id = shift.Id;
            Start = shift.Start;
            End = shift.End;
            UserEmail = shift.UserEmail;
        }

        public IActionResult? OnPost() {
            try {
                Shift shift;
                if (UserEmail is null) {
                    shift = new Shift(Start, End);
                } else {
                    shift = new Shift(UserEmail, Start, End);
                }

                if (IsNew) {
                    _shiftCatalogue.PostShift(shift, _userService.GetCurrentUser());
                } else {
                    shift.Id = Id;
                    _shiftCatalogue.PutShift(shift, _userService.GetCurrentUser());
                }
                return Redirect("~/");
            }
            catch (Exception e) {
                ErrorLine = "Something went wrong";
                return null;
            }
        }

        public IActionResult? OnPostDelete() {
            try {
                _shiftCatalogue.DeleteShift(Id, _userService.GetCurrentUser());

                return Redirect("~/");
            }
            catch (Exception e) {
                ErrorLine = "Something went wrong";
                return null;
            }
        }
    }
}
