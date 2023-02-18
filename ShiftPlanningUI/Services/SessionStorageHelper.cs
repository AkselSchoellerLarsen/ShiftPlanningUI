using ShiftPlanningLibrary;
using System.Text.Json;

namespace ShiftPlanningUI.Services {
    public static class SessionStorageHelper {
        public static T? GetFromSession<T>(IHttpContextAccessor accessor, string key) {
            if (accessor.HttpContext is null) {
                throw new InvalidOperationException("Failed to get HttpContext object from IHttpContextAccessor");
            }

            string? tAsString = accessor.HttpContext.Session.GetString(key);
            if (tAsString is null || tAsString == "") {
                return default;
            }

            return JsonSerializer.Deserialize<T>(tAsString);
        }

        public static bool PutInSession<T>(IHttpContextAccessor accessor, string key, T t) {
            if (accessor.HttpContext is null) {
                throw new InvalidOperationException("Failed to get HttpContext object from IHttpContextAccessor");
            }

            string tAsString = JsonSerializer.Serialize(t);

            accessor.HttpContext.Session.SetString(key, tAsString);

            return true;
        }
    }
}
