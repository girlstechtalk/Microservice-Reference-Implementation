
using Newtonsoft.Json;
/// <summary>
/// DestinationReq Model and other subclass for Request and Response Container
/// Author - Shreema Das
/// </summary>
namespace DemoAPI.Model
{
    public class DestinationReq : Trip
    {
        public string VIN { get; set; }
    }
}
