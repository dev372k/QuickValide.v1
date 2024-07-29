using Microsoft.Extensions.Configuration;

namespace Shared.Exceptions.Messages
{
    public class ExceptionMessages
    {
        public const string USER_DOESNOT_EXIST = "User does not exist.";
        public const string USER_ALREADY_EXIST = "User already exists.";
        public const string INVALID_CREDENTIALS = "Invalid credentials.";


        public const string DOMAIN_ALREADY_EXIST = "Domain already exists.";
        public const string DOMAIN_CONFIGURATION_ISSUE = "Domain configuration issue, please retry another domain name.";
        public const string APP_DOESNOT_EXIST = "App does not exist.";
        public const string RECORD_NOT_FOUND = "Record not found";
        //public const string FAILED = "Failed";
    }
}
