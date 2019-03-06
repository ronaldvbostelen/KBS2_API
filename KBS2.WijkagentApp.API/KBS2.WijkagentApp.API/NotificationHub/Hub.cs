using Microsoft.Azure.NotificationHubs;

namespace KBS2.WijkagentApp.API.NotificationHub
{
    public class Hub : NotificationHubClient
    {
        private const string connectionString = "Endpoint=sb://wijkagentnotificationhub.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=3mtEWnCvhbZAIDJmTycAULWZvAsJy2ba4mOVbdMBetU=";
        private const string hubName = "wijkagentnotificationhub";

        public Hub() : base(connectionString, hubName)
        {
            CreateFcmNativeRegistrationAsync("AIzaSyA_3TzcMVgUkA-Wtxth7qrf6W6TJ71VHbA");
        }

        // create serialised package for processing by app
        public string CreateMessagePackage(string key, string jsonObject) => $"{{\"data\":{{\"key\":\"{key}\",\"content\":{jsonObject}}}}}";
        public string CreateMessagePackage(string key, string jsonObject, string fullName) => $"{{\"data\":{{\"key\":\"{key}\",\"content\":{jsonObject}, \"fullName\":\"{fullName}\"}}}}";
    }
}
