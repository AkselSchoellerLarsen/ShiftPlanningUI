using ShiftPlanningLibrary;

namespace ShiftPlanningUI.Model.Users {
    public class UserCatalogue : IUserCatalogue {
        private HttpClient _client;

        public UserCatalogue() {
            _client = new HttpClient();
        }

        public List<IUser> GetUsers(IUser user) {
            using (HttpRequestMessage request = new HttpRequestMessage()) {
                request.Method = HttpMethod.Get;
                request.RequestUri = new Uri(RESTHelper.GetUsersUri);

                request.Headers.Add("email", user.Email);
                request.Headers.Add("password", user.Password);
                request.Headers.Add("isAdmin", $"{user.IsAdmin}");

                using (HttpResponseMessage response = _client.Send(request)) {
                    List<IUser> users = new List<IUser>();
                    users.AddRange(response.Content.ReadFromJsonAsync<List<User>>().Result);
                    return users;
                }
            }
        }

        public bool MakeUserAdmin(string email, IUser user) {
            using (HttpRequestMessage request = new HttpRequestMessage()) {
                request.Method = HttpMethod.Put;
                request.RequestUri = new Uri(RESTHelper.MakeUserAdminUri);

                request.Headers.Add("email", user.Email);
                request.Headers.Add("password", user.Password);
                request.Headers.Add("isAdmin", $"{user.IsAdmin}");

                request.Content = JsonContent.Create<string>(email);

                using (HttpResponseMessage response = _client.Send(request)) {
                    return response.IsSuccessStatusCode;
                }
            }
        }

        public bool Register(IUser user) {
            using (HttpRequestMessage request = new HttpRequestMessage()) {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(RESTHelper.RegisterUserUri);

                request.Content = JsonContent.Create<IUser>(user);

                using (HttpResponseMessage response = _client.Send(request)) {
                    return response.IsSuccessStatusCode;
                }
            }
        }

        public bool VerifyUser(IUser user) {
            using (HttpRequestMessage request = new HttpRequestMessage()) {
                request.Method = HttpMethod.Get;
                request.RequestUri = new Uri(RESTHelper.VerifyUserUri);

                request.Headers.Add("email", user.Email);
                request.Headers.Add("password", user.Password);
                request.Headers.Add("isAdmin", $"{user.IsAdmin}");

                using (HttpResponseMessage response = _client.Send(request)) {
                    return response.Content.ReadFromJsonAsync<bool>().Result;
                }
            }
        }

        public bool DeleteUser(string email, IUser user) {
            using (HttpRequestMessage request = new HttpRequestMessage()) {
                request.Method = HttpMethod.Delete;
                request.RequestUri = new Uri(RESTHelper.DeleteUserUri);

                request.Headers.Add("email", user.Email);
                request.Headers.Add("password", user.Password);
                request.Headers.Add("isAdmin", $"{user.IsAdmin}");

                request.Content = JsonContent.Create<string>(email);

                using (HttpResponseMessage response = _client.Send(request)) {
                    return response.IsSuccessStatusCode;
                }
            }
        }
        public bool DeleteUser(IUser userToDelete, IUser user) {
            return DeleteUser(userToDelete.Email, user);
        }
    }
}
