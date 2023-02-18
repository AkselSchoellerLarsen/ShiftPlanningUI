using ShiftPlanningLibrary;

namespace ShiftPlanningUI.Services {
    public interface ISelectionService {
        public List<IShift> GetShiftsInSelection();
        public bool UnassignedSelected { get; set; }
        public IComparer<IShift> ShiftSortingMethod { get; set; }
        public List<IUser> SelectedUsers { get; set; }
        public IComparer<IUser> UserSortingMethod { get; set; }
    }
}
