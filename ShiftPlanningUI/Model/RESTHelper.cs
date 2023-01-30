namespace ShiftPlanningUI.Model {
    public static class RESTHelper {

        public static string? BaseAddress { private get; set; }

        public static string GetShiftsURI {
            get {
                if(BaseAddress == null) {
                    throw new Exception("RESTHelper must have BaseAddress defined before GetShiftsURI can be retrived from it.");
                }
                return $"{BaseAddress!}/shift/";
            }
        }

        public static string PostShiftURI {
            get {
                if (BaseAddress == null) {
                    throw new Exception("RESTHelper must have BaseAddress defined before PostShiftURI can be retrived from it.");
                }
                return $"{BaseAddress!}shift/";
            }
        }

        public static string PutShiftURI {
            get {
                if (BaseAddress == null) {
                    throw new Exception("RESTHelper must have BaseAddress defined before PutShiftURI can be retrived from it.");
                }
                return $"{BaseAddress!}shift/";
            }
        }

        public static string DeleteShiftURI(int id) {
            if (BaseAddress == null) {
                throw new Exception("RESTHelper must have BaseAddress defined before DeleteShiftURI can be called.");
            }
            return $"{BaseAddress!}shift/{id}";
        }

    }
}
