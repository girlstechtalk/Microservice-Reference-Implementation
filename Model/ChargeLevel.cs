
using Newtonsoft.Json;
/// <summary>
/// DestinationReq Model and other subclass for Request and Response Container
/// Author - Shreema Das
/// </summary>
namespace DemoAPI.Model
{
    public class ChargeLevel : Error
    {
        public string VIN { get; set; }
        public int currentChargeLevel { get; set; }
        public string ErrorDetails { get; set; }
    }
    public class ChargeLevelReq 
    {
        [JsonProperty("vin")]
        public string vin { get; set; }
    }
}
