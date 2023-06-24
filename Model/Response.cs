
using Newtonsoft.Json;
using System.Collections.Generic;
/// <summary>
/// DestinationReq Model and other subclass for Request and Response Container
/// Author - Shreema Das
/// </summary>
namespace DemoAPI.Model
{
    public class DestinationDetails : Trip
    {
        public string VIN { get; set; }
        public int TransactionId { get; set; }
        //public string source { get; set; }
        //public string destination { get; set; }
        public int Distance { get; set; }
        public int CurrentChargeLevel { get; set; }
        public bool IsChargingRequired { get; set; }
        public List<ChargeStation> ChargingStations { get; set; }

        public List<ErrorModel> Errors { get; set; }
    }
    public class Trip
    {
        public string source { get; set; }
        public string destination { get; set; }

    }
    public class ErrorModel : Error
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("description")]
        public string ErrorDetails { get; set; }

    }
    public interface Error
    {
        [JsonProperty("description")]
        public string ErrorDetails { get; set; }
    }
    public class ErrorResponse
    {
        public int TransactionId { get; set; }
        public List<ErrorModel> Errors { get; set; }
    }
}

