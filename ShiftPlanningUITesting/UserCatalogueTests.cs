using Microsoft.AspNetCore.Identity;
using ShiftPlanningLibrary;
using ShiftPlanningUI.Model;
using ShiftPlanningUI.Model.Shifts;
using ShiftPlanningUI.Model.Users;
using ShiftPlanningUI.Pages;

namespace ShiftPlanningUITesting {
    [TestClass]
    public class UserCatalogueTests {

        private static string RESTURI = "https://shiftplanningrestservice.azurewebsites.net/";
        private static IUser testUser = new User("test@testing.test", "test", true);

        #region GetUsers
        [TestMethod]
        public void UserManagerTestsGetUsersPositive() {
            RESTHelper.BaseAddress = RESTURI;

            IUserCatalogue catalogue = new UserCatalogue();

            List<IUser> users = catalogue.GetUsers(testUser);
            Assert.IsTrue(users.Count > 0);
        }
        #endregion
        #region RegisterUser
        [TestMethod]
        public void UserManagerTestsRegisterUserPositive() {
            RESTHelper.BaseAddress = RESTURI;

            IUserCatalogue catalogue = new UserCatalogue();

            catalogue.GetUsers(testUser);

            List<IUser> pre = catalogue.GetUsers(testUser);

            Random r = new Random();
            string email = $"{r.Next(100000, 1000000)}@{r.Next(1000, 10000)}.com";
            string password = $"not{r.Next(1000, 10000)}";
            IUser user = new User(email, password);
            Assert.IsTrue(catalogue.Register(user));

            List<IUser> post = catalogue.GetUsers(testUser);

            Assert.IsTrue(post.Contains(user));
            Assert.IsTrue(pre.Count + 1 == post.Count);
        }
        #endregion
        #region VerifyUser
        [TestMethod]
        public void UserManagerTestsVerifyUserPositive() {
            RESTHelper.BaseAddress = RESTURI;

            IUserCatalogue catalogue = new UserCatalogue();
            Assert.IsTrue(catalogue.VerifyUser(testUser));

            Random r = new Random();
            string email = $"{r.Next(100000, 1000000)}@{r.Next(1000, 10000)}.com";
            string password = $"not{r.Next(1000, 10000)}";
            IUser user = new User(email, password);

            Assert.IsFalse(catalogue.VerifyUser(user));
            Assert.IsTrue(catalogue.Register(user));
            Assert.IsTrue(catalogue.VerifyUser(user));
        }
        #endregion
        #region MakeUserAdmin
        [TestMethod]
        public void UserManagerTestsMakeUserAdminPositive() {
            RESTHelper.BaseAddress = RESTURI;

            IUserCatalogue catalogue = new UserCatalogue();

            Random r = new Random();
            string email = $"{r.Next(100000, 1000000)}@{r.Next(1000, 10000)}.com";
            string password = $"not{r.Next(1000, 10000)}";
            IUser user = new User(email, password, false);

            catalogue.Register(user);
            List<IUser> users = catalogue.GetUsers(testUser);
            IUser preUser = users.Find((u) => {
                if (u.Email == email) {
                    return true;
                }
                return false;
            }) ?? new User("", "", true);
            Assert.IsFalse(preUser.IsAdmin);

            catalogue.MakeUserAdmin(email, testUser);
            users = catalogue.GetUsers(testUser);
            IUser postUser = users.Find((u) => {
                if (u.Email == email) {
                    return true;
                }
                return false;
            }) ?? new User("", "", false);
            Assert.IsTrue(postUser.IsAdmin);
        }
        #endregion
    }
}