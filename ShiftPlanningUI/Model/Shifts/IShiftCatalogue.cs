using ShiftPlanningLibrary;

namespace ShiftPlanningUI.Model.Shifts {
    public interface IShiftCatalogue {
        public List<IShift> GetShifts();
        public bool PostShift(IShift shift);
        public bool PutShift(IShift shift);
        public bool DeleteShift(int id);
        public bool DeleteShift(IShift shift);
    }
}
