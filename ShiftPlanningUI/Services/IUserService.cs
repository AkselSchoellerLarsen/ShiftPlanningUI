using ShiftPlanningLibrary;

namespace ShiftPlanningUI.Services {
    public interface IUserService {
        public IUser? GetCurrentUser();
        public bool Login(IUser user);
        public bool Register(IUser user);
        public bool MakeUserAdmin(string email);
        public bool Logout();
        public bool IsLoggedIn();
    }
}
