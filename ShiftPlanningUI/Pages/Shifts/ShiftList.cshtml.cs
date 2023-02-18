using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftPlanningLibrary;
using ShiftPlanningUI.Model.Shifts;
using ShiftPlanningUI.Model.Users;
using ShiftPlanningUI.Services;

namespace ShiftPlanningUI.Pages.Shifts {
    public class ShiftListModel : PageModel {
        private readonly IUserCatalogue _userCatalogue;
        private readonly ISelectionService _selectionService;

        [BindProperty]
        public List<IShift> Shifts { get; set; }
        [BindProperty]
        public List<IUser> Users { get; set; }
        [BindProperty]
        public IUserService UserService { get; set; }

        //Helper value, not exactly neccersary, but helps keeping the .cshtml file DRY.
        [BindProperty]
        public bool IsAdmin { get; set; }

        #region Selection service values
        private string _sortingMethod;
        [BindProperty]
        public string SortingMethod {
            get {
                return _sortingMethod;
            }
            set {
                _sortingMethod = value;
                _selectionService.ShiftSortingMethod = SetSortingMethod(value);
            }
        }
        [BindProperty]
        public bool UnassignedSelected {
            get {
                return _selectionService.UnassignedSelected;
            }
            set {
                _selectionService.UnassignedSelected = value;
            }
        }
        private List<bool> _showUsers;
        [BindProperty]
        public List<bool> ShowUsers {
            get {
                return _showUsers;
            }
            set {
                _showUsers = value;

                List<IUser> users = new List<IUser>();
                for(int i=0; i<_showUsers.Count; i++) {
                    if (_showUsers[i]) {
                        users.Add(Users[i]);
                    }
                }
                _selectionService.SelectedUsers = users;
            }
        }
        #endregion

        public ShiftListModel(IUserCatalogue userCatalogue, IUserService userService, ISelectionService selectionService) {
            Shifts = new List<IShift>();

            _userCatalogue = userCatalogue;
            _selectionService = selectionService;

            UserService = userService;
        }

        public void OnGet() {
            Shifts = _selectionService.GetShiftsInSelection();
            Users = _userCatalogue.GetUsers(UserService.GetCurrentUser());
        }

        #region Selection service methods
        public Comparer<IShift> SetSortingMethod(string method) {
            if(method == "start_time") {
                return Comparer<IShift>.Create((x,y) => {
                    return x.Start.CompareTo(y.Start);
                });
            } else if(method == "end_time") {
                return Comparer<IShift>.Create((x, y) => {
                    return x.End.CompareTo(y.End);
                });
            } else if(method == "user") {
                return Comparer<IShift>.Create((x, y) => {
                    return x.UserEmail.CompareTo(y.UserEmail);
                });
            } else {
                throw new ArgumentException("Invalid sorting method");
            }
        }
        #endregion
    }
}
