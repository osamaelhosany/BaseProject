using System;
namespace BaseProject.Models
{
    public class ErrorLogModel : BaseModel
    {
        public string UserID { get; set; }
        public string DeviceLanguage { get; set; }
        public string ApplicationLanguage { get; set; }
        public string Country { get; set; }
        public DateTime DateTimeCrash { get; set; }
        public string MobileNumber { get; set; }
        public string BuildNumber { get; set; }
        public string SystemVersion { get; set; }
        public string DeviceIdiom { get; set; }
        public string DevicePlatform { get; set; }
        public string DeviceModel { get; set; }
        public string ErrorMessage { get; set; }
    }
}
