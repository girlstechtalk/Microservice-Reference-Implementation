/// <summary>
/// DestinationReq Model and other subclass for Request and Response Container
/// Author - Shreema Das
/// </summary>

namespace DemoAPI.Model
{
    public class DistanceModel : Trip, Error
    {
        public int Distance { get; set; }
        public string ErrorDetails { get; set; }
    }
}
