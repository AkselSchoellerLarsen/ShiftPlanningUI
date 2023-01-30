using ShiftPlanningLibrary;

namespace ShiftPlanningUI.Model.Shifts {
    public class ShiftCatalogue : IShiftCatalogue {
        public ShiftCatalogue() { }

        public List<IShift> GetShifts() {
            HttpClient client = new HttpClient();
            HttpResponseMessage hrm = client.GetAsync(RESTHelper.GetShiftsURI).Result;
            List<IShift> shifts = new List<IShift>();
            shifts.AddRange(hrm.Content.ReadFromJsonAsync<List<Shift>>().Result);
            return shifts;
        }

        public bool PostShift(IShift shift) {
            HttpClient client = new HttpClient();
            HttpContent content = JsonContent.Create<IShift>(shift);

            HttpResponseMessage hrm = client.PostAsync(RESTHelper.PostShiftURI, content).Result;
            return hrm.IsSuccessStatusCode;
        }

        public bool PutShift(IShift shift) {
            HttpClient client = new HttpClient();
            HttpContent content = JsonContent.Create<IShift>(shift);

            HttpResponseMessage hrm = client.PutAsync(RESTHelper.PutShiftURI, content).Result;
            return hrm.IsSuccessStatusCode;
        }

        public bool DeleteShift(int id) {
            HttpClient client = new HttpClient();

            HttpResponseMessage hrm = client.DeleteAsync(RESTHelper.DeleteShiftURI(id)).Result;
            return hrm.IsSuccessStatusCode;
        }
        public bool DeleteShift(IShift shift) {
            return DeleteShift(shift.Id);
        }
    }
}
