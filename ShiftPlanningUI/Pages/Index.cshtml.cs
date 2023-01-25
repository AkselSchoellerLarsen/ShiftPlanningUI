using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftPlanningLibrary;
using ShiftPlanningUI.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShiftPlanningUI.Pages {
    public class IndexModel : PageModel {
        public List<IShift> Shifts { get; set; }

        [BindProperty]
        public Shift Shift { get; set; }

        public IndexModel() {
            Shifts = new List<IShift>();
            Shift = new Shift();
        }

        public void OnGet() {
            Shifts = GetShifts();

            Shift.Start = DateTime.Today.AddHours(8);
            Shift.End = DateTime.Today.AddHours(16);
        }

        private List<IShift> GetShifts() {
            HttpClient client = new HttpClient();
            HttpResponseMessage hrm = client.GetAsync(RESTHelper.GetShiftsURI).Result;
            List<IShift> shifts = new List<IShift>();
            shifts.AddRange(hrm.Content.ReadFromJsonAsync<List<Shift>>().Result);
            return shifts;
        }

        public async Task<IActionResult> OnPostAsync() {
            HttpClient client = new HttpClient();
            HttpContent content = JsonContent.Create<IShift>(Shift);

            await client.PostAsync(RESTHelper.PostShiftURI, content);

            return RedirectToPage("Index");
            //return Redirect($"~/");
        }
    }
}