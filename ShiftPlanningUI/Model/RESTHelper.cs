namespace ShiftPlanningUI.Model {
    public static class RESTHelper {

        public static string? BaseAddress { private get; set; }

        #region Shift
        public static string GetShiftsUri {
            get {
                if(BaseAddress == null) {
                    throw new Exception("RESTHelper must have BaseAddress defined before GetShiftsUri can be retrived from it.");
                }
                return $"{BaseAddress!}shift/";
            }
        }

        public static string PostShiftUri {
            get {
                if (BaseAddress == null) {
                    throw new Exception("RESTHelper must have BaseAddress defined before PostShiftUri can be retrived from it.");
                }
                return $"{BaseAddress!}shift/";
            }
        }

        public static string PutShiftUri {
            get {
                if (BaseAddress == null) {
                    throw new Exception("RESTHelper must have BaseAddress defined before PutShiftUri can be retrived from it.");
                }
                return $"{BaseAddress!}shift/";
            }
        }

        public static string DeleteShiftUri(int id) {
            if (BaseAddress == null) {
                throw new Exception("RESTHelper must have BaseAddress defined before DeleteShiftUri can be called.");
            }
            return $"{BaseAddress!}shift/{id}";
        }
        #endregion
        #region User
        public static string GetUsersUri {
            get {
                if (BaseAddress == null) {
                    throw new Exception("RESTHelper must have BaseAddress defined before GetUsersUri can be retrived from it.");
                }
                return $"{BaseAddress!}user/";
            }
        }

        public static string RegisterUserUri {
            get {
                if (BaseAddress == null) {
                    throw new Exception("RESTHelper must have BaseAddress defined before RegisterUserUri can be retrived from it.");
                }
                return $"{BaseAddress!}user/";
            }
        }

        public static string MakeUserAdminUri {
            get {
                if (BaseAddress == null) {
                    throw new Exception("RESTHelper must have BaseAddress defined before MakeUserAdminUri can be retrived from it.");
                }
                return $"{BaseAddress!}user/";
            }
        }

        public static string VerifyUserUri {
            get {
                if (BaseAddress == null) {
                    throw new Exception("RESTHelper must have BaseAddress defined before VerifyUserUri can be called.");
                }
                return $"{BaseAddress!}user/verify";
            }
        }
        #endregion
    }
}
