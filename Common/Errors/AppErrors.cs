namespace Common.Errors
{
    public class AppErrors
    {
        public const string INVALID_CERTIFICATE = "Email or password is incorrect";
        public const string DUPLICATE_EMAIL = "Email already exists";
        public const string DUPLICATE_PHONE = "Phone number already exists";
        public const string NOT_FOUND = "Record not found";
        public const string CREATE_FAILED = "Cannot create new object";
        public const string UPDATE_FAILED = "Cannot update the object";
        public const string MANAGER_HAS_FARM = "This manager already manages the farm";

        public const string DUPLICATE_MEAL_ITEM= "The meal item already has this food";
        public const string DEVICE_TOKEN_EXIST = "Device token already exists";
    }
}
