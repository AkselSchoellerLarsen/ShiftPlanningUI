using ShiftPlanningLibrary;
using ShiftPlanningUI.Model;
using ShiftPlanningUI.Model.Shifts;
using ShiftPlanningUI.Pages;

namespace ShiftPlanningUITesting {
    [TestClass]
    public class ShiftCatalogueTests {

        private static string RESTURI = "https://shiftplanningrestservice.azurewebsites.net/";

        [TestMethod]
        public void ShiftCatalogueGetShiftsTestPositive() {
            RESTHelper.BaseAddress = RESTURI;

            IShiftCatalogue catalogue = new ShiftCatalogue();

            List<IShift> shifts = catalogue.GetShifts();

            Assert.IsNotNull(shifts);
            Assert.IsTrue(shifts.Count > 0);
        }

        [TestMethod]
        public void ShiftCataloguePostShiftTestPositive() {
            RESTHelper.BaseAddress = RESTURI;

            IShiftCatalogue catalogue = new ShiftCatalogue();

            List<IShift> pre = catalogue.GetShifts();

            DateTime start = DateTime.Now;
            DateTime end = start.AddHours(1);

            catalogue.PostShift(new Shift(start, end));

            List<IShift> post = catalogue.GetShifts();

            Assert.IsTrue(pre.Count + 1 == post.Count);
        }

        [TestMethod]
        public void ShiftCataloguePutShiftTestPositive() {
            RESTHelper.BaseAddress = RESTURI;

            IShiftCatalogue catalogue = new ShiftCatalogue();

            List<IShift> pre = catalogue.GetShifts();
            IShift toUpdate = pre[0];

            DateTime start = DateTime.Now;
            DateTime end = start.AddHours(1);
            IShift updated = new Shift(toUpdate.Id, start, end);
            Assert.AreNotEqual(toUpdate, updated);
            catalogue.PutShift(updated);

            List<IShift> post = catalogue.GetShifts();

            Assert.IsFalse(post.Contains(toUpdate));
            Assert.IsTrue(post.Contains(updated));
        }

        [TestMethod]
        public void ShiftCatalogueDeleteShiftTestPositive() {
            RESTHelper.BaseAddress = RESTURI;

            IShiftCatalogue catalogue = new ShiftCatalogue();

            List<IShift> pre = catalogue.GetShifts();
            IShift toRemove = pre[0];

            DateTime start = DateTime.Now;
            DateTime end = start.AddHours(1);
            catalogue.DeleteShift(toRemove);

            List<IShift> post = catalogue.GetShifts();

            Assert.IsFalse(post.Contains(toRemove));
            Assert.IsTrue(pre.Count - 1 == post.Count);
        }
    }
}