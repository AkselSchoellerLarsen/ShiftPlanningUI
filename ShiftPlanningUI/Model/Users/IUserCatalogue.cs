using ShiftPlanningLibrary;

namespace ShiftPlanningUI.Model.Users {
    public interface IUserCatalogue {
        public List<IUser> GetUsers(IUser user);
        public bool Register(IUser user);
        public bool MakeUserAdmin(string email, IUser user);
        public bool VerifyUser(IUser user);
    }
}
