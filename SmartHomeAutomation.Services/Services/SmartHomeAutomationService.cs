using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.Services.Services
{
    public class SmartHomeAutomationService: ISmartHomeAutomationService
    {
        public string ConnectionString { get; }

        public SmartHomeAutomationService()
        {
            ConnectionString =
                "Server=localhost;Database=SmartHomeAutomation;Trusted_Connection=True;ConnectRetryCount=0";
        }
    }
}
