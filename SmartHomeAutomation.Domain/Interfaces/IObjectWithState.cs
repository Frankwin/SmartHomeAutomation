using SmartHomeAutomation.Domain.Enums;

namespace SmartHomeAutomation.Domain.Interfaces
{
    public interface IObjectWithState
    {
        ObjectState ObjectState { get; set; }
    }
}
