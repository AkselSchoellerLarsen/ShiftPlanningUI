using ShiftPlanningLibrary;
using ShiftPlanningUI.Model;
using ShiftPlanningUI.Model.Shifts;
using ShiftPlanningUI.Model.Users;
using ShiftPlanningUI.Pages;

namespace ShiftPlanningUITesting {
    [TestClass]
    public class ShiftCatalogueTests {

        private static string RESTURI = "https://shiftplanningrestservice.azurewebsites.net/";
        private static IUser testUser = new User("test@testing.test", "test", true);

        [TestMethod]
        public void ShiftCatalogueGetShiftsTestPositive() {
            RESTHelper.BaseAddress = RESTURI;

            IShiftCatalogue catalogue = new ShiftCatalogue();

            List<IShift> shifts = catalogue.GetShifts(testUser);

            Assert.IsNotNull(shifts);
            Assert.IsTrue(shifts.Count > 0);
        }

        [TestMethod]
        public void ShiftCataloguePostShiftTestPositive() {
            RESTHelper.BaseAddress = RESTURI;

            IShiftCatalogue catalogue = new ShiftCatalogue();

            List<IShift> pre = catalogue.GetShifts(testUser);

            DateTime start = DateTime.Now;
            DateTime end = start.AddHours(1);

            catalogue.PostShift(new Shift(start, end), testUser);

            List<IShift> post = catalogue.GetShifts(testUser);

            Assert.IsTrue(pre.Count + 1 == post.Count);
        }

        [TestMethod]
        public void ShiftCataloguePutShiftTestPositive() {
            RESTHelper.BaseAddress = RESTURI;

            IShiftCatalogue catalogue = new ShiftCatalogue();

            List<IShift> pre = catalogue.GetShifts(testUser);
            IShift toUpdate = pre[0];

            DateTime start = DateTime.Now;
            DateTime end = start.AddHours(1);
            IShift updated = new Shift(toUpdate.Id, start, end);
            Assert.AreNotEqual(toUpdate, updated);
            Assert.IsTrue(catalogue.PutShift(updated, testUser));

            List<IShift> post = catalogue.GetShifts(testUser);

            Assert.IsTrue(pre.Contains(toUpdate));
            Assert.IsFalse(post.Contains(toUpdate));
            Assert.IsTrue(post.Contains(updated));
        }

        [TestMethod]
        public void ShiftCatalogueDeleteShiftTestPositive() {
            RESTHelper.BaseAddress = RESTURI;

            IShiftCatalogue catalogue = new ShiftCatalogue();

            List<IShift> pre = catalogue.GetShifts(testUser);
            IShift toRemove = pre[0];

            DateTime start = DateTime.Now;
            DateTime end = start.AddHours(1);
            catalogue.DeleteShift(toRemove, testUser);

            List<IShift> post = catalogue.GetShifts(testUser);

            Assert.IsFalse(post.Contains(toRemove));
            Assert.IsTrue(pre.Count - 1 == post.Count);
        }
    }
}