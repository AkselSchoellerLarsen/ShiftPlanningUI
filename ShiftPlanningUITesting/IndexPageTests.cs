using ShiftPlanningLibrary;
using ShiftPlanningUI.Model;
using ShiftPlanningUI.Pages;

namespace ShiftPlanningUITesting {
    [Obsolete("The methods being tested have been moved to a different class")]
    [TestClass]
    public class IndexPageTests {
        /*
        private static string RESTURI = "https://shiftplanningrestservice.azurewebsites.net/";

        [TestMethod]
        public void IndexPageGetShiftsTest() {
            RESTHelper.BaseAddress = RESTURI;

            IndexModel index = new IndexModel();

            index.OnGet();
            List<IShift> shifts = index.Shifts;

            Assert.IsNotNull(shifts);
            Assert.IsTrue(shifts.Count > 0);
        }

        [TestMethod]
        public void IndexPagePostShiftTest() {
            RESTHelper.BaseAddress = RESTURI;

            IndexModel index = new IndexModel();

            index.OnGet();
            List<IShift> preShifts = index.Shifts;

            index.Start = DateTime.Now;
            index.End = index.Start.AddHours(1);
            
            index.OnPostAsync().Wait();

            index.OnGet();
            List<IShift> postShifts = index.Shifts;

            Assert.IsTrue(preShifts.Count + 1 == postShifts.Count);
        }
        */
    }
}