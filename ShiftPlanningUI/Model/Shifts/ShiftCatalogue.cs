using ShiftPlanningLibrary;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ShiftPlanningUI.Model.Shifts {
    public class ShiftCatalogue : IShiftCatalogue {
        private HttpClient _client;

        public ShiftCatalogue() {
            _client = new HttpClient();
        }

        public List<IShift> GetShifts(IUser? user) {
            if(user is null) {
                return new List<IShift>();
            }
            using (HttpRequestMessage request = new HttpRequestMessage()) {
                request.Method = HttpMethod.Get;
                request.RequestUri = new Uri(RESTHelper.GetShiftsUri);

                request.Headers.Add("email", user.Email);
                request.Headers.Add("password", user.Password);
                request.Headers.Add("isAdmin", $"{user.IsAdmin}");

                using (HttpResponseMessage response = _client.Send(request)) {
                    HttpContent content = response.Content;
                    List<IShift> shifts = new List<IShift>();
                    List<Shift>? fromJson = content.ReadFromJsonAsync<List<Shift>>().Result;
                    if(fromJson is not null) {
                        shifts.AddRange(fromJson);
                    }
                    return shifts;
                }
            }
        }

        public bool PostShift(IShift shift, IUser? user) {
            if (user is null) {
                return false;
            }
            using (HttpRequestMessage request = new HttpRequestMessage()) {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(RESTHelper.PostShiftUri);

                request.Headers.Add("email", user.Email);
                request.Headers.Add("password", user.Password);
                request.Headers.Add("isAdmin", $"{user.IsAdmin}");

                request.Content = JsonContent.Create<IShift>(shift);

                using (HttpResponseMessage response = _client.Send(request)) {
                    return response.IsSuccessStatusCode;
                }
            }
        }

        public bool PutShift(IShift shift, IUser? user) {
            if (user is null) {
                return false;
            }
            using (HttpRequestMessage request = new HttpRequestMessage()) {
                request.Method = HttpMethod.Put;
                request.RequestUri = new Uri(RESTHelper.PutShiftUri);

                request.Headers.Add("email", user.Email);
                request.Headers.Add("password", user.Password);
                request.Headers.Add("isAdmin", $"{user.IsAdmin}");

                request.Content = JsonContent.Create<IShift>(shift);

                using (HttpResponseMessage response = _client.Send(request)) {
                    return response.IsSuccessStatusCode;
                }
            }
        }

        public bool DeleteShift(int id, IUser? user) {
            if (user is null) {
                return false;
            }
            using (HttpRequestMessage request = new HttpRequestMessage()) {
                request.Method = HttpMethod.Delete;
                request.RequestUri = new Uri(RESTHelper.DeleteShiftUri(id));

                request.Headers.Add("email", user.Email);
                request.Headers.Add("password", user.Password);
                request.Headers.Add("isAdmin", $"{user.IsAdmin}");

                using (HttpResponseMessage response = _client.Send(request)) {
                    return response.IsSuccessStatusCode;
                }
            }
        }
        public bool DeleteShift(IShift shift, IUser? user) {
            return DeleteShift(shift.Id, user);
        }
    }
}
