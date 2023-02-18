using Microsoft.AspNetCore.Http;
using ShiftPlanningLibrary;
using ShiftPlanningUI.Model.Shifts;
using ShiftPlanningUI.Model.Users;
using System.Text.Json;

namespace ShiftPlanningUI.Services {
    public class SelectionService : ISelectionService {
        private static string areUnassignedSelectedSessionKey = "unassignedSelected";
        private static string shiftSortingMethodSessionKey = "shiftSortingMethod";
        private static string selectedUsersSessionKey = "selectedUsers";
        private static string userSortingMethodSessionKey = "userSortingMethod";

        private readonly IHttpContextAccessor _accessor;
        private readonly IUserService _userService;
        private readonly IUserCatalogue _userCatalogue;
        private readonly IShiftCatalogue _shiftCatalogue;

        public SelectionService(IHttpContextAccessor httpContextAccessor, IUserService userService, IUserCatalogue userCatalogue, IShiftCatalogue shiftCatalogue) {
            _accessor = httpContextAccessor;
            _userService = userService;
            _userCatalogue = userCatalogue;
            _shiftCatalogue = shiftCatalogue;
        }

        #region Shifts
        public bool UnassignedSelected {
            get {
                return SessionStorageHelper.GetFromSession<bool>(_accessor, areUnassignedSelectedSessionKey);
            }
            set {
                SessionStorageHelper.PutInSession<bool>(_accessor, areUnassignedSelectedSessionKey, value);
            }
        }

        public IComparer<IShift>? ShiftSortingMethod {
            get {
                return SessionStorageHelper.GetFromSession<IComparer<IShift>>(_accessor, shiftSortingMethodSessionKey);
            }
            set {
                if(value is null) { return; }
                SessionStorageHelper.PutInSession<IComparer<IShift>>(_accessor, shiftSortingMethodSessionKey, value);
            }
        }

        public List<IShift> GetShiftsInSelection() {
            return _shiftCatalogue.GetShifts(_userService.GetCurrentUser());
            /*
            List<IShift> shifts = _shiftCatalogue.GetShifts(_userService.GetCurrentUser());
            shifts = FilterShifts(shifts);

            if(ShiftSortingMethod is not null) {
                shifts.Sort();
            }

            return shifts;
            */
        }

        private List<IShift> FilterShifts(List<IShift> shifts) {
            List<IShift> toRemove = new List<IShift>();
            foreach(IShift shift in shifts) {
                bool selected = false;
                foreach(IUser user in SelectedUsers) {
                    if(shift.UserEmail == user.Email) { 
                        selected = true;
                        break;
                    }
                }
                if(!selected) {
                    toRemove.Add(shift);
                }
            }
            shifts.RemoveAll((s) => {
                return toRemove.Contains(s);
            });
            return shifts;
        }
        #endregion
        #region Users
        public List<IUser> SelectedUsers {
            get {
                List<IUser>? users = SessionStorageHelper.GetFromSession<List<IUser>>(_accessor, selectedUsersSessionKey);
                if (users is null) {
                    users = new List<IUser>();
                }
                return users;
            }
            set { //Remove passwords before we give the user information them to the browser.
                List<IUser> users = value;
                foreach(IUser user in users) {
                    user.Password = "";
                }
                SessionStorageHelper.PutInSession<List<IUser>>(_accessor, selectedUsersSessionKey, users);
            }
        }

        public IComparer<IUser> UserSortingMethod {
            get {
                return SessionStorageHelper.GetFromSession<IComparer<IUser>>(_accessor, userSortingMethodSessionKey);
            }
            set {
                SessionStorageHelper.PutInSession<IComparer<IUser>>(_accessor, userSortingMethodSessionKey, value);
            }
        }
        #endregion
    }
}
