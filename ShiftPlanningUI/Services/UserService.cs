using Microsoft.AspNetCore.Http;
using ShiftPlanningLibrary;
using ShiftPlanningUI.Model.Users;
using System.Text.Json;

namespace ShiftPlanningUI.Services {
    public class UserService : IUserService {
        private static string currentUserSessionKey = "currentUser";

        private readonly IHttpContextAccessor _accessor;
        private readonly IUserCatalogue _catalogue;

        public UserService(IHttpContextAccessor httpContextAccessor, IUserCatalogue catalogue) {
            _accessor = httpContextAccessor;
            _catalogue = catalogue;
        }

        public IUser? GetCurrentUser() {
            if(_accessor.HttpContext is null) {
                throw new InvalidOperationException("Failed to get HttpContext object from IHttpContextAccessor");
            }

            string? userAsString = _accessor.HttpContext.Session.GetString(currentUserSessionKey);
            if(userAsString is null || userAsString == "") {
                return null;
            }

            return JsonSerializer.Deserialize<User>(userAsString);
        }

        public bool IsLoggedIn() {
            return GetCurrentUser() is not null;
        }

        public bool Login(IUser user) {
            IUser admin = new User(user.Email, user.Password, true);

            if (IsLoggedIn()) {
                return false;
            }
            if (_accessor.HttpContext is null) {
                throw new InvalidOperationException("Failed to get HttpContext object from IHttpContextAccessor");
            }

            string userAsString = JsonSerializer.Serialize(user);
            if (_catalogue.VerifyUser(user)) {
            } else if(_catalogue.VerifyUser(admin)) {
                userAsString = JsonSerializer.Serialize(admin);
            } else {
                return false;
            }
            
            _accessor.HttpContext.Session.SetString(currentUserSessionKey, userAsString);

            return true;
        }

        public bool MakeUserAdmin(string email) {
            if(!IsLoggedIn()) {
                return false;
            }
            if (_accessor.HttpContext is null) {
                throw new InvalidOperationException("Failed to get HttpContext object from IHttpContextAccessor");
            }

            return _catalogue.MakeUserAdmin(email, GetCurrentUser());
        }

        public bool Register(IUser user) {
            if (IsLoggedIn()) {
                return false;
            }
            if (_accessor.HttpContext is null) {
                throw new InvalidOperationException("Failed to get HttpContext object from IHttpContextAccessor");
            }

            return _catalogue.Register(user);
        }

        public bool Logout() {
            if (!IsLoggedIn()) {
                return false;
            }
            if (_accessor.HttpContext is null) {
                throw new InvalidOperationException("Failed to get HttpContext object from IHttpContextAccessor");
            }

            _accessor.HttpContext.Session.SetString(currentUserSessionKey, "");

            return true;
        }

        
    }
}
