using ShiftPlanningLibrary;

namespace ShiftPlanningUI.Model.Shifts {
    public interface IShiftCatalogue {
        public List<IShift> GetShifts(IUser user);
        public bool PostShift(IShift shift, IUser user);
        public bool PutShift(IShift shift, IUser user);
        public bool DeleteShift(int id, IUser user);
        public bool DeleteShift(IShift shift, IUser user);
    }
}
